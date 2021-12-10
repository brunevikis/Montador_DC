using DecompTools.FactoryNW;
using DecompTools.ModelagemNW;
using DecompTools.FactoryDC;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace DecompTools.ModelagemDC {
    public class FP : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }
        public virtual string campo10 { get; set; }
        public virtual string campo11 { get; set; }
        public virtual string campo12 { get; set; }
        public virtual string campo13 { get; set; }
        public virtual string campo14 { get; set; }
        public virtual string campo15 { get; set; }
        public virtual string campo16 { get; set; }
        public virtual string campo17 { get; set; }
        public virtual string campo18 { get; set; }

        public FP() {
            pos = new int[] { 5, 5, 3, 5, 6, 6, 3, 5, 6, 6, 7, 6, 4, 3, 5, 6, 6, 3 };
            nome = "FP";
        }
    }
}
