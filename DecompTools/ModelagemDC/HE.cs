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
    public class HE : blockModel//Restricoes//public class HE : blockModel
    {
        public virtual string bloco { get; set; }

        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public override void definePos()
        {
            if (this.bloco != null)
                switch (this.bloco)
                {
                    case "HE":
                        pos = new int[] { 5, 3, 14, 3, 11, 6 };
                        nome = "HE";
                        break;

                    case "CM":
                        pos = new int[] { 5, 5, 12 };
                        nome = "CM";
                        break;
                }
        }
        //public HE()
        //{

        //    pos = new int[] { 5, 3, 14, 3, 11, 6};
        //    nome = "HE";
        //}

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);
            if (pos.Length > 4)
            {
                for (int i = 1; i <= pos.Length; i++)
                {
                    PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                    if (block.GetValue(this, null) == null)
                        break;

                    if (i == 5)
                        linha = linha.Append(UtilitarioDeTexto.preencheEspacos(UtilitarioDeTexto.zeroDir(double.Parse(campo5.Replace(".", ",")), 2), pos[i - 1]));
                    else
                        linha = linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
                }
            }
            else
            {
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
            }
           

            return linha.ToString();
        }

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < (deck.he.Count()); x++)
            {
                if (deck.he[x].nome == "HE")
                {
                    if (deck.he[x].campo4 == "1")
                    {
                        deck.he.Remove(deck.he[x]);//remove a linha HE 
                        deck.he.Remove(deck.he[x]);// remove a linha CM
                        x--;
                    }
                    else
                        deck.he[x].campo4 = (int.Parse(deck.he[x].campo4) - 1).ToString();
                }
                
            }
        }
    }
}
