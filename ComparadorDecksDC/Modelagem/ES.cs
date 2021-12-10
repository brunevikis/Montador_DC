using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace ComparadorDecksDC.Modelagem {
    public class ES : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }

        public ES() {
            pos = new int[] { 4, 4, 14, 10, 10, 10 };
            nome = "ES";
        }

        public static void atualizarRVX(Deck deck) {
            if (deck.rev == 1) {
                for (int x = 1; x < 5; x++) {
                    ES es = new ES();
                    es.deck = deck;
                    es.campo1 = x.ToString();
                    es.campo2 = "1";
                    es.campo3 = "AAAA";

                    deck.es.Add(es);
                }
            } else {
                foreach (ES es in deck.es) {
                    es.campo2 = (int.Parse(es.campo2) + 1).ToString();
                    es.campo6 = es.campo5;
                    es.campo5 = es.campo4;
                    es.campo4 = es.campo3;
                    es.campo3 = "AAAA";
                }
            }
        }

        public override void escreveLinhaExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol, int numDeck, int difRevDecks, bool escreveTitulo) {

            rol = 3 + int.Parse(campo1.Trim()) + ((numDeck - 1) * 6);
            for (int i = 3; i <= 6; i++) {
                try {
                    PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                    var value = block.GetValue(this);
                    if (value == null) break;
                    mWSheet1.SetValue(rol, i, value.ToString());

                } catch (Exception) {
                    break;
                }
            }
        }
    }
}
