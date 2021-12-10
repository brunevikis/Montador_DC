using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemNW {
    public class MODIF : blockModel {
        public virtual int id { get; set; }
        public virtual int NUM_USINA { get; set; }
        public virtual string NOME { get; set; }
        public virtual string MNEMONICO { get; set; }
        public virtual int MES { get; set; }
        public virtual int ANO { get; set; }
        public virtual double VALOR { get; set; }
        public virtual DeckNW deckNW { get; set; }


        public MODIF() {
            pos = new int[] { 8, 5, 5, 8, 38 };
        }

        public override void preencheCampos(string[] s) {
            try {
                this.MNEMONICO = s[1];
                if (s[2] == null) return; this.NUM_USINA = String.Equals(s[2], String.Empty) ? 0 : int.Parse(s[2]);
                this.MES = this.NUM_USINA;
                if (s[3] == null) return; this.ANO = String.Equals(s[3], String.Empty) ? 0 : int.Parse(s[3]);
                this.VALOR = String.IsNullOrWhiteSpace(s[4]) ? 0 : double.Parse(s[4].Replace(".", ","));
                this.NOME = s[5];
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck) {
            List<MODIF> lst = new List<MODIF>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho)) {
                string sLine = "";
                string usinaNome = "";
                int num_usina = 0;

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.StartsWith(" P.CHAVE") && !sLine.Contains("XXXX")) {
                        MODIF c = new MODIF();

                        c.leLinha(sLine);

                        if (c.MNEMONICO == "USINA") {
                            usinaNome = c.NOME;
                            num_usina = c.NUM_USINA;
                        } else {
                            c.NOME = usinaNome;
                            c.NUM_USINA = num_usina;

                            if (c.MNEMONICO == "NUMMAQ")
                                c.VALOR = c.ANO;
                            else if (c.MNEMONICO == "NUMCNJ")
                                c.VALOR = c.MES;
                            else if (c.MNEMONICO == "VAZMIN")
                                c.VALOR = double.Parse(String.Concat(c.MES.ToString(), c.ANO.ToString()));

                            lst.Add(c);
                        }
                    }
                }

                deck.modif = lst;
            }
        }
    }
}
