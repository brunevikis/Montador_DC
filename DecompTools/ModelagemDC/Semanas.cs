using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class Semanas {
        public virtual int id { get; set; }
        public virtual int mes { get; set; }
        public virtual int ano { get; set; }
        public virtual int semanas { get; set; }
        public virtual DateTime primeiraSemana { get; set; }
        public virtual int diasMes2 { get; set; }

        public virtual int numeroEstagios {
            get {

                if (diasMes2 > 0)
                    return semanas - 1;
                else
                    return semanas;

            }
        }
    }
}
