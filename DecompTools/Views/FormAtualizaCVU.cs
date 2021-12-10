using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
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

namespace DecompTools.Views {
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

                var deck = await Task.Factory.StartNew(() => ControllerDC.controllerCarrega.lerDeck(deckPath));
                var cvu = await RelatorioCVUDAO.GetByIDAsync((int)cbxCVUs.SelectedValue);

                var acoes = DecompTools.ControllerDC.controllerCVU.AtualizaDeck(deck, cvu, deParas);

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
