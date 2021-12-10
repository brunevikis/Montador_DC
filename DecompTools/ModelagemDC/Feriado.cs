using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC {
    public class Feriado {
        public virtual DateTime DATA { get; set; }
        public virtual int ID_SEMANA { get; set; }
        public virtual bool IS_FERIADO { get; set; }
        public virtual bool IS_DU { get; set; }
        public virtual string DS_SEMANA { get; set; }
        public virtual string DS_FERIADO { get; set; }
        public virtual int DS_DIA_UTIL { get; set; }
    }
}
