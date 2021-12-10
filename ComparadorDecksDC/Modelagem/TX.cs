using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class TX : blockModel
    {
        public virtual string campo1 { get; set; }

        

        public TX()
        {
            pos = new int[] { 7 };
            nome = "TX";
        }
    }
}
