using DecompTools.ControllerDC;
using DecompTools.FactoryDC;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DecompTools.ModelagemDC
{
    public class CM : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }

        public CM()
        {
            pos = new int[] { 5, 5, 12};
            nome = "CM";
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

                if (i == 3)
                    linha = linha.Append(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(double.Parse(campo3.Replace(".", ",")), 2), pos[i - 1]));
                else
                    linha = linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
            }

            return linha.ToString();
        }

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < (deck.cm.Count()); x++)
            {
                if (deck.cm[x].campo3 == "1")
                {
                    deck.cm.Remove(deck.cm[x]);
                    x--;
                }
                else
                    deck.cm[x].campo3 = (int.Parse(deck.cm[x].campo3) - 1).ToString();
            }
        }
    }
}
