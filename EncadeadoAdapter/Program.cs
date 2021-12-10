using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ComparadorDecksDC.Controller;
using ComparadorDecksDC.Modelagem;
using ComparadorDecksDC.Factory;
using Renci.SshNet;
using System.IO;

namespace EncadeadoAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0 || args.Count() > 1 )
            {
                Console.Write("Erro nos parametros");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(args[0]);                                                                           // Carregando o arquivo XML do encadeado

            string sLocalSaida = xmlDoc.GetElementsByTagName("LocalSaida")[0].InnerText;                    // Local de saida dos decks de newave
            string sCaminhoDeckBase = xmlDoc.GetElementsByTagName("DeckBaseDC")[0].InnerText;               // Local do deck base do decomp
            string sColetaniaArquivos = xmlDoc.GetElementsByTagName("ColetaniaArquivos")[0].InnerText;      // Local da coletania de arquivos para rodar o decomp

            //rodaGevazp("/home/marco/PrevisaoPLD/alexandre/vaz/win");
            rodaDecomp("/home/marco/PrevisaoPLD/decomp/10_2013/rv0_ccee_teste");

            //int idDCBase;
            //if (!int.TryParse(sCaminhoDeckBase, out idDCBase))
            //{
            //    Console.Write("Deck deve ser informado como id");                                           // TODO: Aceitar um dadger, não apenas do banco
            //    return;
            //}

            //controllerRV0 _cRV0 = new controllerRV0();                                                      // Gerar todos os dadgers
            //_cRV0.gerarMultiRV0( idDCBase, sLocalSaida);

        }

        static bool rodaGevazp( string sCaminho)
        {
            AuthenticationMethod au = new PasswordAuthenticationMethod("marco", "#alfa9876");
            ConnectionInfo ci = new ConnectionInfo("192.168.0.11", "marco", au);
            bool ret = true;

            using (var ssh = new SshClient(ci))
            {
                ssh.Connect();
                //string exec = "cd [$PATH]; /home/marco/PrevisaoPLD/shared/bin/vaz 1>out.xxxx 2>&1;".Replace("[$PATH]", sCaminho);
                string exec = "cd [$PATH]; vaz 0;".Replace("[$PATH]", sCaminho);
                var cmd = ssh.CreateCommand(exec);
                cmd.Execute();

                Console.Write(cmd.Result);
                Console.WriteLine("-------->>>>>>>>>>>>>>>>>>> ERROS <<<<<<<<<<<<<<<<<<<<<<<<-----------------");
                Console.Write(cmd.Error);

                // ----------->>>>>>>>>>>Rodar de forma Assincrona. <<<<<<-----------------

                //var asynch = cmd.BeginExecute(delegate(IAsyncResult ar)
                //{
                //    Console.WriteLine("Finished.");
                //}, null);
                //var reader = new StreamReader(cmd.OutputStream);
                //while (!asynch.IsCompleted)
                //{
                //    var result = reader.ReadToEnd();
                //    if (string.IsNullOrEmpty(result))
                //        continue;
                //    Console.Write(result);
                //}
                //cmd.EndExecute(asynch);
            }

            return ret;
        }

        static bool rodaDecomp(string sCaminho)
        {
            AuthenticationMethod au = new PasswordAuthenticationMethod("root", "#cpas9876");
            ConnectionInfo ci = new ConnectionInfo("192.168.0.11", "root", au);
            bool ret = true;

            using (var ssh = new SshClient(ci))
            {
                ssh.Connect();
                //string exec = "cd [$PATH]; /home/marco/PrevisaoPLD/shared/bin/vaz 1>out.xxxx 2>&1;".Replace("[$PATH]", sCaminho);
                string exec = "ssh linux; su marco; cd [$PATH]; decomp192 0;".Replace("[$PATH]", sCaminho);
                var cmd = ssh.CreateCommand(exec);

                // ----------->>>>>>>>>>>Rodar de forma Assincrona. <<<<<<-----------------

                var asynch = cmd.BeginExecute(delegate(IAsyncResult ar)
                {
                    Console.WriteLine("Finished.");
                }, null);
                var reader = new StreamReader(cmd.OutputStream);
                while (!asynch.IsCompleted)
                {
                    var result = reader.ReadToEnd();
                    if (string.IsNullOrEmpty(result))
                        continue;
                    Console.Write(result);
                }
                cmd.EndExecute(asynch);
            }

            return ret;
        }
    }
}
