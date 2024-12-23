using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC
{
    public class CX : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }

        public CX()
        {
            pos = new int[] { 6, 5 };
            nome = "CX";
        }
    }
}
