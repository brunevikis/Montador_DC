using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC
{
    public class VU : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }





        public VU()
        {
           // pos = new int[] { 4, 6, 8};
            pos = new int[] { 5, 6, 4};
            nome = "VU";
        }
    }
}
