using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


namespace DecompTools.ModelagemNW {
    public class DGER : blockModel {
        public virtual int id { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Valor { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public DGER() {
            pos = new int[] { 21, 4 };
        }

        public override void preencheCampos(string[] s) {
            this.Titulo = s[1];
            this.Valor = s[2];
        }

        public static void leArquivo(string caminho, DeckNW deck) {
            List<DGER> lst = new List<DGER>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho)) {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty) {
                        if (sLine.StartsWith("MES INICIO DO ESTUDO"))
                            deck.mes = int.Parse(sLine.Substring(21, 4).Trim());
                        if (sLine.StartsWith("ANO INICIO DO ESTUDO"))
                            deck.ano = int.Parse(sLine.Substring(21, 4).Trim());

                        DGER d = new DGER();

                        d.leLinha(sLine);
                        lst.Add(d);
                    }
                }

                deck.dger = lst;
            }
        }
    }
}
