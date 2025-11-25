using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DecompTools.ControllerDC
{
    class controllerRVX
    {
        private Boolean ac = false;

        /// <summary>
        /// A partir do deck passado, gera todas as rev. seguintes ate o final do mes
        /// </summary>
        /// <param name="idDeckBase">Deck inicial</param>
        /// <param name="caminho">Caminho de saida para os decks seguintes</param>
        /// <returns>true caso as rev. foram geradas com sucesso, false caso contrario</returns>
        public bool RVX(Deck deckBase, String caminho)
        {

            Semanas s;

            if (deckBase.rev != 0)
                s = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            else
                s = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);

            var numSemanas = s.semanas - (s.diasMes2 != 0 ? 1 : 0);

            string rootFolder = System.IO.Path.GetDirectoryName(deckBase.caminho);
            string renovaveisFile = System.IO.Directory.GetFiles(rootFolder).Where(x => System.IO.Path.GetFileName(x).ToLower().Contains("renovaveis")).FirstOrDefault();
            int iteracao = 0;
            do
            {

                var folder = System.IO.Path.Combine(caminho, "RV" + deckBase.rev.ToString());
                if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);
                deckBase.escreveDeck(folder);
                System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "caso.dat"), "rv" + deckBase.rev.ToString());
                System.IO.File.WriteAllText(System.IO.Path.Combine(folder, "rv" + deckBase.rev.ToString()),
                    "dadger.rv" + deckBase.rev.ToString() +
                    "\nvazoes.rv" + deckBase.rev.ToString() +
                    "\nhidr.dat" +
                    "\nmlt.dat" +
                    "\nperdas.dat" +
                    "\ndadgnl.rv" + deckBase.rev.ToString() +
                    "\n./\n\n");

                //if (deckBase.rev + 1 == s.semanas - 1) this.ac = true;
                this.ac = true;

                Deck deckNew = new Deck();
                deckNew.clone(deckBase);
                deckNew.rev = deckBase.rev + 1;
                deckNew.caminho = folder;
                deckNew.id_deckNW = deckBase.id_deckNW;
                deckNew.id_deckDC_base = deckBase.id;
                deckNew.te = deckBase.te ?? "";
                deckNew.te = deckNew.te.Replace("REV " + (deckNew.rev - 1).ToString(), "REV " + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("REV" + (deckNew.rev - 1).ToString(), "REV" + deckNew.rev.ToString());
                deckNew.te = deckNew.te.Replace("RV" + (deckNew.rev - 1).ToString(), "RV" + deckNew.rev.ToString());

                DateTime semanaInicial = s.primeiraSemana;

                for (int x = 0; x < deckNew.rev; x++)
                {
                    semanaInicial = semanaInicial.AddDays(7);
                }

                deckNew.dia = semanaInicial.Day;
                deckNew.mes = semanaInicial.Month;
                deckNew.ano = semanaInicial.Year;

                atualizarRVX(deckNew, s);
                if (System.IO.File.Exists(renovaveisFile))
                {
                    atualizarRenovaveis(folder, renovaveisFile, iteracao);
                    iteracao++;
                }
                deckBase = deckNew;

            } while (deckBase.rev + 1 <= numSemanas);

            return true;
        }

        public void atualizarRenovaveis(string saveFolder, string renovaveis, int redutor)
        {
            string filename = Path.GetFileName(renovaveis);
            List<string> newArq = new List<string>();

            var renolines = File.ReadAllLines(renovaveis).ToList();

            if (redutor > 0)
            {
                foreach (var line in renolines)
                {
                    var lineSplit = line.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (line.Trim().StartsWith("&") || line.Trim().StartsWith("PEE-CAD") || line.Trim().StartsWith("PEE-SUBM"))
                    {
                        newArq.Add(line);
                    }
                    else if (line.Trim().StartsWith("PEE-CONFIG-PER"))
                    {
                        int estagio = Convert.ToInt32(lineSplit[3]) - redutor;
                        lineSplit[3] = estagio.ToString();
                        newArq.Add(string.Join(";", lineSplit.ToArray()));
                    }
                    else if (line.Trim().StartsWith("PEE-POT-INST-PER"))
                    {
                        int estagio = Convert.ToInt32(lineSplit[3]) - redutor;
                        lineSplit[3] = estagio.ToString();
                        newArq.Add(string.Join(";", lineSplit.ToArray()));
                    }
                    else if (line.Trim().StartsWith("PEE-GER-PER-PAT-CEN"))
                    {
                        int estagioIni = Convert.ToInt32(lineSplit[2]);
                        int estagioFin = Convert.ToInt32(lineSplit[3]);
                        if (estagioIni<= redutor)
                        {
                            continue;
                        }
                        else
                        {
                            lineSplit[2] = (estagioIni - redutor).ToString();
                            lineSplit[3] = (estagioFin - redutor).ToString();
                            newArq.Add(string.Join(";", lineSplit.ToArray()));

                        }

                        //continuar aqui C:\Files\Implementacoes\renovaveis DECOMP\testeRaio\DEC_ONS_092025_RV0_VE_renovaveis
                    }
                    else
                    {
                        newArq.Add(line);
                    }
                }
                File.WriteAllLines(Path.Combine(saveFolder, filename), newArq);
            }
            else
            {
                System.IO.File.Copy(renovaveis, System.IO.Path.Combine(saveFolder, filename), true);
            }

        }

        /// <summary>
        /// Faz o papel de atualizar bloco a bloco, inicialmente os blocos sem inteligencia e depois os "blocos inteligentes"
        /// </summary>
        /// <param name="deck">Deck a ser atualizado</param>
        /// <param name="s">Semana atual</param>
        public void atualizarRVX(Deck deck, Semanas s)
        {
            MP.atualizarRVX(deck, s);
            MT.atualizarRVX(deck, s);
            FD.atualizarRVX(deck, s);
            VE.atualizarRVX(deck, s);
            TI.atualizarRVX(deck, s);
            RQ.atualizarRVX(deck, s);
            DP.atualizarRVX(deck);
            PQ.atualizarRVX(deck);
            // IT.atualizarRVX(deck);
            RI.atualizarRVX(deck);
            VL.atualizarRVX(deck);
            IA.atualizarRVX(deck);
            CT.atualizarRVX(deck);
            VI.atualizarRVX(deck);
            //QI.atualizarRVX(deck);
            //ES.atualizarRVX(deck);
            HE.atualizarRVX(deck);
            Restricoes.atualizarRVX(deck, "RHE");

            Restricoes.atualizarRVX(deck, "RHA");
            Restricoes.atualizarRVX(deck, "RHV");
            Restricoes.atualizarRVX(deck, "RHQ");

            VR.atualizarRVX(deck);

            if (this.ac)
                AC.atualizarRVX(deck);
        }
    }
}
