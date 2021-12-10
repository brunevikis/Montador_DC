using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class VE : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }



        public VE() {
            pos = new int[] { 5, 7, 5, 5, 5, 5, 5, 5 };
            nome = "VE";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase) {
            VE veT = new VE();

            PropertyInfo camp1 = veT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = veT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());

            if (nSemanasAtual < nSemanasBase) {
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, String.Empty, null);

                //this.campo7 = this.campo8;
                //this.campo8 = String.Empty;
            } else {
                PropertyInfo camp3 = veT.GetType().GetProperty("campo" + (nSemanasBase + 1).ToString());
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, camp3.GetValue(this, null), null);

                //this.campo8 = this.campo7;
                //this.campo7 = this.campo6;
            }
        }

        public static void atualizarRVX(Deck deck, Semanas s) {
            int sem = (s.semanas + 1) - deck.rev + 2; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            VE veT = new VE();

            for (int x = 2; x < sem; x++) {
                PropertyInfo camp1 = veT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = veT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (VE ve in deck.ve) {
                    camp1.SetValue(ve, camp2.GetValue(ve, null), null);
                }
            }

            for (int x = sem; x < 9; x++) {
                PropertyInfo camp = veT.GetType().GetProperty("campo" + x.ToString());

                foreach (VE ve in deck.ve) {
                    camp.SetValue(ve, null, null);
                }
            }
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, Semanas sAnoAnterior) {
            int semanaMesSeguinte = sAnoAnterior.semanas + 2 - deckHistorico.rev;
            PropertyInfo block = typeof(VE).GetProperty(String.Concat("campo", semanaMesSeguinte.ToString()));
            IList<VE> listVE = new List<VE>();

            foreach (VE ve in deckHistorico.ve) {
                VE newVE = new VE();

                newVE.ordem = ve.ordem;
                newVE.linha = ve.linha;
                newVE.campo1 = ve.campo1;
                newVE.campo2 = ve.campo2;
                newVE.campo3 = block.GetValue(ve).ToString();

                listVE.Add(newVE);
            }

            novoDeck.ve = listVE;
        }

        internal static void atualizarRV0(Deck deck, Deck deckBase, ModelagemNW.DeckNW deckNW, Semanas s, Semanas sBase) {

            deck.ve.Clear();

            var pInfo = typeof(VE).GetProperty("campo" + (sBase.semanas + 1 - deckBase.rev).ToString());


            int mes2 = UtilitarioDeData.mesFinalReal(s.mes);
            int ano2 = mes2 < s.mes ? s.ano + 1 : s.ano;


            foreach (var vmaxG in deckNW.modif.Where(x => x.MNEMONICO.Trim().Equals("VMAXT", StringComparison.OrdinalIgnoreCase))
                .GroupBy(x => x.NUM_USINA)) {

                VE ve = new VE();

                //estagio 1
                var ve0 = deckBase.ve.Where(x => x.campo1 == vmaxG.Key.ToString()).FirstOrDefault();
                if (ve0 == null) continue;

                var veMes0 = double.Parse(pInfo.GetValue(ve0).ToString(), System.Globalization.NumberFormatInfo.InvariantInfo);

                //fim mes
                var ve1 = vmaxG.Where(x => new DateTime(x.ANO, x.MES, 1) <= new DateTime(s.ano, s.mes, 1))
                    .OrderBy(x => new DateTime(x.ANO, x.MES, 1)).LastOrDefault();

                var veMes1 = ve1 != null ? ve1.VALOR
                    : veMes0;

                //mes seguinte

                var ve2 = vmaxG.Where(x => new DateTime(x.ANO, x.MES, 1) <= new DateTime(ano2, mes2, 1))
                    .OrderBy(x => new DateTime(x.ANO, x.MES, 1)).LastOrDefault();

                var veMes2 = ve2 != null ? ve2.VALOR
                    : veMes1;



                ve.campo1 = vmaxG.Key.ToString();

                for (int i = 1; i <= s.semanas; i++) {

                    typeof(VE).GetProperty("campo" + (i + 1).ToString())
                    .SetValue(ve,
                       ((veMes0 * (s.semanas - i) + veMes1 * (i - 1)) / (s.semanas - 1))
                    .ToString("00.00", System.Globalization.NumberFormatInfo.InvariantInfo).Substring(0, 5));
                }

                typeof(VE).GetProperty("campo" + (s.semanas + 2).ToString())
                    .SetValue(ve, veMes2.ToString("00.00", System.Globalization.NumberFormatInfo.InvariantInfo).Substring(0, 5));

                deck.ve.Add(ve);
            }
        }
    }
}
