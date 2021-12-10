using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using ComparadorDecksDC.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Controller
{
    class controllerEscreve
    {
        private readonly FormEscreve _tela;

        public controllerEscreve(FormEscreve tela)
        {
            _tela = tela;
        }

        /// <summary>
        /// Escreve o deck em um local pre-definido o dadger escolhido na tela.
        /// </summary>
        public void escreverDeck()
        {
            Deck deck = _tela.deck;
            Deck deckFull = new Deck();
            deckFull = DeckDAO.getAllBlocksbyID( deck.id );
            deckFull.escreveDeck( _tela.caminho );

            _tela.showWarning("Deck escrito com sucesso");
        }
    }
}
