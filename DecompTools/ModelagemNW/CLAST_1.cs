using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


namespace DecompTools.ModelagemNW {
    public class CLAST_1 : blockModel {
        public virtual int id { get; set; }
        public virtual int Numero { get; set; }
        public virtual string Usina { get; set; }
        public virtual string Combustivel { get; set; }
        public virtual double Custo_1 { get; set; }
        public virtual double Custo_2 { get; set; }
        public virtual double Custo_3 { get; set; }
        public virtual double Custo_4 { get; set; }
        public virtual double Custo_5 { get; set; }
        public virtual DeckNW deckNW { get; set; }

        public CLAST_1() {
            pos = new int[] { 5, 13, 11, 8, 8, 8, 8, 8 };
        }

        public override void preencheCampos(string[] s) {
            try {
                this.Numero = String.Equals(s[1], String.Empty) ? 0 : int.Parse(s[1]);
                this.Usina = s[2];
                this.Combustivel = s[3];
                this.Custo_1 = String.Equals(s[4], String.Empty) ? 0 : double.Parse(s[4].Replace(".", ","));
                this.Custo_2 = String.Equals(s[5], String.Empty) ? 0 : double.Parse(s[5].Replace(".", ","));
                this.Custo_3 = String.Equals(s[6], String.Empty) ? 0 : double.Parse(s[6].Replace(".", ","));
                this.Custo_4 = String.Equals(s[7], String.Empty) ? 0 : double.Parse(s[7].Replace(".", ","));
                this.Custo_5 = String.Equals(s[8], String.Empty) ? 0 : double.Parse(s[8].Replace(".", ","));
            } catch (IndexOutOfRangeException) {
                // Deixar em branco (??)
            } catch (Exception) {
                // Implementar este tratamento de excessão
            }
        }

        public static void leArquivo(string caminho, DeckNW deck) {
            List<CLAST_1> lst_1 = new List<CLAST_1>();
            List<CLAST_2> lst_2 = new List<CLAST_2>();

            //Abertura do arquivo
            using (StreamReader objReader = new StreamReader(caminho)) {
                string sLine = "";

                //Leitura do arquivo
                while (!objReader.EndOfStream) {
                    sLine = objReader.ReadLine();

                    if (sLine != null && sLine != String.Empty && !sLine.Contains("XXX") && !sLine.StartsWith(" 9999") && !sLine.StartsWith(" NUM")) {
                        if (sLine.Length >= 50) {
                            CLAST_1 c1 = new CLAST_1();

                            c1.leLinha(sLine);
                            lst_1.Add(c1);
                        } else {
                            CLAST_2 c2 = new CLAST_2();

                            c2.leLinha(sLine);
                            lst_2.Add(c2);
                        }
                    }
                }

                deck.clast_1 = lst_1;
                deck.clast_2 = lst_2;
            }
        }
    }
}
