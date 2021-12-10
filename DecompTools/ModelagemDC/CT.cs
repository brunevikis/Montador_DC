using DecompTools.FactoryNW;
using DecompTools.ModelagemNW;
using DecompTools.FactoryDC;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace DecompTools.ModelagemDC
{
    public class CT : blockModel
    {
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

        public CT()
        {
            pos = new int[] { 5, 4, 13, 2, 8, 5, 10, 5, 5, 10, 5, 5, 10 };
            nome = "CT";
        }

        public CT(int ordem, int linha, string c1, string c2, string c3, string c4, double infl, double disp, double CVU)
        {
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

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;
                if (i != 3)
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                else
                {
                    linha.Append("   ");
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1] - 3, 1));
                }
            }

            return linha.ToString();
        }

        public static string[] leNomeUsina(IList<CT> listCT)
        {
            int max = listCT.Max(x => int.Parse(x.campo1));

            string[] nomeUsinas = new string[Math.Max(max + 1, 321)];

            for (int i = 0; i < listCT.Count; i++)
                nomeUsinas[int.Parse(listCT[i].campo1)] = listCT[i].campo3;

            return nomeUsinas;
        }
        public static string[] leCvuUsina(IList<CT> listCT)
        {
            int max = listCT.Max(x => int.Parse(x.campo1));

            string[] cvu = new string[Math.Max(max + 1, 321)];

            for (int i = 0; i < listCT.Count; i++)
                cvu[int.Parse(listCT[i].campo1)] = listCT[i].campo7;

            return cvu;
        }

        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW)
        {

            int nSemanasAtual = sAtual.semanas;
            int ordem = 1;
            int linha = deck.ct[0].linha;

            int mesSeg = UtilitarioDeData.mesFinalReal(sAtual.mes);
            int anoSeg = UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, mesSeg);
            DateTime ultimoDiaRelevante = new DateTime(anoSeg, mesSeg, UtilitarioDeData.diasMes(anoSeg, mesSeg));

            string[] aNomeUsina = leNomeUsina(deck.ct);
            string[] aCvuUsina = leCvuUsina(deck.ct);

            var ctOriginal = deck.ct.Where(x => x.campo4.Trim() == "1").ToList();

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


            var expt = deckNW.expt.OrderBy(c => c.Codigo).Select(x => new
            {
                dataInicial = new DateTime(x.Ano_Inicial, x.Mes_Inicial, 1),
                dataFinal = x.Mes_Final != 0 ? new DateTime(x.Ano_Final, x.Mes_Final, 1) : DateTime.MaxValue,
                x.Codigo,
                x.Usina,
                x.TIPO,
                x.VALOR,
            }).OrderBy(x => x.dataInicial);


            PropertyInfo inflMes = nextTerm.GetType().GetProperty(String.Concat("Mes", sAtual.mes.ToString()));
            PropertyInfo inflMesMais = nextTerm.GetType().GetProperty(String.Concat("Mes", (sAtual.mes + 1).ToString()));

            var dataDeck = new DateTime(sAtual.ano, sAtual.mes, 1);
            var dataDeckMais = dataDeck.AddMonths(1);


            iManutt.MoveNext();
            nextManutt = iManutt.Current;

            while (iTerm.MoveNext() && iClast.MoveNext() && iConft.MoveNext())
            {

                nextTerm = iTerm.Current;
                nextClast = iClast.Current;
                nextConft = iConft.Current;

                var potef = nextTerm.Potencia;
                var potefMais = nextTerm.Potencia;
                var fcmax = nextTerm.FCMX;
                var fcmaxMais = nextTerm.FCMX;
                var teif = nextTerm.TEIF;
                var teifMais = nextTerm.TEIF;
                double gtmin, gtminMais;

                gtmin = (double)inflMes.GetValue(nextTerm, null);
                gtminMais = (double)inflMesMais.GetValue(nextTerm, null);

                var ipterMais = nextTerm.IP;

                if (nextTerm.Codigo != nextClast.Numero || nextClast.Numero != nextConft.NUM)
                {
                    throw new Exception("Existe inconsitencia entre os arquivo de termicas, verifique\r\n" + nextTerm.Codigo.ToString());
                }

                if (!String.Equals(nextConft.EXIS, "EX", StringComparison.OrdinalIgnoreCase))
                {

                    var exptUsina = expt.Where(x => x.Codigo == nextTerm.Codigo)
                        .Where(x => x.dataInicial <= dataDeck && x.dataFinal >= dataDeck);
                    var exptUsinaMais = expt.Where(x => x.Codigo == nextTerm.Codigo)
                        .Where(x => x.dataInicial <= dataDeckMais && x.dataFinal >= dataDeckMais);

                    //potef
                    if (exptUsina.Where(x => x.TIPO == "POTEF").Count() > 0) potef = exptUsina.Where(x => x.TIPO == "POTEF").Last().VALOR;
                    if (exptUsinaMais.Where(x => x.TIPO == "POTEF").Count() > 0) potefMais = exptUsinaMais.Where(x => x.TIPO == "POTEF").Last().VALOR;

                    //fcmax
                    if (exptUsina.Where(x => x.TIPO == "FCMAX").Count() > 0) fcmax = exptUsina.Where(x => x.TIPO == "FCMAX").Last().VALOR;
                    if (exptUsinaMais.Where(x => x.TIPO == "FCMAX").Count() > 0) fcmaxMais = exptUsinaMais.Where(x => x.TIPO == "FCMAX").Last().VALOR;

                    //teif
                    if (exptUsina.Where(x => x.TIPO == "TEIFT").Count() > 0) teif = exptUsina.Where(x => x.TIPO == "TEIFT").Last().VALOR;
                    if (exptUsinaMais.Where(x => x.TIPO == "TEIFT").Count() > 0) teifMais = exptUsinaMais.Where(x => x.TIPO == "TEIFT").Last().VALOR;

                    //gtmin = 
                    if (exptUsina.Where(x => x.TIPO == "GTMIN").Count() > 0) gtmin = exptUsina.Where(x => x.TIPO == "GTMIN").Last().VALOR;
                    if (exptUsinaMais.Where(x => x.TIPO == "GTMIN").Count() > 0) gtminMais = exptUsinaMais.Where(x => x.TIPO == "GTMIN").Last().VALOR;

                }


                if (!String.Equals(nextClast.Combustivel, "GNL")
                    && !String.Equals(nextConft.EXIS, "NC", StringComparison.OrdinalIgnoreCase))
                {

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

                    while (nextManutt != null && nextManutt.Codigo == nextTerm.Codigo)
                    {
                        DateTime dateManutt = new DateTime(nextManutt.ano, nextManutt.mes, nextManutt.dia);
                        if (dateManutt.CompareTo(ultimoDiaRelevante) <= 0)
                            lstManutt.Add(nextManutt);

                        iManutt.MoveNext();
                        if (iManutt.Current != nextManutt)
                            nextManutt = iManutt.Current;
                        else
                            break;
                    }

                    if (lstManutt.Count != 0)
                    {
                        List<CT> lstFoo = trataManutt(lstManutt, sAtual, gtmin, gtminMais, potef, potefMais, fcmax, fcmaxMais, cvu, nomeUsina);
                        foreach (CT ct in lstFoo)
                        {
                            ct.campo2 = nextConft.SU.ToString();
                            lstCT.Add(ct);
                        }

                        lstManutt.Clear();
                    }
                    else
                    {
                        double disp = ((potef * fcmax) / 100);
                        CT ct = new CT(ordem++, linha++, nextConft.NUM.ToString(), nextConft.SU.ToString(), nomeUsina, "1", gtmin, disp, cvu);
                        lstCT.Add(ct);

                        if (gtmin != gtminMais || (potef * fcmax) != (potefMais * fcmaxMais))
                        {
                            CT ctMais = new CT(ordem++, linha++, nextConft.NUM.ToString(), nextConft.SU.ToString(), nomeUsina, (sAtual.semanas + 1).ToString(), gtminMais, (potefMais * fcmaxMais) / 100, cvu);
                            lstCT.Add(ctMais);
                        }
                    }
                }
                else if (nextTerm.Potencia != 0
                    && String.Equals(nextClast.Combustivel, "GNL"))
                {

                    while (nextManutt != null && nextManutt.Codigo == nextTerm.Codigo)
                    {

                        //to do : tratar manutenção de usinas GNL


                        //DateTime dateManutt = new DateTime(nextManutt.ano, nextManutt.mes, nextManutt.dia);
                        //if (dateManutt.CompareTo(ultimoDiaRelevante) <= 0)
                        //   lstManutt.Add(nextManutt);

                        iManutt.MoveNext();
                        if (iManutt.Current != nextManutt)
                            nextManutt = iManutt.Current;
                        else
                            break;
                    }

                }
            }

            foreach (var mt in ctOriginal)
            {
                if (!lstCT.Any(ct => ct.campo1.Trim() == mt.campo1.Trim()))
                {
                    lstCT.Add(ctOriginal.Where(ct => ct.campo1.Trim() == mt.campo1.Trim()).First());
                }
            }

            deck.ct = lstCT.OrderBy(x => x.campo2).ThenBy(x => Double.Parse(x.campo7, System.Globalization.CultureInfo.InvariantCulture)).ToList();
        }

        public static List<CT> trataManutt(List<MANUTT> lstManutt, Semanas sAtual, double infl, double inflMais, double pot, double potMais, double fcmx, double fcmxMais, double cvu, string nomeUsina, string sinal = "menos")
        {
            int mesSeg = UtilitarioDeData.mesFinalReal(sAtual.mes);
            int anoSeg = UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, mesSeg);
            int nDiasMesSeg = UtilitarioDeData.diasMes(anoSeg, mesSeg) - sAtual.diasMes2;
            DateTime ultimoDiaRelevante = new DateTime(anoSeg, mesSeg, UtilitarioDeData.diasMes(anoSeg, mesSeg));

            double[] valSemanal = new double[sAtual.semanas + 1];
            double[] fcSemanal = new double[sAtual.semanas + 1];

            int[] lastDayWeek = new int[sAtual.semanas];
            DateTime[] semanas = new DateTime[sAtual.semanas + 1];

            List<CT> lstCTM = new List<CT>();

            for (int i = 0; i < sAtual.semanas; i++)
            {
                semanas[i] = sAtual.primeiraSemana.AddDays((7 * i));
                lastDayWeek[i] = semanas[i].AddDays(6).Day;
                valSemanal[i] = pot;
                fcSemanal[i] = fcmx;
            }
            semanas[sAtual.semanas] = new DateTime(anoSeg, mesSeg, 1 + sAtual.diasMes2);
            valSemanal[sAtual.semanas] = potMais;
            fcSemanal[sAtual.semanas] = fcmxMais;

            foreach (MANUTT nextM in lstManutt)
            {
                MANUTT m = nextM;
                DateTime dateManutt = new DateTime(m.ano, m.mes, m.dia);
                if (dateManutt.Month == sAtual.mes && dateManutt.Day == 1 && m.deckNW.ano == sAtual.ano && m.deckNW.mes == sAtual.mes && sAtual.primeiraSemana.Day != 1)
                {
                    MANUTT mTemp = BlockNWDAO.getManuttByDate(m, sAtual.ano, sAtual.mes);

                    DateTime dateManuttTemp = new DateTime(mTemp.ano, mTemp.mes, mTemp.dia);
                    DateTime dateFimTemp = dateManuttTemp.AddDays(mTemp.Duracao);
                    DateTime dateFimManut = dateManutt.AddDays(m.Duracao);

                    if (dateFimManut == dateFimTemp)
                    {
                        m = mTemp;
                        dateManutt = dateManuttTemp;
                    }
                    else if (dateManuttTemp < dateManutt && dateFimTemp > dateManutt)
                    {
                        mTemp.Duracao += (int)(dateFimManut.Date - dateFimTemp.Date).TotalDays;
                        m = mTemp;
                        dateManutt = dateManuttTemp;
                    }
                    else if (dateManuttTemp < dateManutt && dateManuttTemp < sAtual.primeiraSemana && dateFimTemp < dateManutt && dateFimTemp > sAtual.primeiraSemana)
                    {
                        var diasMais = (int)(dateFimTemp.Date - sAtual.primeiraSemana.Date).TotalDays;
                        m.Duracao += diasMais;
                        dateManutt = dateManutt.AddDays(-diasMais);
                        m.Inicio = dateManutt.ToString("ddMMyyyy");
                    }
                }

                int diasManutt = m.Duracao;
                int diasSemana;

                if (dateManutt.CompareTo(sAtual.primeiraSemana) <= 0)
                {
                    var dias = (int)(semanas[0].Date - dateManutt.Date).TotalDays;
                    dateManutt = dateManutt.AddDays(dias);
                    diasManutt -= dias;
                }

                for (int i = 0; i < sAtual.semanas; i++)
                {
                    if (dateManutt >= semanas[i] && dateManutt < semanas[i + 1])
                    {
                        var dias = (int)(semanas[i + 1] - dateManutt).TotalDays;



                        if (diasManutt > dias)
                        {
                            if (sinal == "menos")
                                valSemanal[i] -= m.Potencia * dias / 7;
                            else
                                valSemanal[i] += m.Potencia * dias / 7;

                            diasManutt -= dias;
                            dateManutt = dateManutt.AddDays(dias);
                        }
                        else if (diasManutt > 0)
                        {
                            if (sinal == "menos")
                                valSemanal[i] -= m.Potencia * diasManutt / 7;
                            else
                                valSemanal[i] += m.Potencia * diasManutt / 7;

                            diasManutt = 0;
                        }
                    }
                }

                if (diasManutt > 0)
                {
                    diasSemana = ultimoDiaRelevante.Day - dateManutt.Day + 1;

                    if (diasManutt > diasSemana)
                        valSemanal[sAtual.semanas] -= m.Potencia * diasSemana / nDiasMesSeg;
                    else
                        valSemanal[sAtual.semanas] -= m.Potencia * diasManutt / nDiasMesSeg;
                }

            }

            for (int i = 0; i <= sAtual.semanas; i++)
            {
                double infl1 = (i == sAtual.semanas) ? inflMais : infl;
                if (i == 0 || valSemanal[i] != valSemanal[i - 1] || infl1 != infl)
                {
                    CT ct = new CT(1, 1, lstManutt[0].Codigo.ToString(), "", nomeUsina, (i + 1).ToString(), infl1, (valSemanal[i] * fcSemanal[i] / 100), cvu);
                    lstCTM.Add(ct);
                }
            }
            return lstCTM;
        }



        //public static List<double> trataManutt(List<MANUTT> lstManutt, Semanas sAtual, string sinal = "mais") {
        //    int mesSeg = UtilitarioDeData.mesFinalReal(sAtual.mes);
        //    int anoSeg = UtilitarioDeData.anoInicialReal(sAtual.ano, sAtual.mes, mesSeg);
        //    int nDiasMesSeg = UtilitarioDeData.diasMes(anoSeg, mesSeg) - sAtual.diasMes2;
        //    DateTime ultimoDiaRelevante = new DateTime(anoSeg, mesSeg, UtilitarioDeData.diasMes(anoSeg, mesSeg));

        //    double[] valSemanal = new double[sAtual.semanas + 1];
        //    int[] lastDayWeek = new int[sAtual.semanas];
        //    DateTime[] semanas = new DateTime[sAtual.semanas + 1];

        //    List<double> lstCTM = new List<double>();

        //    for (int i = 0; i < sAtual.semanas; i++) {
        //        semanas[i] = sAtual.primeiraSemana.AddDays((7 * i));
        //        lastDayWeek[i] = semanas[i].AddDays(6).Day;
        //        valSemanal[i] = 0;
        //    }
        //    semanas[sAtual.semanas] = new DateTime(anoSeg, mesSeg, 1 + sAtual.diasMes2);
        //    valSemanal[sAtual.semanas] = 0;

        //    foreach (MANUTT nextM in lstManutt) {
        //        MANUTT m = nextM;
        //        DateTime dateManutt = new DateTime(m.ano, m.mes, m.dia);
        //        if (dateManutt.Month == sAtual.mes && dateManutt.Day == 1 && m.deckNW.ano == sAtual.ano && m.deckNW.mes == sAtual.mes && sAtual.primeiraSemana.Day != 1) {
        //            MANUTT mTemp = BlockNWDAO.getManuttByDate(m, sAtual.ano, sAtual.mes);

        //            DateTime dateManuttTemp = new DateTime(mTemp.ano, mTemp.mes, mTemp.dia);
        //            DateTime dateFimTemp = dateManuttTemp.AddDays(mTemp.Duracao);
        //            DateTime dateFimManut = dateManutt.AddDays(m.Duracao);

        //            if (dateFimManut == dateFimTemp) {
        //                m = mTemp;
        //                dateManutt = dateManuttTemp;
        //            } else if (dateManuttTemp < dateManutt && dateFimTemp > dateManutt) {
        //                mTemp.Duracao += (int)(dateFimManut.Date - dateFimTemp.Date).TotalDays;
        //                m = mTemp;
        //                dateManutt = dateManuttTemp;
        //            } else if (dateManuttTemp < dateManutt && dateManuttTemp < sAtual.primeiraSemana && dateFimTemp < dateManutt && dateFimTemp > sAtual.primeiraSemana) {
        //                var diasMais = (int)(dateFimTemp.Date - sAtual.primeiraSemana.Date).TotalDays;
        //                m.Duracao += diasMais;
        //                dateManutt = dateManutt.AddDays(-diasMais);
        //                m.Inicio = dateManutt.ToString("ddMMyyyy");
        //            }
        //        }

        //        int diasManutt = m.Duracao;
        //        int diasSemana;

        //        if (dateManutt.CompareTo(sAtual.primeiraSemana) <= 0) {
        //            var dias = (int)(semanas[0].Date - dateManutt.Date).TotalDays;
        //            dateManutt = dateManutt.AddDays(dias);
        //            diasManutt -= dias;

        //            int i = 0;
        //            while (diasManutt > 0) {
        //                diasSemana = 7 - (dateManutt.Day - semanas[i].Day);

        //                if (diasManutt > diasSemana) {
        //                    if (sinal == "menos")
        //                        valSemanal[i] -= m.Potencia * diasSemana / 7;
        //                    else
        //                        valSemanal[i] += m.Potencia * diasSemana / 7;
        //                    diasManutt -= diasSemana;
        //                    dateManutt = dateManutt.AddDays(diasSemana);

        //                } else {
        //                    if (sinal == "menos")
        //                        valSemanal[i] -= m.Potencia * diasManutt / 7;
        //                    else
        //                        valSemanal[i] += m.Potencia * diasManutt / 7;
        //                    diasManutt = 0;
        //                }

        //                i++;
        //                if (i == sAtual.semanas)
        //                    break;
        //            }
        //        } else if (dateManutt.Month == mesSeg) {
        //            diasSemana = lastDayWeek[sAtual.semanas - 1] - dateManutt.Day + 1;

        //            if (diasSemana > 0) {
        //                if (diasManutt > diasSemana) {
        //                    if (sinal == "menos")
        //                        valSemanal[sAtual.semanas - 1] -= m.Potencia * diasSemana / 7;
        //                    else
        //                        valSemanal[sAtual.semanas - 1] += m.Potencia * diasSemana / 7;
        //                    diasManutt -= diasSemana;
        //                    dateManutt.AddDays(diasSemana);
        //                } else {
        //                    if (sinal == "menos")
        //                        valSemanal[sAtual.semanas - 1] -= m.Potencia * diasManutt / 7;
        //                    else
        //                        valSemanal[sAtual.semanas - 1] += m.Potencia * diasManutt / 7;
        //                    diasManutt = 0;
        //                }
        //            }
        //        } else if (sAtual.mes == dateManutt.Month) {
        //            for (int i = 0; i < lastDayWeek.Length; i++) {
        //                if ((diasSemana = (lastDayWeek[i] - dateManutt.Day) + 1) > 0 || semanas[i] == dateManutt) {
        //                    if (diasSemana < 0)
        //                        diasSemana = 7;

        //                    if (diasManutt > diasSemana) {
        //                        if (sinal == "menos")
        //                            valSemanal[i] -= m.Potencia * diasSemana / 7;
        //                        else
        //                            valSemanal[i] += m.Potencia * diasSemana / 7;
        //                        diasManutt -= diasSemana;
        //                        dateManutt = dateManutt.AddDays(diasSemana);
        //                    } else {
        //                        if (sinal == "menos")
        //                            valSemanal[i] -= m.Potencia * diasManutt / 7;
        //                        else
        //                            valSemanal[i] += m.Potencia * diasManutt / 7;
        //                        diasManutt = 0;
        //                        break;
        //                    }
        //                }
        //            }
        //        }


        //        // Tratar para o proximo mes separadamente.
        //        if (diasManutt > 0) {
        //            diasSemana = ultimoDiaRelevante.Day - dateManutt.Day + 1;

        //            if (diasManutt > diasSemana)
        //                valSemanal[sAtual.semanas] -= m.Potencia * diasSemana / nDiasMesSeg;
        //            else
        //                valSemanal[sAtual.semanas] -= m.Potencia * diasManutt / nDiasMesSeg;
        //        }
        //    }
        //    return valSemanal.ToList();
        //}




        public virtual void preenchePatamar(double infl, double disp, double CVU)
        {
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

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < deck.ct.Count(); x++)
            {
                if (deck.ct[x].campo4 == "1")
                {
                    if (x + 1 < deck.ct.Count() && deck.ct[x + 1].campo4 == "2")
                    {
                        deck.ct.Remove(deck.ct[x]);
                        x--;
                    }
                }
                else
                {
                    deck.ct[x].campo4 = (int.Parse(deck.ct[x].campo4) - 1).ToString();
                }
            }
        }

        public override int getTitleLength() { return 4; }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo3);
            mWSheet1.SetValue(rol, 3, this.campo2);
            mWSheet1.SetValue(rol, 4, this.campo4);
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev)
        {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo4");

            return tupleList.OrderBy(ty => int.Parse(usinaProperty.GetValue(ty.Item2).ToString()))
                .ThenBy(tw => blockModel.ordemRVDif(usinaProperty2.GetValue(tw.Item2).ToString(), difRev, tw.Item1));
        }

        public static double[] calcularManutencaoMensal(List<MANUTT> lstManut, CT[] ctArray, DateTime dataInicial)
        {
            int diasMes = (int)(dataInicial.AddMonths(1).AddDays(-1).Date - dataInicial.Date).TotalDays + 1;

            List<MANUTT> lstMesAnt = lstManut.Where(x => (x.ano == dataInicial.Year && x.mes < dataInicial.Month) || (x.ano < dataInicial.Year)).ToList();
            List<MANUTT> lstMesAtual = lstManut.Where(x => x.ano == dataInicial.Year && x.mes == dataInicial.Month).ToList();

            double[] valMes = new double[350];

            foreach (MANUTT ma in lstMesAnt)
            {
                if (ctArray[ma.Codigo] != null)
                {
                    DateTime inicioManut = new DateTime(ma.ano, ma.mes, ma.dia);
                    var diasAteMesAtual = (dataInicial.Date - inicioManut.Date).TotalDays;

                    if (ma.Duracao > diasAteMesAtual)
                    {
                        var duracaoReal = ma.Duracao - diasAteMesAtual;
                        var potTotal = double.Parse(ctArray[ma.Codigo].campo6.Replace(".", ","));
                        var potManut = (ma.Potencia > potTotal ? potTotal : ma.Potencia);
                        valMes[ma.Codigo] += (potManut * ((diasMes > duracaoReal ? duracaoReal : diasMes)) / diasMes);
                    }
                }
            }

            foreach (MANUTT ma in lstMesAtual)
            {
                if (ctArray[ma.Codigo] != null)
                {
                    var diasPossiveis = diasMes - ma.dia + 1;
                    var potTotal = double.Parse(ctArray[ma.Codigo].campo6.Replace(".", ","));
                    var potManut = (ma.Potencia > potTotal ? potTotal : ma.Potencia);
                    valMes[ma.Codigo] += (potManut * ((diasPossiveis > ma.Duracao ? ma.Duracao : diasPossiveis)) / diasMes);
                }
            }

            return valMes;
        }

        public static void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            deck.ct.Clear();
            deck.clone(deckBase, "CT");
            atualizarRV0(deck, s, sBase, deckNW);
        }
    }
}
