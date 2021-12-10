using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class CD : blockModel
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

        

        public CD()
        {
            pos = new int[] { 4, 5, 11, 4, 8, 10, 5, 10, 5, 10 };
            nome = "CD";
        }

        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW)
        {
            int nSemanasAtual = sAtual.semanas;
            double[] valorPat = new double[5];
            double[] valorPU = new double[5];

            CUSTO_DEF cdNW = deckNW.custo_def.First();

            valorPat[1] = cdNW.PAT1;
            valorPat[2] = cdNW.PAT2;
            valorPat[3] = cdNW.PAT3;
            valorPat[4] = cdNW.PAT4;

            valorPU[1] = cdNW.PU1 * 100;
            valorPU[2] = cdNW.PU2 * 100;
            valorPU[3] = cdNW.PU3 * 100;
            valorPU[4] = cdNW.PU4 * 100;

            foreach (CD cd in deck.cd)
            {
                cd.campo5 = String.Format("{0:0.0}", valorPU[int.Parse(cd.campo1)]).Replace(",",".");
                cd.campo6 = String.Format("{0:0.00}", valorPat[int.Parse(cd.campo1)]).Replace(",", ".");
                cd.campo7 = String.Format("{0:0.0}", valorPU[int.Parse(cd.campo1)]).Replace(",", ".");
                cd.campo8 = String.Format("{0:0.00}", valorPat[int.Parse(cd.campo1)]).Replace(",", ".");
                cd.campo9 = String.Format("{0:0.0}", valorPU[int.Parse(cd.campo1)]).Replace(",", ".");
                cd.campo10 = String.Format("{0:0.00}", valorPat[int.Parse(cd.campo1)]).Replace(",", ".");
            }
        }
    }
}
