using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC
{
    public class FJ : blockModel
    {
        public virtual string campo1 { get; set; }
        





        public FJ()
        {
            pos = new int[] { 14 };
            nome = "FJ";
        }
    }
}