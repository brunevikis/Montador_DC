﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC
{
    public class VL : blockModel
    {
        public virtual string campo1 { get; set; }
        public virtual string campo2 { get; set; }
        public virtual string campo3 { get; set; }
        public virtual string campo4 { get; set; }
        public virtual string campo5 { get; set; }
        public virtual string campo6 { get; set; }
        public virtual string campo7 { get; set; }
        



        public VL()
        {
            pos = new int[] { 4, 8, 26, 16, 16, 16, 16 };
            nome = "VL";
        }
        public static void atualizarRVX(Deck deck)
        {
            
        }
    }
}

        