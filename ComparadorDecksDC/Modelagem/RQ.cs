using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class RQ : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }

        

        public RQ()
        {
            pos = new int[] { 4, 8, 5, 5, 5, 5, 5, 5 };
            nome = "RQ";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase)
        {
           RQ rqT = new RQ();

            PropertyInfo camp1 = rqT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = rqT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());

            if (nSemanasAtual < nSemanasBase)
            {
                camp1.SetValue(this, "0.", null);
                camp2.SetValue(this, String.Empty, null);

                //this.campo7 = "0.";
                //this.campo8 = String.Empty;
            }
            else
            {
                camp2.SetValue(this, String.Equals(this.campo1, "5") ? "0." : "100.", null);
                camp1.SetValue(this, "0.", null);

                //this.campo7 = String.Equals(this.campo1, "5") ? "0." : "100.";
                //this.campo8 = "0.";
            }
        }

        public static void atualizarRVX(Deck deck, Semanas s)
        {
            int sem = (s.semanas + 1) - deck.rev + 2; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            RQ rqT = new RQ();

            for (int x = 2; x < sem; x++)
            {
                PropertyInfo camp1 = rqT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = rqT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (RQ rq in deck.rq)
                {
                    camp1.SetValue(rq, camp2.GetValue(rq, null), null);
                }
            }

            for (int x = sem; x < 9; x++)
            {
                PropertyInfo camp = rqT.GetType().GetProperty("campo" + x.ToString());

                foreach (RQ rq in deck.rq)
                {
                    camp.SetValue(rq, null, null);
                }
            }
        }
    }
}
