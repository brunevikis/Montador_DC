using DecompTools.FactoryPrevs;
using DecompTools.ModelagemPrevs;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DecompTools.ControllerPrevs
{
    public class controllerPrevivaz
    {
        //protected static string caminhoPrevivaz = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, "Previvaz"), "previvaz.exe");

        static object objLock = new object();

        static void RunPrevivaz(Action<Postos, string> previvaz, string caminhoDestinoPrevivaz, Postos p, Semanas_Ano semanaAtual, Dictionary<int, Dictionary<string, string>> strDat = null)
        {
            string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());

            var ok = false;
            var execute = inpDatPorPosto(p, semanaAtual, caminhoPosto, strDat);

            try
            {
                if (execute) previvaz(p, caminhoPosto);
                PosPrevivaz(p, semanaAtual, caminhoPosto);
                ok = true;
            }
            catch { }
            if (!ok)
            {
                previvaz(p, caminhoPosto);
                PosPrevivaz(p, semanaAtual, caminhoPosto);
                ok = true;
            }

        }

        /// <summary>
        /// Comanda o processo de criar os arquivos e executar o previvaz
        /// </summary>
        /// <param name="postosList">Lista com os postos</param>
        /// <param name="caminhoDestino">Caminho para qual os arquivos inp/dat serão escritos</param>
        /// <param name="semanaAtual">Semana atual do novo prevs</param>
        public static void InpDatPrevivaz(IList<Postos> postosList, string caminhoDestino, Semanas_Ano semanaAtual, Dictionary<int, Dictionary<string, string>> strDat = null)
        {
            var newPostList = postosList.Where(p => p.tipo == 0 || p.tipo == 1 || p.tipo == 2);
            int numPostos = postosList.Count(p => p.tipo == 0 || p.tipo == 1 || p.tipo == 2);
            string caminhoDestinoPrevivaz = Path.Combine(caminhoDestino, "arq_previvaz");

            try
            {

                var postosIncrementais = new Dictionary<int, int[]>(){ // <num posto, { postos montantes ... } >
                    { 34, new int[] {18, 33, 99, 241, 261}},
                    { 245, new int[] {34, 243}},
                    { 246, new int[] {245}},
                    { 266, new int[] {63, 246}},

                    { 239, new int[] {237 } },
                    { 242, new int[] {239 } },



                };

                foreach (var pn in postosIncrementais.OrderByDescending(x => x.Key))
                {
                    var p = newPostList.Where(x => x.numero == pn.Key).First();
                    foreach (var pMn in pn.Value)
                    {

                        var pM = postosList.Where(x => x.numero == pMn).First();

                        if (p.prevs_saida.sem1 > pM.prevs_saida.sem1) p.prevs_saida.sem1 -= pM.prevs_saida.sem1;
                        if (p.prevs_saida.sem2 > pM.prevs_saida.sem2) p.prevs_saida.sem2 -= pM.prevs_saida.sem2;
                        if (p.prevs_saida.sem3 > pM.prevs_saida.sem3) p.prevs_saida.sem3 -= pM.prevs_saida.sem3;
                        if (p.prevs_saida.sem4 > pM.prevs_saida.sem4) p.prevs_saida.sem4 -= pM.prevs_saida.sem4;
                        if (p.prevs_saida.sem5 > pM.prevs_saida.sem5) p.prevs_saida.sem5 -= pM.prevs_saida.sem5;
                        if (p.prevs_saida.sem6 > pM.prevs_saida.sem6) p.prevs_saida.sem6 -= pM.prevs_saida.sem6;

                    }
                }

                var parallelism = 4;

                newPostList
                        .ToList().ForEach(p =>
                        {
                            string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());
                            inpDatPorPosto(p, semanaAtual, caminhoPosto, strDat);
                        });

                if (caminhoDestinoPrevivaz.StartsWith("L:"))
                {

                    LinuxQueue.QueueFolders.RegisterFolder(@"L:\cpas_ctl_common\");

                    RodaPrevivazLinux(caminhoDestinoPrevivaz);

                }
                else
                {
                    var result = Parallel.ForEach(newPostList
                        , new ParallelOptions { MaxDegreeOfParallelism = parallelism }, p =>
                        {
                            try
                            {
                                Console.WriteLine("Processing {0} on thread {1}", p.numero, Thread.CurrentThread.ManagedThreadId);

                                string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());
                                RodaPrevivaz(p, caminhoPosto);


                            }
                            catch (Exception ex) { throw new Exception("Problema no previvaz do posto: " + p.numero.ToString(), ex); }
                        }
                    );
                }
                List<string> posErrors = new List<string>();

                newPostList
                       .ToList().ForEach(p =>
                       {
                           string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());
                           try
                           {
                               PosPrevivaz(p, semanaAtual, caminhoPosto);
                           }
                           catch
                           {
                               posErrors.Add("Problema no previvaz do posto: " + p.numero.ToString());
                           }
                       });

                if (posErrors.Count > 0) throw new Exception(string.Join("\r\n", posErrors));

                postosIncrementais.ToList().ForEach(pn =>
                {
                    var p = newPostList.Where(x => x.numero == pn.Key).First();

                    foreach (var pMn in pn.Value)
                    {

                        var pM = postosList.Where(x => x.numero == pMn).First();

                        p.prevs_saida.sem1 += pM.prevs_saida.sem1;
                        p.prevs_saida.sem2 += pM.prevs_saida.sem2;
                        p.prevs_saida.sem3 += pM.prevs_saida.sem3;
                        p.prevs_saida.sem4 += pM.prevs_saida.sem4;
                        p.prevs_saida.sem5 += pM.prevs_saida.sem5;
                        p.prevs_saida.sem6 += pM.prevs_saida.sem6;
                    }
                });

            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Escreve os arquivos CASO.DAT, #.inp e #_str.DAT para o posto #.
        /// </summary>
        /// <param name="p">Posto #</param>
        /// <param name="semanaAtual">Semana atual do posto</param>
        /// <param name="caminhoDestino">Caminho em que os arquivos vão ser escritos</param>
        public static bool inpDatPorPosto(Postos p, Semanas_Ano semanaAtual, string inpPath, Dictionary<int, Dictionary<string, string>> strDat = null)
        {

            bool shouldRun = true;
            Semanas_Ano semanaInicio;
            int numSemanasNoHistorico = 52;


            //Primeira semana do PREVS.
            if (semanaAtual.rev == 0)
                semanaInicio = SemanasAnoDAO.GetByMesAno(UtilitarioDeData.mesAnterior(semanaAtual.mes), UtilitarioDeData.calculaAno(semanaAtual.ano, semanaAtual.mes, -1), 0);
            else
                semanaInicio = SemanasAnoDAO.GetByMesAno(semanaAtual.mes, semanaAtual.ano, 0);

            if (Directory.Exists(inpPath))
                Directory.Delete(inpPath, true);
            Directory.CreateDirectory(inpPath);

            //Escrever Caso.dat
            using (var swCaso = new StreamWriter(Path.Combine(inpPath, "CASO.DAT")))
            {
                swCaso.WriteLine(String.Concat(p.numero.ToString(), ".inp"));
            }

            //Nova versão do previvaz (5.3.5) necessita do arquivo "ENCAD.DAT"
            using (var swCaso = new StreamWriter(Path.Combine(inpPath, "ENCAD.DAT")))
            {
                swCaso.WriteLine("ALGHAO234PGJAGAENCAD");
            }

            //Atualizar .inp
            //using (StreamReader srInp = new StreamReader(p.caminhoInp)) {

            var srInpL = File.ReadAllLines(p.caminhoInp);

            if (srInpL[16][0] == '1') numSemanasNoHistorico = 53;

            using (StreamWriter swInp = new StreamWriter(Path.Combine(inpPath, String.Concat(p.numero.ToString(), ".inp"))))
            {

                for (int linha = 1; linha <= srInpL.Length; linha++)
                {

                    var sLinha = srInpL[linha - 1];

                    if (linha == 9)
                        if ((semanaAtual.semana + p.tipo) > numSemanasNoHistorico)
                            swInp.WriteLine((semanaAtual.semana + p.tipo) - numSemanasNoHistorico);
                        else
                            swInp.WriteLine(semanaAtual.semana + p.tipo);
                    else if (linha == 10)
                        if ((semanaAtual.semana + p.tipo) > numSemanasNoHistorico)
                            swInp.WriteLine(semanaAtual.ano + 1);
                        else
                            swInp.WriteLine(semanaAtual.ano);
                    else if (linha == 16) //indica caminho do arquivo de limites a partir da versão 6 do previvaz.
                    {
                        var arqLim = sLinha.Split(' ');

                        if (!string.IsNullOrWhiteSpace(arqLim[0]))
                        {

                            var limF = Path.Combine(Path.GetDirectoryName(p.caminhoInp), arqLim[0].Trim());
                            if (File.Exists(limF)) File.Copy(limF, Path.Combine(inpPath, arqLim[0].Trim()));
                        }

                        swInp.WriteLine(sLinha);
                    }
                    else
                        swInp.WriteLine(sLinha);
                }
                swInp.Close();
            }


            //Atualizar .dat
            using (StreamReader srDat = new StreamReader(p.caminhoDat))
            using (var strMemory = new MemoryStream())
            using (StreamWriter swDat = new StreamWriter(strMemory))
            {
                var strDatPath = Path.Combine(inpPath, String.Concat(p.numero.ToString(), "_str.DAT"));


                int[][] vazaoAno = new int[10][];
                int numLinhaDat = 0;
                string anoDat = "";

                swDat.WriteLine(srDat.ReadLine());
                string linhaFoo = srDat.ReadLine();

                if ((semanaAtual.semana + p.tipo) > numSemanasNoHistorico)
                    swDat.WriteLine(linhaFoo.Substring(0, 6) + (semanaAtual.ano + 1).ToString() + linhaFoo.Substring(10, linhaFoo.Length - 10));
                else
                    swDat.WriteLine(linhaFoo.Substring(0, 6) + semanaAtual.ano.ToString() + linhaFoo.Substring(10, linhaFoo.Length - 10));

                //Copia as linhas dos anos anteriores e separa os dados do ano atual
                while (!srDat.EndOfStream)
                {
                    string linhaDat = srDat.ReadLine();

                    if (linhaDat.TrimEnd().Length >= (80))
                    {
                        var foo = linhaDat.Substring(76, 4);
                        anoDat = foo;
                    }

                    if (int.Parse(anoDat) < semanaInicio.ano)
                        swDat.WriteLine(linhaDat);
                    else
                    {
                        vazaoAno[numLinhaDat] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 };
                        string[] linhaDatExplode = linhaDat.Trim()
                                                            .Replace(".", "")
                                                            .Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < linhaDatExplode.Length; i++)
                            vazaoAno[numLinhaDat][i] = int.Parse(linhaDatExplode[i]);

                        if (vazaoAno[numLinhaDat][9] == -1)
                        {
                            //vazaoAno[numLinhaDat][8] = 0;
                            //vazaoAno[numLinhaDat][9] = 0;
                            vazaoAno[numLinhaDat][9] = vazaoAno[numLinhaDat - 1][9];
                        }

                        numLinhaDat++;
                    }
                }




                // Atualiza os dados do ano atual
                Semanas_Ano semIt = semanaInicio;

                // if (semanaAtual.semana == 53 && numSemanasNoHistorico == 52) semIt = semIt.semanaProxima();

                int semanaIt = semIt.rev + 1;
                int difAno = semanaAtual.ano - semIt.ano;
                int mudAno = 0;
                while (semIt.semana - ((difAno * numSemanasNoHistorico) + semanaAtual.semana + p.tipo - 1) <= 0)
                {

                    var x = (mudAno + semIt.semana) / 9;
                    var y = ((mudAno + semIt.semana) % 9) - 1;

                    if (y == -1)
                    {
                        x = x - 1;
                        y = 8;
                    }

                    if (vazaoAno[x] == null)
                        vazaoAno[x] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, semIt.ano };

                    PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", semanaIt.ToString()));


                    vazaoAno[x][y] = Convert.ToInt32(semanaPrevs.GetValue(p.prevs_saida, null));
                    vazaoAno[x][9] = semIt.ano;

                    var anoAtual = semIt.ano;
                    semIt = semIt.semanaProxima();
                    //if (semIt.semana == 53) semIt = semIt.semanaProxima();

                    var anoSeg = semIt.ano;

                    if (anoSeg - anoAtual > 0)
                        mudAno = 54; //52 semanas do ano anterior + 2 semanas para completar a linha do dat.

                    difAno = semanaAtual.ano - semIt.ano;
                    semanaIt = semanaIt + 1;

                    if (semanaIt > 6)
                        break;
                }

                //Escreve no arquivo os dados do ano atual
                foreach (int[] vazaoLinha in vazaoAno.Where(z => z != null))
                {
                    StringBuilder datLinha = new StringBuilder();

                    for (int x = 0; x < 9; x++)
                    {
                        datLinha.Append(UtilitarioDeTexto.preencheEspacos(vazaoLinha[x].ToString(), 7));
                        datLinha.Append('.');
                    }
                    datLinha.Append(UtilitarioDeTexto.preencheEspacos(vazaoLinha[9].ToString(), 8));
                    swDat.WriteLine(datLinha.ToString());
                }

                swDat.Flush();
                var fileStr = File.Create(strDatPath);
                fileStr.Write(strMemory.ToArray(), 0, (int)strMemory.Length);
                fileStr.Close();


                lock (objLock)
                {
                    if (strDat != null)
                    {
                        if (!strDat.ContainsKey(p.numero))
                        {
                            strDat.Add(p.numero, new Dictionary<string, string>());
                        }


                        var hash = Util.UtilitarioDeArquivo.GetMD5HashFromFile(strDatPath);

                        if (strDat[p.numero].ContainsKey(hash))
                        {

                            //copy _fut.dat//
                            var copyFrom = strDat[p.numero][hash];

                            if (File.Exists(copyFrom))
                            {
                                File.Copy(
                                    copyFrom,
                                    Path.Combine(inpPath, String.Concat(p.numero.ToString(), "_fut.DAT"))
                                    );

                                File.WriteAllText(
                                    Path.Combine(inpPath, "ok"),
                                    "FUT copiado de \"" + copyFrom + "\"");

                                shouldRun = false;
                            }
                        }
                        else
                        {
                            strDat[p.numero].Add(hash, Path.Combine(inpPath, String.Concat(p.numero.ToString(), "_fut.DAT")));
                        }
                    }
                }
            }

            return shouldRun;
        }

        /// <summary>
        /// Roda o previvaz em 'caminhoBase' e le os resultados.
        /// </summary>
        /// <param name="p">Posto em que esta sendo rodado o previvaz</param>
        /// <param name="caminhoBase">Caminho em que será rodado</param>
        public static void RodaPrevivaz(Postos p, string caminhoBase)
        {
            //Caso já exista algum fut, apaga para evitar problemas futuros.
            if (File.Exists(Path.Combine(caminhoBase, "ok")))
                return;


            if (File.Exists(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT"))))
                File.Delete(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT")));

            var caminhoPrevivaz = ConfigurationManager.AppSettings["previvaz"];

            ///Usando o nome completo da biblioteca para evitar conflitos com a biblioteca ThreadState
            System.Diagnostics.ProcessStartInfo myProcessInfo = new System.Diagnostics.ProcessStartInfo();
            myProcessInfo.CreateNoWindow = true;
            myProcessInfo.UseShellExecute = false;
            myProcessInfo.FileName = caminhoPrevivaz;
            myProcessInfo.WorkingDirectory = caminhoBase;

            myProcessInfo.RedirectStandardOutput = true;
            try
            {

                using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(myProcessInfo))
                {


                    proc.EnableRaisingEvents = true;
                    proc.WaitForExit();
                    var output = proc.StandardOutput.ReadToEnd();

                    if (output.IndexOf("ERRO :") >= 0)
                    {
                        throw new ApplicationException("Previvaz - Posto : " + p.numero.ToString() + "\r\n" + output.Substring(output.IndexOf("ERRO :")));
                    }

                    var exitCode = proc.ExitCode;
                }

                if (File.Exists(Path.Combine(caminhoBase, "PREVP.DAT")))
                    File.Delete(Path.Combine(caminhoBase, "PREVP.DAT"));
                if (File.Exists(Path.Combine(caminhoBase, "PREVT.DAT")))
                    File.Delete(Path.Combine(caminhoBase, "PREVT.DAT"));
                if (File.Exists(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".rel"))))
                    File.Delete(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".rel")));
                if (File.Exists(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".BCX"))))
                    File.Delete(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".BCX")));
                if (File.Exists(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".exc"))))
                    File.Delete(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), ".exc")));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        static LinuxQueue.QueueController controller = new LinuxQueue.QueueController();


        static object lLock = new object();
        public static void RodaPrevivazLinux(string caminhoBase)
        {
            var locks = Path.Combine(caminhoBase, "ok");
            if (File.Exists(locks))
                File.Delete(locks);

            lock (lLock)
            {

                var comm = new LinuxQueue.CommItem()
                {
                    Command = @"/home/producao/PrevisaoPLD/shared/previvaz/previvaz3.sh",
                    CommandName = "pr_" + DateTime.Now.ToString("yyyyMMddHHmmssss"),
                    Cluster = new LinuxQueue.Cluster() { Alias = "Auto", Host = "" },
                    IgnoreQueue = true,
                    WorkingDirectory = caminhoBase,
                    User = "hide"
                };

                controller.Enqueue(comm);

                controller.WaitCompletition(comm, milisecondsInverval: 3000, timeout: 360000);                
            }
        }

        public static void PosPrevivaz(Postos p, Semanas_Ano semanaAtual, String caminhoBase)
        {
            int indPrevs = 4; //Indice da coluna do arquivo #_fut.DAT em que fica a primeira semana prevista do previvaz




            var fut = File.ReadAllLines(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT")));

            //using (StreamReader srFut = new StreamReader(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT"))))
            //{
            ///Pega a saida do previvaz e salva em suas respectivas semanas.
            //                srFut.ReadLine();
            //string saidaPrevivaz = srFut.ReadLine();
            string saidaPrevivaz = fut[1];
            string[] saidaPrevivazExplode = saidaPrevivaz.Trim()
                                                             .Replace('.', ',')
                                                             .Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            if (semanaAtual.rev == 0)
            {
                for (int x = 1; x < (1 + p.tipo); x++)
                {
                    PropertyInfo semanaAnt = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", (semanaAtual.semanaAnterior().rev + 1 + x).ToString()));
                    PropertyInfo semanaRV0 = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", x.ToString()));
                    semanaRV0.SetValue(p.prevs_saida, semanaAnt.GetValue(p.prevs_saida, null), null);
                }
            }
            for (int x = (semanaAtual.rev + 1 + p.tipo); x < 7; x++)
            {
                PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", x.ToString()));
                semanaPrevs.SetValue(p.prevs_saida, double.Parse(saidaPrevivazExplode[indPrevs]), null);
                indPrevs++;
            }
            //}

            


        }
    }
}
