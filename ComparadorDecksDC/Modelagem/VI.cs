using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolBox;

namespace ComparadorDecksDC.Modelagem 
{
    public class VI : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }

        

        public VI()
        {
            pos = new int[] { 5, 5, 7, 5, 5, 5, 5, 5};
            nome = "VI";
        }

        public static void atualizarRVX(Deck deck)
        {
            foreach (VI vi in deck.vi)
            {
                vi.campo7 = vi.campo6;
                vi.campo6 = vi.campo5;
                vi.campo5 = vi.campo4;
                vi.campo4 = vi.campo3;
                vi.campo3 = "AAA";
            }
        }

        public override void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            mWSheet1.SetValue(rol, 1, this.campo1);
            mWSheet1.SetValue(rol, 2, this.campo2);
        }

        public static void atualizarMensal(Deck novoDeck, Deck deckHistorico, int p)
        {
            if (p == 2) //Sazonal
                novoDeck.vi = deckHistorico.vi;
        }
    }
}
