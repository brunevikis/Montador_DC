using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using DecompTools.Util;
using DecompTools.ModelagemNW;
using ToolBox;

namespace DecompTools.ModelagemDC
{

    /// <summary>
    /// Classe abstrata que serve como um modelo para todos os blocos. Implementa a classe Model, e por isto herda as funções
    /// de banco de dados presentes nela.
    /// </summary>
    public abstract class blockModel : Model
    {
        public virtual int id { get; set; }
        public virtual int ordem { get; set; }
        public virtual int linha { get; set; }
        public virtual Deck deck { get; set; }

        public virtual int[] pos { get; set; }
        public virtual string nome { get; set; }

        /// <summary>
        /// Metodo para ler uma linha de um bloco qualquer
        /// </summary>
        /// <param name="linha">A linha do bloco</param>
        public virtual void leLinha(string linha)
        {
            int i;
            int v = 2;

            if (this is Restricoes)
            {
                PropertyInfo block = this.GetType().GetProperty("bloco");
                block.SetValue(this, linha.Substring(0, 2));
                definePos();
            }
            if (linha.Substring(0,2) == "HE" || linha.Substring(0,2) == "CM")
            {
                PropertyInfo block = this.GetType().GetProperty("bloco");
                block.SetValue(this, linha.Substring(0, 2));
                definePos();
            }

            string[] guarda = new string[pos.Length];

            for (i = 0; i < pos.Length; i++)
            {

                if (v + pos[i] <= linha.Length)
                {
                    guarda[i] = linha.Substring(v, pos[i]);
                    if (!(this is AC || this is Restricoes))
                        guarda[i] = guarda[i].Trim();
                    v = v + pos[i];
                }
                else if (v < linha.Length)
                {
                    guarda[i] = linha.Substring(v, linha.Length - v);
                    if (!(this is AC || this is Restricoes))
                        guarda[i] = guarda[i].Trim();
                    break;
                }
            }
            preencheCampos(guarda);
        }

        /// <summary>
        /// Funcao responsavel por atribuir os valores aos seus respectivos campos dentro de um bloco.
        /// </summary>
        /// <param name="s"></param>
        public virtual void preencheCampos(string[] s)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty("campo" + (i + 1).ToString());
                block.SetValue(this, s[i]);
            }
        }

        /// <summary>
        /// Retorna uma string com as informações da linha no formato especifico de seu bloco.
        /// </summary>
        /// <returns></returns>
        public virtual string escreveLinha()
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(nome);

            for (int i = 1; i <= pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;

                if (this is AC || this is Restricoes)
                    linha = linha.Append((string)block.GetValue(this, null));
                else
                    linha = linha.Append(UtilitarioDeTexto.preencheEspacos((string)block.GetValue(this, null), pos[i - 1]));
            }

            return linha.ToString();
        }

        /// <summary>
        /// Funcao utilizada em alguns blocos, os de restricao por exemplo, para definir o valor da var. "pos" de acordo com alguma informacao da linha.
        /// </summary>
        public virtual void definePos() { }

        /// <summary>
        /// Escreve em um arquivo todas as linhas de um bloco especifico. PS: Não funciona para as restrições.
        /// </summary>
        /// <param name="bloco"></param>
        /// <param name="caminho"></param>
        public static void escreveBloco(List<blockModel> bloco, string caminho)
        {
            if (File.Exists(caminho))
                File.Delete(caminho);
            File.Create(caminho).Close();
            using (TextWriter arquivo = File.CreateText(caminho))
            {
                foreach (blockModel linha in bloco)
                {
                    arquivo.WriteLine(linha.escreveLinha());
                }
            }
        }

        public virtual void escreveLinhaExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol, int numDeck, int difRevDecks, bool escreveTitulo)
        {
            int col = this.getTitleLength() + ((this.pos.Length - 1) * (numDeck - 1));
            if ((this is DP || this is PQ) && numDeck == 2) //Blocos com media de valores
                col++;

            if (escreveTitulo) this.escreveTituloExcel(mWSheet1, rol);

            for (int i = 2; i <= pos.Length; i++)
            {
                PropertyInfo block = this.GetType().GetProperty(String.Concat("campo", i.ToString()));

                if (block.GetValue(this, null) == null)
                    break;

                if (numDeck == 2 && (this is MP || this is MT || this is FD || this is TI))
                    mWSheet1.SetValue(rol, col + i - 1 + difRevDecks, block.GetValue(this).ToString());
                else if (this is AC)
                    mWSheet1.SetValue(rol, col + i - 1, block.GetValue(this).ToString().Trim());
                else
                    mWSheet1.SetValue(rol, col + i - 1, block.GetValue(this).ToString());
            }
        }

        public virtual void escreveTituloExcel(Microsoft.Office.Interop.Excel.Worksheet mWSheet1, int rol)
        {
            PropertyInfo blockTitle = this.GetType().GetProperty("campo1");
            mWSheet1.SetValue(rol, 1, blockTitle.GetValue(this).ToString());
        }

        public virtual int getTitleLength() { return 2; }

        public static IOrderedEnumerable<Tuple<int, blockModel>> tupleOrderBy(List<Tuple<int, blockModel>> tupleList, int difRev)
        {
            var usinaProperty = tupleList[0].Item2.GetType().GetProperty("campo1");
            return tupleList.OrderBy(ty => int.Parse(usinaProperty.GetValue(ty.Item2).ToString()));
        }

        public static int ordemRVDif(string campo, int difRev, int numDeck)
        {
            double iCampo = 1;
            string[] campos = campo.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (campos.Length > 0 && double.TryParse(campos[0].Replace(".", ","), out iCampo))
                return (int)(iCampo + ((numDeck - 1) * difRev));
            return 1;
        }

        //public static void atualizarRV0Opcional(Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase)
        //{
        //deck.clone(deckBase, nome);
        //}
    }
}
