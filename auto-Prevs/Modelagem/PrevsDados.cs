using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;
using AutoPrevs.Util;

namespace AutoPrevs.Modelagem
{
    public class PrevsDados : Model
    {
        public virtual int id { get; set; }
        public virtual Prevs prevs { get; set; }
        public virtual int posto { get; set; }
        public virtual double sem1 { get; set; }
        public virtual double sem2 { get; set; }
        public virtual double sem3 { get; set; }
        public virtual double sem4 { get; set; }
        public virtual double sem5 { get; set; }
        public virtual double sem6 { get; set; }


        /// <summary>
        /// Retorna uma string com as informações da linha no formato do Prevs
        /// </summary>
        /// <returns></returns>
        public virtual string escreveLinha(int numLinha)
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(UtilitarioDeTexto.preencheEspacos(numLinha.ToString(), 6));
            linha.Append(UtilitarioDeTexto.preencheEspacos(this.posto.ToString(), 5));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem1, 0)).ToString(), 10));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem2, 0)).ToString(), 10));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem3, 0)).ToString(), 10));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem4, 0)).ToString(), 10));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem5, 0)).ToString(), 10));
            linha.Append(UtilitarioDeTexto.preencheEspacos((Math.Round(this.sem6, 0)).ToString(), 10));

            //CASO QUEIRA IMPRIMIR O PREVS SEM ARREDONDAR, COMENTAR AS LINHAS DE CIMA E DESCOMENTAR AS DE BAIXO.
            //linha.Append(numLinha.ToString()); linha.Append(" ");
            //linha.Append(this.posto.ToString()); linha.Append(" ");
            //linha.Append(String.Concat(this.sem1.ToString(), " "));
            //linha.Append(String.Concat(this.sem2.ToString(), " "));
            //linha.Append(String.Concat(this.sem3.ToString(), " "));
            //linha.Append(String.Concat(this.sem4.ToString(), " "));
            //linha.Append(String.Concat(this.sem5.ToString(), " "));
            //linha.Append(String.Concat(this.sem6.ToString(), " "));

            return linha.ToString();
        }

        // Area em que é explicitada todas as sobrecargas de operados do objeto PREVS.
        // É possivel multiplicar uma linha do prevs por outra. Neste caso, os valores das semanas serão multiplicados em conjunto. 
        // Ex. sem1(prevsNovo) = sem1(prevs1) * sem1(prevs2)
        // Tambem é possivel multiplicar esta linha por um escalar unico ou um vetor de 6 posições. No primeiro caso, será considerada
        // uma multiplicação por escalar, fazendo todas as semanas da linha ser multiplicada por este escalar. Já no segundo caso, cada semana
        // será multiplicada pelo numero na posição relativa do vetor (ex: semana1 * vetor[0] {primeira semana * primeira posição do vetor})
        // Em todos os casos, uma nova linha será gerada, mantendo todos os outros envolvidos intactos.
        // As 4 operações basicas estão englobadas. (*, /, +, -)

        #region sobrecargaOperadores
        public static PrevsDados operator *(PrevsDados p1, PrevsDados p2)
        {
            PrevsDados pResult = new PrevsDados();

            if (p1.posto == p2.posto)
            {
                pResult.posto = p1.posto;
            }

            pResult.sem1 = p1.sem1 * p2.sem1;
            pResult.sem2 = p1.sem2 * p2.sem2;
            pResult.sem3 = p1.sem3 * p2.sem3;
            pResult.sem4 = p1.sem4 * p2.sem4;
            pResult.sem5 = p1.sem5 * p2.sem5;
            pResult.sem6 = p1.sem6 * p2.sem6;

            return pResult;
        }

        public static PrevsDados operator /(PrevsDados p1, PrevsDados p2)
        {
            PrevsDados pResult = new PrevsDados();

            if (p1.posto == p2.posto)
            {
                pResult.posto = p1.posto;
            }

            pResult.sem1 = p1.sem1 / p2.sem1;
            pResult.sem2 = p1.sem2 / p2.sem2;
            pResult.sem3 = p1.sem3 / p2.sem3;
            pResult.sem4 = p1.sem4 / p2.sem4;
            pResult.sem5 = p1.sem5 / p2.sem5;
            pResult.sem6 = p1.sem6 / p2.sem6;

            return pResult;
        }

        public static PrevsDados operator +(PrevsDados p1, PrevsDados p2)
        {
            PrevsDados pResult = new PrevsDados();

            if (p1.posto == p2.posto)
            {
                pResult.posto = p1.posto;
            }

            pResult.sem1 = p1.sem1 + p2.sem1;
            pResult.sem2 = p1.sem2 + p2.sem2;
            pResult.sem3 = p1.sem3 + p2.sem3;
            pResult.sem4 = p1.sem4 + p2.sem4;
            pResult.sem5 = p1.sem5 + p2.sem5;
            pResult.sem6 = p1.sem6 + p2.sem6;

            return pResult;
        }

        public static PrevsDados operator -(PrevsDados p1, PrevsDados p2)
        {
            PrevsDados pResult = new PrevsDados();

            if (p1.posto == p2.posto)
            {
                pResult.posto = p1.posto;
            }

            pResult.sem1 = p1.sem1 - p2.sem1;
            pResult.sem2 = p1.sem2 - p2.sem2;
            pResult.sem3 = p1.sem3 - p2.sem3;
            pResult.sem4 = p1.sem4 - p2.sem4;
            pResult.sem5 = p1.sem5 - p2.sem5;
            pResult.sem6 = p1.sem6 - p2.sem6;

            return pResult;
        }

        //Por escalar unico
        public static PrevsDados operator *(PrevsDados p1, double escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 * escalar);
            pResult.sem2 = (p1.sem2 * escalar);
            pResult.sem3 = (p1.sem3 * escalar);
            pResult.sem4 = (p1.sem4 * escalar);
            pResult.sem5 = (p1.sem5 * escalar);
            pResult.sem6 = (p1.sem6 * escalar);

            return pResult;
        }

        public static PrevsDados operator *(double escalar, PrevsDados p1)
        {
            return p1 * escalar;
        }

        public static PrevsDados operator /(PrevsDados p1, double escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 / escalar);
            pResult.sem2 = (p1.sem2 / escalar);
            pResult.sem3 = (p1.sem3 / escalar);
            pResult.sem4 = (p1.sem4 / escalar);
            pResult.sem5 = (p1.sem5 / escalar);
            pResult.sem6 = (p1.sem6 / escalar);

            return pResult;
        }

        public static PrevsDados operator /(double escalar, PrevsDados p1)
        {
            return p1 / escalar;
        }

        public static PrevsDados operator +(PrevsDados p1, double escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 + escalar);
            pResult.sem2 = (p1.sem2 + escalar);
            pResult.sem3 = (p1.sem3 + escalar);
            pResult.sem4 = (p1.sem4 + escalar);
            pResult.sem5 = (p1.sem5 + escalar);
            pResult.sem6 = (p1.sem6 + escalar);

            return pResult;
        }

        public static PrevsDados operator +(double escalar, PrevsDados p1)
        {
            return p1 + escalar;
        }

        public static PrevsDados operator -(PrevsDados p1, double escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 - escalar);
            pResult.sem2 = (p1.sem2 - escalar);
            pResult.sem3 = (p1.sem3 - escalar);
            pResult.sem4 = (p1.sem4 - escalar);
            pResult.sem5 = (p1.sem5 - escalar);
            pResult.sem6 = (p1.sem6 - escalar);

            return pResult;
        }

        public static PrevsDados operator -(double escalar, PrevsDados p1)
        {
            return p1 - escalar;
        }

        //Por multiEscalar
        public static PrevsDados operator *(PrevsDados p1, double[] escalar)
        {
            PrevsDados pResult = new PrevsDados();


            var ps1 = (p1.sem1 * escalar[0]);
            var ps2 = (p1.sem2 * escalar[1]);
            var ps3 = (p1.sem3 * escalar[2]);
            var ps4 = (p1.sem4 * escalar[3]);
            var ps5 = (p1.sem5 * escalar[4]);
            var ps6 = (p1.sem6 * escalar[5]);

            pResult.sem1 = ps1 >= 1 ? ps1 : 1;
            pResult.sem2 = ps2 >= 1 ? ps2 : 1;
            pResult.sem3 = ps3 >= 1 ? ps3 : 1;
            pResult.sem4 = ps4 >= 1 ? ps4 : 1;
            pResult.sem5 = ps5 >= 1 ? ps5 : 1;
            pResult.sem6 = ps6 >= 1 ? ps6 : 1;

            return pResult;
        }

        public static PrevsDados operator /(PrevsDados p1, double[] escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 / escalar[0]);
            pResult.sem2 = (p1.sem2 / escalar[1]);
            pResult.sem3 = (p1.sem3 / escalar[2]);
            pResult.sem4 = (p1.sem4 / escalar[3]);
            pResult.sem5 = (p1.sem5 / escalar[4]);
            pResult.sem6 = (p1.sem6 / escalar[5]);

            return pResult;
        }

        public static PrevsDados operator +(PrevsDados p1, double[] escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 + escalar[0]);
            pResult.sem2 = (p1.sem2 + escalar[1]);
            pResult.sem3 = (p1.sem3 + escalar[2]);
            pResult.sem4 = (p1.sem4 + escalar[3]);
            pResult.sem5 = (p1.sem5 + escalar[4]);
            pResult.sem6 = (p1.sem6 + escalar[5]);

            return pResult;
        }

        public static PrevsDados operator -(PrevsDados p1, double[] escalar)
        {
            PrevsDados pResult = new PrevsDados();

            pResult.sem1 = (p1.sem1 - escalar[0]);
            pResult.sem2 = (p1.sem2 - escalar[1]);
            pResult.sem3 = (p1.sem3 - escalar[2]);
            pResult.sem4 = (p1.sem4 - escalar[3]);
            pResult.sem5 = (p1.sem5 - escalar[4]);
            pResult.sem6 = (p1.sem6 - escalar[5]);

            return pResult;
        }

        #endregion

        /// <summary>
        /// Verifica em cada semana do prevs se o valor X é menor que a vazão, e retorna o menor valor.
        /// </summary>
        /// <param name="p1">Linha do prevs</param>
        /// <param name="x">valor X a ser comparado</param>
        /// <returns>Linha do prevs com os menores valores semanais entre a vazão da semana e X </returns>
        public static PrevsDados minValue(PrevsDados p1, double x)
        {
            PrevsDados pMin = new PrevsDados();

            pMin.posto = p1.posto;
            pMin.sem1 = (double)p1.sem1 < x ? p1.sem1 : x;
            pMin.sem2 = (double)p1.sem2 < x ? p1.sem2 : x;
            pMin.sem3 = (double)p1.sem3 < x ? p1.sem3 : x;
            pMin.sem4 = (double)p1.sem4 < x ? p1.sem4 : x;
            pMin.sem5 = (double)p1.sem5 < x ? p1.sem5 : x;
            pMin.sem6 = (double)p1.sem6 < x ? p1.sem6 : x;

            return pMin;
        }

        /// <summary>
        /// Idem a anterior, porem retorna o maior valor
        /// </summary>
        /// <param name="p1">Linha do prevs</param>
        /// <param name="x">valor X a ser comparado</param>
        /// <returns>Linha do prevs com os maiores valores semanais entre a vazão da semana e X </returns>
        public static PrevsDados maxValue(PrevsDados p1, double x)
        {
            PrevsDados pMax = new PrevsDados();

            pMax.posto = p1.posto;
            pMax.sem1 = (double)p1.sem1 > x ? p1.sem1 : x;
            pMax.sem2 = (double)p1.sem2 > x ? p1.sem2 : x;
            pMax.sem3 = (double)p1.sem3 > x ? p1.sem3 : x;
            pMax.sem4 = (double)p1.sem4 > x ? p1.sem4 : x;
            pMax.sem5 = (double)p1.sem5 > x ? p1.sem5 : x;
            pMax.sem6 = (double)p1.sem6 > x ? p1.sem6 : x;

            return pMax;
        }
    }
}
