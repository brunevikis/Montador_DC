using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Views {
    public interface DadgnlMensal {
        string InputFile { get; }
        string OutputFolder { get; }
        int MesInicial { get; }
        int AnoInicial { get; }
        int MesFinal { get; }
        int AnoFinal { get; }
        string ReturnMessage { set; }
    }
}
