using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DecompTools.ModelagemDC {
    public class Semanas_Patamares {
        public virtual int id { get; set; }
        public virtual string Semana { get; set; }
        public virtual int pesado { get; set; }
        public virtual int medio { get; set; }
        public virtual int leve { get; set; }


        public Semanas_Patamares() { }

        /// <summary>
        /// Cria uma semana patamar dado o numero de dias do tipo1 e do tipo2.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public Semanas_Patamares(int t1, int t2) {
            pesado = t1 * 3;
            medio = t1 * 14 + 5 * t2;
            leve = t1 * 7 + 19 * t2;
        }


        /// <summary>
        /// Dado duas semanas, retorna uma terceira com a soma das duas em cada patamar
        /// e a semana do primeiro parametro.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public static Semanas_Patamares somaSemanas(Semanas_Patamares s1, Semanas_Patamares s2) {
            Semanas_Patamares s3 = new Semanas_Patamares();
            s3.Semana = s1.Semana;
            s3.pesado = s1.pesado + s2.pesado;
            s3.medio = s1.medio + s2.medio;
            s3.leve = s1.leve + s2.leve;

            return s3;
        }

        /// <summary>
        /// Dado uma lista de semanas, soma cada patamar em todas para retornar o total mensal
        /// </summary>
        /// <param name="lstSemanas"></param>
        public static Semanas_Patamares somaSemanas(List<Semanas_Patamares> lstSemanas) {
            Semanas_Patamares sTotal = new Semanas_Patamares();

            foreach (Semanas_Patamares s in lstSemanas) {
                sTotal.pesado += s.pesado;
                sTotal.medio += s.medio;
                sTotal.leve += s.leve;
            }

            return sTotal;
        }
    }
}
