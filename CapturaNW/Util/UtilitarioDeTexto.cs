using System;
using System.Collections.Generic;
using System.Text;

namespace CapturaNW.Util
{
    public static class UtilitarioDeTexto
    {
        public static string zeroEsq(int num, int casas)
        {
            string numero = num.ToString();

            if (numero.Length < casas)
                for (int i = numero.Length; i < casas; i++)
                    numero = String.Concat("0", numero);

            return numero;
        }

        //O padrao é concatenar os espaços a esquerda
        public static string preencheEspacos(string s, int casas)
        {
            return preencheEspacos(s, casas, 0);
        }

        // se direcao = 1, espacos a direita,
        // se direcao = 0, espacos a esquerda.
        public static string preencheEspacos(string s, int casas, int direcao)
        {
            for (int i = s.Length; i < casas; i++)
                if (direcao == 1)
                    s = String.Concat(s, " ");
                else
                    s = String.Concat(" ", s);

            return s;
        }

        public static string nomeSubmercado(int i)
        {
            string[] subMercados = new string[] { "", "SUDESTE", "SUL", "NORDESTE", "NORTE" };
            if (i < 5)
                return subMercados[i];
            else if (i == 11)
                return "FICT";
            return null;
        }
    }
}
