using System;
using CapturaNW.Util;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


using CapturaNW.Factory;

namespace CapturaNW.Modelagem
{
    public class PEQUENAS : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual int Ano { get; set; }
        public virtual string Intercambio { get; set; }
        public virtual int Mes1 { get; set; }
        public virtual int Mes2 { get; set; }
        public virtual int Mes3 { get; set; }
        public virtual int Mes4 { get; set; }
        public virtual int Mes5 { get; set; }
        public virtual int Mes6 { get; set; }
        public virtual int Mes7 { get; set; }
        public virtual int Mes8 { get; set; }
        public virtual int Mes9 { get; set; }
        public virtual int Mes10 { get; set; }
        public virtual int Mes11 { get; set; }
        public virtual int Mes12 { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public PEQUENAS()
        {
            pos = new int[] { 4, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
        }


        public override void preencheCampos(string[] s)
        {
            try
            {
                this.Ano = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.Mes1 = String.Equals(s[2], String.Empty) ? 0 : int.Parse(s[2].Replace(".", ""));
                this.Mes2 = String.Equals(s[3], String.Empty) ? 0 : int.Parse(s[3].Replace(".", ""));
                this.Mes3 = String.Equals(s[4], String.Empty) ? 0 : int.Parse(s[4].Replace(".", ""));
                this.Mes4 = String.Equals(s[5], String.Empty) ? 0 : int.Parse(s[5].Replace(".", ""));
                this.Mes5 = String.Equals(s[6], String.Empty) ? 0 : int.Parse(s[6].Replace(".", ""));
                this.Mes6 = String.Equals(s[7], String.Empty) ? 0 : int.Parse(s[7].Replace(".", ""));
                this.Mes7 = String.Equals(s[8], String.Empty) ? 0 : int.Parse(s[8].Replace(".", ""));
                this.Mes8 = String.Equals(s[9], String.Empty) ? 0 : int.Parse(s[9].Replace(".", ""));
                this.Mes9 = String.Equals(s[10], String.Empty) ? 0 : int.Parse(s[10].Replace(".", ""));
                this.Mes10 = String.Equals(s[11], String.Empty) ? 0 : int.Parse(s[11].Replace(".", ""));
                this.Mes11 = String.Equals(s[12], String.Empty) ? 0 : int.Parse(s[12].Replace(".", ""));
                this.Mes12 = String.Equals(s[13], String.Empty) ? 0 : int.Parse(s[13].Replace(".", ""));
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
            List<CUSTO_DEF> lstcd = new List<CUSTO_DEF>();
            List<INTERCAMBIO> lstinter = new List<INTERCAMBIO>();
            List<MERCADO> lstmerc = new List<MERCADO>();
            List<PEQUENAS> lstpq = new List<PEQUENAS>();

            int bloco = 0;
            int newBloco;
            string inter1 = "";
            string inter2 = "";
            string inter = "";

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null && !sLine.Contains("XXX") && !sLine.StartsWith(" 999") && !sLine.Contains("B->A") && !sLine.Contains("PATAMAR") && !sLine.StartsWith("POS"))
                    {
                        if ((newBloco = escolheBloco(sLine)) != 0)
                            bloco = newBloco;
                        else
                        {
                            // Custo deficit
                            if (bloco == 1)
                            {
                                CUSTO_DEF c = new CUSTO_DEF();
                                c.leLinha(sLine);
                                lstcd.Add(c);
                            }
                            // Intercambio
                            else if (bloco == 2)
                            {
                                if (sLine.Length == 24)
                                {
                                    inter1 = intercambioFeito(sLine);
                                    inter2 = intercambioFeito(String.Concat(sLine.Substring(4, 4), sLine.Substring(0, 4)));
                                    inter = inter1;
                                }
                                else if (sLine.Length < 24)
                                    inter = inter2;
                                else
                                {
                                    INTERCAMBIO i = new INTERCAMBIO();
                                    i.leLinha(sLine);
                                    i.Intercambio = inter;
                                    lstinter.Add(i);
                                }
                            }
                            // Mercado
                            else if (bloco == 3)
                            {
                                if (sLine.Trim().Length == 1)
                                    inter = UtilitarioDeTexto.nomeSubmercado(int.Parse(sLine.Trim()));
                                else
                                {
                                    MERCADO m = new MERCADO();
                                    m.leLinha(sLine);
                                    m.Intercambio = inter;
                                    lstmerc.Add(m);
                                }
                            }
                            // Pequenas
                            else if (bloco == 4)
                            {
                                if (sLine.Trim().Length == 1)
                                    inter = UtilitarioDeTexto.nomeSubmercado(int.Parse(sLine.Trim()));
                                else
                                {
                                    PEQUENAS m = new PEQUENAS();
                                    m.leLinha(sLine);
                                    m.Intercambio = inter;
                                    lstpq.Add(m);
                                }
                            }
                        }
                    }
                }

                deck.custo_def = lstcd;
                deck.intercambio = lstinter;
                deck.mercado = lstmerc;
                deck.pequenas = lstpq;
            }
        }

        public static int escolheBloco(string s)
        {
            switch (s.Trim())
            {
                case "CUSTO DO DEFICIT":
                    return 1;
                case "LIMITES DE INTERCAMBIO":
                    return 2;
                case "MERCADO DE ENERGIA TOTAL":
                    return 3;
                case "GERACAO DE PEQUENAS USINAS":
                    return 4;
                case "GERACAO DE USINAS NAO SIMULADAS":
                    return 4;
            }

            return 0;
        }

        public static string intercambioFeito(string s)
        {
            string sub1;
            string sub2;

            sub1 = UtilitarioDeTexto.nomeSubmercado(int.Parse(s.Substring(0, 4).Trim()));
            sub2 = UtilitarioDeTexto.nomeSubmercado(int.Parse(s.Substring(4, 4).Trim()));

            return String.Concat(sub1, "->", sub2);
        }
    }
}
