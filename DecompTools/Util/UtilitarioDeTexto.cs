using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;


namespace DecompTools.Util
{
    public static class UtilitarioDeTexto
    {
        /// <summary>
        /// Caso o numero nao tenho o numero de casas passado como parametro, adiciona 0 a esqueda.
        /// </summary>
        /// <param name="num">numero a ser verificado</param>
        /// <param name="casas">numero de casa à esquerda que num deveria ter</param>
        /// <returns>Numero num formatado com o numero de 0 correto</returns>
        public static string zeroEsq(int num, int casas)
        {
            string numero = num.ToString();

            if (numero.Length < casas)
                for (int i = numero.Length; i < casas; i++)
                    numero = String.Concat("0", numero);

            return numero;
        }

        /// <summary>
        /// Verifica o numero de numeros apos a virgula e caso seja menor que o numero de casas passado como parametro
        /// completa com 0.
        /// </summary>
        /// <param name="num">numero a ser verificado</param>
        /// <param name="casas">numero de casa à direita que num deveria ter</param>
        /// <returns>Numero num formatado com o numero de 0 correto</returns>
        public static string zeroDir(double num, int casas)
        {
            string numero = num.ToString().Replace(",",".");

            if (numero.IndexOf(".") == -1)
            {
                numero = String.Concat(numero, ".");
                for (int i = 0; i < casas; i++)
                    numero = String.Concat(numero, "0");
            }
            else if (numero.Length - (numero.LastIndexOf(".") + 1) < casas)
                for (int i = (numero.Length - (numero.LastIndexOf("."))); i <= casas; i++)
                    numero = String.Concat(numero, "0");

            return numero;
        }



        /// <summary>
        /// O padrao é concatenar os espaços a esquerda
        /// </summary>
        /// <param name="s"></param>
        /// <param name="casas"></param>
        /// <returns></returns>
        public static string preencheEspacos(string s, int casas)
        {
            return preencheEspacos(s, casas, 0);
        }

        
        /// <summary>
        /// Preenche os espaços. se direcao = 1, espacos a direita
        /// </summary>
        /// <param name="s"></param>
        /// <param name="casas"></param>
        /// <param name="direcao"></param>
        /// <returns></returns>
        public static string preencheEspacos(string s, int casas, int direcao)
        {
            for (int i = s.Length; i < casas; i++)
                if (direcao == 1)
                    s = String.Concat(s, " ");
                else
                    s = String.Concat(" ", s);

            return s;
        }

        /// <summary>
        /// Retorna o numero da semana dentro do mes operativo.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string nomeSubmercado(int i)
        {
            string[] subMercados = new string[] { "", "SUDESTE", "SUL", "NORDESTE", "NORTE" };
            if (i < 5)
                return subMercados[i];
            return null;
        }

        public static int idSubmercado(string submercado) {
            submercado = submercado.Trim().ToUpperInvariant();
            if (submercado.Contains("SUDESTE")) return 1;
            else if (submercado.Contains("SUL")) return 2;
            else if (submercado.Contains("NORDESTE")) return 3;
            else if (submercado.Contains("NORTE")) return 4;
            else return 0;
        }

        /// <summary>
        /// Zera todos os campos dentro do range 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="ini"></param>
        /// <param name="fim"></param>
        public static void zerarDados(Object o, int ini, int fim)
        {
            for (int x = ini; x <= fim; x++)
            {
                PropertyInfo camp = o.GetType().GetProperty("campo" + x.ToString());
                camp.SetValue(o, null, null);
            }
        }

        public static int[,] splitEna(string ENA) {
            string[] ENAlinhas = ENA.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (ENAlinhas.Length == 0) return null;

            int[,] ENAsplit = new int[ENAlinhas.Length, ENAlinhas[0].Replace("\t", " ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length];

            int _sub = 0;
            foreach (string ENAsubmercado in ENAlinhas) {
                int _sem = 0;
                foreach (string ENAsemana in ENAsubmercado.Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) {
                    ENAsplit[_sub, _sem] = int.Parse(ENAsemana.Replace(".", String.Empty));
                    _sem++;
                }
                _sub++;

                //if (_sub > 3)
                //    break;
            }

            return ENAsplit;
        }

        public static decimal[,] splitEnaDecimal(string ENA) {
            string[] ENAlinhas = ENA.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            decimal[,] ENAsplit = new decimal[ENAlinhas.Length, ENAlinhas[0].Replace("\t", " ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length];

            int _sub = 0;
            foreach (string ENAsubmercado in ENAlinhas) {
                int _sem = 0;
                foreach (string ENAsemana in ENAsubmercado.Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) {
                    ENAsplit[_sub, _sem] = decimal.Parse(ENAsemana.Replace(".", ","), NumberStyles.Float);
                    _sem++;
                }
                _sub++;

                //if (_sub > 3)
                //    break;
            }

            return ENAsplit;
        }
    }
}
