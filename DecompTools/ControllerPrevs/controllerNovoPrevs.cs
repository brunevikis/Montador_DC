using DecompTools.FactoryPrevs;
using DecompTools.ModelagemPrevs;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DecompTools.ControllerPrevs
{

    public enum ModoSensibilidade
    {
        total,
        somentePosteriorPrevisao
    }

    public class controllerNovoPrevs
    {

        public int[,] CheckEnaEntrada(Semanas_Ano semanaAtual, string ENAtext, out int revsToRun)
        {


            string[] ENAlinhas = ENAtext.Split('\n');
            int[,] ENA = new int[4, 6];
            int _sub = 0;
            foreach (string ENAsubmercado in ENAlinhas)
            {
                int _sem = 0;
                foreach (string ENAsemana in ENAsubmercado.Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {

                    ENA[_sub, _sem] = int.Parse(ENAsemana.Replace(".", String.Empty));
                    _sem++;
                }
                _sub++;

                if (_sub > 3)
                    break;
            }


            int[] semanas;

            if (semanaAtual.rev == 0)
            {
                revsToRun = 0;
                if (ENA[0, 1] != 0)
                {
                    semanas = new int[] { semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 3, semanaAtual.semanaAnterior().rev + 2};
                    if (semanas[2] == 7)
                        semanas[2] = 6;
                }
                else
                {
                    semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev +1};
                    if (semanaAtual.semanaAnterior().rev == 4)
                        semanas[2] = 1;
                }
            }
            else
                semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev + 1};

            int[] semanasInformadas = new int[4] { 0, 0, 0, 0 };
            for (int sub = 0; sub < ENA.GetLength(0); sub++)
                for (int sem = 0; sem < ENA.GetLength(1); sem++)
                    if (ENA[sub, sem] != 0) semanasInformadas[sub]++;

            int dif = -1;
            for (int sub = 0; sub < semanas.Length; sub++)
            {
                var difT = semanasInformadas[sub] - semanas[sub];

                if (difT < 0) throw new Exception("ENA informada incompleta");
                if (dif == -1) dif = difT;
                else if (dif != difT) throw new Exception("ENA informada excedente");
            }


            revsToRun = dif + 1;
            return ENA;
        }
        public int[,] SomaSensibilidade(Semanas_Ano semanaAtual, int[,] ena, string sensibilidadeEna, int passo, ModoSensibilidade modo = ModoSensibilidade.total)
        {
            string[] ENASenslinhas = sensibilidadeEna.Split('\n');

            int[,] ENAbase = new int[4, 6];

            for (int sub = 0; sub < ena.GetLength(0); sub++)
            {
                var enaSens = ENASenslinhas[sub].Replace(".", String.Empty).Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                for (int sem = 0; sem < enaSens.Length; sem++)
                {

                    int sens;

                    if (!int.TryParse(enaSens[sem], out sens))
                    {
                        sens = 0;
                    }

                    if (modo == ModoSensibilidade.total
                        || semanaAtual.rev <= sem)
                        ENAbase[sub, sem] = ena[sub, sem] + sens * passo;
                    else
                        ENAbase[sub, sem] = ena[sub, sem];
                }
            }


            return ENAbase;
        }

        /// <summary>
        /// A partir de um prevs, cria um novo prevs para a data escolhida.
        /// </summary>
        /// <param name="prevsBase">Prevs base para o novo</param>
        /// <param name="rdhBase">RDH utilizado na projeção</param>
        /// <param name="semanaAtual">Semana do novo prevs</param>
        /// <param name="caminhoImp">Caminho para a pasta dos arquivos inp/dat</param>
        /// <param name="ENAtext">ENA utilizada como target semanal para cada submercado(String)</param>
        /// <param name="desc">Descrição para o novo prevs</param>
        /// <param name="caminhoSaida">Caminho onde o novo prevs será escrito</param>
        /// <param name="difSemanas">numero de semanas de diferença entre o novo prevs e o antigo</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        public string prevsSemana(Prevs prevsBase, RDH rdhBase, Semanas_Ano semanaAtual, string caminhoImp, string ENAtext, string desc, string caminhoSaida, bool usarRdh, int difSemanas = 1, Dictionary<int, Dictionary<string, string>> strDat = null)
        {
            //DateTime dtInicio = DateTime.Now;
            //Semanas_Ano semanaPrevsImp = new Semanas_Ano();
            //Semanas_Ano semanaCVImp = new Semanas_Ano();

            //Leitura da ENA
            string[] ENAlinhas = ENAtext.Split('\n');
            int[,] ENA = new int[4, 6];

            int _sub = 0;
            foreach (string ENAsubmercado in ENAlinhas)
            {
                int _sem = 0;
                foreach (string ENAsemana in ENAsubmercado.Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (_sem > 5)
                    {
                        return "ENA informada de forma errada, sobrando informação.";
                    }

                    ENA[_sub, _sem] = int.Parse(ENAsemana.Replace(".", String.Empty));
                    _sem++;
                }
                _sub++;

                if (_sub > 3)
                    break;
            }

            return prevsSemana(prevsBase, rdhBase, semanaAtual, caminhoImp, ENA, desc, caminhoSaida, usarRdh, difSemanas, strDat);
        }

        public string prevsSemana(Prevs prevsBase, RDH rdhBase, Semanas_Ano semanaAtual, string caminhoImp, int[,] ENA, string desc, string caminhoSaida, bool usarRdh, int difSemanas = 1, Dictionary<int, Dictionary<string, string>> strDat = null)
        {

            Semanas_Ano semanaPrevsImp = new Semanas_Ano();
            Semanas_Ano semanaCVImp = new Semanas_Ano();

            //Verificando a ENA passada como target
            int[] semanas;
            if (semanaAtual.rev == 0)
            {
                if (ENA[0, 1] != 0)
                {
                    semanas = new int[] { semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 2, semanaAtual.semanaAnterior().rev + 3, semanaAtual.semanaAnterior().rev + 2 };
                    if (semanas[2] == 7)
                        semanas[2] = 6;
                }
                else
                {
                    semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev + 1};
                    if (semanaAtual.semanaAnterior().rev == 4)
                        semanas[2] = 1;
                }
            }
            else
                semanas = new int[] { semanaAtual.rev + 1, semanaAtual.rev + 1, semanaAtual.rev + 2, semanaAtual.rev + 1};
                        int iSubmercado = 0;
            foreach (int semAtual in semanas)
            {
                for (int x = 0; x < semAtual; x++)
                {
                    if (ENA[iSubmercado, x] == 0)
                        return "ENA informada de forma errada, faltanto informação.";
                    else if (ENA[iSubmercado, x] < 0)
                        return "ENA informada de forma errada, ENA negativa.";
                }

                iSubmercado++;
            }

            //SemanaX e semana chuva vazão.
            semanaPrevsImp = semanaAtual.semanaAnterior(difSemanas);
            semanaCVImp = semanaAtual.semanaAnterior(difSemanas - 1);

            //Carregar os dados da tabela complemento do banco
            Regressao regressao = RegressaoDAO.getRegressaoOficial();
            IList<Postos> postos = PostosDAO.GetAll();
            Calculados calc = new Calculados();

            foreach (Postos p in postos)
            {
                //Setando o tipo de posto.
                //String caminhoImpAtual = Path.Combine(caminhoImp, String.Concat(p.numero.ToString(), ".inp"));
                var inps = Directory.GetFiles(caminhoImp, String.Concat(p.numero.ToString(), ".inp"), SearchOption.AllDirectories);

                if (calc.postos.Contains(p.numero))
                {
                    p.tipo = 4;
                }
                else if (inps.Length == 1)
                {
                    p.caminhoInp = inps[0];
                    p.caminhoDat = Path.Combine(
                        Path.GetDirectoryName(p.caminhoInp)
                        , String.Concat(p.numero.ToString(), "_str.DAT"));

                    int tipo = int.Parse(UtilitarioDeArquivo.readLineFromFile(p.caminhoInp, 9));

                    if (p.numero == 168)
                    {
                        if (semanaAtual.rev == 0 && semanaAtual.semanaAnterior().rev + 1 == 5)
                            p.tipo = 1;
                        else
                            p.tipo = 2;
                    }
                    else if (tipo == semanaPrevsImp.semana || (tipo == 1 && semanaPrevsImp.semana == 53))
                        p.tipo = 0;
                    else if (tipo == semanaCVImp.semana || (tipo == 1 && semanaCVImp.semana == 53) || (tipo == 2 && semanaPrevsImp.semana == 53))
                        p.tipo = 1;
                }
                else if (regressao.dados.Any(x => x.postoRegredido == p.numero))
                {
                    p.regressao = regressao.dados.Where(x => x.postoRegredido == p.numero).First();
                    p.tipo = 3;
                }
                else p.tipo = -1;

                if (prevsBase.dados.Any(x => x.posto == p.numero))
                {
                    //Carrega as semanas do prevs base em seu respectivo posto.
                    //Soma 1 para tirar a referencia do prevs base de entrada criando um novo objeto PrevsDados
                    p.prevs_base = prevsBase.dados.Where(x => x.posto == p.numero).First() + 1;

                }
                if (usarRdh &&
                     rdhBase.dados.Any(x => x.posto == p.numero))
                {
                    p.rdh_base = rdhBase.dados.Where(x => x.posto == p.numero).First();             //Carrega o RDH em seu respectivo posto
                }
            }

            //Projetar postos

            Postos.atualizaPostosBase(postos, semanaPrevsImp);
            Postos.atualizaPostosRegredidos(postos, semanaPrevsImp);
            Calculados.atualizar(postos, semanaPrevsImp);

            Escrever(postos, caminhoSaida, "prevs.it1");

            //Ajustar a ENA dos submercados para a ENA target
            IList<Postos> pAjustados = Postos.ajustar(postos, semanaPrevsImp, ENA);

            Escrever(pAjustados, caminhoSaida, "prevs.it2");


            //guardar duas ultimas previsoes do mes dos postos 156 e 158 para se previsao for de rv0, usar na reconstituicao do posto 169;
            double[] prevs156 = null, prevs158 = null;
            if (semanaAtual.rev == 0)
            {
                var p156 = pAjustados.First(x => x.numero == 156);
                var p158 = pAjustados.First(x => x.numero == 158);

                prevs156 = new double[] {
                    p156.prevs_saida.sem1,
                    p156.prevs_saida.sem2,
                    p156.prevs_saida.sem3,
                    p156.prevs_saida.sem4,
                    p156.prevs_saida.sem5,
                    p156.prevs_saida.sem6
                };

                prevs158 = new double[] {
                    p158.prevs_saida.sem1,
                    p158.prevs_saida.sem2,
                    p158.prevs_saida.sem3,
                    p158.prevs_saida.sem4,
                    p158.prevs_saida.sem5,
                    p158.prevs_saida.sem6
                };
            }

            //Escrever os novos arquivos inp/dat e executar o previvaz
            controllerPrevivaz.InpDatPrevivaz(pAjustados, caminhoSaida, semanaAtual, strDat);


            //caso rv0, arrumar primeiras semanas do posto 169 para considerar tempo de viagem... 168 + 156(s-2) + 158(s-2)
            if (semanaAtual.rev == 0)
            {
                var p169 = pAjustados.First(x => x.numero == 169);
                var p168 = pAjustados.First(x => x.numero == 168);


                var sAnt = semanaAtual.semanaAnterior();

                p168.prevs_base.sem1 = p168.prevs_saida.sem1;
                p168.prevs_base.sem2 = p168.prevs_saida.sem2;

                p169.prevs_base.sem1 = p168.prevs_saida.sem1 + prevs158[sAnt.rev - 1] + prevs156[sAnt.rev - 1];
                p169.prevs_base.sem2 = p168.prevs_saida.sem2 + prevs158[sAnt.rev] + prevs156[sAnt.rev];
            }

            Postos.atualizaPostosRegredidos(pAjustados, semanaAtual);
            Calculados.atualizar(pAjustados, semanaAtual);

            Escrever(pAjustados, caminhoSaida, "prevs.it3");

            double[][] diasMes = UtilitarioDeData.diasMeses(semanaAtual);

            if (semanaAtual.rev == 0)
                EscreverMensal(pAjustados, caminhoSaida, "prevs_men.it3", diasMes[1]);

            //Caso seja rev 0, atualizar a ENA target
            if (semanaAtual.rev == 0)
            {
                int[,] ENAaux = new int[4, 6];
                ENAaux[0, 0] = ENA[0, semanaAtual.semanaAnterior().rev + 1];
                ENAaux[1, 0] = ENA[1, semanaAtual.semanaAnterior().rev + 1];
                ENAaux[2, 0] = ENA[2, semanaAtual.semanaAnterior().rev + 1];
                ENAaux[3, 0] = ENA[3, semanaAtual.semanaAnterior().rev + 1];// conferir se precisará tirar essa linha
                if (semanaAtual.semanaAnterior().rev + 1 < 5)
                    ENAaux[2, 1] = ENA[2, semanaAtual.semanaAnterior().rev + 2];
                ENA = ENAaux;
            }
            //Ajustar novamente a ENA dos submercados para a ENA target
            IList<Postos> pFinais = Postos.ajustar(pAjustados, semanaAtual, ENA);



            //Cria o novo prevs e define suas informaçoes e o escreve no local pré definido.
            Prevs pTeste = new Prevs();
            pTeste.rev = semanaAtual.rev;
            pTeste.caminho = caminhoSaida;
            List<PrevsDados> pList = new List<PrevsDados>();

            foreach (Postos p in pFinais.Where(p => p.prevs_saida != null))
                pList.Add(p.prevs_saida);
            //if (p.prevs_saida != null)


            pTeste.dados = pList;
            pTeste.escreverPrevs();

            if (semanaAtual.rev == 0)
                EscreverMensal(pFinais, caminhoSaida, "prevs_men.rv0", diasMes[1]);


            //Escreve log final com a ENA por submercado, media das semanas e % da MLT
            PrevsDados[] ENAfinal = Prevs.calculaENASubmercados(pFinais).ToArray();
            //double[][] diasMes = UtilitarioDeData.diasMeses(semanaAtual);
            double[] ENAMedia = Prevs.calculaENAMedia(ENAfinal, diasMes[1]);

            PropertyInfo mesMLT = (new MltSub()).GetType().GetProperty(String.Concat("mes", semanaAtual.mes.ToString()));
            MltSub[] mlt = MLTDAO.getAll().ToArray();

            using (StreamWriter swLog = new StreamWriter(Path.Combine(caminhoSaida, "resumo.log")))
            {
                swLog.WriteLine("RESULTADOS DA EXECUCAO.");
                for (int x = 0; x < 4; x++)
                {
                    StringBuilder linha = new StringBuilder();
                    double mediaMlt = Math.Round((ENAMedia[x] / (int)mesMLT.GetValue(mlt[x], null)) * 100, 0);

                    linha.Append(Math.Round(ENAfinal[x].sem1, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAfinal[x].sem2, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAfinal[x].sem3, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAfinal[x].sem4, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAfinal[x].sem5, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAfinal[x].sem6, 0)); linha.Append(" ");
                    linha.Append(Math.Round(ENAMedia[x], 0)); linha.Append(" ");
                    linha.Append(mediaMlt); linha.Append("%");

                    swLog.WriteLine(linha.ToString());
                }
            }
            return "";
        }

        private void Escrever(IList<Postos> pAjustados, string caminhoSaida, string nome)
        {
            Prevs pTeste = new Prevs();

            //List<PrevsDados> pList = new List<PrevsDados>();

            //foreach (Postos p in pAjustados.Where(p => p.prevs_base != null))
            //    if (p.prevs_saida != null)
            //        pList.Add(p.prevs_saida);

            //pTeste.dados = pList;

            pTeste.dados = pAjustados.Where(p => p.prevs_saida != null).Select(p => p.prevs_saida).ToList();
            pTeste.escreverPrevs(caminhoSaida, nome);
        }

        private void EscreverMensal(IList<Postos> postosList, string caminhoSaida, string nome, double[] diasMes)
        {

            IList<Postos> postosAjustados = new List<Postos>();
            foreach (Postos p in postosList)
            {
                Postos pAjustado = new Postos(p);
                pAjustado.prevs_saida = new PrevsDados();

                if (p.submercado != 0 && p.prevs_saida != null)
                {

                    pAjustado.prevs_saida.sem1 = p.prevs_saida.sem1;
                    pAjustado.prevs_saida.sem2 = p.prevs_saida.sem2;
                    pAjustado.prevs_saida.sem3 = p.prevs_saida.sem3;
                    pAjustado.prevs_saida.sem4 = p.prevs_saida.sem4;
                    pAjustado.prevs_saida.sem5 = p.prevs_saida.sem5;
                    pAjustado.prevs_saida.sem6 = p.prevs_saida.sem6;

                    pAjustado.prevs_saida.posto = p.numero;
                    postosAjustados.Add(pAjustado);
                }
            }

            foreach (Postos p in postosAjustados.Where(p => p.prevs_base != null))
            {
                if (p.prevs_saida != null)
                {


                    p.prevs_saida.sem1 = ((p.prevs_saida.sem1 * diasMes[0])
                    + (p.prevs_saida.sem2 * diasMes[1])
                    + (p.prevs_saida.sem3 * diasMes[2])
                    + (p.prevs_saida.sem4 * diasMes[3])
                    + (p.prevs_saida.sem5 * diasMes[4])
                    + (p.prevs_saida.sem6 * diasMes[5]))
                    / diasMes.Sum();

                    p.prevs_saida.sem6 = p.prevs_saida.sem5 = p.prevs_saida.sem4 = p.prevs_saida.sem3 = p.prevs_saida.sem2 = p.prevs_saida.sem1;

                }
            }

            Escrever(postosAjustados, caminhoSaida, nome);

        }
    }
}
