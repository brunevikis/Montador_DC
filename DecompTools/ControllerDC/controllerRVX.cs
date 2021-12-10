using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecompTools.ControllerDC {
    class controllerRVX {
        private Boolean ac = false;

        /// <summary>
        /// A partir do deck passado, gera todas as rev. seguintes ate o final do mes
        /// </summary>
        /// <param name="idDeckBase">Deck inicial</param>
        /// <param name="caminho">Caminho de saida para os decks seguintes</param>
        /// <returns>true caso as rev. foram geradas com sucesso, false caso contrario</returns>
        public bool RVX(Deck deckBase, String caminho) {

            Semanas s;

            if (deckBase.rev != 0)
                s = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            else
                s = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);

            var numSemanas = s.semanas - (s.diasMes2 != 0 ? 1 : 0);


            do {

                var folder = System.IO.Path.Combine(caminho, "RV" + deckBase.rev.ToString());
                if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
                deckBase.escreveDeck(folder);
                System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "caso.dat"), "rv" + deckBase.rev.ToString());
                System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "rv" + deckBase.rev.ToString()),
                    "dadger.rv" + deckBase.rev.ToString() +
                    "\nvazoes.rv" + deckBase.rev.ToString() +
                    "\nhidr.dat" +
                    "\nmlt.dat" +
                    "\nloss.dat" +
                    "\ndadgnl.rv" + deckBase.rev.ToString() +
                    "\n./\n\n");

                //if (deckBase.rev + 1 == s.semanas - 1) this.ac = true;
                this.ac = true;

                Deck deckNew = new Deck();
                deckNew.clone(deckBase);
                deckNew.rev = deckBase.rev + 1;
                deckNew.caminho = folder;
                deckNew.id_deckNW = deckBase.id_deckNW;
                deckNew.id_deckDC_base = deckBase.id;
                deckNew.te = deckBase.te ?? "";
                deckNew.te = deckNew.te.Replace("REV " + (deckNew.rev - 1).ToString(), "REV " + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("REV" + (deckNew.rev - 1).ToString(), "REV" + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("RV" + (deckNew.rev - 1).ToString(), "RV" + deckNew.rev.ToString());

                DateTime semanaInicial = s.primeiraSemana;

                for (int x = 0; x < deckNew.rev; x++) {
                    semanaInicial = semanaInicial.AddDays(7);
                }

                deckNew.dia = semanaInicial.Day;
                deckNew.mes = semanaInicial.Month;
                deckNew.ano = semanaInicial.Year;

                atualizarRVX(deckNew, s);

                deckBase = deckNew;

            } while (deckBase.rev + 1 <= numSemanas);

            return true;
        }

        /// <summary>
        /// Faz o papel de atualizar bloco a bloco, inicialmente os blocos sem inteligencia e depois os "blocos inteligentes"
        /// </summary>
        /// <param name="deck">Deck a ser atualizado</param>
        /// <param name="s">Semana atual</param>
        public void atualizarRVX(Deck deck, Semanas s) {
            MP.atualizarRVX(deck, s);
            MT.atualizarRVX(deck, s);
            FD.atualizarRVX(deck, s);
            VE.atualizarRVX(deck, s);
            TI.atualizarRVX(deck, s);
            RQ.atualizarRVX(deck, s);
            DP.atualizarRVX(deck);
            PQ.atualizarRVX(deck);
           // IT.atualizarRVX(deck);
            RI.atualizarRVX(deck);
            VL.atualizarRVX(deck);
            IA.atualizarRVX(deck);
            CT.atualizarRVX(deck);
            VI.atualizarRVX(deck);
            //QI.atualizarRVX(deck);
            //ES.atualizarRVX(deck);

            Restricoes.atualizarRVX(deck, "RHE");

            Restricoes.atualizarRVX(deck, "RHA");
            Restricoes.atualizarRVX(deck, "RHV");
            Restricoes.atualizarRVX(deck, "RHQ");

            VR.atualizarRVX(deck);

            if (this.ac)
                AC.atualizarRVX(deck);
        }
    }
}
