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
    public class EXPT : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Usina { get; set; }
        public virtual int Mes_Inicial { get; set; }
        public virtual int Ano_Inicial { get; set; }
        public virtual int Mes_Final { get; set; }
        public virtual int Ano_Final { get; set; }
        public virtual string TIPO { get; set; }
        public virtual double VALOR { get; set; }
        public virtual int Maq_Num { get; set; }
        public virtual int Indexado { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public EXPT()
        {
            pos = new int[] { 4, 6, 9, 3, 5, 3, 5, 30 };
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                this.Codigo = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.TIPO = s[2];
                this.VALOR = String.Equals(s[3], String.Empty) ? 0 : double.Parse(s[3].Replace(".", ","));
                this.Mes_Inicial = String.Equals(s[4], String.Empty) ? 0 : int.Parse(s[4]);
                this.Ano_Inicial = String.Equals(s[5], String.Empty) ? 0 : int.Parse(s[5]);
                this.Mes_Final = String.Equals(s[6], String.Empty) ? 0 : int.Parse(s[6]);
                this.Ano_Final = String.Equals(s[7], String.Empty) ? 0 : int.Parse(s[7]);
                this.Usina = s[8];
            }
            catch (IndexOutOfRangeException )
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
            string usina = "";
            int pot = 0;
            int gete = 0;
            int ipt = 0;
            int fcma = 0;

            List<EXPT> lst = new List<EXPT>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.StartsWith("XXXX") && !sLine.StartsWith("NUM"))
                    {
                        EXPT e = new EXPT();

                        e.leLinha(sLine);

                        if (!String.Equals(e.Usina, String.Empty))
                        {
                            usina = e.Usina;
                            pot = 0;
                            gete = 0;
                            ipt = 0;
                            fcma = 0;
                        }
                        e.Usina = usina;

                        switch (e.TIPO)
                        {
                            case "POTEF":
                                pot++;
                                e.Maq_Num = pot;
                                break;

                            case "FCMAX":
                                fcma++;
                                e.Maq_Num = fcma;
                                break;

                            case "GTMIN":
                                gete++;
                                e.Maq_Num = gete;
                                break;

                            case "IPTER":
                                ipt++;
                                e.Maq_Num = ipt;
                                break;
                        }

                        lst.Add(e);
                    }
                }

                deck.expt = lst;
            }
        }
    }
}