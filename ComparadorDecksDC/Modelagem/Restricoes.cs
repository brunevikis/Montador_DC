using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ToolBox;

namespace ComparadorDecksDC.Modelagem {
    public class Restricoes : blockModel {
        static string[] bloco1 = new string[] { "HA", "HV", "HQ", "RE" };
        static string[] bloco2 = new string[] { "LA", "LV", "LQ", "LU" };

        public virtual string bloco { get; set; }
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }

        //Apenas para as restrições eletricas (RHE)
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }


        public override void definePos() { }
        public override void preencheCampos(string[] s) {
            try {
                campo1 = s[0];
                campo2 = s[1];
                campo3 = s[2];
            } catch (IndexOutOfRangeException) {
                // Deixar em branco
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }

        public static void atualizarRV0(Deck deck, int nSemanasAtual, int nSemanasBase, string bloco) {
            bool c;
            PropertyInfo blockRest = deck.GetType().GetProperty(bloco.ToLower());
            IList<Restricoes> lstRestricoes = ((IList)blockRest.GetValue(deck, null)).Cast<Restricoes>().ToList();

            Type t = Type.GetType("ComparadorDecksDC.Modelagem." + bloco);
            IList blockListNew = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));

            for (int x = 0; x < lstRestricoes.Count(); x++) {
                c = true;
                if (bloco1.Contains(lstRestricoes[x].bloco)) {
                    lstRestricoes[x].campo3 = lstRestricoes[x].campo3.Replace(nSemanasBase.ToString(), (nSemanasAtual).ToString());
                    lstRestricoes[x].campo3 = lstRestricoes[x].campo3.Replace((nSemanasBase + 1).ToString(), (nSemanasAtual + 1).ToString());
                }

                if (bloco2.Contains(lstRestricoes[x].bloco) && x < lstRestricoes.Count - 1 && String.Equals(lstRestricoes[x].campo2.Trim(), nSemanasBase.ToString()) && String.Equals(lstRestricoes[x + 1].campo2.Trim(), (nSemanasBase + 1).ToString()) && nSemanasAtual < nSemanasBase) {
                    c = false;
                    lstRestricoes.RemoveAt(x);
                    x--;
                } else if (int.Parse(lstRestricoes[x].campo2.Trim()) > 1) {
                    if (String.Equals(lstRestricoes[x].campo2.Trim(), (nSemanasBase + 1).ToString()))
                        lstRestricoes[x].campo2 = lstRestricoes[x].campo2.Replace((nSemanasBase + 1).ToString(), (nSemanasAtual + 1).ToString());
                    else if (String.Equals(lstRestricoes[x].campo2.Trim(), (nSemanasBase).ToString()))
                        lstRestricoes[x].campo2 = lstRestricoes[x].campo2.Replace(nSemanasBase.ToString(), nSemanasAtual.ToString());
                }
                if (c)
                    blockListNew.Add(lstRestricoes[x]);
            }
            blockRest.SetValue(deck, blockListNew, null);
        }

        protected static void atualizarRV0Opcional(Deck deck, Deck deckBase, string bloco) {
            PropertyInfo blockRest = deck.GetType().GetProperty(bloco.ToLower());
            Semanas s = ComparadorDecksDC.Factory.SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            Semanas sBase = ComparadorDecksDC.Factory.SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            Type t = Type.GetType("ComparadorDecksDC.Modelagem." + bloco);
            var blockListNew = new List<Restricoes>();
            var currentBlock = ((IList)blockRest.GetValue(deck)).Cast<Restricoes>();
            var offsetEstagios = sBase.numeroEstagios - deckBase.rev;

            var blocoGrouped = from r in currentBlock
                               let posto = r.campo1.Trim()
                               group r by posto;

            foreach (var rhGrp in blocoGrouped) {

                //trata bloco1
                foreach (var rh in rhGrp.Where(r => bloco1.Contains(r.bloco))) {
                    rh.campo3 = "    " + (s.semanas + 1).ToString();//"    4"
                    blockListNew.Add(rh);
                }

                //trata bloco2

                foreach (var rh in rhGrp.Where(r => bloco2.Contains(r.bloco)).OrderByDescending(r => r.campo2)) {
                    var estagio = int.Parse(rh.campo2.Trim());
                    if (estagio - offsetEstagios <= 1) {
                        rh.campo2 = "   1";
                        blockListNew.Add(rh);
                        break;
                    } else {
                        rh.campo2 = "   " + (estagio - offsetEstagios).ToString();
                        blockListNew.Add(rh);
                    }
                }

                //demais blocos
                foreach (var rh in rhGrp.Where(r => !bloco2.Contains(r.bloco) && !bloco1.Contains(r.bloco))) {
                    blockListNew.Add(rh);
                }
            }

            IList blockConverted = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));

            blockListNew.OrderBy(r => r.ordem).ToList().ForEach(r =>
                blockConverted.Add(r)
                );

            blockRest.SetValue(deck, blockConverted, null);
        }

        public static void atualizarRVX(Deck deck, string bloco) {
            bool c;
            PropertyInfo blockRest = deck.GetType().GetProperty(bloco.ToLower());
            IList<Restricoes> lstRestricoes = ((IList)blockRest.GetValue(deck, null)).Cast<Restricoes>().ToList();

            Type t = Type.GetType("ComparadorDecksDC.Modelagem." + bloco);
            IList blockListNew = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));

            for (int x = 0; x < lstRestricoes.Count(); x++) {
                c = true;
                if (bloco1.Contains(lstRestricoes[x].bloco))
                    lstRestricoes[x].campo3 = lstRestricoes[x].campo3.Replace(lstRestricoes[x].campo3.Trim(), (int.Parse(lstRestricoes[x].campo3.Trim()) - 1).ToString());
                if (bloco2.Contains(lstRestricoes[x].bloco) && x < lstRestricoes.Count - 1 && String.Equals(lstRestricoes[x].campo2.Trim(), "1")) {
                    if (String.Equals(lstRestricoes[x + 1].campo2.Trim(), "2")) {
                        c = false;
                        lstRestricoes.RemoveAt(x);
                        x--;
                    }
                } else if (int.Parse(lstRestricoes[x].campo2.Trim()) > 1) {
                    lstRestricoes[x].campo2 = lstRestricoes[x].campo2.Replace(lstRestricoes[x].campo2.Trim(), (int.Parse(lstRestricoes[x].campo2.Trim()) - 1).ToString());
                }

                if (c)
                    blockListNew.Add(lstRestricoes[x]);
            }
            blockRest.SetValue(deck, blockListNew, null);
        }

        public override void escreveLinhaExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol, int numDeck, int difRevDecks, bool escreveTitulo) {
            this.definePos();
            int col;
            if (this is RHA) col = this.getTitleLength() + (2 * (numDeck - 1));
            else col = this.getTitleLength() + (7 * (numDeck - 1));         //Caso mude o formato das restrições, mudar o numero magico 7

            if (escreveTitulo) this.escreveTituloExcel(mWSheet1, rol);

            StringBuilder linhaTotalBuilder = new StringBuilder();

            if (campo2 != null) linhaTotalBuilder.Append(campo2);
            if (campo3 != null) linhaTotalBuilder.Append(campo3);
            if (campo4 != null) linhaTotalBuilder.Append(campo4);
            if (campo5 != null) linhaTotalBuilder.Append(campo5);

            string[] linhaTotal = linhaTotalBuilder.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string linha in linhaTotal) {
                mWSheet1.SetValue(rol, 1 + col++, linha.Trim());
            }
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol) {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 3, this.bloco);
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev) {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("bloco");
            var usinaProperty3 = tupleList[0].Item2.GetType().GetProperty("campo2");
            var usinaProperty4 = tupleList[0].Item2.GetType().GetProperty("campo3");

            return tupleList.OrderBy(tw => int.Parse(usinaProperty.GetValue(tw.Item2).ToString()))
                .ThenBy(tw => Restricoes.ordemBloco(usinaProperty2.GetValue(tw.Item2).ToString()))
                .ThenBy(tw => Restricoes.ordemRVDif(usinaProperty3.GetValue(tw.Item2).ToString(), difRev, tw.Item1))
                .ThenBy(tw => Restricoes.ordemRVDif(usinaProperty4.GetValue(tw.Item2).ToString(), difRev, tw.Item1));
        }

        public static int ordemBloco(string blocoNome) {
            string[] blocos = new string[14];
            blocos[0] = "HA"; blocos[1] = "RE"; blocos[2] = "HQ"; blocos[3] = "HV";
            blocos[4] = "LA"; blocos[5] = "LU"; blocos[6] = "LQ"; blocos[7] = "LV";
            blocos[8] = "CA"; blocos[9] = "FU"; blocos[10] = "CQ"; blocos[11] = "CV";
            blocos[12] = "FI"; blocos[13] = "FT";

            for (int x = 0; x < blocos.Length; x++)
                if (blocos[x] == blocoNome) return x;

            return blocos.Length + 1;
        }

        public override int getTitleLength() { return 3; }

        public static void atualizarMensal(Deck deck, int rev, Semanas sBase, string bloco) {
            if (rev != -1) {
                int semanaMesSeguinte = sBase.semanas + 1 - rev;

                bool c;
                PropertyInfo blockRest = deck.GetType().GetProperty(bloco.ToLower());
                IList<Restricoes> lstRestricoes = ((IList)blockRest.GetValue(deck, null)).Cast<Restricoes>().ToList();

                Type t = Type.GetType("ComparadorDecksDC.Modelagem." + bloco);
                IList blockListNew = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(t));

                for (int x = 0; x < lstRestricoes.Count(); x++) {
                    c = true;
                    if (bloco1.Contains(lstRestricoes[x].bloco)) {
                        if (int.Parse(lstRestricoes[x].campo3.Trim()) < semanaMesSeguinte)
                            lstRestricoes[x].campo3 = Regex.Replace(lstRestricoes[x].campo3, @"\d", "1");
                        else
                            lstRestricoes[x].campo3 = Regex.Replace(lstRestricoes[x].campo3, @"\d", "2");
                    } else if (bloco2.Contains(lstRestricoes[x].bloco) && int.Parse(lstRestricoes[x].campo2.Trim()) > 1) {
                        if (int.Parse(lstRestricoes[x].campo2.Trim()) < semanaMesSeguinte && bloco2.Contains(lstRestricoes[x - 1].bloco))
                            c = false;
                        else
                            lstRestricoes[x].campo2 = Regex.Replace(lstRestricoes[x].campo2, @"\d", "2");
                    }
                    if (c)
                        blockListNew.Add(lstRestricoes[x]);
                }
                blockRest.SetValue(deck, blockListNew, null);
            }
        }
    }
}
