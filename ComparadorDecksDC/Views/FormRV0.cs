using CapturaNW.Factory;
using CapturaNW.Modelagem;
using ComparadorDecksDC.Controller;
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
using ToolBox.Forms;
using ToolBox.Helpers;

namespace ComparadorDecksDC.Views {
    public partial class FormRV0 : FormBasic {
        private controllerRV0 _controlador;

        // List<Deck> dgvSource;

        public FormRV0() {
            InitializeComponent();

            _controlador = new controllerRV0(this);
            DataGridViewHelper.ApplyDefaults(this.dgvDeck);
            DataGridViewHelper.ApplyDefaults(this.dgvOutro);
            DataGridViewHelper.ApplyDefaults(this.dgvNW);


            txtFolder.Enabled = true;
            txtNWFolder.Enabled = true;
            txtFolder.Text = "C:\\Decomp_Semanal\\";
            txtNWFolder.Text = "C:\\Decomp_Semanal\\";

            lstExcept.DisplayMember = "textShow";

            this.dgvDeck.ApplyFilters();
            this.dgvOutro.ApplyFilters();
            this.dgvNW.ApplyFilters();
        }

        private void dgvDeck_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            Semanas sBase = SemanasDAO.GetBySemanaInicial(this.deck.ano, this.deck.mes, this.deck.dia);

            this.txtTE.Text = this.deck.te.Substring(2);
        }

        private void rbManual_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoExcecao == 1) {
                this.dgvOutro.Visible = true;
                this.txtManual.Visible = false;
            } else if (this.tipoExcecao == 2) {
                this.dgvOutro.Visible = false;
                this.txtManual.Visible = true;
            }
        }

        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoDeckNW == DeckSource.Banco) {
                this.dgvNW.Visible = true;
                this.txtNWFolder.Visible = false;
            } else if (this.tipoDeckNW == DeckSource.Arquivo) {
                this.dgvNW.Visible = false;
                this.txtNWFolder.Visible = true;
            }
        }


        private async void btnEscrever_Click(object sender, EventArgs e) {
            try {
                btnEscrever.Enabled = false;
                IsLoading = true;

                await _controlador.gerarRV0Async();

            } catch (Exception ex) {

                ToolBox.Messages.Error(this, "", ex, true);

            } finally {

                IsLoading = false;
                btnEscrever.Enabled = true;
            }


        }

        private void btnAddExc_Click(object sender, EventArgs e) {
            _controlador.addException();
        }

        #region Variaveis Formulario

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

        public String caminho {
            get { return txtFolder.Text; }
            set { txtFolder.Text = value; }
        }

        public String te {
            get { return txtTE.Text; }
            set { txtTE.Text = value; }
        }

        public int mes {
            get {
                if (String.Equals(txtMes.Text, String.Empty))
                    return 0;
                return int.Parse(txtMes.Text);
            }
            set { txtMes.Text = value.ToString(); }
        }

        public int ano {
            get {
                if (String.Equals(txtAno.Text, String.Empty))
                    return 0;
                return int.Parse(txtAno.Text);
            }
            set { txtAno.Text = value.ToString(); }
        }

        public String nome {
            get { return txtNome.Text; }
            set { txtNome.Text = value; }
        }

        public String descricao {
            get { return txtDesc.Text; }
            set { txtDesc.Text = value; }
        }

        // Parte das exceções
        public int tipoExcecao {
            // Outro Deck = 1, Input = 2
            get { return rbOutro.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbOutro.Checked = true;
                else
                    rbManual.Checked = true;
            }
        }

        public string nomeExcecao {
            get { return cbExcept.Text; }
            set { cbExcept.Text = value; }
        }

        public Deck deckExcept {
            get {
                if (dgvOutro.SelectedRows.Count == 0 || dgvOutro.SelectedRows.Count > 1)
                    return null;
                else {
                    return (Deck)dgvOutro.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }

        public string[] txtExcept {
            get { return txtManual.Lines; }
            set { txtManual.Lines = value; }
        }

        public List<Excecao> exceptions {
            get {
                List<Excecao> lst = new List<Excecao>();
                foreach (Excecao ex in lstExcept.Items)
                    lst.Add(ex);

                return lst;
            }
            set { }
        }

        //Parte do NW
        public DeckSource tipoDeckNW {
            get { return rbBanco.Checked ? DeckSource.Banco : DeckSource.Arquivo; }
            set {
                switch (value) {
                    case DeckSource.Banco:
                        rbBanco.Checked = true;
                        break;
                    case DeckSource.Arquivo:
                        rbPasta.Checked = true;
                        break;
                }
            }
        }

        public DeckNW deckNW {
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

        #endregion

        #region Lista

        public void LimpaLista() {
            lstExcept.Items.Clear();
        }

        public void RemoveFromList(Object o) {
            lstExcept.Items.Remove(o);
        }

        public void AddToList(Object o) {
            lstExcept.Items.Add(o);
        }

        private void lstExcept_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete && lstExcept.SelectedIndex >= 0) {
                lstExcept.Items.RemoveAt(lstExcept.SelectedIndex);
            }
        }
        #endregion

        private void rbOutro_CheckedChanged(object sender, EventArgs e) {

        }

        private async void FormRV0_Load(object sender, EventArgs e) {

            try {
                IsLoading = true;
                btnEscrever.EnterLoadingState();
                
                await Task.WhenAll(LoadDecksAsync(), LoadDeckNWsAsync());

                btnEscrever.ExitLoadingState();

            } catch (Exception ex) {
                ToolBox.Messages.Error(this, "", ex, true);
            } finally {
                IsLoading = false;
                

            }
        }

        async Task LoadDecksAsync() {
            dgvDeck.EnterLoadingState();
            dgvOutro.EnterLoadingState();
            var dgvSource = await DeckDAO.GetAllAsync();
            dgvDeck.DataSource = dgvSource.ToList().ToBindingSource();
            dgvDeck.ExitLoadingState();
            dgvOutro.DataSource = dgvSource.ToList().ToBindingSource();
            dgvOutro.ExitLoadingState();
        }
        async Task LoadDeckNWsAsync() {
            dgvNW.EnterLoadingState();
            var dgvNWSource = await DeckNWDAO.GetAllAsync();
            dgvNW.DataSource = dgvNWSource.ToList().ToBindingSource();
            dgvNW.ExitLoadingState();
        }
    }
}