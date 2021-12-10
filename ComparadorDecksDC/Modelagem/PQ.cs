using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace ComparadorDecksDC.Modelagem 
{
    public class PQ : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo3);
        }

        public PQ()
        {
            pos = new int[] { 10, 4, 5, 8, 5, 5 };
            nome = "PQ";
        }

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;
                if (i != 1)
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                else
                {
                    linha.Append("  ");
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1] - 2, 1));
                }
            }

            return linha.ToString();
        }

        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW)
        {
            int nSemanasAtual = sAtual.semanas;
            int ordem = 1;

            deck.pq.Clear();
            int[] valorMes1 = new int[5];
            int[] valorMes2 = new int[5];

            PEQUENAS pqEx = new PEQUENAS();
            PropertyInfo mesAtual = pqEx.GetType().GetProperty( String.Concat("Mes", sAtual.mes.ToString()) );
            PropertyInfo mesMais1 = pqEx.GetType().GetProperty( String.Concat("Mes", UtilitarioDeData.mesFinalReal(sAtual.mes).ToString()) );

            int x = 1;
            foreach (PEQUENAS pqNW in deckNW.pequenas)
            {
                if (pqNW.Ano == sAtual.ano)
                {
                    valorMes1[x] = (int)mesAtual.GetValue(pqNW, null);
                    if( sAtual.mes != 12)
                        valorMes2[x] = (int)mesMais1.GetValue(pqNW, null);
                    x++;
                }
                else if( sAtual.mes == 12 && pqNW.Ano-1 == sAtual.ano)
                    valorMes2[x-1] = (int)mesMais1.GetValue(pqNW, null);
            }

            for( int j = 1; j<5; j++)
            {
                for (int i = 1; i <= nSemanasAtual; i++)
                {
                    PQ pq = new PQ();
                    pq.deck = deck;
                    pq.ordem = ordem;
                    pq.linha = ordem + 300; //Estimativa, vai fazer o numero da linha ficar errado.
                    pq.campo1 = UtilitarioDeTexto.nomeSubmercado(j);
                    pq.campo2 = j.ToString();
                    pq.campo3 = i.ToString();
                    pq.campo4 = valorMes1[j].ToString();
                    pq.campo5 = valorMes1[j].ToString();
                    pq.campo6 = valorMes1[j].ToString();

                    deck.pq.Add(pq);
                    ordem++;
                }

                PQ pqFinal = new PQ();
                pqFinal.deck = deck;
                pqFinal.ordem = ordem;
                pqFinal.linha = ordem + 300;
                pqFinal.campo1 = UtilitarioDeTexto.nomeSubmercado(j);
                pqFinal.campo2 = j.ToString();
                pqFinal.campo3 = (nSemanasAtual + 1).ToString();
                pqFinal.campo4 = valorMes2[j].ToString();
                pqFinal.campo5 = valorMes2[j].ToString();
                pqFinal.campo6 = valorMes2[j].ToString();
                deck.pq.Add(pqFinal);
                ordem++;
            }
        }

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < (deck.pq.Count()); x++)
            {
                if (deck.pq[x].campo3 == "1")
                {
                    deck.pq.Remove(deck.pq[x]);
                    x--;
                }
                else
                    deck.pq[x].campo3 = (int.Parse(deck.pq[x].campo3) - 1).ToString();
            }
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev)
        {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo2");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo3");

            return tupleList.OrderBy(ty => int.Parse(usinaProperty.GetValue(ty.Item2).ToString()))
                .ThenBy(tw => PQ.ordemRVDif( usinaProperty2.GetValue(tw.Item2).ToString(), difRev, tw.Item1));
        }

        public static void atualizarMensal(Deck novoDeck, DeckNW deckNwBase)
        {
            List<PQ> newPq = new List<PQ>();

            for (int x = 1; x < 5; x++)
            {
                int mes = novoDeck.mes;
                int mesMais = UtilitarioDeData.mesFinalReal(novoDeck.mes);

                PEQUENAS pqEx = new PEQUENAS();
                PropertyInfo mes1 = pqEx.GetType().GetProperty(String.Concat("Mes", mes.ToString()));
                PropertyInfo mes2 = pqEx.GetType().GetProperty(String.Concat("Mes", mesMais.ToString()));

                PQ pq1 = new PQ();
                pq1.campo1 = UtilitarioDeTexto.nomeSubmercado(x);
                pq1.campo2 = x.ToString();
                pq1.campo3 = "1";
                pq1.campo4 = mes1.GetValue(deckNwBase.pequenas.First(y => y.Ano == novoDeck.ano && y.Intercambio == pq1.campo1)).ToString();
                pq1.campo5 = pq1.campo4;
                pq1.campo6 = pq1.campo4;

                PQ pq2 = new PQ();
                pq2.campo1 = UtilitarioDeTexto.nomeSubmercado(x);
                pq2.campo2 = x.ToString();
                pq2.campo3 = "2";
                pq2.campo4 = mes2.GetValue(deckNwBase.pequenas.First(y => y.Ano == UtilitarioDeData.anoInicialReal(novoDeck.ano, mes, mesMais) && y.Intercambio == pq2.campo1)).ToString();
                pq2.campo5 = pq2.campo4;
                pq2.campo6 = pq2.campo4;

                newPq.Add(pq1);
                newPq.Add(pq2);
            }

            novoDeck.pq = newPq;
        }
    }
}
