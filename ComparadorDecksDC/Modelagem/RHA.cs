using CapturaNW.Modelagem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class RHA : Restricoes
    {
        public override void definePos()
        {
            switch (this.bloco)
            {
                case "HA":
                    pos = new int[] { 5, 4, 5 };
                    nome = "HA";
                    break;

                case "LA":
                    pos = new int[] { 5, 4, 30 };
                    nome = "LA";
                    break;

                case "CA":
                    pos = new int[] { 5, 4, 6 };
                    nome = "CA";
                    break;
            }
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            Restricoes.atualizarRV0Opcional(deck, deckBase, "RHA");
        }
    }
}
