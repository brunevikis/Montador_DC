using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ComparadorDecksDC.Controller {
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

                var cvu = new RelatorioCVU();

                string titulo = "";

                if (mWSheet1.Cells[1, 1].Text != "")
                    titulo = mWSheet1.Cells[1, 1].Value;
                else if (mWSheet1.Cells[1, 2].Text != "")
                    titulo = mWSheet1.Cells[1, 2].Value;
                else if (mWSheet1.Cells[1, 3].Text != "")
                    titulo = mWSheet1.Cells[1, 3].Value;
                
                cvu.Titulo = titulo;
                cvu.DataAtualização = DateTime.Now;
                cvu.Arquivo = pathEntrada;
                cvu.Detalhes = new List<RelatorioCVUDetalhe>();


                var r = 3;
                string empreendimento;
                do {
                    empreendimento = (mWSheet1.Cells[r, 1].Value ?? "").ToString();

                    if (!string.IsNullOrWhiteSpace(empreendimento)) {
                        var combustivel = (mWSheet1.Cells[r, 2].Value ?? "").ToString();
                        var leilao = (mWSheet1.Cells[r, 3].Value ?? "").ToString();
                        var produto = (mWSheet1.Cells[r, 4].Value ?? "").ToString();
                        var cvupmo = (mWSheet1.Cells[r, 5].Value ?? "").ToString();


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

    }
}
