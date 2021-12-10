using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class CI : blockModel {
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



        public CI() {
            pos = new int[] { 5, 3, 10, 6, 8, 5, 10, 5, 5, 10, 5, 5, 10 };
            nome = "CI";
        }
    }
}
