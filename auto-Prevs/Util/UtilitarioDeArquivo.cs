using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CapturaNW.Util
{
    public static class UtilitarioDeArquivo
    {
        public static string getExtension(string arq)
        {
            string ext = arq.Substring(arq.LastIndexOf('.'), arq.Length);
            return ext;
        }


        /// <summary>
        /// Le todas as linhas do arquivo e retorna a linha requerida.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="linenumber"></param>
        /// <returns></returns>
        public static string readLineFromFile(string filePath, int linenumber)
        {
            using (var sr = new StreamReader(filePath))
            {
                for (int i = 1; i < linenumber; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }
    }
}
