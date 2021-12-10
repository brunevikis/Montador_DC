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
    public class CLAST_2 : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual int Numero { get; set; }
        public virtual double Custo_6 { get; set; }
        public virtual int Mes_1 { get; set; }
        public virtual int Ano_1 { get; set; }
        public virtual int Mes_2 { get; set; }
        public virtual int Ano_2 { get; set; }
        public virtual string Usina { get; set; }
        public virtual DeckNW deckNW { get; set; }
        
        public CLAST_2()
        {
            pos = new int[] { 5, 10, 4, 5, 4, 5, 14};
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                this.Numero = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.Custo_6 = String.Equals(s[2], String.Empty) ? 0 : double.Parse(s[2].Replace(".",","));
                this.Mes_1 = String.Equals(s[3], String.Empty) ? 0 : int.Parse(s[3]);
                this.Ano_1 = String.Equals(s[4], String.Empty) ? 0 : int.Parse(s[4]);
                this.Mes_2 = String.Equals(s[5], String.Empty) ? 0 : int.Parse(s[5]);
                this.Ano_2 = String.Equals(s[6], String.Empty) ? 0 : int.Parse(s[6]);
                this.Usina = s[7];
            }
            catch (IndexOutOfRangeException)
            {
                // Deixar em branco (??)
            }
            catch (Exception)
            {
                // Implementar este tratamento de excessão
            }
        }
    }
}
