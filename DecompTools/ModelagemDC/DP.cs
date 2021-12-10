using DecompTools.ModelagemNW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DecompTools.FactoryNW;
using System.Reflection;
using DecompTools.FactoryDC;
using DecompTools.Util;
using System.Globalization;
using ToolBox;

namespace DecompTools.ModelagemDC
{
    public class DP : blockModel
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

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo2);
        }

        public DP()
        {
            pos = new int[] { 4, 5, 4, 14, 10, 10, 10, 10, 10 };
            nome = "DP";
        }

        /// <summary>
        /// Substituir o atual
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="deckBase"></param>
        /// <param name="deckNW"></param>
        /// <param name="s"></param>
        /// <param name="sBase"></param>
        public static void atualizarRV0(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            //Parte 1: Carga
            int[] cargaMesBase = new int[4];
            int[] cargaMesBaseSeg = new int[4];
            int[] cargaMesAtual = new int[4];
            int[] cargaMesSeg = new int[4];

            float[,] patCargaMesAtual = new float[3, 4];
            float[,] patCargaMesSeg = new float[3, 4];

            IEnumerator<MERCADO> iMercAtual = deckNW.mercado.GetEnumerator();
            IEnumerator<PAT_CARGA> iPatCarga = deckNW.pat_carga.GetEnumerator();

            int mes2 = UtilitarioDeData.mesFinalReal(s.mes);
            PropertyInfo mesBase = typeof(MERCADO).GetProperty(string.Concat("Mes", sBase.mes.ToString()));
            PropertyInfo mesAtual = typeof(MERCADO).GetProperty(string.Concat("Mes", s.mes.ToString()));
            PropertyInfo mesSeg = typeof(MERCADO).GetProperty(string.Concat("Mes", mes2));

            Func<string, int> getPatIndex = pat =>
            {
                switch (pat.ToUpper())
                {
                    case "PESADO":
                        return 0;
                    case "MEDIO":
                        return 1;
                    case "LEVE":
                        return 2;
                    default:
                        throw new ArithmeticException(pat + " invalido");
                }
            };
            Func<string, int> getSubMercadoIndex = merc =>
            {
                switch (merc.ToUpper())
                {
                    case "SUDESTE":
                        return 0;
                    case "SUL":
                        return 1;
                    case "NORDESTE":
                        return 2;
                    case "NORTE":
                        return 3;
                    default:
                        throw new ArithmeticException(merc + " invalido");
                }
            };


            while (iMercAtual.MoveNext())
            {
                MERCADO ma = iMercAtual.Current;
                if (ma.Ano == s.ano)
                    cargaMesAtual[getSubMercadoIndex(ma.Intercambio)] = (int)mesAtual.GetValue(ma, null);
                if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                    cargaMesSeg[getSubMercadoIndex(ma.Intercambio)] = (int)mesSeg.GetValue(ma, null);
            }

            mesAtual = typeof(PAT_CARGA).GetProperty(string.Concat("Mes", s.mes.ToString()));
            mesSeg = typeof(PAT_CARGA).GetProperty(string.Concat("Mes", mes2));


            while (iPatCarga.MoveNext())
            {
                PAT_CARGA ma = iPatCarga.Current;
                if (ma.Ano == s.ano)
                    patCargaMesAtual[getPatIndex(ma.Patamar), getSubMercadoIndex(ma.Submercado)] = (float)mesAtual.GetValue(ma, null);
                if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                    patCargaMesSeg[getPatIndex(ma.Patamar), getSubMercadoIndex(ma.Submercado)] = (float)mesSeg.GetValue(ma, null);
            }

            //Parte 2: Horas
            int mesProx = UtilitarioDeData.mesFinalReal(s.mes);
            int anoProx = UtilitarioDeData.anoInicialReal(s.ano, s.mes, mesProx);

            List<Semanas_Patamares> lstPat = SemanasPatamaresDAO.GetByMonth(s.mes, s.ano);
            List<Semanas_Patamares> lstPatProxMes = SemanasPatamaresDAO.GetByMonth(mesProx, anoProx);
            Semanas_Patamares patProxMes = new Semanas_Patamares();

            if (s.primeiraSemana.Month != s.mes)
            {
                int mesAnt = UtilitarioDeData.mesInicialReal(s.mes);
                int anoAnt = UtilitarioDeData.anoInicialReal(s.ano, s.mes, mesAnt);
                lstPat[0] = Semanas_Patamares.somaSemanas(lstPat[0], SemanasPatamaresDAO.GetLastOrFirstByMonth(mesAnt, anoAnt, 0));
            }
            if (s.diasMes2 != 0)
            {
                lstPat[lstPat.Count - 1] = Semanas_Patamares.somaSemanas(lstPat[lstPat.Count - 1], SemanasPatamaresDAO.GetLastOrFirstByMonth(mesProx, anoProx, 1));
                lstPatProxMes[0].leve = 0;
                lstPatProxMes[0].medio = 0;
                lstPatProxMes[0].pesado = 0;
            }
            //Valores para a "semana" mes+1
            patProxMes = Semanas_Patamares.somaSemanas(lstPatProxMes);

            var iDpBase = sBase.semanas * 5;

            var tempoMesAtual = lstPat.Sum(e => e.leve + e.medio + e.pesado);

            var PAT = new int[s.semanas, 3, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int ss = 0; ss < 4; ss++)
                {
                    PAT[es, 0, ss] = lstPat[es].pesado;
                    PAT[es, 1, ss] = lstPat[es].medio;
                    PAT[es, 2, ss] = lstPat[es].leve;
                }

            //var ENW = cargaMesAtual;
            //for (int i = 0; i < 4; i++) ENW[i] *= tempoMesAtual;          

            var KNW = new double[3, 4];
            for (int ss = 0; ss < 4; ss++)
            {
                for (int pat = 0; pat < 3; pat++)
                {
                    KNW[pat, ss] = cargaMesAtual[ss] * patCargaMesAtual[pat, ss];
                }
            }

            var KNWSeg = new double[3, 4];
            for (int ss = 0; ss < 4; ss++)
            {
                for (int pat = 0; pat < 3; pat++)
                {
                    KNWSeg[pat, ss] = cargaMesSeg[ss] * patCargaMesSeg[pat, ss];
                }
            }

            var EPAT = new double[3, 4];
            for (int ss = 0; ss < 4; ss++)
            {
                for (int pat = 0; pat < 3; pat++)
                {
                    for (int es = 0; es < s.semanas; es++)
                    {
                        EPAT[pat, ss] += KNW[pat, ss] * PAT[es, pat, ss];
                    }
                }
            }

            /**
             * adicionar efeitos de feriados e sazonabilidade para compor o FSAZ
             * */
            var FFER = new double[s.semanas, 4];
            for (int es = 0; es < s.semanas; es++)
            {
                var feriados = FeriadoDAO.GetBySemanaInicial(s.primeiraSemana.AddDays(es * 7));

                for (int ss = 0; ss < 4; ss++)
                {
                    FFER[es, ss] = 1;

                    foreach (var fer in feriados.Where(f => f.IS_FERIADO))
                    {
                        FFER[es, ss] *= 1 - FeriadoDAO.GetImpactoFeriadoBy(fer, ss);
                    }
                }
            }

            for (int ss = 0; ss < 4; ss++)
            {
                var norm = 0d;
                for (int es = 0; es < s.semanas; es++)
                    norm += FFER[es, ss];

                for (int es = 0; es < s.semanas; es++)
                    FFER[es, ss] /= norm;
            }


            //Utilizar Deck base para sazonabilidade, apenas com as semanas sem feriados
            var FSAZ = new double[s.semanas, 4];
            var dpBase = deckBase.dp.Where(x => !string.IsNullOrWhiteSpace(x.campo4)).Select(x => new
            {
                Es = int.Parse(x.campo1, NumberFormatInfo.InvariantInfo),
                SM = int.Parse(x.campo2, NumberFormatInfo.InvariantInfo),
                K0 = float.Parse(x.campo4, NumberFormatInfo.InvariantInfo),
                K1 = float.Parse(x.campo6, NumberFormatInfo.InvariantInfo),
                K2 = float.Parse(x.campo8, NumberFormatInfo.InvariantInfo),
                P0 = float.Parse(x.campo5, NumberFormatInfo.InvariantInfo),
                P1 = float.Parse(x.campo7, NumberFormatInfo.InvariantInfo),
                P2 = float.Parse(x.campo9, NumberFormatInfo.InvariantInfo)
            }).Where(x => x.P0 == 18f);

            for (int ss = 0; ss < 4; ss++)
            {
                var ssdpBase = dpBase.Where(x => x.SM == ss + 1).ToList();
                for (int es = 0; es < s.semanas; es++)
                {
                    dynamic dp = ssdpBase.Where(x => x.Es == es + 1).FirstOrDefault();

                    if (dp == null || dp.P0 != 18)
                    {

                        if (ssdpBase.Count > 0)
                        {
                            dp = new
                            {
                                K0 = ssdpBase.Average(x => x.K0),
                                K1 = ssdpBase.Average(x => x.K1),
                                K2 = ssdpBase.Average(x => x.K2),
                                P0 = ssdpBase.Average(x => x.P0),
                                P1 = ssdpBase.Average(x => x.P1),
                                P2 = ssdpBase.Average(x => x.P2)
                            };


                            FSAZ[es, ss] = (dp.K0 * dp.P0 + dp.K1 * dp.P1 + dp.K2 * dp.P2) / ssdpBase.Average(x => x.K0 * x.P0 + x.K1 * x.P1 + x.K2 * x.P2);
                        }
                        else
                            FSAZ[es, ss] = 1;

                    }
                    else
                        FSAZ[es, ss] = (dp.K0 * dp.P0 + dp.K1 * dp.P1 + dp.K2 * dp.P2) / ssdpBase.Average(x => x.K0 * x.P0 + x.K1 * x.P1 + x.K2 * x.P2);
                }
            }

            /*
            var FSAZ = new double[s.semanas, 4];
            for (int ss = 0; ss < 4; ss++) {
                var fator = FeriadoDAO.GetImpactoSazonalBy(s.mes, ss);

                for (int es = 0; es < s.semanas; es++) {
                    var f = es - s.semanas / 2;

                    FSAZ[es, ss] = 1d + f * fator;
                }
            }
            */


            var FS = new double[s.semanas, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int ss = 0; ss < 4; ss++)
                    FS[es, ss] = FFER[es, ss] * FSAZ[es, ss];

            var E1 = new double[s.semanas, 3, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int pat = 0; pat < 3; pat++)
                    for (int ss = 0; ss < 4; ss++)
                    {
                        E1[es, pat, ss] = FS[es, ss] * EPAT[pat, ss];
                    }

            /*
             adicionar efeitos de correlação de carga com tempo de carga;
             */
            var PATmed = new float[3, 4];
            for (int pat = 0; pat < 3; pat++)
                for (int ss = 0; ss < 4; ss++)
                    for (int es = 0; es < s.semanas; es++)
                        PATmed[pat, ss] += (float)PAT[es, pat, ss] / s.semanas;


            var F = new double[s.semanas, 3, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int pat = 0; pat < 3; pat++)
                    for (int ss = 0; ss < 4; ss++)
                        F[es, pat, ss] = 1 + (PAT[es, pat, ss] / PATmed[pat, ss] - 1) * 0.87;

            var E = new double[s.semanas, 3, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int pat = 0; pat < 3; pat++)
                    for (int ss = 0; ss < 4; ss++)
                        E[es, pat, ss] = E1[es, pat, ss] * F[es, pat, ss];

            var K = new double[s.semanas, 3, 4];
            for (int es = 0; es < s.semanas; es++)
                for (int pat = 0; pat < 3; pat++)
                    for (int ss = 0; ss < 4; ss++)
                        K[es, pat, ss] = E[es, pat, ss] / PAT[es, pat, ss];




            var newDp = new List<DP>();
            for (int es = 0; es < s.semanas; es++)
            {
                for (int ss = 0; ss < 4; ss++)
                {
                    newDp.Add(new DP()
                    {
                        campo1 = (es + 1).ToString(),
                        campo2 = (ss + 1).ToString(),
                        campo3 = "3",
                        campo4 = UtilitarioDeTexto.zeroDir(Math.Round(K[es, 0, ss]), 1),
                        campo5 = UtilitarioDeTexto.zeroDir(lstPat[es].pesado, 1),
                        campo6 = UtilitarioDeTexto.zeroDir(Math.Round(K[es, 1, ss]), 1),
                        campo7 = UtilitarioDeTexto.zeroDir(lstPat[es].medio, 1),
                        campo8 = UtilitarioDeTexto.zeroDir(Math.Round(K[es, 2, ss]), 1),
                        campo9 = UtilitarioDeTexto.zeroDir(lstPat[es].leve, 1),
                    });
                }

                newDp.Add(new DP()
                {
                    campo1 = (es + 1).ToString(),
                    campo2 = "11",
                    campo3 = "3",
                    campo4 = "",
                    campo5 = UtilitarioDeTexto.zeroDir(lstPat[es].pesado, 1),
                    campo6 = "",
                    campo7 = UtilitarioDeTexto.zeroDir(lstPat[es].medio, 1),
                    campo8 = "",
                    campo9 = UtilitarioDeTexto.zeroDir(lstPat[es].leve, 1),
                });

            }


            int totalMesSeg = patProxMes.leve + patProxMes.medio + patProxMes.pesado;
            for (int ss = 0; ss < 4; ss++)
            {
                newDp.Add(new DP()
                {
                    campo1 = (s.semanas + 1).ToString(),
                    campo2 = (ss + 1).ToString(),
                    campo3 = "3",
                    campo4 = UtilitarioDeTexto.zeroDir(Math.Round(KNWSeg[0, ss]), 1),
                    campo5 = UtilitarioDeTexto.zeroDir(patProxMes.pesado, 1),
                    campo6 = UtilitarioDeTexto.zeroDir(Math.Round(KNWSeg[1, ss]), 1),
                    campo7 = UtilitarioDeTexto.zeroDir(patProxMes.medio, 1),
                    campo8 = UtilitarioDeTexto.zeroDir(Math.Round(KNWSeg[2, ss]), 1),
                    campo9 = UtilitarioDeTexto.zeroDir(patProxMes.leve, 1),
                });
            }
            newDp.Add(new DP()
            {
                campo1 = (s.semanas + 1).ToString(),
                campo2 = "11",
                campo3 = "3",
                campo4 = "",
                campo5 = UtilitarioDeTexto.zeroDir(patProxMes.pesado, 1),
                campo6 = "",
                campo7 = UtilitarioDeTexto.zeroDir(patProxMes.medio, 1),
                campo8 = "",
                campo9 = UtilitarioDeTexto.zeroDir(patProxMes.leve, 1),
            });




            deck.dp = newDp.OrderBy(d => d.campo1).ThenBy(d => int.Parse(d.campo2.Trim())).ToList();

        }

        public static void atualizarRV0_Old(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        {
            //Parte 1: Carga
            int[] cargaMesBase = new int[4];
            int[] cargaMesBaseSeg = new int[4];
            int[] cargaMesAtual = new int[4];
            int[] cargaMesSeg = new int[4];
            List<MERCADO> lstMercBase = BlockNWDAO.GetAllMercByDeck(deckBase.id_deckNW);

            IEnumerator<MERCADO> iMercBase = lstMercBase.GetEnumerator();
            IEnumerator<MERCADO> iMercAtual = deckNW.mercado.GetEnumerator();

            int mes2 = UtilitarioDeData.mesFinalReal(s.mes);
            PropertyInfo mesBase = typeof(MERCADO).GetProperty(string.Concat("Mes", sBase.mes.ToString()));
            PropertyInfo mesAtual = typeof(MERCADO).GetProperty(string.Concat("Mes", s.mes.ToString()));
            PropertyInfo mesSeg = typeof(MERCADO).GetProperty(string.Concat("Mes", mes2));

            while (iMercAtual.MoveNext() && iMercBase.MoveNext())
            {
                MERCADO ma = iMercAtual.Current;
                MERCADO mb = iMercBase.Current;
                if (String.Equals(ma.Intercambio, "SUDESTE"))
                {
                    if (ma.Ano == s.ano)
                        cargaMesAtual[0] = (int)mesAtual.GetValue(ma, null);
                    if (mb.Ano == sBase.ano)
                        cargaMesBase[0] = (int)mesBase.GetValue(mb, null);
                    if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                        cargaMesSeg[0] = (int)mesSeg.GetValue(ma, null);
                }
                else if (String.Equals(ma.Intercambio, "SUL"))
                {
                    if (ma.Ano == s.ano)
                        cargaMesAtual[1] = (int)mesAtual.GetValue(ma, null);
                    if (mb.Ano == sBase.ano)
                        cargaMesBase[1] = (int)mesBase.GetValue(mb, null);
                    if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                        cargaMesSeg[1] = (int)mesSeg.GetValue(ma, null);
                }
                else if (String.Equals(ma.Intercambio, "NORDESTE"))
                {
                    if (ma.Ano == s.ano)
                        cargaMesAtual[2] = (int)mesAtual.GetValue(ma, null);
                    if (mb.Ano == sBase.ano)
                        cargaMesBase[2] = (int)mesBase.GetValue(mb, null);
                    if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                        cargaMesSeg[2] = (int)mesSeg.GetValue(ma, null);
                }
                else if (String.Equals(ma.Intercambio, "NORTE"))
                {
                    if (ma.Ano == s.ano)
                        cargaMesAtual[3] = (int)mesAtual.GetValue(ma, null);
                    if (mb.Ano == sBase.ano)
                        cargaMesBase[3] = (int)mesBase.GetValue(mb, null);
                    if (ma.Ano == UtilitarioDeData.anoInicialReal(s.ano, s.mes, mes2))
                        cargaMesSeg[3] = (int)mesSeg.GetValue(ma, null);
                }
            }
            cargaMesBaseSeg = mesBaseSeguinte(deckBase, sBase);


            //Parte 2: Horas
            int mesProx = UtilitarioDeData.mesFinalReal(s.mes);
            int anoProx = UtilitarioDeData.anoInicialReal(s.ano, s.mes, mesProx);

            List<Semanas_Patamares> lstPat = SemanasPatamaresDAO.GetByMonth(s.mes, s.ano);
            List<Semanas_Patamares> lstPatProxMes = SemanasPatamaresDAO.GetByMonth(mesProx, anoProx);
            Semanas_Patamares patProxMes = new Semanas_Patamares();

            if (s.primeiraSemana.Month != s.mes)
            {
                int mesAnt = UtilitarioDeData.mesInicialReal(s.mes);
                int anoAnt = UtilitarioDeData.anoInicialReal(s.ano, s.mes, mesAnt);
                lstPat[0] = Semanas_Patamares.somaSemanas(lstPat[0], SemanasPatamaresDAO.GetLastOrFirstByMonth(mesAnt, anoAnt, 0));
            }
            if (s.diasMes2 != 0)
            {
                lstPat[lstPat.Count - 1] = Semanas_Patamares.somaSemanas(lstPat[lstPat.Count - 1], SemanasPatamaresDAO.GetLastOrFirstByMonth(mesProx, anoProx, 1));
                lstPatProxMes[0].leve = 0;
                lstPatProxMes[0].medio = 0;
                lstPatProxMes[0].pesado = 0;
            }
            //Valores para a "semana" mes+1
            patProxMes = Semanas_Patamares.somaSemanas(lstPatProxMes);

            //Com as duas partes na memoria, atualizar o bloco do deckBase.
            for (int i = 0; i < sBase.semanas * 5 && i < s.semanas * 5; i++)
            {
                DP dp = deck.dp[i];
                atualizaDP(dp, cargaMesAtual, cargaMesBase, lstPat);
            }

            if (sBase.semanas == s.semanas)
                for (int i = 0; i < 5; i++)
                    atualizaDP(deck.dp[(s.semanas * 5) + i], cargaMesSeg, cargaMesBase, patProxMes);


            else if (sBase.semanas > s.semanas)
            {
                //Sempre fica com j = 5 pois o processo vai removendo a linha, fazendo a linha (semanas *5 + j) ser a linha seguinte.
                int j = 5;
                for (int i = 0; i < 5; i++)
                {
                    atualizaDP(deck.dp[(s.semanas * 5) + j], cargaMesSeg, cargaMesBaseSeg, patProxMes);
                    deck.dp[(s.semanas * 5) + i] = deck.dp[(s.semanas * 5) + j];
                    deck.dp[(s.semanas * 5) + i].campo1 = (s.semanas + 1).ToString();
                    deck.dp.RemoveAt((s.semanas * 5) + j);
                }
            }
            else if (sBase.semanas < s.semanas)
            {
                for (int i = 0; i < 5; i++)
                {
                    DP dpNew = clone(deck.dp[((sBase.semanas - 1) * 5) + i]);

                    dpNew.campo1 = (s.semanas).ToString();
                    deck.dp[(sBase.semanas * 5) + i].campo1 = (s.semanas + 1).ToString();

                    //Ponderar a carga caso o numero de horas na semana a ser copiada nao seja igual a da semana anterior
                    if (i != 4)
                    {
                        dpNew.campo4 = UtilitarioDeTexto.zeroDir(Math.Round(double.Parse(dpNew.campo4, CultureInfo.InvariantCulture) * lstPat[int.Parse(dpNew.campo1) - 1].pesado / double.Parse(dpNew.campo5, CultureInfo.InvariantCulture), 0), 1);
                        dpNew.campo6 = UtilitarioDeTexto.zeroDir(Math.Round(double.Parse(dpNew.campo6, CultureInfo.InvariantCulture) * lstPat[int.Parse(dpNew.campo1) - 1].medio / double.Parse(dpNew.campo7, CultureInfo.InvariantCulture), 0), 1);
                        dpNew.campo8 = UtilitarioDeTexto.zeroDir(Math.Round(double.Parse(dpNew.campo8, CultureInfo.InvariantCulture) * lstPat[int.Parse(dpNew.campo1) - 1].leve / double.Parse(dpNew.campo9, CultureInfo.InvariantCulture), 0), 1);
                    }

                    dpNew.campo5 = UtilitarioDeTexto.zeroDir(lstPat[int.Parse(dpNew.campo1) - 1].pesado, 1);
                    dpNew.campo7 = UtilitarioDeTexto.zeroDir(lstPat[int.Parse(dpNew.campo1) - 1].medio, 1);
                    dpNew.campo9 = UtilitarioDeTexto.zeroDir(lstPat[int.Parse(dpNew.campo1) - 1].leve, 1);

                    //atualizaDP(dpNew, cargaMesSeg, cargaMesBaseSeg, patProxMes);
                    atualizaDP(deck.dp[(sBase.semanas * 5) + i], cargaMesSeg, cargaMesBaseSeg, patProxMes);

                    deck.dp.Add(deck.dp[(sBase.semanas * 5) + i]);
                    deck.dp[(sBase.semanas * 5) + i] = dpNew;
                }
            }
        }

        //public override void atualizarRV0Opcional(Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        //{
        //    DP.atualizarRV0(this.deck, deckBase, deckNW, s, sBase);
        //}

        public static void atualizaDP(DP dp, int[] cargaMesAtual, int[] cargaMesBase, List<Semanas_Patamares> lstPat)
        {
            atualizaDP(dp, cargaMesAtual, cargaMesBase, lstPat[int.Parse(dp.campo1) - 1]);
        }

        public static void atualizaDP(DP dp, int[] cargaMesAtual, int[] cargaMesBase, Semanas_Patamares lstPat)
        {
            //Carga
            if (!string.Equals(dp.campo4, ""))
            {
                dp.campo4 = UtilitarioDeTexto.zeroDir(Math.Round((double.Parse(dp.campo4, CultureInfo.InvariantCulture) * cargaMesAtual[int.Parse(dp.campo2) - 1] / cargaMesBase[int.Parse(dp.campo2) - 1]), 0), 1);
                dp.campo6 = UtilitarioDeTexto.zeroDir(Math.Round((double.Parse(dp.campo6, CultureInfo.InvariantCulture) * cargaMesAtual[int.Parse(dp.campo2) - 1] / cargaMesBase[int.Parse(dp.campo2) - 1]), 0), 1);
                dp.campo8 = UtilitarioDeTexto.zeroDir(Math.Round((double.Parse(dp.campo8, CultureInfo.InvariantCulture) * cargaMesAtual[int.Parse(dp.campo2) - 1] / cargaMesBase[int.Parse(dp.campo2) - 1]), 0), 1);
            }

            //Horas
            dp.campo5 = UtilitarioDeTexto.zeroDir(lstPat.pesado, 1);
            dp.campo7 = UtilitarioDeTexto.zeroDir(lstPat.medio, 1);
            dp.campo9 = UtilitarioDeTexto.zeroDir(lstPat.leve, 1);
        }

        public static DP clone(DP dpBase)
        {
            DP dpNew = new DP();

            dpNew.linha = dpBase.linha + 5;
            dpNew.ordem = dpBase.ordem + 5;

            for (int i = 1; i <= 9; i++)
            {
                PropertyInfo campoI = typeof(DP).GetProperty(String.Concat("campo", i.ToString()));
                campoI.SetValue(dpNew, campoI.GetValue(dpBase, null), null);
            }

            return dpNew;
        }

        public static int[] mesBaseSeguinte(Deck deck, Semanas s)
        {
            int ind = 0;

            if (s.semanas == 4)
                ind = 19;
            else if (s.semanas == 5)
                ind = 24;
            else if (s.semanas == 6)
                ind = 29;

            int[] valores = new int[4];

            for (int i = 1; i <= 4; i++)
            {
                double rad = double.Parse(deck.dp[ind + i].campo4, CultureInfo.InvariantCulture) * double.Parse(deck.dp[ind + i].campo5, CultureInfo.InvariantCulture) + double.Parse(deck.dp[ind + i].campo6, CultureInfo.InvariantCulture) * double.Parse(deck.dp[ind + i].campo7, CultureInfo.InvariantCulture) + double.Parse(deck.dp[ind + i].campo8, CultureInfo.InvariantCulture) * double.Parse(deck.dp[ind + i].campo9, CultureInfo.InvariantCulture);
                double div = double.Parse(deck.dp[ind + i].campo5, CultureInfo.InvariantCulture) + double.Parse(deck.dp[ind + i].campo7, CultureInfo.InvariantCulture) + double.Parse(deck.dp[ind + i].campo9, CultureInfo.InvariantCulture);
                valores[i - 1] = (int)Math.Round((rad / div), 0);
            }

            return valores;
        }

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < (deck.dp.Count()); x++)
            {
                if (deck.dp[x].campo1 == "1")
                {
                    deck.dp.Remove(deck.dp[x]);
                    x--;
                }
                else
                    deck.dp[x].campo1 = (int.Parse(deck.dp[x].campo1) - 1).ToString();
            }
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev)
        {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo2");

            return tupleList.OrderBy(ty => DP.ordemRVDif(usinaProperty.GetValue(ty.Item2).ToString(), difRev, ty.Item1))
                .ThenBy(tw => int.Parse(usinaProperty2.GetValue(tw.Item2).ToString()));
        }

        public static void atualizarMensal(Deck novoDeck, DeckNW deckNwBase, DateTime dataInicio)
        {
            DateTime dataFim = dataInicio.AddMonths(1);
            Semanas_Patamares patamar = SemanasPatamaresDAO.GetByPeriod(dataInicio, dataFim.AddDays(-1));
            Semanas_Patamares patamar2 = SemanasPatamaresDAO.GetByPeriod(dataFim, dataFim.AddMonths(1).AddDays(-1));

            novoDeck.dp = calculaLinhaMensal(deckNwBase, dataInicio, 1, patamar);
            novoDeck.dp = novoDeck.dp.Concat(calculaLinhaMensal(deckNwBase, dataFim, 2, patamar2)).ToList<DP>();
        }

        public static List<DP> calculaLinhaMensal(DeckNW deckNwBase, DateTime dataBase, int indiceSemana, Semanas_Patamares patamar)
        {
            PropertyInfo MercadoMes = typeof(MERCADO).GetProperty("Mes" + dataBase.Month.ToString());
            PropertyInfo PatMes = typeof(PAT_CARGA).GetProperty("Mes" + dataBase.Month.ToString());

            List<DP> newDP = new List<DP>();

            for (int x = 1; x < 5; x++)
            {
                MERCADO m = deckNwBase.mercado.Where(y => y.Ano == dataBase.Year && y.Intercambio == UtilitarioDeTexto.nomeSubmercado(x)).First<MERCADO>();
                List<PAT_CARGA> lstPat = deckNwBase.pat_carga.Where(y => y.Ano == dataBase.Year && y.Submercado == UtilitarioDeTexto.nomeSubmercado(x)).ToList<PAT_CARGA>();

                double carga1 = double.Parse(MercadoMes.GetValue(m).ToString());
                double pat1 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Pesado").First<PAT_CARGA>()).ToString());
                double pat2 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Medio").First<PAT_CARGA>()).ToString());
                double pat3 = double.Parse(PatMes.GetValue(lstPat.Where(y => y.Patamar == "Leve").First<PAT_CARGA>()).ToString());

                DP dp = new DP();
                dp.campo1 = indiceSemana.ToString();
                dp.campo2 = x.ToString();
                dp.campo3 = "3";
                dp.campo4 = UtilitarioDeTexto.zeroDir(Math.Round(carga1 * pat1, 0), 1);
                dp.campo5 = UtilitarioDeTexto.zeroDir(patamar.pesado, 1);
                dp.campo6 = UtilitarioDeTexto.zeroDir(Math.Round(carga1 * pat2, 0), 1);
                dp.campo7 = UtilitarioDeTexto.zeroDir(patamar.medio, 1);
                dp.campo8 = UtilitarioDeTexto.zeroDir(Math.Round(carga1 * pat3, 0), 1);
                dp.campo9 = UtilitarioDeTexto.zeroDir(patamar.leve, 1);

                newDP.Add(dp);
            }

            DP dp5 = new DP();
            dp5.campo1 = indiceSemana.ToString();
            dp5.campo2 = "11";
            dp5.campo3 = "3";
            dp5.campo4 = "";
            dp5.campo5 = UtilitarioDeTexto.zeroDir(patamar.pesado, 1);
            dp5.campo6 = "";
            dp5.campo7 = UtilitarioDeTexto.zeroDir(patamar.medio, 1);
            dp5.campo8 = "";
            dp5.campo9 = UtilitarioDeTexto.zeroDir(patamar.leve, 1);
            newDP.Add(dp5);

            return newDP;
        }
    }
}
