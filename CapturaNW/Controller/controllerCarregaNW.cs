using CapturaNW.Factory;
using CapturaNW.Modelagem;
using CapturaNW.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace CapturaNW.Controller
{
    public class controllerCarregaNW
    {

        /// <summary>
        /// Adiciona o deck no banco a partir do caminho passado como parametro
        /// Apos isto, retorna uma string com o id do deck ou em caso de erro uma msg.
        /// </summary>
        /// <param name="caminho">Caminho para o deck Newave</param>
        /// <param name="nome">Nome para o deck a ser carregado</param>
        /// <param name="desc">Descrição para o novo deck</param>
        /// <returns></returns>
        public static string CarregaDeckNW(string caminho, string nome, string desc, bool oficial)
        {
            string msg = "";
            bool erro = false;

            DeckNW deck = new DeckNW();

            string[] arquivos = new string[deck.blocos.Length];

            for (int i = 0; i < deck.blocos.Length; i++)
            {
                if (File.Exists(Path.Combine(caminho, deck.blocos[i])))
                {
                    msg = String.Concat(msg, deck.blocos[i], " : Encontrado\n");
                    arquivos[i] = Path.Combine(caminho, deck.blocos[i]);
                }
                else if (File.Exists(Path.Combine(caminho, deck.blocos[i].ToLower())))
                {
                    msg = String.Concat(msg, deck.blocos[i], " : Encontrado\n");
                    arquivos[i] = Path.Combine(caminho, deck.blocos[i].ToLower());
                }
                else
                {
                    msg = String.Concat(msg, deck.blocos[i], " : Não Encontrado\n");
                    erro = true;
                }
            }

            if (erro == true)
                return msg.Replace("\n", Environment.NewLine);

            deck.nome = nome;
            deck.descricao = desc;

            MODIF.leArquivo(arquivos[11], deck);
            DGER.leArquivo(arquivos[0], deck);
            EXPH.leArquivo(arquivos[1], deck);
            EXPT.leArquivo(arquivos[2], deck);
            EAFPAST.leArquivo(arquivos[3], deck);
            C_ADIC.leArquivo(arquivos[4], deck);
            CLAST_1.leArquivo(arquivos[5], deck);
            MANUTT.leArquivo(arquivos[6], deck);
            TERM.leArquivo(arquivos[7], deck);
            PEQUENAS.leArquivo(arquivos[8], deck);
            CONFT.leArquivo(arquivos[9], deck);
            PAT_CARGA.leArquivo(arquivos[10], deck);

            if (oficial)
            {
                DeckNW ex_oficial = DeckNWDAO.getDeckOficialByMonth(deck.mes, deck.ano);
                if (ex_oficial != null)
                {
                    ex_oficial.oficial = 0;
                    ex_oficial.save();
                }

                deck.oficial = 1;
            }

            deck.save();
            return deck.id.ToString();
        }
    }
}
