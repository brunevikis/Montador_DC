using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;




namespace DecompTools.ModelagemNW {
    public class TERM : blockModel {
        public virtual int id { get; set; }
        public virtual int Codigo { get; set; }
        public virtual double Potencia { get; set; }
        public virtual string Usina { get; set; }
        public virtual double FCMX { get; set; }
        public virtual double TEIF { get; set; }
        public virtual double IP { get; set; }
        public virtual double Mes1 { get; set; }
        public virtual double Mes2 { get; set; }
        public virtual double Mes3 { get; set; }
        public virtual double Mes4 { get; set; }
        public virtual double Mes5 { get; set; }
        public virtual double Mes6 { get; set; }
        public virtual double Mes7 { get; set; }
        public virtual double Mes8 { get; set; }
        public virtual double Mes9 { get; set; }
        public virtual double Mes10 { get; set; }
        public virtual double Mes11 { get; set; }
        public virtual double Mes12 { get; set; }
        public virtual double Mes13 { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public TERM() {
            pos = new int[] { 4, 13, 7, 5, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
        }

        public override void preencheCampos(string[] s) {
            try {
                this.Codigo = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.Usina = s[2];
                this.Potencia = String.Equals(s[3], String.Empty) ? 0 : double.Parse(s[3].Replace(".", ","));
                this.FCMX = String.Equals(s[4], String.Empty) ? 0 : double.Parse(s[4].Replace(".", ","));
                this.TEIF = String.Equals(s[5], String.Empty) ? 0 : double.Parse(s[5].Replace(".", ","));
                this.IP = String.Equals(s[6], String.Empty) ? 0 : double.Parse(s[6].Replace(".", ","));
                this.Mes1 = String.Equals(s[7], String.Empty) ? 0 : double.Parse(s[7].Replace(".", ","));
                this.Mes2 = String.Equals(s[8], String.Empty) ? 0 : double.Parse(s[8].Replace(".", ","));
                this.Mes3 = String.Equals(s[9], String.Empty) ? 0 : double.Parse(s[9].Replace(".", ","));
                this.Mes4 = String.Equals(s[10], String.Empty) ? 0 : double.Parse(s[10].Replace(".", ","));
                this.Mes5 = String.Equals(s[11], String.Empty) ? 0 : double.Parse(s[11].Replace(".", ","));
                this.Mes6 = String.Equals(s[12], String.Empty) ? 0 : double.Parse(s[12].Replace(".", ","));
                this.Mes7 = String.Equals(s[13], String.Empty) ? 0 : double.Parse(s[13].Replace(".", ","));
                this.Mes8 = String.Equals(s[14], String.Empty) ? 0 : double.Parse(s[14].Replace(".", ","));
                this.Mes9 = String.Equals(s[15], String.Empty) ? 0 : double.Parse(s[15].Replace(".", ","));
                this.Mes10 = String.Equals(s[16], String.Empty) ? 0 : double.Parse(s[16].Replace(".", ","));
                this.Mes11 = String.Equals(s[17], String.Empty) ? 0 : double.Parse(s[17].Replace(".", ","));
                this.Mes12 = String.Equals(s[18], String.Empty) ? 0 : double.Parse(s[18].Replace(".", ","));
                this.Mes13 = String.Equals(s[19], String.Empty) ? 0 : double.Parse(s[19].Replace(".", ","));
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck) {
            List<TERM> lst = new List<TERM>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho)) {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.Contains("XXXX") && !sLine.StartsWith(" NUM")) {
                        TERM m = new TERM();
                        m.leLinha(sLine);
                        lst.Add(m);
                    }
                }

                deck.term = lst;
            }
        }
    }
}
