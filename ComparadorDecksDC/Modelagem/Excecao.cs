using ComparadorDecksDC.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Modelagem
{
    public class Excecao
    {
        public int tipo { get; set; }
        public ArrayList conteudo { get; set; }
        public int idDeck { get; set; }
        public string bloco { get; set; }
        public string textShow { get; set; } //Apenas para mostrar na list, criada apenas por deficiencia minha.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tela"></param>
        public Excecao(FormRV0 _tela)
        {
            //Outro Deck
            if (_tela.tipoExcecao == 1)
            {
                this.tipo = 1;
                this.idDeck = _tela.deckExcept.id;
                this.bloco = _tela.nomeExcecao;
                this.textShow = String.Concat(this.bloco, " -> ", _tela.deckExcept.nome);
            }

            //Input de Texto
            else if (_tela.tipoExcecao == 2)
            {
                ArrayList lst = new ArrayList(_tela.txtExcept);

                this.tipo = 2;
                this.conteudo = lst;
                this.bloco = _tela.nomeExcecao;
                this.textShow = String.Concat(this.bloco, " -> Input manual.");
            }
        }
    }
}