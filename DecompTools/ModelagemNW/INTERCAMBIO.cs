using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;



namespace DecompTools.ModelagemNW {
    public class INTERCAMBIO : blockModel {
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

        public INTERCAMBIO() {
            pos = new int[] { 4, 10, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
        }


        public override void preencheCampos(string[] s) {
            try {
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
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }
    }
}
