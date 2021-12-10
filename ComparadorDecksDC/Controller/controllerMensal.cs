using CapturaNW.Modelagem;
using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Controller {
    public class controllerMensal {
        public static string gerarMensal(Deck deckDcBase, DeckNW deckNwBase, string ENAP, string ENAF, string reserv, DateTime dataInicio, DateTime dataFim, string exitPath, int[] configBlock) {
            int diferenca = ((dataFim.Year - dataInicio.Year) * 12) + dataFim.Month - dataInicio.Month;

            int[,] enaPsplit = UtilitarioDeTexto.splitEna(ENAP);
            int[,] enaFsplit = UtilitarioDeTexto.splitEna(ENAF);

            if (enaFsplit.GetLength(1) != diferenca + 1)
                return "Numero de meses da ENA futura diferente do numero de meses a avançar";

            decimal[,] reservSplit = UtilitarioDeTexto.splitEnaDecimal(reserv);

            if (reservSplit.GetLength(1) != diferenca + 1)
                return "Numero de meses do reservatorio diferente do numero de meses a avançar";

            string[] a = ENAP.Split('\n');
            string[] b = ENAF.Split('\n');
            StringBuilder c = new StringBuilder();

            for (int x = 0; x < 4; x++) {
                if (a.Length > 1) {
                    c.Append(a[x]);
                    c.Append(" ");
                }
                c.Append(b[x]);
                c.Append(" \n ");
            }

            int[,] ENAsplit = UtilitarioDeTexto.splitEna(c.ToString());
            Deck deckBase = deckDcBase;
            List<CT> ctBase = CT.normalizaBaseMensal(deckBase);

            while (diferenca >= 0) {
                Deck novoDeck = new Deck();

                //Copia todos os blocos do deckBase para o novo deck
                novoDeck.clone(deckBase);

                //Seta as informações do deck e salva o novo deck
                novoDeck.caminho = Path.Combine(exitPath, string.Concat(dataInicio.Year.ToString(), UtilitarioDeTexto.zeroEsq(dataInicio.Month, 2)));
                novoDeck.te = String.Concat("TE  ", "Deck mensal gerado automaticamente - Estudo para Ano " + dataInicio.Year.ToString() + " e Mes " + dataInicio.Month.ToString());
                novoDeck.rev = -1;                          //Flag para deck mensal
                novoDeck.ano = dataInicio.Year;
                novoDeck.mes = dataInicio.Month;
                novoDeck.dia = 1;

                atualizaDadosMensal(novoDeck, deckBase, deckNwBase, dataInicio, dataFim, ENAsplit, reservSplit, diferenca, configBlock);
                novoDeck.ct = CT.atualizarMensal(ctBase, dataInicio, deckNwBase);        //Esta aqui apenas para não enviar o ctBase para a outra função.

                System.IO.Directory.CreateDirectory(novoDeck.caminho);
                novoDeck.escreveDeck(novoDeck.caminho, "dadger." + UtilitarioDeData.NomeMes(dataInicio.Month).ToLower(), UtilitarioDeData.NomeMes(dataInicio.Month).ToLower());


                deckBase = novoDeck;
                diferenca = diferenca - 1;
                dataInicio = dataInicio.AddMonths(1);
            }

            return "Processo finalizado com sucesso";
        }


        public static async Task<string> gerarMensalAsync(Deck deckDcBase, DeckNW deckNwBase, string ENAP, string ENAF, string reserv, DateTime dataInicio, DateTime dataFim, string exitPath, int[] configBlock) {
            int diferenca = ((dataFim.Year - dataInicio.Year) * 12) + dataFim.Month - dataInicio.Month;

            int[,] enaPsplit = UtilitarioDeTexto.splitEna(ENAP);
            int[,] enaFsplit = UtilitarioDeTexto.splitEna(ENAF);

            if (enaFsplit.GetLength(1) != diferenca + 1)
                return "Numero de meses da ENA futura diferente do numero de meses a avançar";

            decimal[,] reservSplit = UtilitarioDeTexto.splitEnaDecimal(reserv);

            if (reservSplit.GetLength(1) != diferenca + 1)
                return "Numero de meses do reservatorio diferente do numero de meses a avançar";

            string[] a = ENAP.Split('\n');
            string[] b = ENAF.Split('\n');
            StringBuilder c = new StringBuilder();

            for (int x = 0; x < enaFsplit.GetLength(0); x++) {
                if (a.Length > 1) {
                    c.Append(a[x]);
                    c.Append(" ");
                }
                c.Append(b[x]);
                c.Append(" \n ");
            }

            int[,] ENAsplit = UtilitarioDeTexto.splitEna(c.ToString());
            Deck deckBase = deckDcBase;
            var tctBase = Task.Run(() => CT.normalizaBaseMensal(deckBase));

            while (diferenca >= 0) {
                Deck novoDeck = new Deck();

                //Copia todos os blocos do deckBase para o novo deck
                novoDeck.clone(deckBase);

                //Seta as informações do deck e salva o novo deck
                novoDeck.caminho = Path.Combine(exitPath, string.Concat(dataInicio.Year.ToString(), UtilitarioDeTexto.zeroEsq(dataInicio.Month, 2)));
                novoDeck.te = String.Concat("TE  ", "Deck mensal gerado automaticamente - Estudo para Ano " + dataInicio.Year.ToString() + " e Mes " + dataInicio.Month.ToString());
                novoDeck.rev = -1;                          //Flag para deck mensal
                novoDeck.ano = dataInicio.Year;
                novoDeck.mes = dataInicio.Month;
                novoDeck.dia = 1;

                await atualizaDadosMensalAsync(novoDeck, deckBase, deckNwBase, dataInicio, dataFim, ENAsplit, reservSplit, diferenca, configBlock);
                novoDeck.ct = CT.atualizarMensal(await tctBase, dataInicio, deckNwBase);        //Esta aqui apenas para não enviar o ctBase para a outra função.

                System.IO.Directory.CreateDirectory(novoDeck.caminho);
                novoDeck.escreveDeck(novoDeck.caminho, "dadger." + UtilitarioDeData.NomeMes(dataInicio.Month).ToLower(), UtilitarioDeData.NomeMes(dataInicio.Month).ToLower());


                deckBase = novoDeck;
                diferenca = diferenca - 1;
                dataInicio = dataInicio.AddMonths(1);
            }

            return "Processo finalizado com sucesso";
        }


        private static void atualizaDadosMensal(Deck novoDeck, Deck deckBase, DeckNW deckNwBase, DateTime dataInicio, DateTime dataFim, int[,] ENA, decimal[,] reservSplit, int diferenca, int[] configBlock) {
            Semanas sAtual = SemanasDAO.GetBySemanaInicial(novoDeck.ano, novoDeck.mes, novoDeck.dia);
            Semanas sBase = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);
            Semanas sAnoAnterior = SemanasDAO.GetByMesAno(sAtual.mes, sAtual.ano - 1);

            Deck deckHistorico = DeckDAO.getDeckOficialByDate(sAnoAnterior, 0);

            //Apenas quando o deck anterior não é mensal
            AC.atualizarMensal(novoDeck, deckNwBase, sAtual);
            MT.atualizarMensal(novoDeck);
            IA.atualizarMensal(novoDeck);
            TI.atualizarMensal(novoDeck, deckBase.rev, sBase);
            FD.atualizarMensal(novoDeck, deckBase.rev, sBase);


            VR.atualizarMensal(novoDeck, sAtual);
            EA.atualizarMensal(novoDeck, deckBase, ENA, diferenca);

            //Limpa bloco ES se existir
            novoDeck.es.Clear();

            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHA");           //enquanto as restrições sazonais não entrarem
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHE");
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHQ");
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHV");
            PQ.atualizarMensal(novoDeck, deckNwBase);
            DP.atualizarMensal(novoDeck, deckNwBase, dataInicio);
            VE.atualizarMensal(novoDeck, deckHistorico, sAnoAnterior);


            //Blocos com alternativas
            VI.atualizarMensal(novoDeck, deckHistorico, configBlock[0]);
            QI.atualizarMensal(novoDeck, deckHistorico, configBlock[1]);
            IT.atualizarMensal(novoDeck, deckHistorico, deckNwBase, dataInicio, configBlock[2]);
            MP.atualizarMensal(novoDeck, deckBase, deckHistorico, sAnoAnterior, configBlock[3]);

            novoDeck.uh = UH.atualizarMensal(deckBase, reservSplit, (diferenca + 1), dataInicio, configBlock[4]);
        }

        private static async Task atualizaDadosMensalAsync(Deck novoDeck, Deck deckBase, DeckNW deckNwBase, DateTime dataInicio, DateTime dataFim, int[,] ENA, decimal[,] reservSplit, int diferenca, int[] configBlock) {
            Semanas sAtual = SemanasDAO.GetBySemanaInicial(novoDeck.ano, novoDeck.mes, novoDeck.dia);
            Semanas sBase = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);
            Semanas sAnoAnterior = SemanasDAO.GetByMesAno(sAtual.mes, sAtual.ano - 1);

            Deck deckHistorico = DeckDAO.getDeckOficialByDate(sAnoAnterior, 0);

            var tUh = Task.Run(() => UH.atualizarMensal(deckBase, reservSplit, (diferenca + 1), dataInicio, configBlock[4]));

            //Apenas quando o deck anterior não é mensal
            AC.atualizarMensal(novoDeck, deckNwBase, sAtual);
            MT.atualizarMensal(novoDeck);
            IA.atualizarMensal(novoDeck);
            TI.atualizarMensal(novoDeck, deckBase.rev, sBase);
            FD.atualizarMensal(novoDeck, deckBase.rev, sBase);


            VR.atualizarMensal(novoDeck, sAtual);
            EA.atualizarMensal(novoDeck, deckBase, ENA, diferenca);

            //Limpa bloco ES se existir
            novoDeck.es.Clear();

            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHA");           //enquanto as restrições sazonais não entrarem
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHE");
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHQ");
            Restricoes.atualizarMensal(novoDeck, deckBase.rev, sBase, "RHV");
            PQ.atualizarMensal(novoDeck, deckNwBase);
            DP.atualizarMensal(novoDeck, deckNwBase, dataInicio);
            VE.atualizarMensal(novoDeck, deckHistorico, sAnoAnterior);


            //Blocos com alternativas
            VI.atualizarMensal(novoDeck, deckHistorico, configBlock[0]);
            QI.atualizarMensal(novoDeck, deckHistorico, configBlock[1]);
            IT.atualizarMensal(novoDeck, deckHistorico, deckNwBase, dataInicio, configBlock[2]);
            MP.atualizarMensal(novoDeck, deckBase, deckHistorico, sAnoAnterior, configBlock[3]);

            novoDeck.uh = await tUh;
        }
    }
}
