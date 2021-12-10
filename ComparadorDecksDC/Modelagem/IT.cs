using CapturaNW.Modelagem;
using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;

namespace ComparadorDecksDC.Modelagem {
    public class IT : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }



        public IT() {
            pos = new int[] { 4, 6, 4, 8, 5, 5, 5, 5, 5 };
            nome = "IT";
        }

        public static void atualizarRV0(Deck deck, Semanas s, Semanas sBase) {
            foreach (IT it in deck.it)
                if (String.Equals(it.campo1, (sBase.semanas + 1).ToString()))
                    it.campo1 = (s.semanas + 1).ToString();
        }

        public static void atualizarRVX(Deck deck) {
            for (int x = 0; x < deck.it.Count(); x++) {
                if (deck.it[x].campo1 == "1") {
                    if (x + 1 < deck.it.Count() && deck.it[x + 1].campo1 == "2") {
                        deck.it.Remove(deck.it[x]);
                        x--;
                    }
                } else {
                    deck.it[x].campo1 = (int.Parse(deck.it[x].campo1) - 1).ToString();
                }
            }
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            mWSheet1.SetValue(rol, 2, this.campo1);
            mWSheet1.SetValue(rol, 1, this.campo2);
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev) {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            return tupleList.OrderBy(ty => blockModel.ordemRVDif(usinaProperty.GetValue(ty.Item2).ToString(), difRev, ty.Item1));
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, int p) {
            if (p == 2) //Sazonal
                novoDeck.it = deckHistorico.it;

            foreach (IT it in novoDeck.it)
                if (int.Parse(it.campo1) > 1)
                    it.campo1 = "2";
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, DeckNW deckNwBase, DateTime dataInicio, int p) {
            if (p == 2) //Sazonal

                novoDeck.it = deckHistorico.it;

            else if (p == 1) {

                foreach (IT it in novoDeck.it)
                    if (int.Parse(it.campo1) > 1)
                        it.campo1 = "2";

            } else if (p == 3) { //NW C_ADIC
                DateTime dataFim = dataInicio.AddMonths(1);
                Semanas_Patamares patamar = SemanasPatamaresDAO.GetByPeriod(dataInicio, dataFim.AddDays(-1));
                Semanas_Patamares patamar2 = SemanasPatamaresDAO.GetByPeriod(dataFim, dataFim.AddMonths(1).AddDays(-1));

                novoDeck.it = new List<IT>(); 
                calculaLinhaMensal(deckNwBase, dataInicio, 1, patamar);
                novoDeck.it.Add(calculaLinhaMensal(deckNwBase, dataInicio, 1, patamar));
                novoDeck.it.Add(calculaLinhaMensal(deckNwBase, dataFim, 2, patamar2));
            }

        }

        public static IT calculaLinhaMensal(DeckNW deckNwBase, DateTime dataBase, int indiceSemana, Semanas_Patamares patamar) {
            PropertyInfo MercadoMes = typeof(C_ADIC).GetProperty("Mes" + dataBase.Month.ToString());
            PropertyInfo PatMes = typeof(PAT_CARGA).GetProperty("Mes" + dataBase.Month.ToString());


            var c_adic = deckNwBase.c_adic.Where(y => y.Ano == dataBase.Year && (y.Submercado == UtilitarioDeTexto.nomeSubmercado(1) || y.Submercado.Trim() == "1"))
                .Sum( y=> double.Parse(MercadoMes.GetValue(y).ToString()));
            List<PAT_CARGA> lstPat = deckNwBase.pat_carga.Where(y => y.Ano == dataBase.Year && y.Submercado == UtilitarioDeTexto.nomeSubmercado(1)).ToList<PAT_CARGA>();

            double pat1 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Pesado").First<PAT_CARGA>()).ToString());
            double pat2 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Medio").First<PAT_CARGA>()).ToString());
            double pat3 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Leve").First<PAT_CARGA>()).ToString());

            IT dp = new IT();
            dp.campo1 = indiceSemana.ToString();
            dp.campo2 = "66";
            dp.campo3 = "1";
            dp.campo4 = Math.Round(c_adic * pat1 + 1900, 0).ToString();
            dp.campo5 = Math.Round(c_adic * pat1 , 0).ToString();
            dp.campo6 = Math.Round(c_adic * pat2 + 1900, 0).ToString();
            dp.campo7 = Math.Round(c_adic * pat2 , 0).ToString();
            dp.campo8 = Math.Round(c_adic * pat3 + 1900, 0).ToString();
            dp.campo9 = Math.Round(c_adic * pat3 , 0).ToString();


            return dp;
        }
    }
}
