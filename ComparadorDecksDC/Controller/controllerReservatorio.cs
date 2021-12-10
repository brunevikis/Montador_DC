using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComparadorDecksDC.Controller {
    public class controllerReservatorio {
        public static string createReserv(Deck idDeckBase, decimal[] subTarget, string earmmax, string exitPath) {
            Deck deckBase = DeckDAO.getAllBlocksbyID(idDeckBase.id);

            //Leitura da Earm
            string[] Earmlinhas = earmmax.Split('\n');
            decimal[] subTotal = new decimal[4];

            for (int x = 0; x < 4; x++) {
                subTarget[x] = subTarget[x] / 100;
                subTotal[x] = decimal.Parse(Earmlinhas[x]);
                subTotal[x] = subTotal[x] * 730.5m;
            }

            //Caso o usuario Atualizar o Earmax, verifica e salva no banco.
            EarmMax.confereNovoEarm(subTotal);

            List<UH> uhResul = runReserv(deckBase, subTarget, subTotal);

            //Escrevendo o UH de saida.
            if (exitPath.Substring(exitPath.Length - 3, 2) != "rv") {
                using (TextWriter arquivo = File.CreateText(exitPath)) {
                    foreach (UH linha in uhResul)
                        arquivo.WriteLine(linha.escreveLinha());
                    arquivo.Flush();
                }
            } else {
                Deck deck = new Deck();
                deck = controllerCarrega.lerDeck(exitPath);
                deck.uh = uhResul;
                deck.escreveDeck(Path.GetDirectoryName(exitPath), String.Concat("new_dadger.rv", deck.rev.ToString()));
            }

            return "Processo realizado com sucesso";
        }

        public static List<UH> runReserv(Deck deckBase, decimal[] subTarget, decimal[] subTotal) {
            List<CadUsh> cadUsinas = CadUshDAO.GetAll();
            List<infoUsinasFict> infoFict = infoUsinasFictDAO.GetAll();
            List<UH> uhResult = new List<UH>();
            CadUsh[] usinas = new CadUsh[350];

            //Ler as informações do CadUSH
            foreach (CadUsh cad in cadUsinas)
                usinas[cad.codUsina] = cad;

            //Ler informações do dadger e sobrepor as do CadUSH
            foreach (AC ac in deckBase.ac) {
                var campo2 = ac.campo2.Trim();
                if (campo2 == "VOLMAX") {
                    usinas[int.Parse(ac.campo1.Trim())].volMax = decimal.Parse(ac.campo3.Replace(".", ","));
                } else if (campo2 == "VOLMIN") {
                    usinas[int.Parse(ac.campo1.Trim())].volMin = decimal.Parse(String.Concat(ac.campo3, ac.campo4).Replace(".", ","));   //Veri
                } else if (campo2 == "JUSENA") {
                    usinas[int.Parse(ac.campo1.Trim())].jusante = int.Parse(ac.campo3.Replace(".", ","));  //Veri
                } else if (campo2 == "JUSMED") {
                    if (ac.campo6 != null && ac.campo6.Trim() == "1")
                        usinas[int.Parse(ac.campo1.Trim())].canalFugaMed = decimal.Parse(ac.campo4.Replace(".", ","));  //veri
                } else if (campo2 == "NUMCON") {
                    if (ac.campo6 != null && ac.campo6.Trim() == "1" && ac.campo3.Trim() == "0")
                        usinas[int.Parse(ac.campo1.Trim())].prodEsp = 0;  //veri
                } else if (campo2 == "PERHID") {
                    usinas[int.Parse(ac.campo1.Trim())].perdaVal = decimal.Parse(ac.campo3.Replace(".", ","));
                } else if (campo2 == "TIPUSI") {
                    usinas[int.Parse(ac.campo1.Trim())].reg = ac.campo3;
                } else if (campo2 == "PROESP") {
                    usinas[int.Parse(ac.campo1.Trim())].prodEsp = decimal.Parse(ac.campo3.Replace(".", ","));
                } else if (campo2 == "TIPERH") {
                    usinas[int.Parse(ac.campo1.Trim())].perdaTipo = int.Parse(ac.campo3.Replace(".", ","));
                } else if (campo2 == "PERHID") {
                    usinas[int.Parse(ac.campo1.Trim())].perdaVal = decimal.Parse(ac.campo3.Replace(".", ","));
                }
            }
            //Para as postos em dois submercados, ler o percentual de limitação do volume
            foreach (EZ ez in deckBase.ez)
                usinas[int.Parse(ez.campo1)].ficEZ = decimal.Parse(ez.campo2.Replace(".", ","));

            //Ler o UH - Volume
            foreach (UH uh in deckBase.uh) {
                var uhPost = decimal.Parse(uh.campo3.Replace(".", ","));
                var posto = int.Parse(uh.campo1);

                usinas[posto].uhBase = uhPost;
                usinas[posto].inDadger = true;
            }

            foreach (infoUsinasFict inf in infoFict) {
                if (usinas[inf.idCorresp].ficEZ != 0 && usinas[inf.idCorresp].ficEZ <= usinas[inf.idCorresp].uhBase)
                    usinas[inf.idFict].uhBase = usinas[inf.idCorresp].ficEZ;
                else
                    usinas[inf.idFict].uhBase = usinas[inf.idCorresp].uhBase;
                usinas[inf.idFict].ficSub = usinas[inf.idCorresp].sistema;
                usinas[inf.idCorresp].ficBaseSub = usinas[inf.idFict].sistema;
            }
            
            //usinas não modeladas com exceção de 176
            foreach (var ush in cadUsinas.Where(u => !u.inDadger && u.codUsina != 176)) {
                ush.prodEsp = 0;
            }

            cadUsinas = ajustarSubmercado(cadUsinas, subTarget, subTotal);

            //ordenação deve obedecer a proveniente do deckBase.uh
            foreach (UH uhBase in deckBase.uh) {
                var posto = int.Parse(uhBase.campo1);
                var ush = cadUsinas.Where(u => u.codUsina == posto).FirstOrDefault();
                var uh = new UH();
                uh.campo1 = ush.codUsina.ToString();
                uh.campo2 = ush.sistema.ToString();
                uh.campo3 = Math.Round(ush.uhBase, 2).ToString().Replace(",", ".");
                uh.campo4 = "1";

                uhResult.Add(uh);
            }

            return uhResult;
        }

        public static void atualizaQueda(List<CadUsh> cadUsinas) {
            decimal cotaRelativa, vIni, vUtil, v1, v2;

            //Calcular a queda e consequentemente, a produtibilidade de cada usina.
            foreach (CadUsh usina in cadUsinas) {
                if (usina.reg == "D")
                    cotaRelativa = usina.cotaMax;
                else {
                    vIni = usina.uhBase * (usina.volMax - usina.volMin) / 100;
                    //vIni = (vIni == 0) ? 0.01m : vIni;
                    if (vIni != 0) {
                        vUtil = vIni + usina.volMin;
                        v1 = vUtil;
                        v2 = usina.volMin;
                        cotaRelativa = usina.PCV0 * (v1 - v2);
                        cotaRelativa += (usina.PCV1 / 2) * (v1 * v1 - v2 * v2);
                        cotaRelativa += (usina.PCV2 / 3) * (v1 * v1 * v1 - v2 * v2 * v2);
                        cotaRelativa += (usina.PCV3 / 4) * (v1 * v1 * v1 * v1 - v2 * v2 * v2 * v2);
                        cotaRelativa += (usina.PCV4 / 5) * (v1 * v1 * v1 * v1 * v1 - v2 * v2 * v2 * v2 * v2);
                        cotaRelativa = cotaRelativa * (1 / vIni);
                    } else
                        cotaRelativa = usina.cotaMin;
                }

                if (usina.perdaTipo == 1)
                    usina.quedaCalc = (cotaRelativa - usina.canalFugaMed) * (100 - usina.perdaVal) / 100;
                else
                    usina.quedaCalc = cotaRelativa - usina.canalFugaMed - usina.perdaVal;
            }
        }

        public static List<CadUsh> ajustarSubmercado(List<CadUsh> cadUsinas, decimal[] subTarget, decimal[] subTotal) {
            decimal[] media = new decimal[4];
            decimal[] fator = new decimal[4] { 1m, 1m, 1m, 1m };
            List<CadUsh> cadAjustados = new List<CadUsh>();
            //List<infoUsinasFict> infoFict = infoUsinasFictDAO.GetAll();

            decimal erro = 100;
            int itNumber = 0;

            while (erro > 0.00001m && itNumber < 200) {
                cadAjustados.Clear();
                erro = 0;
                foreach (CadUsh ush in cadUsinas) {
                    CadUsh novaUsh = new CadUsh(ush);

                    if (ush.ficSub != 0)
                        novaUsh.uhBase = ush.uhBase * fator[(int)ush.ficSub - 1];
                    else
                        novaUsh.uhBase = ush.uhBase * fator[ush.sistema - 1];

                    if (novaUsh.uhBase > 100) novaUsh.uhBase = 100;
                    if (novaUsh.uhBase < 0) novaUsh.uhBase = 0;

                    if (novaUsh.codUsina == 291 && novaUsh.uhBase > 55) novaUsh.uhBase = 55;
                    cadAjustados.Add(novaUsh);
                }
                atualizaQueda(cadAjustados);
                CadUsh.calculaSomaProd(cadAjustados);
                media = CadUsh.calculaTotalSub(cadAjustados);
                for (int x = 0; x < 4; x++) {
                    media[x] = media[x] / subTotal[x];
                    erro = erro + Math.Abs(media[x] - subTarget[x]);
                    fator[x] = (subTarget[x] / media[x]) * fator[x];
                }

                itNumber++;
            }
            return cadAjustados;
        }
    }
}
