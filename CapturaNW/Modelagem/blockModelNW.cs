using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

using CapturaNW.Util;

namespace CapturaNW.Modelagem
{
    public abstract class blockModelNW : Model
    {
        public virtual int[] pos { get; set; }

        public virtual void leLinha( string linha )
        {
            int i;
            int v = 0;
            string[] guarda = new string[pos.Length+1];

            for( i=0; i<pos.Length; i++ )
            {
                try
                {
                    guarda[i+1] = linha.Substring(v, pos[i]).Trim();
                    v = v + pos[i];
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (v < linha.Length)
                    {
                        guarda[i + 1] = linha.Substring(v, linha.Length - v).Trim();
                        break;
                    }
                }
            }
            preencheCampos(guarda);
        }

        public abstract void preencheCampos(string[] s);
    }
}
