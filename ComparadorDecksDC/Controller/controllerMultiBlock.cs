using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ComparadorDecksDC.Controller
{
    class controllerMultiBlock
    {
        /// <summary>
        /// Escreve os decks intermediarios entre o deckPre e o deckOficial, trocando os blocos pré definidos um a um(Jack).
        /// </summary>
        /// <param name="pathDeckPre">caminho do deckPre(Base)</param>
        /// <param name="pathDeckOficial">caminho do deckOficial(Destino)</param>
        /// <param name="pathSaida">caminho para onde os decks intermediarios serão escritos</param>
        /// <param name="bgw">backgroundworker, para atualizar o status do processo na tela</param>
        /// <returns>Mensagem se o processo ocorreu bem ou o erro</returns>
        public string MultiBlock(string pathDeckPre, string pathDeckOficial, string pathSaida, BackgroundWorker bgw = null)
        {
            if (bgw != null)
                bgw.ReportProgress(Convert.ToInt32(25));

            string[][] blocos = new string[12][];
            
            for (int x = 0; x < blocos.Length; x++) 
            {
                if( x == 7 || x == 9)
                    blocos[x] = new string[2];
                else
                blocos[x] = new string[1];
            }

            //Blocos pré definidos para serem atualizados um a um do deckPre para o deckOficial.
            blocos[0][0] = "UH";
            blocos[1][0] = "CT";
            blocos[2][0] = "DP";
            blocos[3][0] = "RHE"; 
            blocos[4][0] = "RHA";  
            blocos[5][0] = "RHQ"; 
            blocos[6][0] = "RHV";
            blocos[7][0] = "VI"; blocos[7][1] = "QI";
            blocos[8][0] = "IA";
            blocos[9][0] = "MP"; blocos[9][1] = "MT";
            blocos[10][0] = "FD";
            blocos[11][0] = "AC";

            if (bgw != null)
                bgw.ReportProgress(Convert.ToInt32(50));

            Deck deckPre = controllerCarrega.lerDeck(pathDeckPre);
            Deck deckOficial = controllerCarrega.lerDeck(pathDeckOficial);

            int numArquivo = 1;
            string arquivoNome = string.Concat("dadger[NUM_ARQUIVO].rv", deckPre.rev.ToString());

            foreach( string[] listBlocos in blocos)
            {
                foreach( string blocoAtual in listBlocos)
                {
                    PropertyInfo block = deckPre.GetType().GetProperty(blocoAtual.ToLower());
                    block.SetValue( deckPre, block.GetValue( deckOficial, null), null);
                }
                deckPre.escreveDeck(pathSaida, arquivoNome.Replace("[NUM_ARQUIVO]", numArquivo++.ToString()));
            }

            deckOficial.escreveDeck(pathSaida, arquivoNome.Replace("[NUM_ARQUIVO]", numArquivo++.ToString()));

            if (bgw != null)
                bgw.ReportProgress(Convert.ToInt32(100));

            return "Realizado com sucesso!";
        }
    }
}
