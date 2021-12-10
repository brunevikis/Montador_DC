using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class NI : blockModel
    {
        public virtual string campo1 { get; set; }

        

        public NI()
        {
            pos = new int[] { 10 };
            nome = "NI";
        }

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);
            linha.Append("  ");
            linha.Append(UtilitarioDeTexto.preencheEspacos( this.campo1 , pos[0] - 2, 1).Trim() );

            return linha.ToString();
        }
    }
}
