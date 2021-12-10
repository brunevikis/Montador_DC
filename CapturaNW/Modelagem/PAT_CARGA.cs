using CapturaNW.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapturaNW.Modelagem
{
    public class PAT_CARGA : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual string Submercado { get; set; }
        public virtual string Patamar { get; set; }
        public virtual float Ano { get; set; }
        public virtual float Mes1 { get; set; }
        public virtual float Mes2 { get; set; }
        public virtual float Mes3 { get; set; }
        public virtual float Mes4 { get; set; }
        public virtual float Mes5 { get; set; }
        public virtual float Mes6 { get; set; }
        public virtual float Mes7 { get; set; }
        public virtual float Mes8 { get; set; }
        public virtual float Mes9 { get; set; }
        public virtual float Mes10 { get; set; }
        public virtual float Mes11 { get; set; }
        public virtual float Mes12 { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public PAT_CARGA()
        {
            pos = new int[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7};
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                int i = (s.Length - 12);

                this.Mes1 = float.Parse(s[i++].Replace(".",","));
                this.Mes2 = float.Parse(s[i++].Replace(".", ","));
                this.Mes3 = float.Parse(s[i++].Replace(".", ","));
                this.Mes4 = float.Parse(s[i++].Replace(".", ","));
                this.Mes5 = float.Parse(s[i++].Replace(".", ","));
                this.Mes6 = float.Parse(s[i++].Replace(".", ","));
                this.Mes7 = float.Parse(s[i++].Replace(".", ","));
                this.Mes8 = float.Parse(s[i++].Replace(".", ","));
                this.Mes9 = float.Parse(s[i++].Replace(".", ","));
                this.Mes10 = float.Parse(s[i++].Replace(".", ","));
                this.Mes11 = float.Parse(s[i++].Replace(".", ","));
                this.Mes12 = float.Parse(s[i++].Replace(".", ","));
            }
            catch (IndexOutOfRangeException)
            {
                // Deixar em branco (??)
            }
            catch (Exception)
            {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck)
        {
            string strAno;
            string Submercado = "";
            string Patamar = "Pesado";
            int bloco = 0;                                            //Bloco 1 = Carga, 2 = Intercambio
            int ano = 0;
            List<PAT_CARGA> lst_carga = new List<PAT_CARGA>();
            List<PAT_INTERCAMBIO> lst_inter = new List<PAT_INTERCAMBIO>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();

                    if (sLine.Contains("CARGA(P.U.DEMANDA MED.)"))
                        bloco = 1;
                    else if (sLine.StartsWith("9999"))
                        bloco = 0;
                    else if (sLine.Contains("INTERCAMBIO(P.U.INTERC.MEDIO)"))
                        bloco = 2;


                    else if ( bloco != 0 && sLine != null && sLine != String.Empty && !sLine.Contains("XXX"))
                    {
                        if (sLine.Length < 10)
                        {
                            if (bloco == 1)
                                Submercado = UtilitarioDeTexto.nomeSubmercado(int.Parse(sLine.Trim()));
                            else
                                Submercado = PEQUENAS.intercambioFeito(sLine);
                        }

                        else
                        {
                            if( ( strAno = sLine.Substring(0,7).Trim()).Length == 4)
                                ano = int.Parse(strAno);

                            if (bloco == 1)
                            {
                                PAT_CARGA linha = new PAT_CARGA();

                                linha.Ano = ano;
                                linha.Submercado = Submercado;
                                linha.Patamar = Patamar;
                                linha.leLinha(sLine);
                                linha.deckNW = deck;

                                lst_carga.Add(linha);
                            }
                            else
                            {
                                PAT_INTERCAMBIO linha = new PAT_INTERCAMBIO();

                                linha.Ano = ano;
                                linha.Submercado = Submercado;
                                linha.Patamar = Patamar;
                                linha.leLinha(sLine);
                                linha.deckNW = deck;

                                lst_inter.Add(linha);
                            }

                            Patamar = atualizaPat(Patamar);
                        }
                    }
                }

                deck.pat_carga = lst_carga;
                deck.pat_intercambio = lst_inter;
            }
        }

        public static string atualizaPat(string patamar)
        {
            if (patamar == "Pesado")
                return "Medio";
            if (patamar == "Medio")
                return "Leve";
            if (patamar == "Leve")
                return "Pesado";

            return "";
        }
    }
}
