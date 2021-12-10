using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class QI : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
                
        public QI() {
            pos = new int[] { 5, 7, 5, 5, 5, 5 };
            nome = "QI";
        }

        public static void atualizarRVX(Deck deck) {
            foreach (QI qi in deck.qi) {
                qi.campo6 = qi.campo5;
                qi.campo5 = qi.campo4;
                qi.campo4 = qi.campo3;
                qi.campo3 = qi.campo2;
                qi.campo2 = "AAA";
            }
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, int p) {
            if (p == 2) //Sazonal
                novoDeck.qi = deckHistorico.qi;
        }
    }
}
