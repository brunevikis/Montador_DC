using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class VR : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }



        public VR() {
            pos = new int[] { 4, 4, 7 };
            nome = "VR";
        }

        public static void atualizarRV0(Deck deck, Semanas s) {
            deck.vr.Clear();

            if (s.mes == 10) {
                VR vr = new VR();
                vr.deck = deck;
                vr.campo1 = "11";
                vr.campo2 = " ";
                vr.campo3 = "INI";
                deck.vr.Add(vr);
            } else if (s.mes == 11) {
                VR vr = new VR();
                vr.deck = deck;
                vr.campo1 = "11";
                vr.campo2 = "2";
                if (s.primeiraSemana.Day == 1)
                    vr.campo2 = "1";
                vr.campo3 = "INI";
                deck.vr.Add(vr);
            } else if (s.mes == 1) {
                VR vr = new VR();
                vr.deck = deck;
                vr.campo1 = "02";
                vr.campo2 = " ";
                vr.campo3 = "FIM";
                deck.vr.Add(vr);
            } else if (s.mes == 2) {
                VR vr = new VR();
                vr.deck = deck;
                vr.campo1 = "02";
                vr.campo2 = "4";
                if (s.primeiraSemana.Day == 1)
                    vr.campo2 = "3";
                vr.campo3 = "FIM";
                deck.vr.Add(vr);
            }
        }

        public static void atualizarRVX(Deck deck) {
            int x;
            if (deck.vr != null && deck.vr.Count > 0) {
                VR vr = deck.vr[0];
                if (int.TryParse(vr.campo2, out x)) {
                    vr.campo2 = (x - 1).ToString();

                    if (vr.campo2 == "0")
                        deck.vr.Remove(vr);
                }
            }
        }

        public static void atualizarMensal(Deck deck, Semanas s) {
            atualizarRV0(deck, s);

            if (s.mes == 10 || s.mes == 2)
                deck.vr.First<VR>().campo2 = "0";
        }
    }
}
