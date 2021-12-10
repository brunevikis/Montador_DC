using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace DecompTools.FactoryPrevs {
    public class AcomphDAO {


        public static string path = @"H:\Middle - Preço\Acompanhamento de vazões\ACOMPH\1_historico";

        public static IList<Acomph> GetAll() {

            var f = System.IO.Directory.GetFiles(path, "ACOMPH_*.xls", System.IO.SearchOption.AllDirectories);

            return f.Select(c => new Acomph(c)).OrderByDescending(x => x.dt_acomph).ToList();
        }

    }

    public class Acomph {

        string path;


        public string file { get { return System.IO.Path.GetFileNameWithoutExtension(path); } }

        public DateTime dt_acomph {
            get;
            set;
        }

        IList<Acomph_dados> _dados = null;
        object _dadosLock = new object();
        public IList<Acomph_dados> dados {
            get {

                if (_dados == null) {

                    lock (_dadosLock) {
                        if (_dados == null) {
                            lerDados();
                        }
                    }
                }

                return _dados;
            }
        }

        public Acomph(string path) {
            this.path = path;


            var pat = @"ACOMPH_(?'dia'\d{2})_(?'mes'\d{2})_(?'ano'\d{4})";
            var m = System.Text.RegularExpressions.Regex.Match(path, pat, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (m.Success) dt_acomph = new DateTime(int.Parse(m.Groups["ano"].Value), int.Parse(m.Groups["mes"].Value), int.Parse(m.Groups["dia"].Value));
        }

        void lerDados() {


            _dados = new List<Acomph_dados>();
            //Blocos que serão comparados.
            string[] blocos = new string[] { "UH", "CT", "DP", "PQ", "IT", "IA", "MP", "MT", "FD", "VI", "QI", "PI", "TI", "EZ", "EA", "ES", "AC", "RHA", "RHV", "RHQ", "RHE" };

            Excel.Application oXL = null;
            Excel.Workbooks mWorkBooks = null;
            Excel.Workbook mWorkBook = null;
            Excel.Sheets mWorkSheets = null;

            oXL = new Microsoft.Office.Interop.Excel.Application();

            oXL.Visible = false;
            oXL.DisplayAlerts = false;
            oXL.ScreenUpdating = false;

            try {

                if (!System.IO.File.Exists(path))
                    throw new System.IO.FileNotFoundException("Erro: Excel modelo não foi encontrado!");
                mWorkBooks = oXL.Workbooks;
                mWorkBook = mWorkBooks.Open(path, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, false, false, false);
                mWorkSheets = mWorkBook.Worksheets;

                foreach (Excel.Worksheet sheet in mWorkSheets) {

                    object[,] valMatrix = sheet.Range["A1", "FA35"].Value2;

                    int idxP = 9;

                    while (valMatrix[1, idxP] is double || valMatrix[1, idxP] is int) {


                        int p = Convert.ToInt32(valMatrix[1, idxP]);

                        for (int idt = 0; idt < 30; idt++) {

                            var dt = this.dt_acomph.AddDays(-30 + idt);

                            _dados.Add(
                                new Acomph_dados() { dt = dt, posto = p, qInc = (double)valMatrix[6 + idt, idxP - 1], qNat = (double)valMatrix[6 + idt, idxP] }
                            );

                        }
                        idxP += 8;
                    }
                }


                mWorkBook.Close(false);
                oXL.Quit();

            } finally {
                if (oXL != null) {
                    oXL.Visible = true;
                    oXL.DisplayAlerts = true;
                    oXL.ScreenUpdating = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkSheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                mWorkBooks = null;
                mWorkBook = null;
                mWorkSheets = null;
                oXL = null;

            }
        }

        public async Task lerDadosAsync() {
            await Task.Factory.StartNew((Action)lerDados);
        }

    }

    public class Acomph_dados {
        public DateTime dt { get; set; }
        public int posto { get; set; }
        public double qInc { get; set; }
        public double qNat { get; set; }
    }
}
