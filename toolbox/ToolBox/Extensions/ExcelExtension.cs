using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox {
    public static class ExcelExtension {

        static int setTrial = 0;
        public static void SetValue(this Microsoft.Office.Interop.Excel.Worksheet sheet, object row, object col, object value) {
            try {
                sheet.Cells[row, col] = value;
                setTrial = 0;
            } catch (System.Runtime.InteropServices.COMException ex) {
                if (++setTrial < 3) {
                    System.Threading.Thread.Sleep(500);
                    SetValue(sheet, row, col, value);
                } else {
                    setTrial = 0;
                    throw ex;
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
