using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class EZ : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        
        

        public EZ()
        {
            pos = new int[] { 5, 7 };
            nome = "EZ";
        }
    }
}
