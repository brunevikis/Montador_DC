using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem
{
    public class FC : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }

        

        public FC()
        {
            pos = new int[] { 8, 255 };
            nome = "FC";
        }

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo1, pos[0]));
            linha.Append("    ");
            linha.Append(this.campo2);

            return linha.ToString();
        }
    }
}
