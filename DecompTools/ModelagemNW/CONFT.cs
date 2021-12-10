using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


namespace DecompTools.ModelagemNW {
    public class CONFT : blockModel {
        public virtual int id { get; set; }
        public virtual int NUM { get; set; }
        public virtual string NOME { get; set; }
        public virtual int SU { get; set; }
        public virtual string EXIS { get; set; }
        public virtual int CLASSE { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public CONFT() {
            pos = new int[] { 5, 13, 7, 7, 7 };
        }

        public override void preencheCampos(string[] s) {
            try {
                this.NUM = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.NOME = s[2];
                this.SU = String.Equals(s[3], String.Empty) ? 0 : int.Parse(s[3]);
                this.EXIS = s[4];
                this.CLASSE = String.Equals(s[5], String.Empty) ? 0 : int.Parse(s[5]);
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck) {
            List<CONFT> lst = new List<CONFT>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho)) {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.StartsWith(" NUM") && !sLine.Contains("XXXX")) {
                        CONFT c = new CONFT();

                        c.leLinha(sLine);
                        lst.Add(c);
                    }
                }

                deck.conft = lst;
            }
        }
    }
}
