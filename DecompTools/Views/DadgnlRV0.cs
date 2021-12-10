using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.Views {
    public interface DadgnlRV0 {
        string InputFile { get; }
        string OutputFolder { get; }
        int Mes { get; }
        int Ano { get; }
        string ReturnMessage { set; }
    }
}
