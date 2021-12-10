using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DecompTools.ModelagemPrevs {
    public class Estudos : Model {
        public virtual int id { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual Prevs prevs_base { get; set; }
        public virtual Prevs prevs_saida { get; set; }
        public virtual Regressao regressao { get; set; }
        public virtual Calculados calculado { get; set; }
        public virtual int rev { get; set; }
        public virtual int ano { get; set; }
        public virtual int mes { get; set; }
    }
}
