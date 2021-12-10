using System;
using CapturaNW.Util;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using CapturaNW.Factory;

namespace CapturaNW.Modelagem
{
    public class DESVAGUA : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual int Ano { get; set; }
        public virtual int Usina { get; set; }
        public virtual double Mes1 { get; set; }
        public virtual double Mes2 { get; set; }
        public virtual double Mes3 { get; set; }
        public virtual double Mes4 { get; set; }
        public virtual double Mes5 { get; set; }
        public virtual double Mes6 { get; set; }
        public virtual double Mes7 { get; set; }
        public virtual double Mes8 { get; set; }
        public virtual double Mes9 { get; set; }
        public virtual double Mes10 { get; set; }
        public virtual double Mes11 { get; set; }
        public virtual double Mes12 { get; set; }
        public virtual int Flag { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public DESVAGUA()
        {
            pos = new int[] { 5, 8, 9, 10, 38, 3 };
        }

        public override void preencheCampos(string[] s)
        {

        }
    }
}
