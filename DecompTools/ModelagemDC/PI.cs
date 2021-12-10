using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class PI : blockModel {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }

        public PI() {
            pos = new int[] { 5, 7, 5, 5, 5, 5, 5, 5, 5 };
            nome = "PI";
        }
    }
}
