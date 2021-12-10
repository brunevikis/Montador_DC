using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;
using DecompTools.FactoryPrevs;
using DecompTools.Util;

namespace DecompTools.ModelagemPrevs {
    public class Postos : Model {
        public virtual int numero { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual string nome { get; set; }
        public virtual string bacia { get; set; }
        public virtual int submercado { get; set; }
        public virtual double produtibilidade { get; set; }
        public virtual string observacao { get; set; }

        //variaveis não consistidas no banco.
        public virtual PrevsDados prevs_base { get; set; }
        public virtual PrevsDados prevs_saida { get; set; }
        public virtual RegressaoDados regressao { get; set; }
        public virtual CalculadosDados calculado { get; set; }
        public virtual RDHDados rdh_base { get; set; }

        // Tipo do posto: 0 -> Prevs, 1 -> ChuvaVazão, 2 -> 168 (posto do inferno), 3 -> Regressao, 4 -> Calculados e -1 -> não operando
        public virtual int tipo { get; set; }
        public virtual string caminhoInp { get; set; }
        public virtual string caminhoDat { get; set; }

        public Postos(Postos p) {
            this.numero = p.numero;
            this.nome = p.nome;
            this.bacia = p.bacia;
            this.submercado = p.submercado;
            this.produtibilidade = p.produtibilidade;
            this.observacao = p.observacao;
            this.prevs_base = p.prevs_base;
            this.regressao = p.regressao;
            this.calculado = p.calculado;
            this.rdh_base = p.rdh_base;
            this.tipo = p.tipo;
            this.caminhoInp = p.caminhoInp;
            this.caminhoDat = p.caminhoDat;
        }

        public Postos() {
        }

        /// <summary>
        /// Organiza um array com os postos onde o numero do posto condiz com seu indice.
        /// </summary>
        /// <param name="postosOld">list com os postos a serem organizados</param>
        /// <returns>array com os postos organizados</returns>
        public static Postos[] reorganizaPostos(IList<Postos> postosList) {
            Postos[] newArrayPostos = new Postos[361];  //321 é o numero maximo de postos + 1 (pois nao usarei o 0)
            foreach (Postos p in postosList)
                newArrayPostos[p.numero] = p;

            return newArrayPostos;
        }



        /// <summary>
        /// Realiza a projeção de todos os postos base.
        /// </summary>
        /// <param name="postosList">List com as informações de todos os postos</param>
        /// <param name="semanaProjecao">semana do novo prevs</param>
        public static void atualizaPostosBase(IList<Postos> postosList, Semanas_Ano semanaProjecao) {
            PropertyInfo semanaX;
            int revX;
            if (semanaProjecao.rev == 0)
                revX = semanaProjecao.semanaAnterior().rev + 1;
            else
                revX = semanaProjecao.rev;

            semanaX = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", revX.ToString()));

            foreach (Postos p in postosList.Where(p => p.tipo == 0 || p.tipo == 1 || p.tipo == 2 || p.tipo == 3 || (p.tipo == -1 && p.prevs_base != null))) {
                if (p.prevs_saida == null) {
                    p.prevs_saida = new PrevsDados();
                    p.prevs_saida.posto = p.numero;
                }

                for (int x = 1; x < 7; x++) {
                    PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", x.ToString()));

                    if (x < revX) {
                        semanaPrevs.SetValue(p.prevs_saida, semanaPrevs.GetValue(p.prevs_base, null), null);
                    } else if (x == revX) {
                        if (p.rdh_base != null && p.rdh_base.vazaoUltMax != 0)
                            semanaPrevs.SetValue(p.prevs_saida, p.rdh_base.vazaoUltMax, null);
                        else
                            semanaPrevs.SetValue(p.prevs_saida, semanaPrevs.GetValue(p.prevs_base, null), null);
                    } else {
                        double valorSemanaX;
                        if (p.rdh_base != null && p.rdh_base.vazaoUltMin != 0)
                            valorSemanaX = p.rdh_base.vazaoUltMin * (double)semanaPrevs.GetValue(p.prevs_base, null) / (double)semanaX.GetValue(p.prevs_base, null);
                        else
                            valorSemanaX = (double)semanaPrevs.GetValue(p.prevs_base, null);                //TODO: Verificar se este é o melhor metodo quando não encontra o posto no RDH

                        semanaPrevs.SetValue(p.prevs_saida, valorSemanaX, null);
                    }
                }
            }
        }

        /// <summary>
        /// Calcula os postos regredidos a partir da projeção dos postos base.
        /// </summary>
        /// <param name="postosList">List com as informações de todos os postos, com a projeção dos postos base já criada</param>
        /// <param name="semanaProjecao">semana do novo prevs</param>
        public static void atualizaPostosRegredidos(IList<Postos> postosList, Semanas_Ano semanaProjecao) {
            RegressaoDados regObj = new RegressaoDados();

            PropertyInfo a0Mes1 = regObj.GetType().GetProperty(String.Concat("a0_", UtilitarioDeData.mesAnterior(semanaProjecao.mes).ToString()));
            PropertyInfo a0Mes2 = regObj.GetType().GetProperty(String.Concat("a0_", semanaProjecao.mes.ToString()));
            PropertyInfo a0Mes3 = regObj.GetType().GetProperty(String.Concat("a0_", UtilitarioDeData.mesPosterior(semanaProjecao.mes).ToString()));
            PropertyInfo a1Mes1 = regObj.GetType().GetProperty(String.Concat("a1_", UtilitarioDeData.mesAnterior(semanaProjecao.mes).ToString()));
            PropertyInfo a1Mes2 = regObj.GetType().GetProperty(String.Concat("a1_", semanaProjecao.mes.ToString()));
            PropertyInfo a1Mes3 = regObj.GetType().GetProperty(String.Concat("a1_", UtilitarioDeData.mesPosterior(semanaProjecao.mes).ToString()));

            Postos[] postosReorg = reorganizaPostos(postosList);
            double[][] diasMes = UtilitarioDeData.diasMeses(semanaProjecao);           //Necessario apenas para ponderar os a0's e a1's entre 2 meses

            //Calculo da regressão
            foreach (Postos p in postosList.Where(p => p.tipo == 3)) {

                if (postosReorg[p.regressao.postoBase].prevs_saida == null)

                    throw new Exception("Vazão do posto base não definida, verifique a pasta de arquivos de entrada (inp).\r\n Posto: " + p.numero.ToString() + "\tBase: " + p.regressao.postoBase.ToString());



                if (p.prevs_saida == null) {
                    p.prevs_saida = new PrevsDados();
                    p.prevs_saida.posto = p.numero;
                }

                for (int x = semanaProjecao.rev; x < 6; x++) {
                    PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", (x + 1).ToString()));

                    double prevsValue = diasMes[0][x] * (double)a0Mes1.GetValue(p.regressao, null) + diasMes[1][x] * (double)a0Mes2.GetValue(p.regressao, null) + diasMes[2][x] * (double)a0Mes3.GetValue(p.regressao, null);
                    prevsValue += diasMes[0][x] * (double)a1Mes1.GetValue(p.regressao, null) * (double)semanaPrevs.GetValue(postosReorg[p.regressao.postoBase].prevs_saida, null);
                    prevsValue += diasMes[1][x] * (double)a1Mes2.GetValue(p.regressao, null) * (double)semanaPrevs.GetValue(postosReorg[p.regressao.postoBase].prevs_saida, null);
                    prevsValue += diasMes[2][x] * (double)a1Mes3.GetValue(p.regressao, null) * (double)semanaPrevs.GetValue(postosReorg[p.regressao.postoBase].prevs_saida, null);

                    semanaPrevs.SetValue(p.prevs_saida, prevsValue / 7, null);
                }
            }
        }

        /// <summary>
        /// Ajustar a ENA dos submercados para a ENA target.
        /// </summary>
        /// <param name="postosList">List com as informações de todos os postos, com a projeção dos postos já criada e os dependentes já calculados</param>
        /// <param name="semanaAtual">Semana do novo Prevs</param>
        /// <param name="ENA">Target de ENA por semana e por submercado que deve-se atingir</param>
        /// <returns>List dos postos ajustados para a ENA target</returns>
        public static IList<Postos> ajustar(IList<Postos> postosList, Semanas_Ano semanaAtual, int[,] ENA) {
            double[][] fatorx = new double[4][];
            for (var i = 0; i < fatorx.Length; i++)
                fatorx[i] = new double[6] { 1, 1, 1, 1, 1, 1 };

            IList<Postos> postosAjustados = new List<Postos>();

            int[] semanas;
            if (semanaAtual.rev == 0) {
                if (ENA[0, 1] != 0) {
                    semanas = new int[] { semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 3, semanaAtual.semanaAnterior().rev + 2 };
                    if (semanas[2] == 7)
                        semanas[2] = 6;
                } else {
                    semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev + 1};
                    if (semanaAtual.semanaAnterior().rev == 4)
                        semanas[2] = 1;
                }
            } else
                semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev + 1};

