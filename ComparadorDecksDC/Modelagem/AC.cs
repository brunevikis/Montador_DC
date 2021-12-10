using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace ComparadorDecksDC.Modelagem {
    public class AC : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }

        public AC() {
            pos = new int[] { 5, 8, 9, 10, 38, 3 };
            nome = "AC";
        }


        public static void atualizarRVX(Deck deck) {
            foreach (AC ac in deck.ac) {
                if (!String.Equals(ac.campo5, null) && ac.campo5.Trim().Length > 0)
                    if (!String.Equals(ac.campo6, null) && ac.campo6.Trim().Length > 0)
                        if (String.Concat(ac.campo5.Trim(), ac.campo6.Trim()) == String.Concat(UtilitarioDeData.NomeMes(deck.mes), "2")) {
                            ac.campo5 = ac.campo5.Replace(UtilitarioDeData.NomeMes(deck.mes), UtilitarioDeData.NomeMes(deck.mes + 1));
                            ac.campo6 = null;
                        }
            }
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            PropertyInfo blockTitle1 = this.GetType().GetProperty("campo1");
            PropertyInfo blockTitle2 = this.GetType().GetProperty("campo2");
            mWSheet1.SetValue(rol, 1, blockTitle1.GetValue(this).ToString());
            mWSheet1.SetValue(rol, 3, blockTitle2.GetValue(this).ToString());
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev) {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo2");
            var usinaProperty3 = tupleList[0].Item2.GetType().GetProperty("campo3");
            var usinaProperty4 = tupleList[0].Item2.GetType().GetProperty("campo6");

            return tupleList.OrderBy(tw => int.Parse(usinaProperty.GetValue(tw.Item2).ToString()))
                .ThenBy(tw => usinaProperty2.GetValue(tw.Item2))
                .ThenBy(tw => usinaProperty3.GetValue(tw.Item2))
                .ThenBy(tw => usinaProperty4.GetValue(tw.Item2));
        }

        public override int getTitleLength() { return 3; }

        public static void atualizarMensal(Deck novoDeck, DeckNW deckNwBase, Semanas sAtual) {
            DateTime dtInicio = new DateTime(sAtual.ano, sAtual.mes, 1);
            DateTime dtMes2 = dtInicio.AddMonths(1);

            List<AC> lstAC = new List<AC>();


            foreach (AC ac in novoDeck.ac
                    .Where(x => x.campo2.Trim() != "JUSMED")
                    .Where(x => x.campo2.Trim() != "NUMCON")
                    .Where(x => x.campo2.Trim() != "NUMMAQ")
                )
                if (ac.campo5 == null || ac.campo5.Trim() == "")
                    lstAC.Add(ac);


            var nomeMesFinalRealAtual = UtilitarioDeData.NomeMes(UtilitarioDeData.mesFinalReal(sAtual.mes));

            MensalJusmed(deckNwBase, sAtual.mes, sAtual.ano, dtMes2.Month, dtMes2.Year, lstAC, nomeMesFinalRealAtual);
            MensalExpansoes(deckNwBase, sAtual.mes, sAtual.ano, dtMes2.Month, dtMes2.Year, lstAC, nomeMesFinalRealAtual);

            novoDeck.ac = lstAC;
        }

        public static void atualizarRV0(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase) {
            var mesBase = sBase.mes;
            var mesAtual = s.mes;
            var anoBase = sBase.ano;
            var anoAtual = s.ano;
            var mesSeguinte = UtilitarioDeData.mesFinalReal(mesAtual);
            var anoSeguinte = mesSeguinte > mesAtual ? anoAtual : anoAtual + 1;

            List<AC> lstAC = new List<AC>();

            deck.ac.Where(a => a.campo5 == null || a.campo5.Trim() == "").ToList().ForEach(a =>
                lstAC.Add(a)
                );

            var nomeMesBase = UtilitarioDeData.NomeMes(mesBase);
            var nomeMesAtual = UtilitarioDeData.NomeMes(mesAtual);
            var nomeMesFinalRealBase = UtilitarioDeData.NomeMes(UtilitarioDeData.mesFinalReal(mesBase));
            var nomeMesFinalRealAtual = UtilitarioDeData.NomeMes(UtilitarioDeData.mesFinalReal(mesAtual));


            var acList = deck.ac.Where(a => a.campo5 != null && a.campo5.Trim() != "" && a.campo2.Trim() != "JUSMED").ToList();


            //Desloca Meses
            foreach (var acPosto in acList.GroupBy(a => a.campo1.Trim())) {

                var microBlocos = acPosto.GroupBy(a => new {
                    mes = UtilitarioDeData.MesNome(a.campo5.Trim()),
                    estagio = (string.IsNullOrWhiteSpace(a.campo6) ? "1" : a.campo6.Trim().Split(' ')[0])
                });

                var keys = from m in microBlocos
                           let estagio = m.Key.estagio.Trim().Split(' ')[0]
                           let ano = m.Key.mes < mesBase ? anoBase + 1 : anoBase
                           select new { m.Key.mes, estagio, ano, keyOrg = m.Key };

                var keyBase = keys.OrderByDescending(k => k.ano)
                    .ThenByDescending(k => k.mes)
                    .ThenByDescending(k => k.estagio)
                    .Where(k => (k.ano.ToString() + k.mes.ToString("00") + k.estagio).CompareTo(anoAtual.ToString() + mesAtual.ToString("00") + "1") <= 0).FirstOrDefault();

                if (keyBase != null)
                    foreach (var ac in microBlocos.Where(c => c.Key.mes == keyBase.keyOrg.mes && c.Key.estagio == keyBase.keyOrg.estagio)
                        .First()) {
                        lstAC.Add(new AC() {
                            linha = ac.linha,
                            nome = ac.nome,
                            ordem = ac.ordem,
                            campo1 = ac.campo1,
                            campo2 = ac.campo2,
                            campo3 = ac.campo3,
                            campo4 = ac.campo4,
                            campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesAtual, 38),
                            campo6 = "  1     "
                        });
                    }

                foreach (var key in keys.OrderBy(k => k.ano).ThenBy(k => k.mes).ThenBy(k => k.estagio)
                    .Where(k => (k.ano.ToString() + k.mes.ToString("00") + k.estagio).CompareTo(anoAtual.ToString() + mesAtual.ToString("00") + "1") > 0))
                    foreach (var ac in microBlocos.Where(c => c.Key.mes == key.keyOrg.mes && c.Key.estagio == key.keyOrg.estagio).First()) {
                        lstAC.Add(ac);
                    }
            }



            //Expansões
            RV0Expansoes(deckNW, mesAtual, anoAtual, mesSeguinte, anoSeguinte, lstAC, nomeMesAtual, nomeMesFinalRealAtual);

            // Atualiza JUSMED
            RV0Jusmed(deckNW, mesAtual, anoAtual, mesSeguinte, anoSeguinte, lstAC, nomeMesAtual, nomeMesFinalRealAtual);

            deck.ac = lstAC.OrderBy(x => x.ordem).ToList();
        }

        private static void RV0Jusmed(DeckNW decksNW, int mesAtual, int anoAtual, int mesSeguinte, int anoSeguinte, List<AC> lstAC, string nomeMesAtual, string nomeMesFinalRealAtual) {
            var anomes = string.Format("{0:0000}{1:00}", anoAtual, mesAtual);
            foreach (var cfugaGruped in decksNW.modif
                .Where(a => a.MNEMONICO == "CFUGA")
                //.Where(a => string.Format("{0:0000}{1:00}", a.ANO, a.MES).CompareTo(anomes) >= 0)
                .GroupBy(c => c.NUM_USINA)) {
                var cfuga1 = cfugaGruped
                    .Where(a => string.Format("{0:0000}{1:00}", a.ANO, a.MES).CompareTo(anomes) <= 0)
                .OrderByDescending(y => y.ANO)
                .ThenByDescending(z => z.MES)
                .FirstOrDefault();

                if (cfuga1 != null) {
                    double valMes = cfuga1.VALOR;

                    AC ac2 = new AC();
                    ac2.campo1 = UtilitarioDeTexto.preencheEspacos(cfuga1.NUM_USINA.ToString(), 5);
                    ac2.campo2 = "  JUSMED";
                    ac2.campo3 = "         ";
                    ac2.campo4 = UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(valMes, 2), 5), 11, 1);
                    ac2.campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesAtual, 37);
                    ac2.campo6 = "  1     ";                    //"  1 2015"

                    lstAC.Add(ac2);
                }

                var cfuga2 = cfugaGruped
                    .Where(a => a.ANO == anoSeguinte && a.MES == mesSeguinte)
                .FirstOrDefault() ?? cfuga1;
                if (cfuga2 != null) {
                    double valMes = cfuga2.VALOR;

                    AC ac2 = new AC();
                    ac2.campo1 = UtilitarioDeTexto.preencheEspacos(cfuga1.NUM_USINA.ToString(), 5);
                    ac2.campo2 = "  JUSMED";
                    ac2.campo3 = "         ";
                    ac2.campo4 = UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(valMes, 2), 5), 11, 1);
                    ac2.campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesFinalRealAtual, 37);
                    if (anoSeguinte != anoAtual)
                        ac2.campo6 = string.Format("    {0}", anoSeguinte);                    //"  1 2015"
                    else
                        ac2.campo6 = "        ";

                    lstAC.Add(ac2);
                }

            }
        }

        private static void MensalJusmed(DeckNW decksNW, int mesAtual, int anoAtual, int mesSeguinte, int anoSeguinte, List<AC> lstAC, string nomeMesFinalRealAtual) {
            var anomes = string.Format("{0:0000}{1:00}", anoAtual, mesAtual);
            foreach (var cfugaGruped in decksNW.modif
                .Where(a => a.MNEMONICO == "CFUGA")
                //.Where(a => string.Format("{0:0000}{1:00}", a.ANO, a.MES).CompareTo(anomes) >= 0)
                .GroupBy(c => c.NUM_USINA)) {
                var cfuga1 = cfugaGruped
                    .Where(a => string.Format("{0:0000}{1:00}", a.ANO, a.MES).CompareTo(anomes) <= 0)
                .OrderByDescending(y => y.ANO)
                .ThenByDescending(z => z.MES)
                .FirstOrDefault();

                if (cfuga1 != null) {
                    double valMes = cfuga1.VALOR;

                    AC ac2 = new AC();
                    ac2.campo1 = UtilitarioDeTexto.preencheEspacos(cfuga1.NUM_USINA.ToString(), 5);
                    ac2.campo2 = "  JUSMED";
                    ac2.campo3 = "         ";
                    ac2.campo4 = UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(valMes, 2), 5), 11, 1);
                    ac2.campo5 = UtilitarioDeTexto.preencheEspacos("", 37);
                    ac2.campo6 = "        ";                    //"  1 2015"

                    lstAC.Add(ac2);
                }

                var cfuga2 = cfugaGruped
                    .Where(a => a.ANO == anoSeguinte && a.MES == mesSeguinte)
                .FirstOrDefault() ?? cfuga1;
                if (cfuga2 != null) {
                    double valMes = cfuga2.VALOR;

                    AC ac2 = new AC();
                    ac2.campo1 = UtilitarioDeTexto.preencheEspacos(cfuga1.NUM_USINA.ToString(), 5);
                    ac2.campo2 = "  JUSMED";
                    ac2.campo3 = "         ";
                    ac2.campo4 = UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(valMes, 2), 5), 11, 1);
                    ac2.campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesFinalRealAtual, 37);
                    ac2.campo6 = string.Format("    {0}", anoSeguinte);                    //"  1 2015"


                    lstAC.Add(ac2);
                }

            }
        }


        private static void RV0Expansoes(DeckNW deckNW, int mesAtual, int anoAtual, int mesSeguinte, int anoSeguinte, List<AC> lstAC, string nomeMesAtual, string nomeMesFinalRealAtual) {

            var exp = from e in deckNW.exph
                      where (e.Entrada ?? "").Contains("/")
                      let mes = e.Entrada.Trim().Split('/')[0]
                      let ano = e.Entrada.Trim().Split('/')[1]
                      select new { e.Codigo, ano, mes, e.Conjunto, e.Pot };

            var modif = from m in deckNW.modif
                        where m.MNEMONICO == "NUMMAQ"
                        select new { m.NUM_USINA, Conjunto = m.ANO, NumMaq = m.MES };

            var expAtual = exp.Where(e => (e.ano + e.mes.PadLeft(2, '0'))
                .CompareTo(anoAtual.ToString() + mesAtual.ToString("00")) <= 0)
                .GroupBy(e => new { Cod = e.Codigo.ToString(), Con = e.Conjunto.ToString() });

            var expSeguinte = exp.Where(e => (e.ano + e.mes.PadLeft(2, '0'))
                .CompareTo(anoSeguinte.ToString() + mesSeguinte.ToString("00")) <= 0)
                .GroupBy(e => new { Cod = e.Codigo.ToString(), Con = e.Conjunto.ToString() });

            var postos = modif.Select(x => x.NUM_USINA).Union(expSeguinte.Select(x => int.Parse(x.Key.Cod)));

            var q = from e in expAtual
                    join m in modif on new { Cod = e.Key.Cod, Con = e.Key.Con } equals new { Cod = m.NUM_USINA.ToString(), Con = m.Conjunto.ToString() } into mj
                    from m2 in mj.DefaultIfEmpty()
                    select new { e, m2 };

            var qSeguinte = from e in expSeguinte
                            join m in modif on new { Cod = e.Key.Cod, Con = e.Key.Con } equals new { Cod = m.NUM_USINA.ToString(), Con = m.Conjunto.ToString() } into mj
                            from m2 in mj.DefaultIfEmpty()
                            select new { e, m2 };


            var expBlocks = from e in q
                            let maqAnt = e.m2 == null ? 0 : e.m2.NumMaq
                            let pot = e.e.Sum(x => x.Pot) / e.e.Count()
                            select new { maq = maqAnt + e.e.Count(), pot, cod = e.e.Key.Cod, con = e.e.Key.Con, modif = e.m2 };

            var expBlocksSeguinte = from e in qSeguinte
                                    let maqAnt = e.m2 == null ? 0 : e.m2.NumMaq
                                    let pot = e.e.Sum(x => x.Pot) / e.e.Count()
                                    select new { maq = maqAnt + e.e.Count(), pot, cod = e.e.Key.Cod, con = e.e.Key.Con, modif = e.m2 };

            /*
            NUMCON
            NUMMAQ
            ALTEFE
            VAZEFE
            POTEFE
            */

            foreach (var expBlock in expBlocks) {

                var acBase = lstAC.Where(a => a.campo1.Trim() == expBlock.cod && a.campo3.Trim() == expBlock.con && a.campo5.Trim() == nomeMesAtual)
                    .Where(a => a.campo2.Trim() == "NUMMAQ")
                    .FirstOrDefault();

                // Exite no decomp base e é menor que newwave
                if (acBase != null) {
                    if (int.Parse(acBase.campo4.Trim()) < expBlock.maq) {
                        acBase.campo4 = expBlock.maq.ToString().PadLeft(5) + "     ";
                    }
                } else {
                    lstAC.Add(new AC {

                        campo1 = UtilitarioDeTexto.preencheEspacos(expBlock.cod.ToString(), 5)
                       ,
                        campo2 = "  NUMMAQ"
                       ,
                        campo3 = "        " + expBlock.con.ToString()
                       ,
                        campo4 = expBlock.maq.ToString().PadLeft(5) + "     "
                       ,
                        campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesAtual, 38)
                       ,
                        campo6 = string.Format("  1 {0}", anoAtual)
                    });
                }
            }

            foreach (var expBlock in expBlocksSeguinte) {

                var acBase = lstAC.Where(a => a.campo1.Trim() == expBlock.cod && a.campo3.Trim() == expBlock.con && a.campo5.Trim() == nomeMesFinalRealAtual)
                    .Where(a => a.campo2.Trim() == "NUMMAQ")
                    .FirstOrDefault();

                // Exite no decomp base e é menor que newwave
                if (acBase != null) {
                    if (
                        int.Parse(acBase.campo4.Trim()) < expBlock.maq) {
                        acBase.campo4 = expBlock.maq.ToString().PadLeft(5) + "     ";
                    }
                } else {
                    lstAC.Add(new AC {
                        campo1 = UtilitarioDeTexto.preencheEspacos(expBlock.cod.ToString(), 5)
                        ,
                        campo2 = "  NUMMAQ"
                       ,
                        campo3 = "        " + expBlock.con.ToString()
                       ,
                        campo4 = expBlock.maq.ToString().PadLeft(5) + "     "
                       ,
                        campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesFinalRealAtual, 38)
                       ,
                        campo6 = string.Format("        ", anoSeguinte)
                    });
                }
            }
        }

        private static void MensalExpansoes(DeckNW deckNW, int mesAtual, int anoAtual, int mesSeguinte, int anoSeguinte, List<AC> lstAC, string nomeMesFinalRealAtual) {

            var exp = from e in deckNW.exph
                      where (e.Entrada ?? "").Contains("/")
                      let mes = e.Entrada.Trim().Split('/')[0]
                      let ano = e.Entrada.Trim().Split('/')[1]
                      select new { e.Codigo, ano, mes, e.Conjunto, e.Pot };

            var modif = from m in deckNW.modif
                        where m.MNEMONICO == "NUMMAQ"
                        select new { m.NUM_USINA, Conjunto = m.ANO, NumMaq = m.MES };

            var expAtual = exp.Where(e => (e.ano + e.mes.PadLeft(2, '0'))
                .CompareTo(anoAtual.ToString() + mesAtual.ToString("00")) <= 0)
                .GroupBy(e => new { Cod = e.Codigo, Con = e.Conjunto });

            var expSeguinte = exp.Where(e => (e.ano + e.mes.PadLeft(2, '0'))
                .CompareTo(anoSeguinte.ToString() + mesSeguinte.ToString("00")) <= 0)
                .GroupBy(e => new { Cod = e.Codigo, Con = e.Conjunto });

            var usinas = modif.Select(x => x.NUM_USINA)
                .Union(expSeguinte.Select(x => x.Key.Cod))
                .Union(expAtual.Select(x => x.Key.Cod))
                .Distinct();

            foreach (var uhe in usinas) {

                //1st
                int[] numMaqsIni = { 0, 0, 0, 0, 0 };
                int[] numMaqs1 = { 0, 0, 0, 0, 0 };
                int[] numMaqs2 = { 0, 0, 0, 0, 0 };
                foreach (var m in modif.Where(x => x.NUM_USINA == uhe)) numMaqsIni[m.Conjunto - 1] = m.NumMaq;

                for (int i = 0; i < 5; i++) {
                    numMaqs1[i] = numMaqs2[i] = numMaqsIni[i];
                }

                foreach (var e in expAtual.Where(x => x.Key.Cod == uhe)) numMaqs1[e.Key.Con - 1] = numMaqsIni[e.Key.Con - 1] + e.Count();
                foreach (var e in expSeguinte.Where(x => x.Key.Cod == uhe)) numMaqs2[e.Key.Con - 1] = numMaqsIni[e.Key.Con - 1] + e.Count();

                if (!numMaqs1.Any(x => x != 0) && numMaqs2.Any(x => x != 0)) {
                    lstAC.Add(new AC {

                        campo1 = UtilitarioDeTexto.preencheEspacos(uhe.ToString(), 5),
                        campo2 = "  NUMCON",
                        campo3 = "        0",
                        campo4 = "          ",
                        campo5 = UtilitarioDeTexto.preencheEspacos("", 38),
                        campo6 = "        "
                    });
                } else if (numMaqs1.Any(x => x != 0)) {
                    lstAC.Add(new AC {

                        campo1 = UtilitarioDeTexto.preencheEspacos(uhe.ToString(), 5),
                        campo2 = "  NUMCON",
                        campo3 = "       " + numMaqs2.Count(x => x != 0).ToString().PadLeft(2),
                        campo4 = "          ",
                        campo5 = UtilitarioDeTexto.preencheEspacos("", 38),
                        campo6 = "        "
                    });


                    for (int c = 0; c < numMaqs2.Count(x => x != 0); c++) {
                        lstAC.Add(new AC {

                            campo1 = UtilitarioDeTexto.preencheEspacos(uhe.ToString(), 5),
                            campo2 = "  NUMMAQ",
                            campo3 = "       " + (c + 1).ToString().PadLeft(2),
                            campo4 = numMaqs1[c].ToString().PadLeft(5) + "     ",
                            campo5 = UtilitarioDeTexto.preencheEspacos("", 38),
                            campo6 = "        "
                        });
                    }
                }




                //2nd
                if (numMaqs2.Any(x => x != 0)) {
                    lstAC.Add(new AC {

                        campo1 = UtilitarioDeTexto.preencheEspacos(uhe.ToString(), 5),
                        campo2 = "  NUMCON",
                        campo3 = "       " + numMaqs2.Count(x => x != 0).ToString().PadLeft(2),
                        campo4 = "          ",
                        campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesFinalRealAtual, 38),
                        campo6 = "    " + anoSeguinte.ToString()
                    });


                    for (int c = 0; c < numMaqs2.Count(x => x != 0); c++) {
                        lstAC.Add(new AC {

                            campo1 = UtilitarioDeTexto.preencheEspacos(uhe.ToString(), 5),
                            campo2 = "  NUMMAQ",
                            campo3 = "       " + (c + 1).ToString().PadLeft(2),
                            campo4 = numMaqs2[c].ToString().PadLeft(5) + "     ",
                            campo5 = UtilitarioDeTexto.preencheEspacos(nomeMesFinalRealAtual, 38),
                            campo6 = "    " + anoSeguinte.ToString()
                        });
                    }
                }

            }
        }

        public static new void atualizarRV0Opcional(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase) {
            atualizarRV0(deck, deckBase, deckNW, s, sBase);
        }
    }
}
