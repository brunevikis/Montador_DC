using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class GP : blockModel {
        public virtual string campo1 { get; set; }

        public GP() {
            pos = new int[] { 12 };
            nome = "GP";
        }
    }
}