            PrevsDados[] ENAproj = Prevs.calculaENASubmercados(postosList).ToArray();

            double erro = 100;
            int itNumber = 0;

            while (erro > 20 && itNumber < 20) {
                //Calculando o fatorX para cada semana e cada submercado
                for (int x = 0; x < 4; x++)
                    for (int y = 0; y < 6; y++) {
                        if (y < semanas[x]) {
                            PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", (y + 1).ToString()));
                            fatorx[x][y] = fatorx[x][y] * (double)ENA[x, y] / (double)semanaPrevs.GetValue(ENAproj[x], null);
                        } else {
                            if (y == 0)
                                fatorx[x][y] = 1;
                            else
                                fatorx[x][y] = fatorx[x][y - 1];
                        }
                    }

                //Ajustando cada posto
                postosAjustados.Clear();
                foreach (Postos p in postosList) {
                    Postos pAjustado = new Postos(p);

                    if (p.submercado != 0 && p.prevs_saida != null) {
                        pAjustado.prevs_saida = p.prevs_saida * fatorx[p.submercado - 1];
                        pAjustado.prevs_saida.posto = p.numero;

                        postosAjustados.Add(pAjustado);
                    }
                }

                Postos.atualizaPostosRegredidos(postosAjustados, semanaAtual);
                Calculados.atualizar(postosAjustados, semanaAtual);

                //Verificando o ajuste
                ENAproj = Prevs.calculaENASubmercados(postosAjustados).ToArray();

                erro = 0;
                for (int x = 0; x < 4; x++)
                    for (int y = 0; y < semanas[x]; y++) {
                        PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", (y + 1).ToString()));
                        erro = erro + Math.Abs((double)ENA[x, y] - (double)semanaPrevs.GetValue(ENAproj[x], null));
                    }

                itNumber++;
            }

            return postosAjustados;
        }
    }
}
