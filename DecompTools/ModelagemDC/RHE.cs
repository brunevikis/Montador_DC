using DecompTools.ModelagemNW;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DecompTools.ModelagemDC {

    public class RHE : Restricoes {
        public override void definePos() {
            if (this.bloco != null)
                switch (this.bloco) {
                    case "RE":
                        pos = new int[] { 6, 3, 5 };
                        nome = "RE";
                        break;

                    case "LU":
                        pos = new int[] { 6, 3, 13, 20, 30 };
                        nome = "LU";
                        break;

                    case "FU":
                        pos = new int[] { 6, 3, 6, 12, 3};
                        nome = "FU";
                        break;

                    case "FI":
                        pos = new int[] { 6, 3, 5, 5, 13 };
                        nome = "FI";
                        break;

                    case "FT":
                        pos = new int[] { 6, 3, 6, 3, 14 };
                        nome = "FT";
                        break;

                    case "FE":
                        pos = new int[] { 6, 3, 5, 5, 13 };
                        nome = "FE";
                        break;
                }
        }

        public override void preencheCampos(string[] s) {
            base.preencheCampos(s);
            if (s.Length > 3) campo4 = s[3];
            if (s.Length > 4) campo5 = s[4];
        }

        public static void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            deck.clone(deckBase, "RHE");
            Restricoes.atualizarRV0Opcional(deck, deckBase, "RHE", s, sBase);
        }
    }
}
