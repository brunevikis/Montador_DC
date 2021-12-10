using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using ToolBox;
using System.Threading.Tasks;

namespace DecompTools.ControllerDC {
    
    public class controllerCompara {

        public static string comparar(string caminhoDeck1, string caminhoDeck2) {
            //Blocos que serão comparados.
            string[] blocos = new string[] { "UH", "CT", "DP", "PQ", "IT", "IA", "MP", "MT", "FD", "VI", "QI", "PI", "TI", "EZ", "EA", "ES", "AC", "RHA", "RHV", "RHQ", "RHE" };

            Excel.Application oXL = null;
            Excel.Workbooks mWorkBooks = null;
            Excel.Workbook mWorkBook = null;
            Excel.Sheets mWorkSheets = null;
            Excel.Worksheet mWSheet1 = null;

            //Caminho para o Excel Modelo
            string pathEntrada = "H:\\Middle - Preço\\Estudos\\21 - Comparador DADGERs\\ModeloParcial.xlsx";
            //Caminho para o Excel Preenchido
            //string pathSaida = "H:\\Middle - Preço\\Estudos\\21 - Comparador DADGERs\\Modelo1.xlsx";

            //Carrega as informações de ambos os decks.
            Deck deck1 = controllerCarrega.lerDeck(caminhoDeck1);
            Deck deck2 = controllerCarrega.lerDeck(caminhoDeck2);

            //Linhas apenas para teste, para evitar perca de tempo atribuindo decks para os decks.
            //Deck deck1 = DeckDAO.getAllBlocksbyID(432);
            //Deck deck2 = DeckDAO.getAllBlocksbyID(400); 

            int difRevDecks = deck2.rev - deck1.rev;

            // Iniciando abertura do arquivo excel

            oXL = new Microsoft.Office.Interop.Excel.Application();


            oXL.Visible = false;
            oXL.DisplayAlerts = false;
            oXL.ScreenUpdating = false;

            try {

                if (!File.Exists(pathEntrada))
                    return "Errro: Excel modelo não foi encontrado!";
                mWorkBooks = oXL.Workbooks;
                mWorkBook = mWorkBooks.Open(pathEntrada, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                mWorkSheets = mWorkBook.Worksheets;

                //Ler as informações(nome) das postos hidroeletricas e escrever na tabela PostoNome
                List<CadUsh> cadUsinas = CadUshDAO.GetAll();
                mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("PostoNome");
                int linha = 1;
                foreach (CadUsh posto in cadUsinas) {
                    mWSheet1.SetValue(linha, 1, posto.codUsina);
                    mWSheet1.SetValue(linha++, 2, posto.nomeUsina);
                }

                //Iniciando a comparação bloco-a-bloco entre os dois decks
                foreach (var blocoAtual in blocos) {
                    int rol = 3;
                    //Seleciona a tabela do bloco em questão
                    mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item(blocoAtual);

                    //Caso seja o bloco UH, escreve o titulo de ambos os decks em seus respectivos locais
                    if (blocoAtual == "UH") {
                        mWSheet1.SetValue(2, 4, deck1.te.Trim());
                        mWSheet1.SetValue(2, 7, deck2.te.Trim());
                    }

                    IList deck1Block, deck2Block;

                    //Carrega as informações do bloco em questao de cada deck
                    PropertyInfo blocoAtualTipo = typeof(Deck).GetProperty(blocoAtual.ToLower());
                    deck1Block = (IList)blocoAtualTipo.GetValue(deck1);
                    deck2Block = (IList)blocoAtualTipo.GetValue(deck2);

                    //Cria uma lista de Tuplas que, em cada tupla, vai conter a informação de uma linha do bloco e o numero do deck de origem desta linha
                    List<Tuple<int, blockModel>> tupleList = new List<Tuple<int, blockModel>>();

                    //Preenche a tupla com as informações de ambos os decks
                    if (deck1Block != null)
                        foreach (blockModel linhaAtual in deck1Block) { tupleList.Add(Tuple.Create(1, linhaAtual)); }
                    if (deck2Block != null)
                        foreach (blockModel linhaAtual in deck2Block) { tupleList.Add(Tuple.Create(2, linhaAtual)); }

                    if (tupleList.Count > 0) {
                        //Ordena a lista de tuplas de acordo com as regras do bloco atual.
                        Type tipo = tupleList[0].Item2.GetType();
                        var methodInfo = tipo.GetMethod("tupleOrderBy", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);
                        object[] parameterArray = new object[] { tupleList, difRevDecks };
                        var ordenedTuple = (IOrderedEnumerable<Tuple<int, blockModel>>)methodInfo.Invoke(tupleList, parameterArray);

                        int tipoUsinaAnt = 0;
                        //Com a lista ordenada, escreve as informações no arquivo de comparação
                        foreach (var tupleAtual in ordenedTuple) {
                            bool escreveTitulo = false;
                            if (tipoUsinaAnt >= tupleAtual.Item1 || tipoUsinaAnt == 0) {
                                rol = rol + 1;
                                escreveTitulo = true;
                            }

                            tupleAtual.Item2.escreveLinhaExcel(mWSheet1, rol, tupleAtual.Item1, difRevDecks, escreveTitulo);
                            tipoUsinaAnt = tupleAtual.Item1;
                        }
                    }
                }

                //Mostra o resultado na tela.

                //mWorkBook.SaveAs(pathSaida, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //não salvar o arquivo, pode ter problema de concorrencia, se quiser o usuário salva manualmente

                // Não é necessario fechar o arquivo pois o usuario fica responsavel disto.
                //mWorkBook.Close();   
                //oXL.Quit();

                return "Comparação terminada.";
            } finally {
                if (oXL != null) {
                    oXL.Visible = true;
                    oXL.DisplayAlerts = true;
                    oXL.ScreenUpdating = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWSheet1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkSheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                mWorkBooks = null;
                mWorkBook = null;
                mWorkSheets = null;
                mWSheet1 = null;
                oXL = null;

            }
        }

        public static async Task<string> compararAsync(string caminhoDeck1, string caminhoDeck2) {
            //Blocos que serão comparados.
            string[] blocos = new string[] { "UH", "CT", "DP", "PQ", "IT", "IA", "MP", "MT", "FD", "VI", "QI", "PI", "TI", "EZ", "EA", "ES", "AC", "RHA", "RHV", "RHQ", "RHE" };

            Excel.Application oXL = null;
            Excel.Workbooks mWorkBooks = null;
            Excel.Workbook mWorkBook = null;
            Excel.Sheets mWorkSheets = null;
            Excel.Worksheet mWSheet1 = null;

            //Caminho para o Excel Modelo
            string pathEntrada = "H:\\Middle - Preço\\Estudos\\21 - Comparador DADGERs\\ModeloParcial.xlsx";
            //Caminho para o Excel Preenchido
            //string pathSaida = "H:\\Middle - Preço\\Estudos\\21 - Comparador DADGERs\\Modelo1.xlsx";


            Deck[] decks = await Task.WhenAll(
                Task<Deck>.Factory.StartNew(() => controllerCarrega.lerDeck(caminhoDeck1)),
                Task<Deck>.Factory.StartNew(() => controllerCarrega.lerDeck(caminhoDeck2))
                );

            //Carrega as informações de ambos os decks.
            Deck deck1 = decks[0];
            Deck deck2 = decks[1];

            //Linhas apenas para teste, para evitar perca de tempo atribuindo decks para os decks.
            //Deck deck1 = DeckDAO.getAllBlocksbyID(432);
            //Deck deck2 = DeckDAO.getAllBlocksbyID(400); 

            int difRevDecks = deck2.rev - deck1.rev;

            // Iniciando abertura do arquivo excel

            oXL = new Microsoft.Office.Interop.Excel.Application();

            oXL.Visible = false;
            oXL.DisplayAlerts = false;
            oXL.ScreenUpdating = false;

            try {

                if (!File.Exists(pathEntrada))
                    return "Erro: Excel modelo não foi encontrado!";

                await Task.Factory.StartNew(() => {

                    mWorkBooks = oXL.Workbooks;
                    mWorkBook = mWorkBooks.Open(pathEntrada, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                    mWorkSheets = mWorkBook.Worksheets;

                    //Ler as informações(nome) das postos hidroeletricas e escrever na tabela PostoNome
                    List<CadUsh> cadUsinas = CadUshDAO.GetAll();
                    mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item("PostoNome");
                    int linha = 1;
                    foreach (CadUsh posto in cadUsinas) {
                        mWSheet1.SetValue(linha, 1, posto.codUsina);
                        mWSheet1.SetValue(linha++, 2, posto.nomeUsina);
                    }

                    //Iniciando a comparação bloco-a-bloco entre os dois decks
                    foreach (var blocoAtual in blocos) {
                        WriteBlock(mWorkSheets, deck1, deck2, difRevDecks, blocoAtual);
                    }
                });

                return "Comparação terminada.";
            } finally {
                if (oXL != null) {
                    oXL.Visible = true;
                    oXL.DisplayAlerts = true;
                    oXL.ScreenUpdating = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWSheet1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkSheets);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(mWorkBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                mWorkBooks = null;
                mWorkBook = null;
                mWorkSheets = null;
                mWSheet1 = null;
                oXL = null;

            }
        }

        private static void WriteBlock(Excel.Sheets mWorkSheets, Deck deck1, Deck deck2, int difRevDecks, string blocoAtual) {
            int rol = 3;
            //Seleciona a tabela do bloco em questão
            var mWSheet1 = (Excel.Worksheet)mWorkSheets.get_Item(blocoAtual);

            //Caso seja o bloco UH, escreve o titulo de ambos os decks em seus respectivos locais
            if (blocoAtual == "UH") {
                mWSheet1.SetValue(2, 4, deck1.te.Trim());
                mWSheet1.SetValue(2, 7, deck2.te.Trim());
            }

            IList deck1Block, deck2Block;

            //Carrega as informações do bloco em questao de cada deck
            PropertyInfo blocoAtualTipo = typeof(Deck).GetProperty(blocoAtual.ToLower());
            deck1Block = (IList)blocoAtualTipo.GetValue(deck1);
            deck2Block = (IList)blocoAtualTipo.GetValue(deck2);

            //Cria uma lista de Tuplas que, em cada tupla, vai conter a informação de uma linha do bloco e o numero do deck de origem desta linha
            List<Tuple<int, blockModel>> tupleList = new List<Tuple<int, blockModel>>();

            //Preenche a tupla com as informações de ambos os decks
            if (deck1Block != null)
                foreach (blockModel linhaAtual in deck1Block) { tupleList.Add(Tuple.Create(1, linhaAtual)); }
            if (deck2Block != null)
                foreach (blockModel linhaAtual in deck2Block) { tupleList.Add(Tuple.Create(2, linhaAtual)); }

            if (tupleList.Count > 0) {
                //Ordena a lista de tuplas de acordo com as regras do bloco atual.
                Type tipo = tupleList[0].Item2.GetType();
                var methodInfo = tipo.GetMethod("tupleOrderBy", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);
                object[] parameterArray = new object[] { tupleList, difRevDecks };
                var ordenedTuple = (IOrderedEnumerable<Tuple<int, blockModel>>)methodInfo.Invoke(tupleList, parameterArray);

                int tipoUsinaAnt = 0;
                //Com a lista ordenada, escreve as informações no arquivo de comparação
                foreach (var tupleAtual in ordenedTuple) {
                    bool escreveTitulo = false;
                    if (tipoUsinaAnt >= tupleAtual.Item1 || tipoUsinaAnt == 0) {
                        rol = rol + 1;
                        escreveTitulo = true;
                    }

                    tupleAtual.Item2.escreveLinhaExcel(mWSheet1, rol, tupleAtual.Item1, difRevDecks, escreveTitulo);
                    tipoUsinaAnt = tupleAtual.Item1;
                }
            }
        }
    }
}
