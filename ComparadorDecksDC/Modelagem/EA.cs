using CapturaNW.Modelagem;
using ComparadorDecksDC.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox;

namespace ComparadorDecksDC.Modelagem 
{
    public class EA : blockModel
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
        public virtual string campo10 { get; set; }
        public virtual string campo11 { get; set; }
        public virtual string campo12 { get; set; }

        

        public EA()
        {
            pos = new int[] {4, 13, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            nome = "EA";
        }

        //Atualizar pelo EAFPAST
        public static void atualizarRV0(Deck deck, Semanas sAtual, Semanas sBase, DeckNW deckNW)
        {
            int nSemanasAtual = sAtual.semanas;
            int valorMes;
            List<EA> lstEA = new List<EA>();

            foreach (EAFPAST eafpast in deckNW.eafpast)
            {
                EA ea = new EA();
                ea.campo1 = eafpast.num;

                valorMes = sAtual.mes - 1;
                if (valorMes == 0) valorMes = 12;
                for (int j = 2; j <= 12; j++)
                {
                    PropertyInfo campoMes = ea.GetType().GetProperty(String.Concat("campo", j.ToString()));
                    PropertyInfo campoEAF = eafpast.GetType().GetProperty(String.Concat("Mes", valorMes.ToString()));
                    campoMes.SetValue(ea, campoEAF.GetValue(eafpast, null).ToString(), null);

                    valorMes--;
                    if (valorMes == 0) valorMes = 12;
                }
                lstEA.Add(ea);
            }

            deck.ea = lstEA;
        }

        public override void escreveLinhaExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol, int numDeck, int difRevDecks, bool escreveTitulo)
        {
            //if (this.campo1.Trim() != "11")
            //{
                rol = 3 + int.Parse(campo1.Trim()) + ((numDeck - 1) * 6);
                for (int i = 1; i <= 12; i++)
                {
                    PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));
                    mWSheet1.SetValue(rol, i+1, block.GetValue(this).ToString());
                }
            //}
        }

        public static void atualizarMensal(Deck novoDeck,Deck deckBase, int[,] ENA, int diferenca)
        {
            List<EA> nListEA = new List<EA>();
            int sub = 0;
            int mesIndice = ENA.GetLength(1) - 1 - diferenca;

            foreach (EA ea in novoDeck.ea)
            {
                //if (ea.campo1 == "5")
                //    nListEA.Add(ea);
                //else
                //{
                    EA eaNew = new EA();
                    eaNew.campo1 = ea.campo1;

                    int numMes = 2;
                    if (deckBase.rev == -1)
                    {
                        eaNew.campo2 = ENA[sub, mesIndice].ToString();
                        numMes++;
                    }
                    else
                    {
                        int mesMAtual = 2;
                        for (int i = mesIndice; i >= 0; i--)
                        {
                            PropertyInfo block = ea.GetType().GetProperty(String.Concat("campo", mesMAtual.ToString()));
                            block.SetValue(eaNew, ENA[sub, i].ToString());
                            numMes++;
                            mesMAtual++;
                        }
                    }

                    int numMes2 = 2;
                    for (int i = numMes; i < 13; i++)
                    {
                        PropertyInfo block = ea.GetType().GetProperty(String.Concat("campo", i.ToString()));
                        PropertyInfo block2 = ea.GetType().GetProperty(String.Concat("campo", numMes2.ToString()));

                        block.SetValue(eaNew, block2.GetValue(ea));
                        numMes2++;
                    }
                    sub++;
                    nListEA.Add(eaNew);
                //}
            }

            novoDeck.ea = nListEA;
        }
    }
}
