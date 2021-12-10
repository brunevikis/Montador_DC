using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class MP : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }



        public MP() {
            pos = new int[] { 5, 2, 5, 5, 5, 5, 5, 5, 5 };
            nome = "MP";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase) {
            int sem = (nSemanasAtual + 3); //(Nº de semanas + 1) + ( 2 para ajustar nos campos)
            MP mpT = new MP();

            PropertyInfo camp1 = mpT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = mpT.GetType().GetProperty("campo" + (nSemanasBase + 3).ToString());

            camp1.SetValue(this, "1.000", null);
            // camp1.SetValue(this, camp2.GetValue(this, null), null);

            if (nSemanasAtual > nSemanasBase) {
                PropertyInfo camp3 = mpT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());
                camp2.SetValue(this, camp3.GetValue(this, null), null);                                     //this.campo7 = this.campo6;
            }

            UtilitarioDeTexto.zerarDados(this, sem, 9);

            //if (nSemanasAtual < nSemanasBase)
            //{
            //    this.campo7 = "1.000";
            //    //this.campo7 = this.campo8
            //    this.campo8 = String.Empty;
            //}
            //else
            //{
            //    this.campo8 = "1.000";
            //    //this.campo8 = this.campo7;
            //    this.campo7 = this.campo6;
            //}
        }

        public static void atualizarRVX(Deck deck, Semanas s) {
            int sem = (s.semanas + 1) - deck.rev + 3; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            MP mpT = new MP();

            for (int x = 3; x < sem; x++) {
                PropertyInfo camp1 = mpT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = mpT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (MP mp in deck.mp) {
                    camp1.SetValue(mp, camp2.GetValue(mp, null), null);
                }
            }

            for (int x = sem; x < 10; x++) {
                PropertyInfo camp = mpT.GetType().GetProperty("campo" + x.ToString());

                foreach (MP mp in deck.mp) {
                    camp.SetValue(mp, null, null);
                }
            }
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckBase, Deck deckHistorico, Semanas sBaseHist, int p) {
            IList<MP> listMP = new List<MP>();

            if (p == 1) {     //Sem Manutenção
                if (deckBase.rev != -1) // Criar um novo bloco mensal sem manutenção. Caso o anterior já seja mensal, não alterar.
                {
                    foreach (MP mp in novoDeck.mp) {
                        MP mpNew = new MP();

                        mpNew.ordem = mp.ordem;
                        mpNew.linha = mp.linha;
                        mpNew.campo1 = mp.campo1;
                        mpNew.campo2 = "1.000";
                        mpNew.campo3 = "1.000";

                        listMP.Add(mpNew);
                    }
                    novoDeck.mp = listMP;
                }
            } else if (p == 2)            //Sazonal
            {
                int semanaMesSeguinte = sBaseHist.semanas + 2 - deckHistorico.rev;

                PropertyInfo block = typeof(MP).GetProperty(String.Concat("campo", semanaMesSeguinte.ToString()));

                foreach (MP mp in deckHistorico.mp) {
                    MP newMP = new MP();

                    newMP.ordem = mp.ordem;
                    newMP.linha = mp.linha;
                    newMP.campo1 = mp.campo1;
                    newMP.campo2 = mp.campo2;
                    newMP.campo3 = block.GetValue(mp).ToString();

                    listMP.Add(newMP);
                }

                novoDeck.mp = listMP;
            }
        }
    }
}
