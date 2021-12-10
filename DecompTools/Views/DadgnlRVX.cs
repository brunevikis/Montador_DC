using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.Views {
    public interface DadgnlRVX {
        string InputFile { get; }
        string OutputFolder { get; }
        string ReturnMessage { set; }
    }
}
