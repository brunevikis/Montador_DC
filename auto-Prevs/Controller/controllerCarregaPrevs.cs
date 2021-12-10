using AutoPrevs.Factory;
using AutoPrevs.Modelagem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AutoPrevs.Controller {
    public class controllerCarregaPrevs {
        /// <summary>
        /// Carrega um prevs para o banco
        /// </summary>
        /// <param name="caminho">Caminho do arquivo PREVS a ser lido</param>
        /// <param name="nome">Nome que o arquivo receberá no banco</param>
        /// <param name="desc">Descrição que o arquvio receberá no banco</param>
        /// <param name="oficial">1 caso seja oficial, 0 caso contrario</param>
        /// <param name="ano">Ano do prevs</param>
        /// <param name="mes">Mes do prevs</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        public string carregarPrevs(string caminho, string nome, string desc, bool oficial, int ano, int mes) {
            if (!File.Exists(caminho)) {
                return "Arquivo não existe!";
            }

            Prevs prevs = lePrevs(caminho, ano, mes);


            if (oficial) {
                Prevs prevsOficial = PrevsDAO.getPrevsOficialByMonth(prevs.mes, prevs.ano);
                if (prevsOficial != null) {
                    prevsOficial.oficial = 0;
                    prevsOficial.save();
                }
                prevs.oficial = 1;
            }

            prevs.save();
            return prevs.id.ToString();
        }

        public Prevs lePrevs(string caminho, int ano, int mes) {

            Prevs prevs = new Prevs();
            prevs.rev = int.Parse(caminho.Substring(caminho.Length - 1, 1));
            prevs.caminho = caminho;
            prevs.oficial = 0;
            prevs.ano = ano;
            prevs.mes = mes;

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(prevs.caminho)) {
                string sLine = "";
                ArrayList arrText = new ArrayList();

                List<PrevsDados> prevsDados = new List<PrevsDados>();

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    PrevsDados dados = new PrevsDados();

                    dados.prevs = prevs;
                    dados.posto = int.Parse(sLine.Substring(6, 5).Trim());
                    dados.sem1 = int.Parse(sLine.Substring(11, 10).Trim());
                    dados.sem2 = int.Parse(sLine.Substring(21, 10).Trim());
                    dados.sem3 = int.Parse(sLine.Substring(31, 10).Trim());
                    dados.sem4 = int.Parse(sLine.Substring(41, 10).Trim());
                    dados.sem5 = int.Parse(sLine.Substring(51, 10).Trim());
                    dados.sem6 = int.Parse(sLine.Substring(61, 10).Trim());

                    prevsDados.Add(dados);
                }

                prevs.dados = prevsDados;
            }
            return prevs;
        }       
    }
}
