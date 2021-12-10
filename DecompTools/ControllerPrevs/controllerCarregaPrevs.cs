using DecompTools.FactoryPrevs;
using DecompTools.ModelagemPrevs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DecompTools.ControllerPrevs
{
    public class controllerCarregaPrevs
    {
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
        public string carregarPrevs(string caminho, string nome, string desc, bool oficial, int ano, int mes)
        {
            if (!File.Exists(caminho))
            {
                return "Arquivo não existe!";
            }

            Prevs prevs = lePrevs(caminho, ano, mes);

            IList<Postos> postos = PostosDAO.GetAll();
            Calculados calc = new Calculados();
            foreach (Postos p in postos)
            {

                if (prevs.dados.Any(x => x.posto == p.numero))
                    p.prevs_base = prevs.dados.Where(x => x.posto == p.numero).First();

                p.prevs_saida = p.prevs_base;
            }



            Semanas_Ano s = new Semanas_Ano(prevs.ano, prevs.mes, prevs.rev, 0);

            Calculados.atualizar(postos, s, true);

            PrevsDados[] ENAfinal = Prevs.calculaENASubmercados(postos).ToArray();
            double[][] diasMes = DecompTools.Util.UtilitarioDeData.diasMeses(s);
            double[] ENAMedia = Prevs.calculaENAMedia(ENAfinal, diasMes[1]);

            System.Reflection.PropertyInfo mesMLT = (new MltSub()).GetType().GetProperty(String.Concat("mes", s.mes.ToString()));
            MltSub[] mlt = MLTDAO.getAll().ToArray();


            StringBuilder linha = new StringBuilder();
            for (int x = 0; x < 4; x++)
            {

                double mediaMlt = Math.Round((ENAMedia[x] / (int)mesMLT.GetValue(mlt[x], null)) * 100, 0);

                linha.Append(Math.Round(ENAfinal[x].sem1, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAfinal[x].sem2, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAfinal[x].sem3, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAfinal[x].sem4, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAfinal[x].sem5, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAfinal[x].sem6, 0)); linha.Append("\t");
                linha.Append(Math.Round(ENAMedia[x], 0)); linha.Append("\t");
                linha.AppendLine(mediaMlt.ToString() + "%");

            }

            if (System.Windows.Forms.MessageBox.Show(linha.ToString() + "\r\n\r\n Confirma o carregamento?", "ENA", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {

                if (oficial)
                {
                    Prevs prevsOficial = PrevsDAO.getPrevsOficialByMonth(prevs.mes, prevs.ano);
                    if (prevsOficial != null)
                    {
                        prevsOficial.oficial = 0;
                        prevsOficial.save();
                    }
                    prevs.oficial = 1;
                }

                prevs.save();
                return prevs.id.ToString();

            }
            else return "Cancelado";

        }

        public Prevs lePrevs(string caminho, int ano, int mes)
        {

            Prevs prevs = new Prevs();
            prevs.rev = int.Parse(caminho.Substring(caminho.Length - 1, 1));
            prevs.caminho = caminho;
            prevs.oficial = 0;
            prevs.ano = ano;
            prevs.mes = mes;

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(prevs.caminho))
            {
                string sLine = "";
                ArrayList arrText = new ArrayList();

                List<PrevsDados> prevsDados = new List<PrevsDados>();

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
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
