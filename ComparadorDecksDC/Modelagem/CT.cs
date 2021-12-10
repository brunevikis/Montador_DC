using CapturaNW.Factory;
using CapturaNW.Modelagem;
using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace ComparadorDecksDC.Modelagem {
    public class CT : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }
        public virtual string campo10 { get; set; }
        public virtual string campo11 { get; set; }
        public virtual string campo12 { get; set; }
        public virtual string campo13 { get; set; }

        public CT() {
            pos = new int[] { 5, 4, 13, 2, 8, 5, 10, 5, 5, 10, 5, 5, 10 };
            nome = "CT";
        }

        public CT(int ordem, int linha, string c1, string c2, string c3, string c4, double infl, double disp, double CVU) {
            pos = new int[] { 5, 4, 13, 2, 8, 5, 10, 5, 5, 10, 5, 5, 10 };
            nome = "CT";

            this.ordem = ordem++;
            this.linha = linha++;
            this.campo1 = c1;
            this.campo2 = c2;
            this.campo3 = c3;
            this.campo4 = c4;
            if (infl > disp)
                infl = disp;
            this.preenchePatamar(infl, disp, CVU);
        }

        public override string escreveLinha() {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++) {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;
                if (i != 3)
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                else {
                    linha.Append("   ");
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1] - 3, 1));
                }
            }

            return linha.ToString();
        }

        public static string[] leNomeUsina(IList<CT> listCT) {
            int max = int.Parse(listCT[0].campo1);

            for (int i = 1; i < listCT.Count; i++)
                if (int.Parse(listCT[i].campo1) > max)
                    max = int.Parse(listCT[i].campo1);

            string[] nomeUsinas = new string[max + 1];

            for (int i = 0; i < listCT.Count; i++)
                nomeUsinas[int.Parse(listCT[i].campo1)] = listCT[i].campo3;

            return nomeUsinas;
        }
        public static string[] leCvuUsina(IList<CT> listCT) {
            int max = int.Parse(listCT[0].campo1);

            for (int i = 1; i < listCT.Count; i++)
                if (int.Parse(listCT[i].campo1) > max)
                    max = int.Parse(listCT[i].campo1);

            string[] cvu = new string[max + 1];

            for (int i = 0; i < listCT.Count; i++)
                cvu[int.Parse(listCT[i].campo1)] = listCT[i].campo7;

            return cvu;
        }

        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW) {
            double infl, inflMais;
            int nSemanasAtual = sAtual.semanas;
            int ordem = 1;
            int linha = deck.ct[0].linha;

            int mesSeg = UtilitarioDeData.mesFinalReal(sAtual.mes);
            int anoSeg = UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, mesSeg);
            DateTime ultimoDiaRelevante = new DateTime(anoSeg, mesSeg, UtilitarioDeData.diasMes(anoSeg, mesSeg));

            string[] aNomeUsina = leNomeUsina(deck.ct);
            string[] aCvuUsina = leCvuUsina(deck.ct);

            deck.ct.Clear();

            List<CT> lstCT = new List<CT>();
            List<MANUTT> lstManutt = new List<MANUTT>();

            TERM nextTerm = new TERM();
            CLAST_1 nextClast = new CLAST_1();
            CONFT nextConft = new CONFT();
            MANUTT nextManutt = new MANUTT();

            IEnumerator<TERM> iTerm = deckNW.term.OrderBy(c => c.Codigo).GetEnumerator();
            IEnumerator<CLAST_1> iClast = deckNW.clast_1.OrderBy(c => c.Numero).GetEnumerator();
            IEnumerator<CONFT> iConft = deckNW.conft.OrderBy(c => c.NUM).GetEnumerator();
            IEnumerator<MANUTT> iManutt = deckNW.manutt.OrderBy(c => c.Codigo).GetEnumerator();

            PropertyInfo inflMes = nextTerm.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo inflMesMais = nextTerm.GetType().GetProperty(String.Concat("Mes", mesSeg));

            iManutt.MoveNext();
            nextManutt = iManutt.Current;

            while (iTerm.MoveNext() && iClast.MoveNext() && iConft.MoveNext()) {
                nextTerm = iTerm.Current;
                nextClast = iClast.Current;
                nextConft = iConft.Current;

                if (nextTerm.Potencia != 0
                    && !String.Equals(nextClast.Combustivel, "GNL")
                    && !String.Equals(nextConft.EXIS, "NC", StringComparison.OrdinalIgnoreCase)) {
                    infl = (double)inflMes.GetValue(nextTerm, null);
                    inflMais = (double)inflMesMais.GetValue(nextTerm, null);
                    string nomeUsina;
                    double cvu;

                    if (aNomeUsina.Length < nextTerm.Codigo || String.Equals(aNomeUsina[nextTerm.Codigo], String.Empty) || aNomeUsina[nextTerm.Codigo] == null)
                        if (nextTerm.Usina.Length > 10)
                            nomeUsina = nextTerm.Usina.Substring(0, 10);
                        else
                            nomeUsina = nextTerm.Usina;
                    else
                        nomeUsina = aNomeUsina[nextTerm.Codigo];

                    if (aCvuUsina.Length < nextTerm.Codigo || String.IsNullOrWhiteSpace(aCvuUsina[nextTerm.Codigo])
                        || !Double.TryParse(aCvuUsina[nextTerm.Codigo], NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out cvu))
                        cvu = nextClast.Custo_1;

                    while (nextManutt != null && nextManutt.Codigo == nextTerm.Codigo) {
                        DateTime dateManutt = new DateTime(nextManutt.ano, nextManutt.mes, nextManutt.dia);
                        if (dateManutt.CompareTo(ultimoDiaRelevante) <= 0)
                            lstManutt.Add(nextManutt);

                        iManutt.MoveNext();
                        if (iManutt.Current != nextManutt)
                            nextManutt = iManutt.Current;
                        else
                            break;
                    }

                    if (lstManutt.Count != 0) {
                        List<CT> lstFoo = trataManutt(lstManutt, sAtual, infl, inflMais, nextTerm.Potencia, nextTerm.FCMX, cvu, nomeUsina);
                        foreach (CT ct in lstFoo) {
                            ct.campo2 = nextConft.SU.ToString();
                            lstCT.Add(ct);
                        }

                        lstManutt.Clear();
                    } else {
                        double disp = ((nextTerm.Potencia * nextTerm.FCMX) / 100);
                        CT ct = new CT(ordem++, linha++, nextConft.NUM.ToString(), nextConft.SU.ToString(), nomeUsina, "1", infl, disp, cvu);
                        lstCT.Add(ct);

                        if (infl != inflMais) {
                            CT ctMais = new CT(ordem++, linha++, nextConft.NUM.ToString(), nextConft.SU.ToString(), nomeUsina, (sAtual.semanas + 1).ToString(), inflMais, disp, cvu);
                            lstCT.Add(ctMais);
                        }
                    }
                }
            }

            deck.ct = lstCT.OrderBy(x=>x.campo2).ThenBy(x=>Double.Parse(x.campo7, System.Globalization.CultureInfo.InvariantCulture)).ToList();
        }

        public static List<CT> trataManutt(List<MANUTT> lstManutt, Semanas sAtual, double infl, double inflMais, double pot, double fcmx, double cvu, string nomeUsina, string sinal = "menos") {
            int mesSeg = UtilitarioDeData.mesFinalReal(sAtual.mes);
            int anoSeg = UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, mesSeg);
            int nDiasMesSeg = UtilitarioDeData.diasMes(anoSeg, mesSeg) - sAtual.diasMes2;
            DateTime ultimoDiaRelevante = new DateTime(anoSeg, mesSeg, UtilitarioDeData.diasMes(anoSeg, mesSeg));

            double[] valSemanal = new double[sAtual.semanas + 1];
            int[] lastDayWeek = new int[sAtual.semanas];
            DateTime[] semanas = new DateTime[sAtual.semanas + 1];

            List<CT> lstCTM = new List<CT>();

            for (int i = 0; i < sAtual.semanas; i++) {
                semanas[i] = sAtual.primeiraSemana.AddDays((7 * i));
                lastDayWeek[i] = semanas[i].AddDays(6).Day;
                valSemanal[i] = pot;
            }
            semanas[sAtual.semanas] = new DateTime(anoSeg, mesSeg, 1 + sAtual.diasMes2);
            valSemanal[sAtual.semanas] = pot;

            foreach (MANUTT nextM in lstManutt) {
                MANUTT m = nextM;
                DateTime dateManutt = new DateTime(m.ano, m.mes, m.dia);
                if (dateManutt.Month == sAtual.mes && dateManutt.Day == 1 && m.deckNW.ano == sAtual.ano && m.deckNW.mes == sAtual.mes && sAtual.primeiraSemana.Day != 1) {
                    MANUTT mTemp = BlockNWDAO.getManuttByDate(m, sAtual.ano, sAtual.mes);

                    DateTime dateManuttTemp = new DateTime(mTemp.ano, mTemp.mes, mTemp.dia);
                    DateTime dateFimTemp = dateManuttTemp.AddDays(mTemp.Duracao);
                    DateTime dateFimManut = dateManutt.AddDays(m.Duracao);

                    if (dateFimManut == dateFimTemp) {
                        m = mTemp;
                        dateManutt = dateManuttTemp;
                    } else if (dateManuttTemp < dateManutt && dateFimTemp > dateManutt) {
                        mTemp.Duracao += (int)(dateFimManut.Date - dateFimTemp.Date).TotalDays;
                        m = mTemp;
                        dateManutt = dateManuttTemp;
                    } else if (dateManuttTemp < dateManutt && dateManuttTemp < sAtual.primeiraSemana && dateFimTemp < dateManutt && dateFimTemp > sAtual.primeiraSemana) {
                        var diasMais = (int)(dateFimTemp.Date - sAtual.primeiraSemana.Date).TotalDays;
                        m.Duracao += diasMais;
                        dateManutt = dateManutt.AddDays(-diasMais);
                        m.Inicio = dateManutt.ToString("ddMMyyyy");
                    }
                }

                int diasManutt = m.Duracao;
                int diasSemana;

                if (dateManutt.CompareTo(sAtual.primeiraSemana) <= 0) {
                    var dias = (int)(semanas[0].Date - dateManutt.Date).TotalDays;
                    dateManutt = dateManutt.AddDays(dias);
                    diasManutt -= dias;

                    int i = 0;
                    while (diasManutt > 0) {
                        diasSemana = 7 - (dateManutt.Day - semanas[i].Day);

                        if (diasManutt > diasSemana) {
                            if (sinal == "menos")
                                valSemanal[i] -= m.Potencia * diasSemana / 7;
                            else
                                valSemanal[i] += m.Potencia * diasSemana / 7;
                            diasManutt -= diasSemana;
                            dateManutt = dateManutt.AddDays(diasSemana);

                        } else {
                            if (sinal == "menos")
                                valSemanal[i] -= m.Potencia * diasManutt / 7;
                            else
                                valSemanal[i] += m.Potencia * diasManutt / 7;
                            diasManutt = 0;
                        }

                        i++;
                        if (i == sAtual.semanas)
                            break;
                    }
                } else if (dateManutt.Month == mesSeg) {
                    diasSemana = lastDayWeek[sAtual.semanas - 1] - dateManutt.Day + 1;

                    if (diasSemana > 0) {
                        if (diasManutt > diasSemana) {
                            if (sinal == "menos")
                                valSemanal[sAtual.semanas - 1] -= m.Potencia * diasSemana / 7;
                            else
                                valSemanal[sAtual.semanas - 1] += m.Potencia * diasSemana / 7;
                            diasManutt -= diasSemana;
                            dateManutt.AddDays(diasSemana);
                        } else {
                            if (sinal == "menos")
                                valSemanal[sAtual.semanas - 1] -= m.Potencia * diasManutt / 7;
                            else
                                valSemanal[sAtual.semanas - 1] += m.Potencia * diasManutt / 7;
                            diasManutt = 0;
                        }
                    }
                } else if (sAtual.mes == dateManutt.Month) {
                    for (int i = 0; i < lastDayWeek.Length; i++) {
                        if ((diasSemana = (lastDayWeek[i] - dateManutt.Day) + 1) > 0 || semanas[i] == dateManutt) {
                            if (diasSemana < 0)
                                diasSemana = 7;

                            if (diasManutt > diasSemana) {
                                if (sinal == "menos")
                                    valSemanal[i] -= m.Potencia * diasSemana / 7;
                                else
                                    valSemanal[i] += m.Potencia * diasSemana / 7;
                                diasManutt -= diasSemana;
                                dateManutt = dateManutt.AddDays(diasSemana);
                            } else {
                                if (sinal == "menos")
                                    valSemanal[i] -= m.Potencia * diasManutt / 7;
                                else
                                    valSemanal[i] += m.Potencia * diasManutt / 7;
                                diasManutt = 0;
                                break;
                            }
                        }
                    }
                }


                // Tratar para o proximo mes separadamente.
                if (diasManutt > 0) {
                    diasSemana = ultimoDiaRelevante.Day - dateManutt.Day + 1;

                    if (diasManutt > diasSemana)
                        valSemanal[sAtual.semanas] -= m.Potencia * diasSemana / nDiasMesSeg;
                    else
                        valSemanal[sAtual.semanas] -= m.Potencia * diasManutt / nDiasMesSeg;
                }
            }

            for (int i = 0; i <= sAtual.semanas; i++) {
                double infl1 = (i == sAtual.semanas) ? inflMais : infl;
                if (i == 0 || valSemanal[i] != valSemanal[i - 1] || infl1 != infl) {
                    CT ct = new CT(1, 1, lstManutt[0].Codigo.ToString(), "", nomeUsina, (i + 1).ToString(), infl1, (valSemanal[i] * fcmx / 100), cvu);
                    lstCTM.Add(ct);
                }
            }
            return lstCTM;
        }

        public virtual void preenchePatamar(double infl, double disp, double CVU) {
            string sInfl;
            string sDisp;

            if (infl < 0)
                infl = 0;
            if (disp < 0)
                disp = 0;

            if (infl > disp) infl = disp;

            sInfl = infl.ToString(CultureInfo.InvariantCulture);
            sDisp = disp.ToString(CultureInfo.InvariantCulture);

            if ((sInfl.IndexOf('.') != -1 && sInfl.Substring(0, sInfl.IndexOf('.')).Length > 3) || (sInfl.IndexOf('.') == -1 && sInfl.Length > 3))
                sInfl = Math.Round(infl, 0).ToString().Replace(",", ".");
            else
                sInfl = Math.Round(infl, 1).ToString().Replace(",", ".");

            if ((sDisp.IndexOf('.') != -1 && sDisp.Substring(0, sDisp.IndexOf('.')).Length > 3) || (sDisp.IndexOf('.') == -1 && sDisp.Length > 3))
                sDisp = Math.Round(disp, 0).ToString().Replace(",", ".");
            else
                sDisp = Math.Round(disp, 1).ToString().Replace(",", ".");

            string sCVU = UtilitarioDeTexto.zeroDir(Math.Round(CVU, 2), 2);

            this.campo5 = sInfl;
            this.campo8 = sInfl;
            this.campo11 = sInfl;
            this.campo6 = sDisp;
            this.campo9 = sDisp;
            this.campo12 = sDisp;
            this.campo7 = sCVU;
            this.campo10 = sCVU;
            this.campo13 = sCVU;
        }

        public static void atualizarRVX(Deck deck) {
            for (int x = 0; x < deck.ct.Count(); x++) {
                if (deck.ct[x].campo4 == "1") {
                    if (x + 1 < deck.ct.Count() && deck.ct[x + 1].campo4 == "2") {
                        deck.ct.Remove(deck.ct[x]);
                        x--;
                    }
                } else {
                    deck.ct[x].campo4 = (int.Parse(deck.ct[x].campo4) - 1).ToString();
                }
            }
        }

        public override int getTitleLength() { return 4; }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo3);
            mWSheet1.SetValue(rol, 3, this.campo2);
            mWSheet1.SetValue(rol, 4, this.campo4);
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev) {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo4");

            return tupleList.OrderBy(ty => int.Parse(usinaProperty.GetValue(ty.Item2).ToString()))
                .ThenBy(tw => blockModel.ordemRVDif(usinaProperty2.GetValue(tw.Item2).ToString(), difRev, tw.Item1));
        }

        public static List<CT> normalizaBaseMensal(Deck deckBase) {
            Semanas sAtual = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);
            DeckNW deckNw = DeckNWDAO.getAllBlocksbyID(deckBase.id_deckNW);
            List<CT> lstOldCt = deckBase.ct.Where(x => x.campo4 == "1").OrderBy(c => int.Parse(c.campo1)).ToList();
            IEnumerator<MANUTT> iManut = deckNw.manutt.Where(c => c.mes <= sAtual.mes).OrderBy(c => c.Codigo).ThenBy(c => c.Duracao).GetEnumerator();

            iManut.MoveNext();
            MANUTT nextManutt = iManut.Current;

            List<CT> lstNewCt = new List<CT>();
            List<MANUTT> lstManutTemp = new List<MANUTT>();
            foreach (CT ct in lstOldCt) {
                TERM termUsina = deckNw.term.Where(x => x.Codigo.ToString() == ct.campo1).FirstOrDefault();

                while (termUsina != null && nextManutt.Codigo.ToString() == ct.campo1) {
                    lstManutTemp.Add(nextManutt);
                    iManut.MoveNext();
                    if (iManut.Current != nextManutt)
                        nextManutt = iManut.Current;
                    else
                        break;
                }

                if (lstManutTemp.Count == 0)
                    lstNewCt.Add(ct);
                else {
                    PropertyInfo block = typeof(TERM).GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
                    double infl = (double)block.GetValue(termUsina);

                    CT ctFoo = trataManutt(lstManutTemp, sAtual, infl, infl, double.Parse(ct.campo6.Replace(".", ",")), 100.0, double.Parse(ct.campo7.Replace(".", ",")), ct.campo3, "mais").First();
                    ctFoo.campo2 = ct.campo2;

                    if (double.Parse(ctFoo.campo6.Replace(".", ",")) > termUsina.Potencia)
                        ctFoo.preenchePatamar(double.Parse(ctFoo.campo5.Replace(".", ",")), termUsina.Potencia, double.Parse(ctFoo.campo7.Replace(".", ",")));
                    lstNewCt.Add(ctFoo);
                    lstManutTemp.Clear();
                }
            }

            return lstNewCt;
        }

        public static IList<CT> atualizarMensal(List<CT> ctBase, DateTime dataInicio, DeckNW deckNwBase) {
            List<MANUTT> lstSemGNL = deckNwBase.manutt.Where(x => x.Codigo != 86 && x.Codigo != 15).ToList();

            CT[] ctArray = new CT[350];
            foreach (CT ct in ctBase)
                ctArray[int.Parse(ct.campo1)] = new CT(ct.ordem, ct.linha, ct.campo1, ct.campo2, ct.campo3, ct.campo4, double.Parse(ct.campo5.Replace(".", ",")), double.Parse(ct.campo6.Replace(".", ",")), double.Parse(ct.campo7.Replace(".", ",")));

            double[] valMes1 = calcularManutencaoMensal(lstSemGNL, ctArray, dataInicio);
            double[] valMes2 = calcularManutencaoMensal(lstSemGNL, ctArray, dataInicio.AddMonths(1));

            List<CT> lstNewCT = new List<CT>();
            for (int x = 0; x < 350; x++) {
                if (ctArray[x] != null) {
                    var disp = double.Parse(ctArray[x].campo6.Replace(".", ","));
                    var infl = double.Parse(ctArray[x].campo5.Replace(".", ","));

                    if (valMes1[x] > 1.0) {
                        disp -= valMes1[x];
                        disp = (disp < 0 ? 0 : disp);
                        infl = (infl > disp ? disp : infl);
                    }
                    lstNewCT.Add(new CT(ctArray[x].ordem, ctArray[x].linha, ctArray[x].campo1, ctArray[x].campo2, ctArray[x].campo3, ctArray[x].campo4, infl, disp, double.Parse(ctArray[x].campo7.Replace(".", ","))));

                    if (valMes2[x] > 1.0) {
                        disp = double.Parse(ctArray[x].campo6.Replace(".", ",")) - valMes2[x];
                        disp = (disp < 0 ? 0 : disp);
                        infl = double.Parse(ctArray[x].campo5.Replace(".", ","));
                        infl = (infl > disp ? disp : infl);
                        lstNewCT.Add(new CT(ctArray[x].ordem, ctArray[x].linha, ctArray[x].campo1, ctArray[x].campo2, ctArray[x].campo3, "2", infl, disp, double.Parse(ctArray[x].campo7.Replace(".", ","))));
                    }
                }
            }

            return lstNewCT.OrderBy(x => int.Parse(x.campo1)).ToList();
        }

        public static double[] calcularManutencaoMensal(List<MANUTT> lstManut, CT[] ctArray, DateTime dataInicial) {
            int diasMes = (int)(dataInicial.AddMonths(1).AddDays(-1).Date - dataInicial.Date).TotalDays + 1;

            List<MANUTT> lstMesAnt = lstManut.Where(x => (x.ano == dataInicial.Year && x.mes < dataInicial.Month) || (x.ano < dataInicial.Year)).ToList();
            List<MANUTT> lstMesAtual = lstManut.Where(x => x.ano == dataInicial.Year && x.mes == dataInicial.Month).ToList();

            double[] valMes = new double[350];

            foreach (MANUTT ma in lstMesAnt) {
                if (ctArray[ma.Codigo] != null) {
                    DateTime inicioManut = new DateTime(ma.ano, ma.mes, ma.dia);
                    var diasAteMesAtual = (dataInicial.Date - inicioManut.Date).TotalDays;

                    if (ma.Duracao > diasAteMesAtual) {
                        var duracaoReal = ma.Duracao - diasAteMesAtual;
                        var potTotal = double.Parse(ctArray[ma.Codigo].campo6.Replace(".", ","));
                        var potManut = (ma.Potencia > potTotal ? potTotal : ma.Potencia);
                        valMes[ma.Codigo] += (potManut * ((diasMes > duracaoReal ? duracaoReal : diasMes)) / diasMes);
                    }
                }
            }

            foreach (MANUTT ma in lstMesAtual) {
                if (ctArray[ma.Codigo] != null) {
                    var diasPossiveis = diasMes - ma.dia + 1;
                    var potTotal = double.Parse(ctArray[ma.Codigo].campo6.Replace(".", ","));
                    var potManut = (ma.Potencia > potTotal ? potTotal : ma.Potencia);
                    valMes[ma.Codigo] += (potManut * ((diasPossiveis > ma.Duracao ? ma.Duracao : diasPossiveis)) / diasMes);
                }
            }

            return valMes;
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase) {

            var semana = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            var offsetEstagios = semana.numeroEstagios - deckBase.rev;

            var blocoCTAgrupado = from ct in deck.ct
                                  group ct by ct.campo1;

            var newBloco = new List<CT>();
            foreach (var postoGrp in blocoCTAgrupado) {
                foreach (CT ct in postoGrp.OrderByDescending(c => c.campo4)) {
                    var estagio = int.Parse(ct.campo4);
                    if (estagio - offsetEstagios <= 1) {
                        ct.campo4 = "1";
                        newBloco.Add(ct);
                        break;
                    } else {
                        ct.campo4 = (estagio - offsetEstagios).ToString();
                        newBloco.Add(ct);
                    }
                }
            }
            deck.ct = newBloco.OrderBy(ct => ct.ordem).ToList();
        }
    }
}
