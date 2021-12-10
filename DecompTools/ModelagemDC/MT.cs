using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace DecompTools.ModelagemDC {
    public class MT : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }

        public override int getTitleLength() { return 3; }

        public MT() {
            pos = new int[] { 5, 4, 8, 5, 5, 5, 5, 5, 5 };
            nome = "MT";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase) {
            int sem = (nSemanasAtual + 4); //(Nº de semanas + 1) + ( 3 para ajustar nos campos)
            MT mtT = new MT();

            PropertyInfo camp1 = mtT.GetType().GetProperty("campo" + (nSemanasAtual + 3).ToString());
            PropertyInfo camp2 = mtT.GetType().GetProperty("campo" + (nSemanasBase + 3).ToString());

            camp1.SetValue(this, "1.000", null);
            // camp1.SetValue(this, camp2.GetValue(this, null), null);

            for (int x = sem; x < 10; x++) {
                PropertyInfo camp = mtT.GetType().GetProperty("campo" + x.ToString());
                camp.SetValue(this, String.Empty, null);
            }


            //if (nSemanasAtual < nSemanasBase)
            //    this.campo9 = String.Empty;
            //else
            //    this.campo9 = "1.000";
        }

        public static void atualizarRVX(Deck deck, Semanas s) {
            int sem = (s.semanas + 1) - deck.rev + 3; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            MT mtT = new MT();

            if (deck.mt != null)
            {
                for (int x = 3; x < sem; x++)
                {
                    PropertyInfo camp1 = mtT.GetType().GetProperty("campo" + x.ToString());
                    PropertyInfo camp2 = mtT.GetType().GetProperty("campo" + (x + 1).ToString());

                    foreach (MT mt in deck.mt)
                    {
                        camp1.SetValue(mt, camp2.GetValue(mt, null), null);
                    }
                }

                for (int x = sem; x < 10; x++)
                {
                    PropertyInfo camp = mtT.GetType().GetProperty("campo" + x.ToString());

                    foreach (MT mt in deck.mt)
                    {
                        camp.SetValue(mt, null, null);
                    }
                }
            }
            
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 3, this.campo2);
        }

        public override void escreveLinhaExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol, int numDeck, int difRevDecks, bool escreveTitulo) {
            base.escreveLinhaExcel(mWSheet1, rol, numDeck, difRevDecks, escreveTitulo);

            if (numDeck > 1) {
                int col = this.getTitleLength() + ((this.pos.Length - 1) * (numDeck - 1));
                mWSheet1.SetValue(rol, col + 1 + difRevDecks, "");
            }
        }

        public static void atualizarMensal(Deck novoDeck) {
            foreach (MT mt in novoDeck.mt) {
                mt.campo3 = "1.000";
                mt.campo4 = "1.000";

                UtilitarioDeTexto.zerarDados(mt, 5, 9);
            }
        }
    }
}
