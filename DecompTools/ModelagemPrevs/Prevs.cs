using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;
using DecompTools.FactoryPrevs;
using System.Threading;

namespace DecompTools.ModelagemPrevs {
    public class Prevs : Model {
        public virtual int id { get; set; }
        public virtual DateTime dt_entrada { get; set; }
        public virtual int rev { get; set; }
        public virtual string caminho { get; set; }
        public virtual int ano { get; set; }
        public virtual int mes { get; set; }
        public virtual int oficial { get; set; }
        public virtual IList<PrevsDados> dados { get; set; }

        public virtual IList<Estudos> estudo_dependentes { get; set; }
        public virtual IList<Estudos> estudo_gerador { get; set; }

        public virtual void escreverPrevs() {
            this.escreverPrevs(this.caminho);
        }

        public virtual void escreverPrevs(string caminho) {
            var arquivo = String.Concat("PREVS.RV", this.rev.ToString());
            escreverPrevs(caminho, arquivo);
        }

        public virtual void escreverPrevs(string caminho, string nomeArquivo) {
            caminho = Path.Combine(caminho, nomeArquivo);

            if (File.Exists(caminho))
                File.Delete(caminho);
            File.Create(caminho).Close();
            using (TextWriter arquivo = File.CreateText(caminho)) {
                int linha = 1;
                foreach (PrevsDados dados in this.dados) {
                    arquivo.WriteLine(dados.escreveLinha(linha));
                    linha++;
                }

                arquivo.Flush();
            }
        }

        /// <summary>
        /// Dado uma lista de postos, calcula a ENA de cada submercado em cada semana multiplicando o valor do reservatorio pela produtibilidade.
        /// </summary>
        /// <param name="postosList"></param>
        /// <returns>list<PrevsDados> com a ENA consolidada de cada semana em cada submercado</returns>
        public static List<PrevsDados> calculaENASubmercados(IList<Postos> postosList) {
            List<PrevsDados> ENASubmercado = new List<PrevsDados>();
            ENASubmercado.Add(calculaENASubmercados(postosList, 1));
            ENASubmercado.Add(calculaENASubmercados(postosList, 2));
            ENASubmercado.Add(calculaENASubmercados(postosList, 3));
            ENASubmercado.Add(calculaENASubmercados(postosList, 4));

            return ENASubmercado;
        }

        /// <summary>
        /// Calcula a ENA de cada semana em um submercado especifico
        /// </summary>
        /// <param name="postosList">List com as informações dos postos</param>
        /// <param name="submercado">submercado especifico</param>
        /// <returns>Objeto Prevs com a ENA do submercado Especifico em cadas semana (campo prevs_saida)</returns>
        public static PrevsDados calculaENASubmercados(IList<Postos> postosList, int submercado) {
            PrevsDados ENATotal = new PrevsDados();

            foreach (Postos p in postosList.Where(posto => posto.submercado == submercado))
                if (p.prevs_saida != null) {
                    PrevsDados foo = new PrevsDados();
                    foo = p.prevs_saida * p.produtibilidade;
                    ENATotal = ENATotal + foo;
                }

            return ENATotal;
        }

        /// <summary>
        /// Calcuça a ENA media para o mes atual em cada submercado.
        /// </summary>
        /// <param name="ENAsubmercado">ENA por semana de cada submercado já consolidada</param>
        /// <param name="diasMes">Numero de dias do mes atual em cada semana</param>
        /// <returns>array com a ena media de cada submercado</returns>
        public static double[] calculaENAMedia(PrevsDados[] ENAsubmercado, double[] diasMes) {
            double[] ENAmedia = new double[4];

            for (int x = 0; x < 4; x++) {
                ENAmedia[x] = ENAsubmercado[x].sem1 * diasMes[0];
                ENAmedia[x] += ENAsubmercado[x].sem2 * diasMes[1];
                ENAmedia[x] += ENAsubmercado[x].sem3 * diasMes[2];
                ENAmedia[x] += ENAsubmercado[x].sem4 * diasMes[3];
                ENAmedia[x] += ENAsubmercado[x].sem5 * diasMes[4];
                ENAmedia[x] += ENAsubmercado[x].sem6 * diasMes[5];

                ENAmedia[x] = ENAmedia[x] / diasMes.Sum();
            }

            return ENAmedia;
        }
    }
}
