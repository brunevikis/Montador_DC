using ComparadorDecksDC.Controller;
using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ComparadorDecksDC.Modelagem 
{
    public class UH : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        
        public UH()
        {
            pos = new int[] { 5, 4, 13, 16 };
            nome = "UH";
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

                if( i == 3)
                    linha = linha.Append(UtilitarioDeTexto.preencheEspacos( UtilitarioDeTexto.zeroDir(double.Parse(campo3.Replace(".",",")), 2), pos[i - 1]));
                else
                    linha = linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
            }

            return linha.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deckBase"></param>
        /// <param name="reservSplit"></param>
        /// <param name="p"></param>
        /// <param name="dataInicial"></param>
        /// <param name="tipo">1 = MLT, 2 = base</param>
        /// <returns></returns>
        public static IList<UH> atualizarMensal(Deck deckBase, decimal[,] reservSplit, int p, DateTime dataInicial, int tipo)
        {
            decimal[] subTotal = new decimal[4];
            
            int i = 0;
            foreach (EarmMax eam in EarmMaxDAO.GetAll())
                subTotal[i++] = eam.valor * 730.5m;

            var indiceMes = reservSplit.GetLength(1) - p;
            decimal[] target = new decimal[4] { reservSplit[0, indiceMes] / 100, reservSplit[1, indiceMes] / 100, reservSplit[2, indiceMes] / 100, reservSplit[3, indiceMes] / 100 };


            if (tipo == 1) {
                //preserv base order;
                var uhMlt = MltPostoDAO.getUhListByMonth(dataInicial.Month);
                var newUh = new List<UH>();

                foreach (var uh in deckBase.uh) {
                    var mlt = uhMlt.FirstOrDefault(u => u.campo1 == uh.campo1);

                    if (mlt != null) {
                        newUh.Add(mlt);
                    } else
                        newUh.Add(uh);
                }

                deckBase.uh = newUh;
            }

            return controllerReservatorio.runReserv(deckBase, target, subTotal);
        }
    }
}
