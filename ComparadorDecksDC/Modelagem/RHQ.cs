using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class RHQ : Restricoes
    {  
        public override void definePos()
        {
            switch (this.bloco)
            {
                case "HQ":
                    pos = new int[] { 5, 4, 5 };
                    nome = "HQ";
                    break;

                case "LQ":
                    pos = new int[] { 5, 4, 67 };
                    nome = "LQ";
                    break;

                case "CQ":
                    pos = new int[] { 5, 4, 27 };
                    nome = "CQ";
                    break;
            }
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            Restricoes.atualizarRV0Opcional(deck, deckBase, "RHQ");
        }
    }
}
