using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class TI : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }



        public TI() {
            pos = new int[] { 5, 7, 5, 5, 5, 5, 5, 5 };
            nome = "TI";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase) {
            TI tiT = new TI();

            PropertyInfo camp1 = tiT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = tiT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());

            if (nSemanasAtual < nSemanasBase) {
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, String.Empty, null);

                //this.campo7 = this.campo8;
                //this.campo8 = String.Empty;
            } else {
                PropertyInfo camp3 = tiT.GetType().GetProperty("campo" + (nSemanasBase + 1).ToString());
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, camp3.GetValue(this, null), null);

                //this.campo8 = this.campo7;
                //this.campo7 = this.campo6;
            }
        }

        public static void atualizarRVX(Deck deck, Semanas s) {
            int sem = (s.semanas + 1) - deck.rev + 2; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            TI tiT = new TI();

            for (int x = 2; x < sem; x++) {
                PropertyInfo camp1 = tiT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = tiT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (TI ti in deck.ti) {
                    camp1.SetValue(ti, camp2.GetValue(ti, null), null);
                }
            }

            foreach (TI ti in deck.ti)
                UtilitarioDeTexto.zerarDados(ti, sem, 8);
        }

        public static void atualizarMensal(Deck novoDeck, int p, Semanas sBase) {
            if (p != -1)            //Verifica se o deck anterior já era mensal.
            {
                int sem = (sBase.semanas + 1) - p + 1; //(Nº de semanas + 1) - (nº semanas passadas)

                foreach (TI ti in novoDeck.ti) {
                    string foo;
                    PropertyInfo camp = ti.GetType().GetProperty("campo" + sem.ToString());
                    foo = camp.GetValue(ti).ToString();

                    if (foo != "0.0")
                        ti.campo3 = foo;

                    UtilitarioDeTexto.zerarDados(ti, 4, 8);
                }
            }
        }
    }
}
