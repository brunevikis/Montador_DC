using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;


namespace ComparadorDecksDC.Modelagem 
{
    public class IA : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }
        public virtual string campo9 { get; set; }

        public override int getTitleLength() { return 3; }

        public IA()
        {
            pos = new int[] { 4, 5, 5, 13, 10, 10, 10, 10, 10 };
            nome = "IA";
        }

        public override string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();

            if( campo2.Length == 1)
                this.campo2 = String.Concat(this.campo2, " ");
            if( campo3.Length == 1)
                this.campo3 = String.Concat(this.campo3, " ");

            linha.Append(nome);
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo1, pos[0]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo2, pos[1]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo3, pos[2]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo4, pos[3]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo5, pos[4]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo6, pos[5]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo7, pos[6]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo8, pos[7]));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.campo9, pos[8]));

            return linha.ToString();
        }

        public static void atualizarRVX(Deck deck)
        {
            for (int x = 0; x < deck.ia.Count(); x++)
            {
                if (deck.ia[x].campo1 == "1")
                {
                    if (x + 1 < deck.ia.Count() && deck.ia[x + 1].campo1 == "2")
                    {
                        deck.ia.Remove(deck.ia[x]);
                        x--;
                    }
                }
                else
                {
                    deck.ia[x].campo1 = (int.Parse(deck.ia[x].campo1) - 1).ToString();
                }
            }
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo2);
            mWSheet1.SetValue(rol, 3, this.campo3);
        }

        public static new IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev)
        {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo2");
            var usinaProperty2 = tupleList[0].Item2.GetType().GetProperty("campo3");
            var usinaProperty3 = tupleList[0].Item2.GetType().GetProperty("campo1");

            return tupleList.OrderBy(ty => usinaProperty.GetValue(ty.Item2))
                .ThenBy(tw => usinaProperty2.GetValue(tw.Item2))
                .ThenBy(tw => blockModel.ordemRVDif( usinaProperty3.GetValue(tw.Item2).ToString(), difRev, tw.Item1) );
        }

        public static void atualizarMensal(Deck novoDeck)
        {
            List<IA> newListIA = new List<IA>();
            string ant = "";

            foreach (IA ia in novoDeck.ia.OrderBy(x => x.campo2+x.campo3 ).ThenByDescending( x => int.Parse(x.campo1)))
            {
                if (ant != ia.campo2 + ia.campo3)
                {
                    ia.campo1 = "1";
                    newListIA.Add(ia);
                    ant = ia.campo2 + ia.campo3;
                }
            }

            novoDeck.ia = newListIA.OrderBy(x => x.ordem).ToList<IA>();
        }
    }
}
