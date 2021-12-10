using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class VE : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }

        

        public VE()
        {
            pos = new int[] { 5, 7, 5, 5, 5, 5, 5, 5 };
            nome = "VE";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase)
        {
            VE veT = new VE();

            PropertyInfo camp1 = veT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = veT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());

            if (nSemanasAtual < nSemanasBase)
            {
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, String.Empty, null);

                //this.campo7 = this.campo8;
                //this.campo8 = String.Empty;
            }
            else
            {
                PropertyInfo camp3 = veT.GetType().GetProperty("campo" + (nSemanasBase + 1).ToString());
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, camp3.GetValue(this, null), null);

                //this.campo8 = this.campo7;
                //this.campo7 = this.campo6;
            }
        }

        public static void atualizarRVX(Deck deck, Semanas s)
        {
            int sem = (s.semanas + 1) - deck.rev + 2; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            VE veT = new VE();

            for (int x = 2; x < sem; x++)
            {
                PropertyInfo camp1 = veT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = veT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (VE ve in deck.ve)
                {
                    camp1.SetValue(ve, camp2.GetValue(ve, null), null);
                }
            }

            for (int x = sem; x < 9; x++)
            {
                PropertyInfo camp = veT.GetType().GetProperty("campo" + x.ToString());

                foreach (VE ve in deck.ve)
                {
                    camp.SetValue(ve, null, null);
                }
            }
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, Semanas sAnoAnterior)
        {
            int semanaMesSeguinte = sAnoAnterior.semanas + 2 - deckHistorico.rev;
            PropertyInfo block = typeof(VE).GetProperty(String.Concat("campo", semanaMesSeguinte.ToString()));
            IList<VE> listVE = new List<VE>();

            foreach (VE ve in deckHistorico.ve)
            {
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
    }
}
