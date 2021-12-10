using System;
using CapturaNW.Util;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;

using CapturaNW.Factory;

namespace CapturaNW.Modelagem
{
    public class MANUTT : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual string Empresa { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Usina { get; set; }
        public virtual int Unidade { get; set; }
        public virtual string Inicio { get; set; }
        public virtual int Duracao { get; set; }
        public virtual double Potencia { get; set; }
        public virtual DeckNW deckNW { get; set; }

        //Variaveis sem persistencia no BD
        #region var_data
        public virtual int dia{
            get{
                return int.Parse(this.Inicio.Substring(0, 2));
            }
            set{}
        }

        public virtual int mes{
            get{
                return int.Parse(this.Inicio.Substring(2, 2));
            }
            set { }
        }

        public virtual int ano{
            get{
                return int.Parse(this.Inicio.Substring(4));
            }
            set{}
        }
        #endregion

        public MANUTT()
        {
            pos = new int[] { 13, 7, 13, 6, 9, 4, 10 };
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                this.Empresa = s[1];
                this.Codigo = String.Equals(s[2], String.Empty) ? 0 : int.Parse(s[2]);
                this.Usina = s[3];
                this.Unidade = String.Equals(s[4], String.Empty) ? 0 : int.Parse(s[4]);
                this.Inicio = s[5];
                this.Duracao = String.Equals(s[6], String.Empty) ? 0 : int.Parse(s[6]);
                this.Potencia = String.Equals(s[7], String.Empty) ? 0 : double.Parse(s[7].Replace(".",","));
            }
            catch (IndexOutOfRangeException)
            {
                // Deixar em branco (??)
            }
            catch (Exception)
            {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck)
        {
            List<MANUTT> lst = new List<MANUTT>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.Contains("XXXX") && !sLine.StartsWith("EMPRESA"))
                    {
                        MANUTT m = new MANUTT();
                        m.leLinha(sLine);
                        lst.Add(m);
                    }
                }

                deck.manutt = lst;
            }
        }
    }
}
