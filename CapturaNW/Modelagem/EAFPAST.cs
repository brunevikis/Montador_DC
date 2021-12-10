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
    public class EAFPAST : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual string num { get; set;}
        public virtual string Submercado { get; set; }
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

        public EAFPAST()
        {
            pos = new int[] { 4, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11};
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                this.num = s[1];
                this.Submercado = s[2];
                this.Mes1 = String.Equals(s[3], String.Empty) ? 0 : (int)double.Parse(s[3].Replace(".", ","));
                this.Mes2 = String.Equals(s[4], String.Empty) ? 0 : (int)double.Parse(s[4].Replace(".", ","));
                this.Mes3 = String.Equals(s[5], String.Empty) ? 0 : (int)double.Parse(s[5].Replace(".", ","));
                this.Mes4 = String.Equals(s[6], String.Empty) ? 0 : (int)double.Parse(s[6].Replace(".", ","));
                this.Mes5 = String.Equals(s[7], String.Empty) ? 0 : (int)double.Parse(s[7].Replace(".", ","));
                this.Mes6 = String.Equals(s[8], String.Empty) ? 0 : (int)double.Parse(s[8].Replace(".", ","));
                this.Mes7 = String.Equals(s[9], String.Empty) ? 0 : (int)double.Parse(s[9].Replace(".", ","));
                this.Mes8 = String.Equals(s[10], String.Empty) ? 0 : (int)double.Parse(s[10].Replace(".", ","));
                this.Mes9 = String.Equals(s[11], String.Empty) ? 0 : (int)double.Parse(s[11].Replace(".", ","));
                this.Mes10 = String.Equals(s[12], String.Empty) ? 0 : (int)double.Parse(s[12].Replace(".", ","));
                this.Mes11 = String.Equals(s[13], String.Empty) ? 0 : (int)double.Parse(s[13].Replace(".", ","));
                this.Mes12 = String.Equals(s[14], String.Empty) ? 0 : (int)double.Parse(s[14].Replace(".", ","));
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
            List<EAFPAST> lst = new List<EAFPAST>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.Contains("XXXX") && !sLine.StartsWith(" NUM"))
                    {
                        EAFPAST m = new EAFPAST();
                        m.leLinha(sLine);
                        lst.Add(m);
                    }
                }

                deck.eafpast = lst;
            }
        }
    }
}
