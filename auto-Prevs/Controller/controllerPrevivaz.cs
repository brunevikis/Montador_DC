using AutoPrevs.Factory;
using AutoPrevs.Modelagem;
using AutoPrevs.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoPrevs.Controller {
    public class controllerPrevivaz {
        protected static string caminhoPrevivaz = Path.Combine(Path.Combine(System.Environment.CurrentDirectory, "Previvaz"), "previvaz.exe");

        /// <summary>
        /// Comanda o processo de criar os arquivos e executar o previvaz
        /// </summary>
        /// <param name="postosList">Lista com os postos</param>
        /// <param name="caminhoDestino">Caminho para qual os arquivos inp/dat serão escritos</param>
        /// <param name="semanaAtual">Semana atual do novo prevs</param>
        public static void InpDatPrevivaz(IList<Postos> postosList, string caminhoDestino, Semanas_Ano semanaAtual) {
            var newPostList = postosList.Where(p => p.tipo == 0 || p.tipo == 1 || p.tipo == 2);
            int numPostos = postosList.Count(p => p.tipo == 0 || p.tipo == 1 || p.tipo == 2);
            string caminhoDestinoPrevivaz = Path.Combine(caminhoDestino, "arq_previvaz");

            try {

                var postosIncrementais = new Dictionary<int, int[]>(){ // <num posto, { postos montantes ... } >
                    {34, new int[] {18, 33, 99, 241, 261}},
                    {245, new int[] {34, 243}},
                    {246, new int[] {245}},
                    {266, new int[] {63, 246}},                    
                };

                foreach (var pn in postosIncrementais.OrderByDescending(x => x.Key)) {
                    var p = newPostList.Where(x => x.numero == pn.Key).First();
                    foreach (var pMn in pn.Value) {

                        var pM = postosList.Where(x => x.numero == pMn).First();

                        p.prevs_saida.sem1 -= pM.prevs_saida.sem1;
                        p.prevs_saida.sem2 -= pM.prevs_saida.sem2;
                        p.prevs_saida.sem3 -= pM.prevs_saida.sem3;
                        p.prevs_saida.sem4 -= pM.prevs_saida.sem4;
                        p.prevs_saida.sem5 -= pM.prevs_saida.sem5;
                        p.prevs_saida.sem6 -= pM.prevs_saida.sem6;

                    }
                }

                var parallelism = 4;

                var result = Parallel.ForEach(newPostList.Where(x => !postosIncrementais.Keys.Contains(x.numero))
                    , new ParallelOptions { MaxDegreeOfParallelism = parallelism }, p => {
                        string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());
                        inpDatPorPosto(p, semanaAtual, caminhoPosto);
                        RodaPrevivaz(p, semanaAtual, caminhoPosto);
                        PosPrevivaz(p, semanaAtual, caminhoPosto);
                        Console.WriteLine("Processing {0} on thread {1}", p.numero, Thread.CurrentThread.ManagedThreadId);
                    }
                );

                foreach (var pn in postosIncrementais) {
                    var p = newPostList.Where(x => x.numero == pn.Key).First();

                    string caminhoPosto = Path.Combine(caminhoDestinoPrevivaz, p.numero.ToString());
                    inpDatPorPosto(p, semanaAtual, caminhoPosto);
                    RodaPrevivaz(p, semanaAtual, caminhoPosto);
                    PosPrevivaz(p, semanaAtual, caminhoPosto);
                    Console.WriteLine("Processing {0} on thread {1}", p.numero, Thread.CurrentThread.ManagedThreadId);

                    foreach (var pMn in pn.Value) {

                        var pM = postosList.Where(x => x.numero == pMn).First();

                        p.prevs_saida.sem1 += pM.prevs_saida.sem1;
                        p.prevs_saida.sem2 += pM.prevs_saida.sem2;
                        p.prevs_saida.sem3 += pM.prevs_saida.sem3;
                        p.prevs_saida.sem4 += pM.prevs_saida.sem4;
                        p.prevs_saida.sem5 += pM.prevs_saida.sem5;
                        p.prevs_saida.sem6 += pM.prevs_saida.sem6;
                    }
                }

            } catch (AggregateException ex) {
                throw ex.InnerException;
            }
        }


        /// <summary>
        /// Escreve os arquivos CASO.DAT, #.inp e #_str.DAT para o posto #.
        /// </summary>
        /// <param name="p">Posto #</param>
        /// <param name="semanaAtual">Semana atual do posto</param>
        /// <param name="caminhoDestino">Caminho em que os arquivos vão ser escritos</param>
        public static void inpDatPorPosto(Postos p, Semanas_Ano semanaAtual, string inpPath) {
            Semanas_Ano semanaInicio;

            //Primeira semana do PREVS.
            if (semanaAtual.rev == 0)
                semanaInicio = SemanasAnoDAO.GetByMesAno(UtilitarioDeData.mesAnterior(semanaAtual.mes), UtilitarioDeData.calculaAno(semanaAtual.ano, semanaAtual.mes, -1), 0);
            else
                semanaInicio = SemanasAnoDAO.GetByMesAno(semanaAtual.mes, semanaAtual.ano, 0);

            if (Directory.Exists(inpPath))
                Directory.Delete(inpPath, true);
            Directory.CreateDirectory(inpPath);

            //Escrever Caso.dat
            using (var swCaso = new StreamWriter(Path.Combine(inpPath, "CASO.DAT"))) {
                swCaso.WriteLine(String.Concat(p.numero.ToString(), ".inp"));
            }

            //Nova versão do previvaz (5.3.5) necessita do arquivo "ENCAD.DAT"
            using (var swCaso = new StreamWriter(Path.Combine(inpPath, "ENCAD.DAT"))) {
                swCaso.WriteLine("ALGHAO234PGJAGAENCAD");
            }

            //Atualizar .inp
            using (StreamReader srInp = new StreamReader(p.caminhoInp)) {
                StreamWriter swInp = new StreamWriter(Path.Combine(inpPath, String.Concat(p.numero.ToString(), ".inp")));

                int linha = 1;
                while (!srInp.EndOfStream) {
                    var sLinha = srInp.ReadLine();
                    if (linha == 9)
                        if ((semanaAtual.semana + p.tipo) > 52)
                            swInp.WriteLine((semanaAtual.semana + p.tipo) - 52);
                        else
                            swInp.WriteLine(semanaAtual.semana + p.tipo);
                    else if (linha == 10)
                        if ((semanaAtual.semana + p.tipo) > 52)
                            swInp.WriteLine(semanaAtual.ano + 1);
                        else
                            swInp.WriteLine(semanaAtual.ano);
                    else if (linha == 16) //indica caminho do arquivo de limites a partir da versão 6 do previvaz.
                    {
                        var arqLim = sLinha.Split(' ');

                        if (!string.IsNullOrWhiteSpace(arqLim[0])) {

                            var limF = Path.Combine(Path.GetDirectoryName(p.caminhoInp), arqLim[0].Trim());
                            if (File.Exists(limF)) File.Copy(limF, Path.Combine(inpPath, arqLim[0].Trim()));
                        }

                        swInp.WriteLine(sLinha);
                    } else
                        swInp.WriteLine(sLinha);

                    linha++;
                }

                swInp.Close();
            }

            //Atualizar .dat
            using (StreamReader srDat = new StreamReader(p.caminhoDat)) {
                StreamWriter swDat = new StreamWriter(Path.Combine(inpPath, String.Concat(p.numero.ToString(), "_str.DAT")));
                int[][] vazaoAno = new int[10][];
                int numLinhaDat = 0;
                string anoDat = "";

                swDat.WriteLine(srDat.ReadLine());
                string linhaFoo = srDat.ReadLine();

                if ((semanaAtual.semana + p.tipo) > 53)
                    swDat.WriteLine(linhaFoo.Substring(0, 6) + (semanaAtual.ano + 1).ToString() + linhaFoo.Substring(10, linhaFoo.Length - 10));
                else
                    swDat.WriteLine(linhaFoo.Substring(0, 6) + semanaAtual.ano.ToString() + linhaFoo.Substring(10, linhaFoo.Length - 10));

                //Copia as linhas dos anos anteriores e separa os dados do ano atual
                while (!srDat.EndOfStream) {
                    string linhaDat = srDat.ReadLine();

                    if (linhaDat.Length >= (80)) {
                        var foo = linhaDat.Substring(76, 4);
                        anoDat = foo;
                    }

                    if (int.Parse(anoDat) < semanaInicio.ano)
                        swDat.WriteLine(linhaDat);
                    else {
                        vazaoAno[numLinhaDat] = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, -1 };
                        string[] linhaDatExplode = linhaDat.Trim()
                                                            .Replace(".", "")
                                                            .Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < linhaDatExplode.Length; i++)
                            vazaoAno[numLinhaDat][i] = int.Parse(linhaDatExplode[i]);

                        if (vazaoAno[numLinhaDat][9] == -1) {
                            vazaoAno[numLinhaDat][8] = 0;
                            vazaoAno[numLinhaDat][9] = 0;
                        }

                        numLinhaDat++;
                    }
                }




                // Atualiza os dados do ano atual
                Semanas_Ano semIt = semanaInicio;
                int semanaIt = semIt.rev + 1;
                int difAno = semanaAtual.ano - semIt.ano;
                int mudAno = 0;
                while (semIt.semana - ((difAno * 52) + semanaAtual.semana + p.tipo - 1) <= 0) {
                    var x = (mudAno + semIt.semana) / 9;
                    var y = ((mudAno + semIt.semana) % 9) - 1;

                    if (y == -1) {
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
                    var anoSeg = semIt.ano;

                    if (anoSeg - anoAtual > 0)
                        mudAno = 54; //52 semanas do ano anterior + 2 semanas para completar a linha do dat.

                    difAno = semanaAtual.ano - semIt.ano;
                    semanaIt = semanaIt + 1;

                    if (semanaIt > 6)
                        break;
                }

                //Escreve no arquivo os dados do ano atual
                foreach (int[] vazaoLinha in vazaoAno.Where(z => z != null)) {
                    StringBuilder datLinha = new StringBuilder();

                    for (int x = 0; x < 9; x++) {
                        datLinha.Append(UtilitarioDeTexto.preencheEspacos(vazaoLinha[x].ToString(), 7));
                        datLinha.Append('.');
                    }
                    datLinha.Append(UtilitarioDeTexto.preencheEspacos(vazaoLinha[9].ToString(), 8));
                    swDat.WriteLine(datLinha.ToString());
                }

                swDat.Close();
            }
        }


        /// <summary>
        /// Roda o previvaz em 'caminhoBase' e le os resultados.
        /// </summary>
        /// <param name="p">Posto em que esta sendo rodado o previvaz</param>
        /// <param name="caminhoBase">Caminho em que será rodado</param>
        /// <param name="semanaAtual">Semana atual do posto</param>
        public static void RodaPrevivaz(Postos p, Semanas_Ano semanaAtual, string caminhoBase) {
            //Caso já exista algum fut, apaga para evitar problemas futuros.
            if (File.Exists(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT"))))
                File.Delete(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT")));

            ///Usando o nome completo da biblioteca para evitar conflitos com a biblioteca ThreadState
            System.Diagnostics.ProcessStartInfo myProcessInfo = new System.Diagnostics.ProcessStartInfo();
            myProcessInfo.CreateNoWindow = true;
            myProcessInfo.UseShellExecute = false;
            myProcessInfo.FileName = caminhoPrevivaz;
            myProcessInfo.WorkingDirectory = caminhoBase;

            myProcessInfo.RedirectStandardOutput = true;
            try {

                using (System.Diagnostics.Process proc = System.Diagnostics.Process.Start(myProcessInfo)) {


                    proc.EnableRaisingEvents = true;
                    proc.WaitForExit();
                    var output = proc.StandardOutput.ReadToEnd();

                    if (output.IndexOf("ERRO :") >= 0) {
                        throw new ApplicationException("Previvaz - Posto : " + p.numero.ToString() + "\r\n" + output.Substring(output.IndexOf("ERRO :")));
                    }

                    var exitCode = proc.ExitCode;
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public static void PosPrevivaz(Postos p, Semanas_Ano semanaAtual, String caminhoBase) {
            int indPrevs = 4; //Indice da coluna do arquivo #_fut.DAT em que fica a primeira semana prevista do previvaz

            if (File.Exists(Path.Combine(caminhoBase, "PREVP.DAT")))
                File.Delete(Path.Combine(caminhoBase, "PREVP.DAT"));

            if (File.Exists(Path.Combine(caminhoBase, "PREVT.DAT")))
                File.Delete(Path.Combine(caminhoBase, "PREVT.DAT"));

            using (StreamReader srFut = new StreamReader(Path.Combine(caminhoBase, String.Concat(p.numero.ToString(), "_fut.DAT")))) {
                ///Pega a saida do previvaz e salva em suas respectivas semanas.
                srFut.ReadLine();
                string saidaPrevivaz = srFut.ReadLine();
                string[] saidaPrevivazExplode = saidaPrevivaz.Trim()
                                                             .Replace('.', ',')
                                                             .Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                if (semanaAtual.rev == 0) {
                    for (int x = 1; x < (1 + p.tipo); x++) {
                        PropertyInfo semanaAnt = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", (semanaAtual.semanaAnterior().rev + 1 + x).ToString()));
                        PropertyInfo semanaRV0 = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", x.ToString()));
                        semanaRV0.SetValue(p.prevs_saida, semanaAnt.GetValue(p.prevs_saida, null), null);
                    }
                }
                for (int x = (semanaAtual.rev + 1 + p.tipo); x < 7; x++) {
                    PropertyInfo semanaPrevs = (new PrevsDados()).GetType().GetProperty(String.Concat("sem", x.ToString()));
                    semanaPrevs.SetValue(p.prevs_saida, double.Parse(saidaPrevivazExplode[indPrevs]), null);
                    indPrevs++;
                }
            }
        }
    }
}
