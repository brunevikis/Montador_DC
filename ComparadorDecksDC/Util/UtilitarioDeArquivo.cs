using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Util
{
    public static class UtilitarioDeArquivo
    {
        /// <summary>
        /// Retorna a extensao do arquivo.
        /// </summary>
        /// <param name="arq"></param>
        /// <returns>extensao do arquivo no formato string</returns>
        public static string getExtension(string arq)
        {
            string ext = arq.Substring(arq.LastIndexOf('.'), arq.Length);
            return ext;
        }
    }
}
