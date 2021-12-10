using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;



namespace DecompTools.ModelagemNW {
    public abstract class blockModel : Model {
        public virtual int[] pos { get; set; }

        public virtual void leLinha(string linha) {
            int i;
            int v = 0;
            string[] guarda = new string[pos.Length + 1];

            for (i = 0; i < pos.Length; i++) {
                if (v + pos[i] <= linha.Length) {
                    guarda[i + 1] = linha.Substring(v, pos[i]).Trim();
                    v = v + pos[i];
                } else if ( v < linha.Length ){
                    guarda[i + 1] = linha.Substring(v, linha.Length - v).Trim();
                    break;
                }
            }
            preencheCampos(guarda);
        }

        public abstract void preencheCampos(string[] s);
    }
}
