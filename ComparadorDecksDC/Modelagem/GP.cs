using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class GP : blockModel
    {
        public virtual string campo1 { get; set; }

        

        public GP()
        {
            pos = new int[] { 12 };
            nome = "GP";
        }
    }
}
