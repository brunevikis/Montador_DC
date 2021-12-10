using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC
{
    public class RI : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }

        public virtual string campo10 { get; set; }
        public virtual string campo11 { get; set; }
        public virtual string campo12 { get; set; }
        public virtual string campo13 { get; set; }
        public virtual string campo14 { get; set; }
        public virtual string campo15 { get; set; }
        public virtual string campo16 { get; set; }
        public virtual string campo17 { get; set; }
        public virtual string campo18 { get; set; }



        public RI()
        {
            pos = new int[] { 5, 4, 4, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
            nome = "RI";
        }


        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < deck.ri.Count(); x++)
            {
                if (deck.ri[x].campo2 == "1")
                {
                    if (x + 1 < deck.ri.Count() && deck.ri[x + 1].campo2 == "2")
                    {
                        deck.ri.Remove(deck.ri[x]);
                        x--;
                    }
                }
                else
                {
                    deck.ri[x].campo2 = (int.Parse(deck.ri[x].campo2) - 1).ToString();
                }
            }
        }
        public static void atualizarRV0(Deck deck, Semanas s, Semanas sBase)
        {
            foreach (RI ri in deck.ri)
                if (String.Equals(ri.campo2, (sBase.semanas + 1).ToString()))
                    ri.campo2 = (s.semanas + 1).ToString();
        }










    }
}
