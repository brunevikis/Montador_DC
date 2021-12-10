using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor;
using Executor.ssh;
using System.Threading;
using System.IO;

namespace previvazLinux {



    class Program {

        static bool ok = false;
        static void Main(string[] args) {

            //var caminho = args[0];
            var caminhos = args;

            var locks = caminhos.Select(c=>
                Path.Combine(c, "lock")
                );
            

            //var lockFile = Path.Combine(caminho, "lock");

           var comm = new LinuxQueue.CommItem() {
                Command = @"/home/marco/PrevisaoPLD/shared/previvaz/previvaz2.sh",
                CommandName = "pr" + DateTime.Now.ToString("yyyyMMddHHmmssss"),
                Cluster = new LinuxQueue.Cluster() { Alias = "Auto", Host = "" },
                IgnoreQueue = true,
                WorkingDirectory = caminhos[0],
                User = "auto"
            };



                   
                   

            
            



                            await controller.EnqueueAsync(comm); 


            /*
            var con = new Node11Wrapper();
            con.OnReceive += new RcvdData(p_objConnection_OnReceive);

            if (con.Connect()) {

                foreach (var caminho in caminhos) {
                    var lockFile = Path.Combine(caminho, "lock");
                    var caminhoL = caminho.Replace(@"\", "/").Replace("L:", "/home/marco/PrevisaoPLD");
                    caminhoL = "\"" + caminhoL + "\"";

                    Task.Delay(2000).Wait();

                    var commando = "/home/marco/PrevisaoPLD/shared/previvaz/previvaz.sh " + caminhoL + "\n";

                    var t = Task.Factory.StartNew(() => con.Send(commando));

                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
                while (locks.Any(l=> File.Exists(l))) {
                    Thread.Sleep(1000);
                }

            }
             * */

            while (locks.Any(l => File.Exists(l))) {
                Thread.Sleep(1000);
            }

        }

        static void p_objConnection_OnReceive(object sender, string s) {
            Console.WriteLine(s);
        }
    }
}
