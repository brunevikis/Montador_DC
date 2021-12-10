using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace DecompTools.Util
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

        public static string GetMD5HashFromFile(string fileName) {
            using (var md5 = System.Security.Cryptography.MD5.Create()) {
                using (var stream = File.OpenRead(fileName)) {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}
