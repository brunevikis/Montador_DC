using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace ComparadorDecksDC.Modelagem 
{
    public class FD : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        public virtual string campo8 { get; set; }

        

        public FD()
        {
            pos = new int[] {5, 7, 5, 5, 5, 5, 5, 5 };
            nome = "FD";
        }

        public virtual void atualizarRV0(int nSemanasAtual, int nSemanasBase)
        {
            FD fdT = new FD();

            PropertyInfo camp1 = fdT.GetType().GetProperty("campo" + (nSemanasAtual + 2).ToString());
            PropertyInfo camp2 = fdT.GetType().GetProperty("campo" + (nSemanasBase + 2).ToString());

            if (nSemanasAtual < nSemanasBase)
            {
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, String.Empty, null);

                //this.campo7 = this.campo8;
                //this.campo8 = String.Empty;
            }
            else
            {
                PropertyInfo camp3 = fdT.GetType().GetProperty("campo" + (nSemanasBase + 1).ToString());
                camp1.SetValue(this, camp2.GetValue(this, null), null);
                camp2.SetValue(this, camp3.GetValue(this, null), null);

                //this.campo8 = this.campo7;
                //this.campo7 = this.campo6;
            }
        }

        public static void atualizarRVX(Deck deck, Semanas s)
        {
            int sem = (s.semanas + 1) - deck.rev + 2; //(Nº de semanas + 1) - (nº semanas passadas) + ( 2 para ajustar nos campos)
            FD fdT = new FD();

            for (int x = 2; x < sem; x++)
            {
                PropertyInfo camp1 = fdT.GetType().GetProperty("campo" + x.ToString());
                PropertyInfo camp2 = fdT.GetType().GetProperty("campo" + (x + 1).ToString());

                foreach (FD fd in deck.fd)
                {
                    camp1.SetValue(fd, camp2.GetValue(fd, null), null);
                }
            }

            foreach (FD fd in deck.fd)
                UtilitarioDeTexto.zerarDados(fd, sem, 8);
        }

        public static void atualizarMensal(Deck novoDeck, int p, Semanas sBase)
        {
            if (p != -1)            //Verifica se o deck anterior já era mensal.
            {
                int sem = (sBase.semanas + 1) - p + 1; //(Nº de semanas + 1) - (nº semanas passadas) + ( 1 para ajustar nos campos)

                foreach (FD fd in novoDeck.fd)
                {
                    string foo;
                    PropertyInfo camp = fd.GetType().GetProperty("campo" + sem.ToString());
                    foo = camp.GetValue(fd).ToString();

                    if (foo != "0.0")
                        fd.campo3 = foo;

                    UtilitarioDeTexto.zerarDados(fd, 4, 8);
                }
            }
        }
    }
}
