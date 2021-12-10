using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class UE : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }

        

        public UE()
        {
            pos = new int[] { 5, 4, 15, 6, 5, 12, 10, 10 };
            nome = "UE";
        }

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;
                if (i != 3)
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                else
                {
                    linha.Append("   ");
                    linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1] - 3, 1));
                }
            }

            return linha.ToString();
        }
    }
}
