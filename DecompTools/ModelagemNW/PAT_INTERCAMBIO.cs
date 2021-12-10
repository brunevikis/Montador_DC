using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemNW {
    public class PAT_INTERCAMBIO : blockModel {
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

        public PAT_INTERCAMBIO() {
            pos = new int[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
        }

        public override void preencheCampos(string[] s) {
            try {
                int i = (s.Length - 12);

                this.Mes1 = float.Parse(s[i++].Replace(".", ","));
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
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }
    }
}
