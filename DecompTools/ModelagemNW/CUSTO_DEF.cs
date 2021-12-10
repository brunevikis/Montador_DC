using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


namespace DecompTools.ModelagemNW {
    public class CUSTO_DEF : blockModel {
        public virtual int id { get; set; }
        public virtual int NUM { get; set; }
        public virtual string NOME_SSIS { get; set; }
        public virtual double FORA { get; set; }
        public virtual double PAT1 { get; set; }
        public virtual double PAT2 { get; set; }
        public virtual double PAT3 { get; set; }
        public virtual double PAT4 { get; set; }
        public virtual double PU1 { get; set; }
        public virtual double PU2 { get; set; }
        public virtual double PU3 { get; set; }
        public virtual double PU4 { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public CUSTO_DEF() {
            pos = new int[] { 4, 11, 3, 8, 8, 8, 8, 6, 6, 6, 6 };
        }


        public override void preencheCampos(string[] s) {
            try {
                this.NUM = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.NOME_SSIS = s[2];
                this.FORA = String.Equals(s[3], String.Empty) ? 0 : double.Parse(s[3].Replace(".", ","));
                this.PAT1 = String.Equals(s[4], String.Empty) ? 0 : double.Parse(s[4].Replace(".", ","));
                this.PAT2 = String.Equals(s[5], String.Empty) ? 0 : double.Parse(s[5].Replace(".", ","));
                this.PAT3 = String.Equals(s[6], String.Empty) ? 0 : double.Parse(s[6].Replace(".", ","));
                this.PAT4 = String.Equals(s[7], String.Empty) ? 0 : double.Parse(s[7].Replace(".", ","));
                this.PU1 = String.Equals(s[8], String.Empty) ? 0 : double.Parse(s[8].Replace(".", ","));
                this.PU2 = String.Equals(s[9], String.Empty) ? 0 : double.Parse(s[9].Replace(".", ","));
                this.PU3 = String.Equals(s[10], String.Empty) ? 0 : double.Parse(s[10].Replace(".", ","));
                this.PU4 = String.Equals(s[11], String.Empty) ? 0 : double.Parse(s[11].Replace(".", ","));
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }
    }
}
