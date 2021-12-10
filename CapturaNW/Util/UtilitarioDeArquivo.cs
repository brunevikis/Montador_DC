using System;
using System.Collections.Generic;
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
    }
}
