using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem {
    public class RHE : Restricoes {
        public override void definePos() {
            if (this.bloco != null)
                switch (this.bloco) {
                    case "RE":
                        pos = new int[] { 5, 4, 5 };
                        nome = "RE";
                        break;

                    case "LU":
                        pos = new int[] { 5, 4, 13, 20, 30 };
                        nome = "LU";
                        break;

                    case "FU":
                        pos = new int[] { 5, 4, 6, 12 };
                        nome = "FU";
                        break;

                    case "FI":
                        pos = new int[] { 5, 4, 5, 5, 13 };
                        nome = "FI";
                        break;

                    case "FT":
                        pos = new int[] { 5, 4, 6, 3, 14 };
                        nome = "FT";
                        break;
                }
        }

        public override void preencheCampos(string[] s) {
            base.preencheCampos(s);
            if (s.Length > 3) campo4 = s[3];
            if (s.Length > 4) campo5 = s[4];
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase) {
            Restricoes.atualizarRV0Opcional(deck, deckBase, "RHE");
        }
    }
}
