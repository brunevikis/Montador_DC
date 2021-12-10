using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DecompTools.ModelagemPrevs {
    public class MltSub : Model {
        public virtual int submercado { get; set; }
        public virtual DateTime dt_atualizacao { get; set; }
        public virtual int mes1 { get; set; }
        public virtual int mes2 { get; set; }
        public virtual int mes3 { get; set; }
        public virtual int mes4 { get; set; }
        public virtual int mes5 { get; set; }
        public virtual int mes6 { get; set; }
        public virtual int mes7 { get; set; }
        public virtual int mes8 { get; set; }
        public virtual int mes9 { get; set; }
        public virtual int mes10 { get; set; }
        public virtual int mes11 { get; set; }
        public virtual int mes12 { get; set; }

        public override void save() {
            this.dt_atualizacao = DateTime.Now;
            base.save();
        }
    }
}
