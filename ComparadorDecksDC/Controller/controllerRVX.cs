using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComparadorDecksDC.Controller
{
    class controllerRVX
    {
        private Boolean ac = false;

        /// <summary>
        /// A partir do deck passado, gera todas as rev. seguintes ate o final do mes
        /// </summary>
        /// <param name="idDeckBase">Deck inicial</param>
        /// <param name="caminho">Caminho de saida para os decks seguintes</param>
        /// <returns>true caso as rev. foram geradas com sucesso, false caso contrario</returns>
        public bool RVX(Deck deckBase, String caminho)
        {
            deckBase.escreveDeck(caminho);
            Semanas s;            

            if (deckBase.rev != 0)
                s = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            else
                s = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);

            for (int y = (deckBase.rev+1); y < s.semanas; y++)
            {
                if (y == s.semanas - 1)
                {
                    //if (s.diasMes2 != 0)
                        //break;
                    //else
                        this.ac = true;
                }

                Deck deckNew = new Deck();
                deckNew.clone(deckBase);
                deckNew.rev = deckBase.rev + 1;
                deckNew.caminho = caminho;
                deckNew.id_deckNW = deckBase.id_deckNW;
                deckNew.id_deckDC_base = deckBase.id;
                deckNew.te = deckBase.te ?? "";
                deckNew.te = deckNew.te.Replace("REV " + (deckNew.rev - 1).ToString(), "REV " + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("REV" + (deckNew.rev - 1).ToString(), "REV" + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("RV" + (deckNew.rev - 1).ToString(), "RV" + deckNew.rev.ToString());

                DateTime semanaInicial = s.primeiraSemana;

                for (int x = 0; x < deckNew.rev; x++)
                {
                    semanaInicial = semanaInicial.AddDays(7);
                }

                deckNew.dia = semanaInicial.Day;
                deckNew.mes = semanaInicial.Month;
                deckNew.ano = semanaInicial.Year;

                atualizarRVX(deckNew, s);

                //deckNew.save();
                deckNew.escreveDeck(caminho);

                deckBase = deckNew;
            }

            return true;
        }

        /// <summary>
        /// Faz o papel de atualizar bloco a bloco, inicialmente os blocos sem inteligencia e depois os "blocos inteligentes"
        /// </summary>
        /// <param name="deck">Deck a ser atualizado</param>
        /// <param name="s">Semana atual</param>
        public void atualizarRVX(Deck deck, Semanas s)
        {
            MP.atualizarRVX(deck, s);
            MT.atualizarRVX(deck, s);
            FD.atualizarRVX(deck, s);
            VE.atualizarRVX(deck, s);
            TI.atualizarRVX(deck, s);
            RQ.atualizarRVX(deck, s);
            DP.atualizarRVX(deck);
            PQ.atualizarRVX(deck);
            IT.atualizarRVX(deck);
            IA.atualizarRVX(deck);
            CT.atualizarRVX(deck);
            VI.atualizarRVX(deck);
            QI.atualizarRVX(deck);
            ES.atualizarRVX(deck);

            Restricoes.atualizarRVX(deck, "RHE");

            Restricoes.atualizarRVX(deck, "RHA");            
            Restricoes.atualizarRVX(deck, "RHV");
            Restricoes.atualizarRVX(deck, "RHQ");

            VR.atualizarRVX(deck);

            if( this.ac)
                AC.atualizarRVX(deck);
        }
    }
}
