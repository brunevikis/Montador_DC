using DecompTools.ModelagemNW;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace DecompTools.ModelagemDC {
    public class PQ : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo3);
        }

        public PQ() {
            pos = new int[] { 12, 2, 5, 8, 5, 5 };
            nome = "PQ";
        }

        public override string escreveLinha() {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++) {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;
                if (i != 1)
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                else {
                    linha.Append("  ");
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1] - 2, 1));
                }
            }

            return linha.ToString();
        }

        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW) {
            int nSemanasAtual = sAtual.semanas;
            int ordem = 1;

            deck.pq.Clear();
            int[,] valorMes1 = new int[5,4];
            int[,] valorMes2 = new int[5,4];


            int[] adic_valorMes1 = new int[] { 0, 0, 0, 0, 0 };
            int[] adic_valorMes2 = new int[] { 0, 0, 0, 0, 0 };


            float[,] patCargaMesAtual = new float[3, 5];
            float[,] patCargaMesSeg = new float[3, 5];
            
            IEnumerator<PAT_CARGA> iPatCarga = deckNW.pat_carga.GetEnumerator();
            PAT_CARGA pat_carga = new PAT_CARGA();
            PAT_NAO_SIMULADAS pat_nao_simuladas = new PAT_NAO_SIMULADAS();

            PropertyInfo pat_carga_mesAtual = pat_carga.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo pat_carga_mesMais1 = pat_carga.GetType().GetProperty(String.Concat("Mes", UtilitarioDeData.mesFinalReal(sAtual.mes).ToString()));

            Func<string, int> getPatIndex = pat => {
                switch (pat.ToUpper()) { case "PESADO": return 0; case "MEDIO": return 1; case "LEVE": return 2; default: throw new ArithmeticException(pat + " invalido"); }
            };
            while (iPatCarga.MoveNext()) {
                PAT_CARGA ma = iPatCarga.Current;
                if (ma.Ano == sAtual.ano)
                    patCargaMesAtual[getPatIndex(ma.Patamar), UtilitarioDeTexto.idSubmercado(ma.Submercado)] = (float)pat_carga_mesAtual.GetValue(ma, null);
                if (ma.Ano == UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, UtilitarioDeData.mesFinalReal(sAtual.mes)))
                    patCargaMesSeg[getPatIndex(ma.Patamar), UtilitarioDeTexto.idSubmercado(ma.Submercado)] = (float)pat_carga_mesMais1.GetValue(ma, null);
            }

            C_ADIC c_adiEx = new C_ADIC();
            PropertyInfo adic_mesAtual = c_adiEx.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo adic_mesMais1 = c_adiEx.GetType().GetProperty(String.Concat("Mes", UtilitarioDeData.mesFinalReal(sAtual.mes).ToString()));

            PEQUENAS pqEx = new PEQUENAS();
            PropertyInfo mesAtual = pqEx.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo mesMais1 = pqEx.GetType().GetProperty(String.Concat("Mes", UtilitarioDeData.mesFinalReal(sAtual.mes).ToString()));

            PAT_NAO_SIMULADAS patEx = new PAT_NAO_SIMULADAS();
            PropertyInfo pat_mesAtual = patEx.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo pat_mesMais1 = patEx.GetType().GetProperty(String.Concat("Mes", UtilitarioDeData.mesFinalReal(sAtual.mes).ToString()));

            int x = 1;
           

            var Soma_Peq = deckNW.pequenas.Where(y => y.Desc_Usina == "1").Sum(z => z.Mes1);
            foreach (PEQUENAS pqNW in deckNW.pequenas) {
                float[] pat_valor = new float[4];
                float[] pat_valor2 = new float[4];
                if (deckNW.pat_nao_simuladas != null)
                {
                    foreach (var pat in deckNW.pat_nao_simuladas)
                    {
                        if (pat.Submercado == pqNW.Intercambio && pat.Desc_Usina == pqNW.Desc_Usina && pat.Patamar == "Pesado" && pat.Ano == sAtual.ano)
                        {

                            pat_valor[1] = (float)pat_mesAtual.GetValue(pat, null);
                            pat_valor2[1] = (float)pat_mesMais1.GetValue(pat, null);
                        }
                        else if (pat.Submercado == pqNW.Intercambio && pat.Desc_Usina == pqNW.Desc_Usina && pat.Patamar == "Medio" && pat.Ano == sAtual.ano)
                        {

                            pat_valor[2] = (float)pat_mesAtual.GetValue(pat, null);
                            pat_valor2[2] = (float)pat_mesMais1.GetValue(pat, null);
                        }
                        else if (pat.Submercado == pqNW.Intercambio && pat.Desc_Usina == pqNW.Desc_Usina && pat.Patamar == "Leve" && pat.Ano == sAtual.ano)
                        {

                            pat_valor[3] = (float)pat_mesAtual.GetValue(pat, null);
                            pat_valor2[3] = (float)pat_mesMais1.GetValue(pat, null);
                        }
                    }
                }
                if (pat_valor[1] != 0)
                {
                    if (pqNW.Ano == sAtual.ano)
                    {

                        // valorMes1[x] = (int)mesAtual.GetValue(pqNW, null);
                        if (pqNW.Intercambio == "SUDESTE")
                        {

                            valorMes1[1, 1] = valorMes1[1, 1] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[1]));
                            valorMes1[1, 2] = valorMes1[1, 2] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[2]));
                            valorMes1[1, 3] = valorMes1[1, 3] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[3]));
                        }
                        else if (pqNW.Intercambio == "SUL")
                        {
                            valorMes1[2, 1] = valorMes1[2, 1] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[1]));
                            valorMes1[2, 2] = valorMes1[2, 2] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[2]));
                            valorMes1[2, 3] = valorMes1[2, 3] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[3]));
                        }
                        else if (pqNW.Intercambio == "NORDESTE")
                        {
                            valorMes1[3, 1] = valorMes1[3, 1] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[1]));
                            valorMes1[3, 2] = valorMes1[3, 2] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[2]));
                            valorMes1[3, 3] = valorMes1[3, 3] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[3]));
                        }
                        else if (pqNW.Intercambio == "NORTE")
                        {
                            valorMes1[4, 1] = valorMes1[4, 1] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[1]));
                            valorMes1[4, 2] = valorMes1[4, 2] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[2]));
                            valorMes1[4, 3] = valorMes1[4, 3] + Convert.ToInt32(((int)mesAtual.GetValue(pqNW, null) * pat_valor[3]));
                        }

                        if (sAtual.mes != 12)
                        {
                            // valorMes2[x] = (int)mesMais1.GetValue(pqNW, null);

                            if (pqNW.Intercambio == "SUDESTE")
                            {
                                valorMes2[1, 1] = valorMes2[1, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                                valorMes2[1, 2] = valorMes2[1, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                                valorMes2[1, 3] = valorMes2[1, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                            }
                            else if (pqNW.Intercambio == "SUL")
                            {
                                valorMes2[2, 1] = valorMes2[2, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                                valorMes2[2, 2] = valorMes2[2, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                                valorMes2[2, 3] = valorMes2[2, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                            }
                            else if (pqNW.Intercambio == "NORDESTE")
                            {
                                valorMes2[3, 1] = valorMes2[3, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                                valorMes2[3, 2] = valorMes2[3, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                                valorMes2[3, 3] = valorMes2[3, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                            }
                            else if (pqNW.Intercambio == "NORTE")
                            {
                                valorMes2[4, 1] = valorMes2[4, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                                valorMes2[4, 2] = valorMes2[4, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                                valorMes2[4, 3] = valorMes2[4, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                            }
                        }


                       // x++;
                    }
                    else if (sAtual.mes == 12 && pqNW.Ano - 1 == sAtual.ano)
                    {
                        if (pqNW.Intercambio == "SUDESTE")
                        {
                            valorMes2[1, 1] = valorMes2[1, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                            valorMes2[1, 2] = valorMes2[1, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                            valorMes2[1, 3] = valorMes2[1, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                        }
                        else if (pqNW.Intercambio == "SUL")
                        {
                            valorMes2[2, 1] = valorMes2[2, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                            valorMes2[2, 2] = valorMes2[2, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                            valorMes2[2, 3] = valorMes2[2, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                        }
                        else if (pqNW.Intercambio == "NORDESTE")
                        {
                            valorMes2[3, 1] = valorMes2[3, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                            valorMes2[3, 2] = valorMes2[3, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                            valorMes2[3, 3] = valorMes2[3, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                        }
                        else if (pqNW.Intercambio == "NORTE")
                        {
                            valorMes2[4, 1] = valorMes2[4, 1] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[1]);
                            valorMes2[4, 2] = valorMes2[4, 2] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[2]);
                            valorMes2[4, 3] = valorMes2[4, 3] + Convert.ToInt32((int)mesMais1.GetValue(pqNW, null) * pat_valor2[3]);
                        }
                        // valorMes2[x - 1,1] = (int)mesMais1.GetValue(pqNW, null);
                    }
                }
                else
                {
                    if (pqNW.Ano == sAtual.ano)
                    {

                        valorMes1[x, 1] = valorMes1[x, 2] = valorMes1[x, 3] = (int)mesAtual.GetValue(pqNW, null);
                        if (sAtual.mes != 12)
                        {
                            valorMes2[x, 1] = valorMes2[x, 2] = valorMes2[x, 3] = (int)mesMais1.GetValue(pqNW, null);
                        }
                        x++;

                    }
                    else if (sAtual.mes == 12 && pqNW.Ano - 1 == sAtual.ano)
                    {
                        valorMes2[x - 1, 1] = valorMes2[x - 1, 2] = valorMes2[x - 1,3] = (int)mesMais1.GetValue(pqNW, null);
                    }
                }

            }


            foreach (C_ADIC pqNW in deckNW.c_adic) {
                if (pqNW.Ano == sAtual.ano) {
                    var valor = (int)adic_mesAtual.GetValue(pqNW, null);

                    if (valor < 0) adic_valorMes1[UtilitarioDeTexto.idSubmercado(pqNW.Submercado)] += valor;

                    if (sAtual.mes != 12) {
                        valor = (int)adic_mesMais1.GetValue(pqNW, null);
                        if (valor < 0) adic_valorMes2[UtilitarioDeTexto.idSubmercado(pqNW.Submercado)] += valor;
                    }
                    x++;
                } else if (sAtual.mes == 12 && pqNW.Ano - 1 == sAtual.ano) {
                    var valor = (int)adic_mesMais1.GetValue(pqNW, null);
                    if (valor < 0) adic_valorMes2[UtilitarioDeTexto.idSubmercado(pqNW.Submercado)] += valor;
                }
            }
            
            for (int j = 1; j < 5; j++) {


               if (adic_valorMes1[j] < 0) {
                    PQ pqAdic = new PQ();
                    pqAdic.deck = deck;
                    pqAdic.ordem = ordem;
                    pqAdic.linha = ordem + 300; //Estimativa, vai fazer o numero da linha ficar errado.
                    pqAdic.campo1 = ("ad" + UtilitarioDeTexto.nomeSubmercado(j) + "       ").Substring(0, 8);
                    pqAdic.campo2 = j.ToString();
                    pqAdic.campo3 = "1";
                    pqAdic.campo4 = (-1 * adic_valorMes1[j] * patCargaMesAtual[0, j]).ToString("#");
                    pqAdic.campo5 = (-1 * adic_valorMes1[j] * patCargaMesAtual[1, j]).ToString("#");
                    pqAdic.campo6 = (-1 * adic_valorMes1[j] * patCargaMesAtual[2, j]).ToString("#");

                    deck.pq.Add(pqAdic);
                    ordem++;

                }


                PQ pq = new PQ();
                pq.deck = deck;
                pq.ordem = ordem;
                pq.linha = ordem + 300; //Estimativa, vai fazer o numero da linha ficar errado.
                pq.campo1 = UtilitarioDeTexto.nomeSubmercado(j);
                pq.campo2 = j.ToString();
                pq.campo3 = "1";
                pq.campo4 = valorMes1[j,1].ToString();
                pq.campo5 = valorMes1[j,2].ToString();
                pq.campo6 = valorMes1[j,3].ToString();

                deck.pq.Add(pq);
                ordem++;

                if (adic_valorMes2[j] < 0) {
                    PQ pqAdic = new PQ();
                    pqAdic.deck = deck;
                    pqAdic.ordem = ordem;
                    pqAdic.linha = ordem + 300; //Estimativa, vai fazer o numero da linha ficar errado.
                    pqAdic.campo1 = ("ad" + UtilitarioDeTexto.nomeSubmercado(j) + "       ").Substring(0, 8);
                    pqAdic.campo2 = j.ToString();
                    pqAdic.campo3 = (nSemanasAtual + 1).ToString();
                    pqAdic.campo4 = (-1 * adic_valorMes2[j] * patCargaMesSeg[0, j]).ToString("#");
                    pqAdic.campo5 = (-1 * adic_valorMes2[j] * patCargaMesSeg[1, j]).ToString("#");
                    pqAdic.campo6 = (-1 * adic_valorMes2[j] * patCargaMesSeg[2, j]).ToString("#");

                    deck.pq.Add(pqAdic);
                    ordem++;

                }
                

                PQ pqFinal = new PQ();
                pqFinal.deck = deck;
                pqFinal.ordem = ordem;
                pqFinal.linha = ordem + 300;
                pqFinal.campo1 = UtilitarioDeTexto.nomeSubmercado(j);
                pqFinal.campo2 = j.ToString();
                pqFinal.campo3 = (nSemanasAtual + 1).ToString();
                pqFinal.campo4 = valorMes2[j,1].ToString();
                pqFinal.campo5 = valorMes2[j,2].ToString();
                pqFinal.campo6 = valorMes2[j,3].ToString();
                deck.pq.Add(pqFinal);
                ordem++;
            }
        }

        public static void atualizarRVX(Deck deck) {
            PQ pq1 = null;
            int sub = 0;//
            for (int x = 0; x < (deck.pq.Count()); x++) {
                if (deck.pq[x].campo3 == "1" && /*apagar*/sub != int.Parse(deck.pq[x].campo2)/*apagar*/)
                {
                    //deck.pq.Remove(deck.pq[x]);//nao
                    //x--;//nao
                    pq1 = deck.pq[x];
                    continue;
                } 
                else if (deck.pq[x].campo3 == "2" && pq1 != null) 
                {
                    deck.pq.Remove(pq1);
                    x--;
                }
                else if (deck.pq[x].campo3 == "1" && sub == int.Parse(deck.pq[x].campo2))//apagar todo o else if
                {
                    pq1 = deck.pq[x];
                    deck.pq.Remove(pq1);
                    x--;
                    pq1 = null;
                    continue;
                }
                pq1 = null;

                if (deck.pq[x].campo3 != "1")
                {
                    sub = int.Parse(deck.pq[x].campo2);//apagar
                    deck.pq[x].campo3 = (int.Parse(deck.pq[x].campo3) - 1).ToString();

                }
            }
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev) {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo2");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo3");

            return tupleList.OrderBy(ty => int.Parse(usinaProperty.GetValue(ty.Item2).ToString()))
                .ThenBy(tw => PQ.ordemRVDif(usinaProperty2.GetValue(tw.Item2).ToString(), difRev, tw.Item1));
        }

        public static void atualizarMensal(Deck novoDeck, DeckNW deckNwBase) {
            List<PQ> newPq = new List<PQ>();

            for (int x = 1; x < 5; x++) {
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
