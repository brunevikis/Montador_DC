using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC
{
    public class FA : blockModel
    {
        public virtual string campo1 { get; set; }






        public FA()
        {
            pos = new int[] { 13 };
            nome = "FA";
        }
    }
}