using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DecompTools.ModelagemPrevs {
    public class RegressaoDados : Model {
        public virtual int id { get; set; }
        public virtual Regressao regressao { get; set; }
        public virtual int postoRegredido { get; set; }
        public virtual int postoBase { get; set; }
        public virtual double a0_1 { get; set; }
        public virtual double a0_2 { get; set; }
        public virtual double a0_3 { get; set; }
        public virtual double a0_4 { get; set; }
        public virtual double a0_5 { get; set; }
        public virtual double a0_6 { get; set; }
        public virtual double a0_7 { get; set; }
        public virtual double a0_8 { get; set; }
        public virtual double a0_9 { get; set; }
        public virtual double a0_10 { get; set; }
        public virtual double a0_11 { get; set; }
        public virtual double a0_12 { get; set; }
        public virtual double a1_1 { get; set; }
        public virtual double a1_2 { get; set; }
        public virtual double a1_3 { get; set; }
        public virtual double a1_4 { get; set; }
        public virtual double a1_5 { get; set; }
        public virtual double a1_6 { get; set; }
        public virtual double a1_7 { get; set; }
        public virtual double a1_8 { get; set; }
        public virtual double a1_9 { get; set; }
        public virtual double a1_10 { get; set; }
        public virtual double a1_11 { get; set; }
        public virtual double a1_12 { get; set; }

    }
}
