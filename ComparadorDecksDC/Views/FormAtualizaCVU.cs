using ComparadorDecksDC.Factory;
using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolBox;
using ToolBox.Forms;

namespace ComparadorDecksDC.Views {
    public partial class FormAtualizaCVU : FormBasic {

        Task<List<DeParaNomePosto>> _deParasTsk = null;

        public string DeckPath {
            get {
                return this.txtDeck.Text;
            }
        }

        public RelatorioCVU CVU {
            get {
                return (RelatorioCVU)cbxCVUs.SelectedItem;
            }
        }

        public string SumarioAtualizacao {
            set {
                FormAtualizaCVUResultado frm = new FormAtualizaCVUResultado();
                frm.Resultado = value;
                frm.ShowDialog(this);

            }
        }

        public DeParaNomePosto GetDePara(string de) {
            FormAtualizaCVUDeParaNovo frm = new FormAtualizaCVUDeParaNovo(de);
            frm.StartPosition = FormStartPosition.CenterScreen;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                return frm.DePara;
            } else
                return null;
        }

        public bool ConfirmarAlteracao(string alteracoes) {
            FormAtualizaCVUResultado frm = new FormAtualizaCVUResultado();
            frm.Resultado = alteracoes;
            var result = frm.ShowDialog(this);

            return result == System.Windows.Forms.DialogResult.Yes;
        }

        public FormAtualizaCVU() {
            InitializeComponent();
            txtDeck.Enabled = true;
        }

        private async void FormAtualizaCVU_Load(object sender, EventArgs e) {
            try {
                IsLoading = true;
                btnAtualizar.Enabled = false;

                _deParasTsk = DeParaNomePostoDAO.GetAllAsync();

                await LoadCVUs();




                btnAtualizar.Enabled = true;
            } catch (Exception ex) {
                Messages.Error(this, ex.Message, ex, true);
            } finally {
                IsLoading = false;
            }
        }

        private async Task LoadCVUs() {
            cbxCVUs.EnterLoadingState();

            cbxCVUs.DataSource = (await RelatorioCVUDAO.GetAllAsync()).OrderByDescending(x => x.Id).ToList();
            cbxCVUs.ValueMember = "Id";
            cbxCVUs.DisplayMember = "";

            //to do

            cbxCVUs.ExitLoadingState();
        }

        private async void btnAtualizar_Click(object sender, EventArgs e) {
            try {
                IsLoading = true;
                btnAtualizar.Enabled = false;

                var deParas = await _deParasTsk;

                var deckPath = this.DeckPath;

                var deck = await Task.Factory.StartNew(() => Controller.controllerCarrega.lerDeck(deckPath));
                var cvu = await RelatorioCVUDAO.GetByIDAsync((int)cbxCVUs.SelectedValue);

                var blocoCT = deck.ct;

                var acoes = new List<string>();

                foreach (var cvudetalhe in cvu.Detalhes) {

                    float cvuValue;

                    if (!float.TryParse(cvudetalhe.CVU_PMO, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.GetCultureInfo("pt-BR").NumberFormat, out cvuValue)) {
                        acoes.Add(cvudetalhe.Empreendimento + " - não possivel converter cvu: " + cvudetalhe.CVU_PMO);
                        continue;
                    }

                    if (cvuValue == 0) {
                        continue;
                    }


                    var deParas2 = deParas.Where(d => d.De.ToUpper() == cvudetalhe.Empreendimento.ToUpper()).Select(d => d.Para.ToUpper()).ToList();

                    //deParas2.Insert(0, cvudetalhe.Empreendimento.ToUpper());

                    var ct = blocoCT.Where(c => deParas2.Contains(c.campo1.ToUpper().Trim())).ToList();


                    // Somente aceita cadastrar novo de para se nenhum cadastrado
                    if (ct.Count == 0 && deParas2.Count == 0) {
                        DeParaNomePosto novoDePara = null;


                        novoDePara = GetDePara(cvudetalhe.Empreendimento);
                        //se não ignorou, tenta a busca novamente e grava o de-para
                        if (novoDePara != null) {
                            deParas.Add(novoDePara);
                            ct.AddRange(blocoCT.Where(c => novoDePara.Para.ToUpper().Equals(c.campo1.ToUpper())).ToList());
                            novoDePara.save();
                        }
                        else
                            acoes.Add(cvudetalhe.Empreendimento + " ignorado");    
                    }

                    foreach (var ctline in ct) {

                        var novocvu = cvuValue.ToString("#.00", System.Globalization.NumberFormatInfo.InvariantInfo);

                        if (novocvu == ctline.campo13) {
                            continue;
                        }
                        acoes.Add(cvudetalhe.Empreendimento + " (" + ctline.campo1 + " - " + ctline.campo3 + ") alterdado. " + ctline.campo13 + " -> " + novocvu);

                        ctline.campo13 = ctline.campo10 = ctline.campo7 = novocvu;
                    }
                }

                if (ConfirmarAlteracao(string.Join("\r\n", acoes))) {
                    deck.escreveDeck(System.IO.Path.GetDirectoryName(deckPath), System.IO.Path.GetFileName(deckPath));
                }

            } catch (Exception ex) {
                Messages.Error(this, ex.Message, ex, true);
            } finally {
                IsLoading = false;
                btnAtualizar.Enabled = true;
            }
        }
    }
}
