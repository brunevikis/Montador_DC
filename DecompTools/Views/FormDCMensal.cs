using DecompTools.ControllerNW;
using DecompTools.FactoryNW;
using DecompTools.ModelagemNW;
using DecompTools.ControllerDC;
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
using ToolBox.Forms;
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormDCMensal : FormBasic {
        public FormDCMensal() {
            InitializeComponent();
            txtExitPath.Enabled = true;
            txtNWFolder.Enabled = true;


            DataGridViewHelper.ApplyDefaults(this.dgvDeck);
            DataGridViewHelper.ApplyDefaults(this.dgvNW);
            dgvDeck.ApplyFilters();
            dgvNW.ApplyFilters();
            //txtExitPath.Text = "C:\\Users\\alexandre.carvalho.CPASS\\Downloads\\decomp\\testeMensal\\";
        }

        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (tipoDeckNW == 1) {
                dgvNW.Visible = true;
                txtNWFolder.Visible = false;
            } else if (tipoDeckNW == 2) {
                dgvNW.Visible = false;
                txtNWFolder.Visible = true;
            }
        }

        private async void btnGerar_Click(object sender, EventArgs e) {
            try {
                IsLoading = true;
                btnGerar.EnterLoadingState();
                int idDeckNW;
                if (this.tipoDeckNW == 1)
                    idDeckNW = this.deckNw.id;
                else {
                    string foo = controllerCarregaNW.CarregaDeckNW(this.caminhoNW, "{0}", "DeckNW carregado automaticamente no processo do deck {0}", false);
                    if (!int.TryParse(foo, out idDeckNW))
                        this.showError("Problema ao carregar deck NW." + foo);
                }

                Deck deckBase = DeckDAO.getAllBlocksbyID(this.deck.id);
                DeckNW deckNWBase = DeckNWDAO.getAllBlocksbyID(idDeckNW);
                String EnaP = this.ENAPast;
                String EnaF = this.ENAFut;
                String Reserv = this.Reservatorio;
                DateTime dataInicio = new DateTime(this.anoIni, this.mesIni, 1);
                DateTime dataFim = new DateTime(this.anoFim, this.mesFim, 1);
                var caminhoSaida = this.CaminhoSaida;
                //Deck deckBase = DeckDAO.getAllBlocksbyID(479);
                //DeckNW deckNWBase = DeckNWDAO.getAllBlocksbyID(215);
                int[] configBlocos = new int[5] { this.VItipo, this.QItipo, this.ITtipo, this.MPtipo, this.UHtipo };

                showWarning(await controllerMensal.gerarMensalAsync(deckBase, deckNWBase, EnaP, EnaF, Reserv, dataInicio, dataFim, caminhoSaida, configBlocos));

            } catch (Exception ex) {
                ToolBox.Messages.Error(this, "", ex, true);
            } finally {
                btnGerar.ExitLoadingState();
                IsLoading = false;
            }
        }

        #region Variaveis Formulario

        //Dadger Base
        public Deck deck {
            get {
                if (dgvDeck.SelectedRows.Count == 0 || dgvDeck.SelectedRows.Count > 1)
                    return null;
                else {
                    return (Deck)dgvDeck.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }


        //Parte NW
        public int tipoDeckNW {
            // Deck DB = 1, Pasta = 2
            get { return rbBanco.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbBanco.Checked = true;
                else
                    rbBanco.Checked = true;
            }
        }

        public DeckNW deckNw {
            get {
                if (dgvNW.SelectedRows.Count == 0 || dgvNW.SelectedRows.Count > 1)
                    return null;
                else {
                    return (DeckNW)dgvNW.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }

        public String caminhoNW {
            get { return txtNWFolder.Text; }
            set { txtNWFolder.Text = value; }
        }

        public String ENAPast {
            get { return txtEna.Text; }
            set { txtEna.Text = value.ToString(); }
        }

        public String ENAFut {
            get { return txtEna2.Text; }
            set { txtEna2.Text = value.ToString(); }
        }

        public String Reservatorio {
            get { return txtReservatorio.Text; }
            set { txtReservatorio.Text = value.ToString(); }
        }

        //Datas
        public int mesIni {
            get {
                if (String.Equals(txtMesIni.Text, String.Empty))
                    return 0;
                return int.Parse(txtMesIni.Text);
            }
            set { txtMesIni.Text = value.ToString(); }
        }

        public int anoIni {
            get {
                if (String.Equals(txtAnoIni.Text, String.Empty))
                    return 0;
                return int.Parse(txtAnoIni.Text);
            }
            set { txtAnoIni.Text = value.ToString(); }
        }

        public int mesFim {
            get {
                if (String.Equals(txtMesFim.Text, String.Empty))
                    return 0;
                return int.Parse(txtMesFim.Text);
            }
            set { txtMesFim.Text = value.ToString(); }
        }

        public int anoFim {
            get {
                if (String.Equals(txtAnoFim.Text, String.Empty))
                    return 0;
                return int.Parse(txtAnoFim.Text);
            }
            set { txtAnoFim.Text = value.ToString(); }
        }

        public String CaminhoSaida {
            get { return txtExitPath.Text; }
            set { txtExitPath.Text = value.ToString(); }
        }

        //Blocos
        public int VItipo {
            // Atual = 1, Sazo = 2
            get { return rbVIAtual.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbVIAtual.Checked = true;
                else
                    rbVISazo.Checked = true;
            }
        }

        public int QItipo {
            // Atual = 1, Sazo = 2
            get { return rbQIAtual.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbQIAtual.Checked = true;
                else
                    rbQISazo.Checked = true;
            }
        }

        public int ITtipo {
            // Atual = 1, Sazo = 2, NW = 3
            get {
                return rbITAtual.Checked ? 1 : (
                    rbITSazo.Checked ? 2 :
                    3
                    );
            }
            set {
                if (value == 1)
                    rbITAtual.Checked = true;
                else if (value == 2)
                    rbITSazo.Checked = true;
                else
                    rbITNW.Checked = true;
            }
        }

        public int MPtipo {
            // Sem Manutenção = 1, Sazo = 2
            get { return rbMPManut.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbMPManut.Checked = true;
                else
                    rbMPSazo.Checked = true;
            }
        }

        public int UHtipo {
            // MLT = 1, Base = 2
            get { return rbUHmlt.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbUHmlt.Checked = true;
                else
                    rbUHbase.Checked = true;
            }
        }

        #endregion

        private async void FormDCMensal_Load(object sender, EventArgs e) {




            try {
                IsLoading = true;
                //Inicializando o DataGrid
                btnGerar.EnterLoadingState();
                dgvDeck.EnterLoadingState();
                dgvNW.EnterLoadingState();

                var t1 = DeckDAO.GetAllAsync();
                var t2 = DeckNWDAO.GetAllAsync();

                await Task.WhenAll(t1, t2);

                dgvDeck.DataSource = (t1.Result).ToBindingSource();
                dgvNW.DataSource = (t2.Result).ToBindingSource();

            } catch (Exception ex) {
                ToolBox.Messages.Error(this, "", ex, true);
            } finally {
                dgvDeck.ExitLoadingState();
                dgvNW.ExitLoadingState();
                btnGerar.ExitLoadingState();
                IsLoading = false;


            }

        }
    }
}
