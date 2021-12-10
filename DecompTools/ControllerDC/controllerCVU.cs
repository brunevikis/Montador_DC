using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DecompTools.ControllerDC {

    public class controllerCVU {

        public static RelatorioCVU LerCVU(string pathEntrada) {
            Excel.Application oXL = null;
            Excel.Workbooks mWorkBooks = null;
            Excel.Workbook mWorkBook = null;
            Excel.Sheets mWorkSheets = null;
            Excel.Worksheet mWSheet1 = null;

            try {

                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                oXL.DisplayAlerts = false;
                oXL.ScreenUpdating = false;

                mWorkBooks = oXL.Workbooks;
                mWorkBook = mWorkBooks.Open(pathEntrada, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, true, false, true);
                mWorkSheets = mWorkBook.Worksheets;
                mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item(1);

                if (!mWSheet1.Name.ToUpperInvariant().Contains("_CUSTO_")) mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item(2);
                if (!mWSheet1.Name.ToUpperInvariant().Contains("_CUSTO_")) mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item(3);


                var cvu = new RelatorioCVU();

                string titulo = "";

                if (mWSheet1.Cells[1, 1].Text != "")
                    titulo = mWSheet1.Cells[1, 1].Value;
                else if (mWSheet1.Cells[1, 2].Text != "")
                    titulo = mWSheet1.Cells[1, 2].Value;
                else if (mWSheet1.Cells[1, 3].Text != "")
                    titulo = mWSheet1.Cells[1, 3].Value;
                else if (mWSheet1.Cells[1, 4].Text != "")
                    titulo = mWSheet1.Cells[1, 4].Value;

                cvu.Titulo = titulo;
                cvu.DataAtualização = DateTime.Now;
                cvu.Arquivo = pathEntrada;
                cvu.Detalhes = new List<RelatorioCVUDetalhe>();


                var r = 0;
                var c = 0;


                for (var rt = 1; rt < 10; rt++) {
                    for (var ct = 1; ct < 10; ct++) {
                        var inicio = ((string)(mWSheet1.Cells[rt, ct].Value ?? "").ToString()).Trim().ToUpperInvariant();
                        if (inicio == "EMPREENDIMENTO") { r = rt; c = ct; rt = 100; break; }
                    }
                }

                if (r != 0 || c != 0) {

                    string empreendimento;
                    do {
                        empreendimento = (mWSheet1.Cells[r, c].Value ?? "").ToString();

                        if (!string.IsNullOrWhiteSpace(empreendimento)) {
                            var combustivel = (mWSheet1.Cells[r, c + 1].Value ?? "").ToString();
                            var leilao = (mWSheet1.Cells[r, c + 2].Value ?? "").ToString();
                            var produto = (mWSheet1.Cells[r, c + 3].Value ?? "").ToString();
                            var cvupmo = (mWSheet1.Cells[r, c + 4].Value ?? "").ToString();


                            cvu.Detalhes.Add(new RelatorioCVUDetalhe() {
                                Empreendimento = empreendimento,
                                Combustivel = combustivel,
                                Leilao = leilao,
                                Produto = produto,
                                CVU_PMO = cvupmo
                            });
                        } else
                            break;
                        r++;
                    } while (true);

                }

                if (cvu.Detalhes.Count == 0) throw new Exception("Não foi lido nenhum cvu, verificar o arquivo.");

                cvu.save();
                return cvu;

            } finally {
                oXL.ScreenUpdating = true;
                oXL.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWSheet1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkSheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                mWorkBooks = null;
                mWorkBook = null;
                mWorkSheets = null;
                mWSheet1 = null;
                oXL = null;


            }
        }

        public static List<string> AtualizaDeck(Deck deck, RelatorioCVU cvu, List<DeParaNomePosto> deParas) {
            var blocoCT = deck.ct;

            var acoes = new List<string>();

            foreach (var cvudetalhe in cvu.Detalhes) {

                float cvuValue;

                if (!float.TryParse(cvudetalhe.CVU_PMO, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.GetCultureInfo("pt-BR").NumberFormat, out cvuValue)) {
                    acoes.Add(cvudetalhe.Empreendimento + " - não possivel converter cvu: " + cvudetalhe.CVU_PMO);
                    continue;
                }

                if (cvuValue == 0) {
                    continue;
                }


                var deParas2 = deParas.Where(d => d.De.ToUpper() == cvudetalhe.Empreendimento.ToUpper()).Select(d => d.Para.ToUpper()).ToList();

                //deParas2.Insert(0, cvudetalhe.Empreendimento.ToUpper());

                var ct = blocoCT.Where(c => deParas2.Contains(c.campo1.ToUpper().Trim())).ToList();


                // Somente aceita cadastrar novo de para se nenhum cadastrado
                if (ct.Count == 0 && deParas2.Count == 0) {
                    DeParaNomePosto novoDePara = null;


                    novoDePara = GetDePara(cvudetalhe.Empreendimento);
                    //se não ignorou, tenta a busca novamente e grava o de-para
                    if (novoDePara != null) {
                        deParas.Add(novoDePara);
                        ct.AddRange(blocoCT.Where(c => novoDePara.Para.ToUpper().Equals(c.campo1.ToUpper())).ToList());
                        novoDePara.save();
                    } else
                        acoes.Add(cvudetalhe.Empreendimento + " ignorado");
                }

                foreach (var ctline in ct) {

                    var novocvu = cvuValue.ToString("#.00", System.Globalization.NumberFormatInfo.InvariantInfo);

                    if (novocvu == ctline.campo13) {
                        continue;
                    }
                    acoes.Add(cvudetalhe.Empreendimento + " (" + ctline.campo1 + " - " + ctline.campo3 + ") alterdado. " + ctline.campo13 + " -> " + novocvu);

                    ctline.campo13 = ctline.campo10 = ctline.campo7 = novocvu;
                }
            }
            return acoes;

        }

        public static DeParaNomePosto GetDePara(string de) {
            DecompTools.Views.FormAtualizaCVUDeParaNovo frm = new DecompTools.Views.FormAtualizaCVUDeParaNovo(de);
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                return frm.DePara;
            } else
                return null;
        }

    }
}
