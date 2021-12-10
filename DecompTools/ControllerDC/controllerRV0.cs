using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using DecompTools.Views;
using DecompTools.Util;
using DecompTools.ControllerNW;
using DecompTools.ModelagemNW;
using DecompTools.FactoryNW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DecompTools.ControllerDC {
    public class controllerRV0 {
        private readonly FormRV0 _tela;

        public controllerRV0(FormRV0 tela) {
            _tela = tela;
        }

        public controllerRV0() {
        }


        /// <summary>
        /// Gerar todos os dadgers de um estudo do encadeado(Adapter).
        /// </summary>
        /// <param name="idDeck">Id do dadger base, que será usado para gerar os proximos dadgers</param>
        /// <param name="caminhoNW">Caminho do estudo do encadeado</param>
        /// <param name="bgw">backgroundworker, para atualizar a posição </param>
        /// <param name="primeira">flag para identificar se deve ser realizado o processo para o primeiro mes.</param>
        /// <returns></returns>
        public string gerarMultiRV0(int idDeck, string caminhoNW, BackgroundWorker bgw = null, Boolean primeira = false) {
            string caminho;
            int anoP, mesP, idDeckNW;
            double total;

            Deck deckBase = DeckDAO.getAllBlocksbyID(idDeck); //Primeiro deck base
            Semanas sBase = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);

            //Rodar para todas as pastas dentro do diretorio raiz
            DirectoryInfo dir = new DirectoryInfo(caminhoNW);

            total = dir.GetDirectories().Length;
            double i = 0;
            foreach (DirectoryInfo d in dir.GetDirectories().Select(fn => new DirectoryInfo(fn.FullName)).OrderBy(f => f.Name)) {
                if (bgw != null)
                    bgw.ReportProgress(Convert.ToInt32(((0.33 + i) / total) * 100));


                if (i == 0 && primeira)                         //Se primeira = true, pula o primeiro mes.
                    primeira = false;
                else {
                    caminho = d.FullName;
                    try {
                        anoP = int.Parse(d.Name.Substring(0, 4));
                        mesP = int.Parse(d.Name.Substring(4, 2));
                    } catch (FormatException) {
                        return "Pasta base contem uma pasta que não esta no formato \"AAAAMM\" ";
                    } catch (Exception ex) {
                        return ex.Message;
                    }

                    string caminhoDadger = Path.Combine(d.FullName, "decomp");
                    Directory.CreateDirectory(caminhoDadger);
                    Deck deckNew = new Deck();
                    DeckNW deckNW = new DeckNW();

                    Semanas s = SemanasDAO.GetByMesAno(mesP, anoP);

                    string foo = controllerCarregaNW.CarregaDeckNW(caminho, String.Concat("Deck encadeado ", d.Name), String.Concat("DeckNW carregado automaticamente no processo encadeado mes: ", d.Name), false);
                    if (!int.TryParse(foo, out idDeckNW))
                        _tela.showError("Problema ao carregar deck NW {0}".Replace("{0}", foo));

                    deckNW = DeckNWDAO.getAllBlocksbyID(idDeckNW);

                    if (bgw != null)
                        bgw.ReportProgress(Convert.ToInt32(((0.66 + i) / total) * 100));

                    //Copia todos os blocos do deckBase para o novo deck
                    deckNew.clone(deckBase);

                    //Atualizando RV0
                    atualizarRV0(deckNew, deckBase, deckNW, s, sBase);

                    //Seta as informações do deck e salva o novo deck
                    deckNew.id_deckNW = idDeckNW;
                    deckNew.id_deckDC_base = deckBase.id;
                    deckNew.nome = String.Concat("Encadeado ", d.Name);
                    deckNew.descricao = String.Concat("Encadeado ", d.Name);
                    deckNew.caminho = caminhoDadger;
                    deckNew.te = String.Concat("TE  ", UtilitarioDeData.NomeMes(mesP), " - ", UtilitarioDeData.NomeMes(mesP + 1), " Encadeado ", d.Name);
                    deckNew.rev = 0;
                    deckNew.ano = s.primeiraSemana.Year;
                    deckNew.mes = s.primeiraSemana.Month;
                    deckNew.dia = s.primeiraSemana.Day;
                    deckNew.save();

                    deckNew.escreveDeck(caminhoDadger);

                    deckBase = deckNew;
                    sBase = s;
                }

                i++;
                if (bgw != null)
                    bgw.ReportProgress(Convert.ToInt32((i / total) * 100));

            }

            return "Todos os decks foram gerados com sucesso";
        }


        /// <summary>
        /// Gerar uma nova RV0 com os dados da tela.
        /// </summary>
        public async Task gerarRV0Async() {
            if (_tela.ano == 0 || _tela.mes == 0) {
                _tela.showError("A data do novo deck deve ser informada.");
            } else {
                Deck deckBase = DeckDAO.getAllBlocksbyID(_tela.deck.id);
                Deck deckNew = new Deck();
                DeckNW deckNW = null;// new DeckNW();
                int idDeckNW = 0;

                Semanas s = SemanasDAO.GetByMesAno(_tela.mes, _tela.ano);
                Semanas sBase = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);

                if (_tela.tipoDeckNW == DeckSource.Banco) {
                    idDeckNW = _tela.deckNW.id;
                    deckNW = DeckNWDAO.getAllBlocksbyID(idDeckNW);
                    deckNew.id_deckNW = idDeckNW;
                } else {
                    deckNW = controllerCarregaNW.LerDeck(_tela.caminhoNW);
                }

                deckNew.id_deckDC_base = deckBase.id;

                List<Excecao> lstExcept = _tela.exceptions;

                //Copia todos os blocos do deckBase para o novo deck
                deckNew.clone(deckBase);

                //Seta as informações do deck e salva o novo deck
                deckNew.nome = _tela.nome;
                deckNew.descricao = _tela.descricao;
                deckNew.caminho = _tela.caminho;
                deckNew.te = String.Concat("TE  ", _tela.te);
                deckNew.rev = 0;
                deckNew.ano = s.primeiraSemana.Year;
                deckNew.mes = s.primeiraSemana.Month;
                deckNew.dia = s.primeiraSemana.Day;

                var cvu = _tela.RelatorioCVU;
                List<DeParaNomePosto> deparas = null;
                if (cvu.HasValue) deparas = _tela.DeParasCVU;



                await Task.Factory.StartNew(() => {

                    //Atualizando RV0
                    atualizarRV0(deckNew, deckBase, deckNW, s, sBase);

                    //Apos atualizar, carrega as exceções
                    trataExcecoes(deckNew, lstExcept, deckNW, s);

                    if (cvu.HasValue) {
                        trataCVU(deckNew, cvu.Value, deparas);
                    }

                });



                deckNew.escreveDeck(_tela.caminho);
                _tela.showWarning("Deck gerado com sucesso");

                if (MessageBox.Show("Deseja salvar novo deck em banco de dados", "Salvar deck", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                    if (idDeckNW == 0) {
                        int nwId;
                        var foo = controllerCarregaNW.CarregaDeckNW(_tela.caminhoNW, _tela.nome, "DeckNW carregado automaticamente no processo do deck {0}".Replace("{0}", _tela.nome), false);
                        if (!int.TryParse(foo, out nwId))
                            _tela.showError("Problema ao carregar deck NW {0}".Replace("{0}", foo));
                        else idDeckNW = nwId;
                    }

                    if (idDeckNW != 0) {
                        deckNew.id_deckNW = idDeckNW;
                        deckNew.save();
                    }
                }
            }
        }

        private void trataCVU(Deck deckNew, int cvuId, List<DeParaNomePosto> deParas) {

            var tsk = RelatorioCVUDAO.GetByIDAsync(cvuId);
            tsk.Wait();
            var cvu = tsk.Result;

            var acoes = DecompTools.ControllerDC.controllerCVU.AtualizaDeck(deckNew, cvu, deParas);
        }

        /// <summary>
        /// Dada uma lista de excecoes e o deck que esta sendo gerado, atualiza os blocos de acordo com a excecao.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="lstExcept"></param>
        public void trataExcecoes(Deck deck, List<Excecao> lstExcept, DeckNW deckNW, Semanas s) {
            int numLinha = 0;

            foreach (Excecao ex in lstExcept) {
                PropertyInfo block = deck.GetType().GetProperty(ex.bloco.ToLower());
                //block.SetValue(deck, null, null);
                

                if (ex.tipo == 1) {
                    Deck deckBase = DeckDAO.getAllBlocksbyID(ex.idDeck);
                   // deck.clone(deckBase, ex.bloco);

                    Semanas sBase = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);

                    Type t = Type.GetType("DecompTools.ModelagemDC." + ex.bloco);
                    var atualizarRV0Opcional = t.GetMethod("atualizarRV0Opcional");
                    if (atualizarRV0Opcional != null)
                    {
                        atualizarRV0Opcional.Invoke(null, new object[] { deck, deckBase, deckNW, s, sBase });//Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase
                    }
                    else
                    {
                        deckBase = DeckDAO.getAllBlocksbyID(ex.idDeck);
                        deck.clone(deckBase, ex.bloco);
                    }

                } else {
                    block.SetValue(deck, null, null);
                    deck.add(ex.conteudo, ex.bloco, numLinha);
                }
            }
        }

        /// <summary>
        /// Faz o papel de atualizar bloco a bloco, inicialmente os blocos sem inteligencia e depois os "blocos inteligentes"
        /// </summary>
        /// <param name="deck">Deck a ser atualizado</param>
        /// <param name="deckBase">Deck utilizado como base para o novo deck</param>
        /// <param name="deckNW">Deck Newave base para a atualização</param>
        /// <param name="s">Semana do novo deck</param>
        /// <param name="sBase">Semana do deck base</param>
        public void atualizarRV0(Deck deck, Deck deckBase, DeckNW deckNW, Semanas s, Semanas sBase) {
            int nSemanasAtual = s.semanas;
            int nSemanasBase = sBase.semanas - deckBase.rev;
            int mesAtual = s.mes;
            int mesBase = sBase.mes;

            if (nSemanasAtual != nSemanasBase) {
                foreach (MP mp in deck.mp) mp.atualizarRV0(nSemanasAtual, nSemanasBase);
                foreach (MT mt in deck.mt) mt.atualizarRV0(nSemanasAtual, nSemanasBase);
                foreach (FD fd in deck.fd) fd.atualizarRV0(nSemanasAtual, nSemanasBase);
                //foreach (VE ve in deck.ve) ve.atualizarRV0(nSemanasAtual, nSemanasBase);
                foreach (TI ti in deck.ti) ti.atualizarRV0(nSemanasAtual, nSemanasBase);
                foreach (RQ rq in deck.rq) rq.atualizarRV0(nSemanasAtual, nSemanasBase);

                RHA.atualizarRV0(deck, nSemanasAtual, nSemanasBase, "RHA");
                RHE.atualizarRV0(deck, nSemanasAtual, nSemanasBase, "RHE");
                RHQ.atualizarRV0(deck, nSemanasAtual, nSemanasBase, "RHQ");
                RHV.atualizarRV0(deck, nSemanasAtual, nSemanasBase, "RHV");
                RI.atualizarRV0(deck, s, sBase);
                //IT.atualizarRV0(deck, s, sBase);
            }

            VR.atualizarRV0(deck, s);
            CD.atualizarRV0(deck, s, sBase, deckNW);
            PQ.atualizarRV0(deck, s, sBase, deckNW);
            //EA.atualizarRV0(deck, s, sBase, deckNW);
            CT.atualizarRV0(deck, s, sBase, deckNW);
            DP.atualizarRV0(deck, deckBase, deckNW, s, sBase);

            VE.atualizarRV0(deck, deckBase, deckNW, s, sBase);

            //Atualizar Bloco AC
            //foreach (AC ac in deck.ac) ac.atualizarRV0(mesAtual, mesBase);

            AC.atualizarRV0(deck, deckBase, deckNW, s, sBase);
        }

        /// <summary>
        /// Trata uma excecao da tela, inserindo na lista de excecoes presente na tela.
        /// </summary>
        public void addException() {
            if (String.Equals(_tela.nomeExcecao, String.Empty))
                _tela.showError("Escolha o nome da exceção.");
            else if (_tela.tipoExcecao == 1 && _tela.deckExcept == null)
                _tela.showError("Escolha o deck da exceção.");
            else if (_tela.tipoExcecao == 2 && _tela.txtExcept.Length == 0)
                _tela.showError("Digite o novo valor para a exceção");
            else {
                for (int i = 0; i < _tela.exceptions.Count; i++)
                    if (_tela.exceptions[i].bloco == _tela.nomeExcecao) {
                        var result = MessageBox.Show("Deseja substituir a exceção já existente deste bloco?", "Confirmação",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                            return;
                        else
                            _tela.RemoveFromList(_tela.exceptions[i]);
                    }

                Excecao ex = new Excecao(_tela);
                _tela.AddToList(ex);

                _tela.txtExcept = null;
                _tela.nomeExcecao = null;
            }
        }
    }
}
