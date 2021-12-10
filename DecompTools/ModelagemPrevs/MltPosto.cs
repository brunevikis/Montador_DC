using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DecompTools.ModelagemPrevs {
    public class MltPosto : Model {
        public virtual int numPosto { get; set; }
        public virtual int submercado { get; set; }
        public virtual DateTime dt_atualizacao { get; set; }
        public virtual double mes1 { get; set; }
        public virtual double mes2 { get; set; }
        public virtual double mes3 { get; set; }
        public virtual double mes4 { get; set; }
        public virtual double mes5 { get; set; }
        public virtual double mes6 { get; set; }
        public virtual double mes7 { get; set; }
        public virtual double mes8 { get; set; }
        public virtual double mes9 { get; set; }
        public virtual double mes10 { get; set; }
        public virtual double mes11 { get; set; }
        public virtual double mes12 { get; set; }

        public override void save() {
            this.dt_atualizacao = DateTime.Now;
            base.save();
        }
    }
}
