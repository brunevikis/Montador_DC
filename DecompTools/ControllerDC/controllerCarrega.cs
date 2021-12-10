using DecompTools.Views;
using DecompTools.ModelagemDC;
using DecompTools.FactoryDC;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using DecompTools.ControllerNW;

namespace DecompTools.ControllerDC {
    public class controllerCarrega {
        public controllerCarrega() {
        }

        /// <summary>
        /// Carrega um dadger no banco de dados, usando valores da tela.
        /// </summary>
        /// <param name="dadger">Caminho para o dadger</param>
        /// <param name="idDeckNW">id do deck newave base</param>
        /// <param name="nome">Nome atrelado ao dadger</param>
        /// <param name="descricao">Descrição atraldada ao dadger</param>
        /// <param name="oficial">0 se não oficial, 1 caso seja um deck oficial</param>
        /// <returns>id deck carregado, em formato string</returns>
        public string carregarDeck(string dadger, int idDeckNW, string nome, string descricao, bool oficial) {
            if (!File.Exists(dadger))
                return "Arquivo não existe!";

            Deck deck = new Deck();

            deck = lerDeck(dadger);

            deck.nome = nome;
            deck.descricao = descricao;
            deck.id_deckNW = idDeckNW;

            if (oficial) {
                Deck deckOficial = DeckDAO.getDeckOficialByMonth(deck);
                if (deckOficial != null) {
                    deckOficial.oficial = 0;
                    deckOficial.save();
                }
                deck.oficial = 1;
            }
            deck.save();

            return deck.id.ToString();
        }

        /// <summary>
        /// Ler os dados do dadger na memoria a partir de um caminho
        /// </summary>
        /// <param name="dadger">Caminho do dadger</param>
        /// <returns></returns>
        public static Deck lerDeck(string dadger) {
            int numLinha = 1;
            string blocoAnt = "";
            string blocoAtual = "";

            string[] exclude = { "SB", "IR", "AR", "CE", "VM", "DF", "EV", "FJ", "RT" }; //Blocos não persistidos em banco de dados.

            Deck deck = new Deck();
            deck.caminho = dadger;


            int rev;

            if (int.TryParse(deck.caminho.Substring(deck.caminho.Length - 1, 1), out rev))
                deck.rev = rev;
            else deck.rev = 0;

            deck.te = "";

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(deck.caminho)) {

                string sLine = "";
                ArrayList arrText = new ArrayList();

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !String.Equals(sLine.Substring(0, 1), "&") && !exclude.Contains(sLine.Substring(0, 2))) {
                        if (String.Equals(sLine.Substring(0, 2), "TE")) {
                            deck.te = sLine;
                        } else if (String.Equals(sLine.Substring(0, 2), "DT")) {
                            deck.dia = int.Parse(sLine.Substring(4, 2));
                            deck.mes = int.Parse(sLine.Substring(9, 2));
                            deck.ano = int.Parse(sLine.Substring(14, 4));
                        } else {
                            blocoAtual = deck.escolheBloco(sLine.Substring(0, 2));

                            if (blocoAtual != blocoAnt) {
                                deck.add(arrText, blocoAnt, numLinha);
                                numLinha = numLinha + arrText.Count;
                                arrText.Clear();
                            }
                            blocoAnt = blocoAtual;
                            arrText.Add(sLine);
                        }
                    }
                }
                //Para ler o ultimo bloco, ja que ele nunca entra na condição de (blocoAtual != blocoAnt)
                deck.add(arrText, blocoAnt, numLinha);
                numLinha = numLinha + arrText.Count;
                arrText.Clear();
            }

            return deck;
        }
    }
}
