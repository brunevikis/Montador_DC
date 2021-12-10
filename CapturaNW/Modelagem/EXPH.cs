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
    public class EXPH : blockModelNW
    {
        public virtual int id { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Usina { get; set; }
        public virtual string Enchimento { get; set; }
        public virtual int Duracao { get; set; }
        public virtual double Volume { get; set; }
        public virtual string Entrada { get; set; }
        public virtual double Pot { get; set; }
        public virtual int Maquina { get; set; }
        public virtual int Conjunto { get; set; }
        public virtual int Ordena { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public EXPH()
        {
            pos = new int[] { 4, 13, 8, 8, 9, 9, 7, 4, 3 };
        }

        public override void preencheCampos(string[] s)
        {
            try
            {
                this.Codigo = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.Usina = s[2];
                this.Enchimento = s[3];
                this.Duracao = String.Equals(s[4], String.Empty) ? 0 : int.Parse(s[4]);
                this.Volume = String.Equals(s[5], String.Empty) ? 0 : double.Parse(s[5].Replace(".", ","));
                this.Entrada = s[6];
                this.Pot = String.Equals(s[7], String.Empty) ? 0 : double.Parse(s[7].Replace(".",","));
                this.Maquina = String.Equals(s[8], String.Empty) ? 0 : int.Parse(s[8]);
                this.Conjunto = String.Equals(s[9], String.Empty) ? 0 : int.Parse(s[9]);
            }
            catch (IndexOutOfRangeException )
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
            string usina = "";
            int codigo = 0;
            int cont = 0;

            List<EXPH> lst = new List<EXPH>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho))
            {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream)
                {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.Contains("XX/XXXX") && !sLine.Contains("ENCHIMENTO") && !sLine.Contains("INICIO") && !sLine.StartsWith("9999"))
                    {
                        EXPH e = new EXPH();

                        e.leLinha(sLine);

                        if (!String.Equals(e.Usina, String.Empty))
                        {
                            usina = e.Usina;
                            codigo = e.Codigo;
                            cont = 1;
                        }
                        else
                        {
                            e.Usina = usina;
                            e.Codigo = codigo;
                            cont = cont + 1;
                        }
                        e.Ordena = cont;
                        lst.Add(e);
                    }
                }

                deck.exph = lst;
            }
        }
    }
}
