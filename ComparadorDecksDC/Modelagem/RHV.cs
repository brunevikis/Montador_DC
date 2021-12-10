using CapturaNW.Modelagem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class RHV : Restricoes
    {
        public override void definePos()
        {
            switch (this.bloco)
            {
                case "HV":
                    pos = new int[] { 5, 4, 5 };
                    nome = "HV";
                    break;

                case "LV":
                    pos = new int[] { 5, 4, 24 };
                    nome = "LV";
                    break;

                case "CV":
                    pos = new int[] { 5, 4, 27 };
                    nome = "CV";
                    break;
            }
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            Restricoes.atualizarRV0Opcional(deck, deckBase, "RHV");
        }
    }
}
