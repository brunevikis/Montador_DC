using DecompTools.FactoryNW;
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
using System.Windows.Forms;
using ToolBox.Forms;
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormRVX : FormBasic {
        private controllerRVX _controlador;

        public FormRVX() {
            InitializeComponent();
            _controlador = new controllerRVX();

            //Inicializando o DataGrid
            txtFolder.Enabled = true;
            txtArquivo.Enabled = true;
            txtFolder.Text = "C:\\Decomp_Semanal\\";
            DataGridViewHelper.ApplyDefaults(this.dgvDeck);
            this.dgvDeck.ApplyFilters();
            dgvDeck.DataSource = DeckDAO.GetAll().ToBindingSource();
        }

        private void btnEscrever_Click(object sender, EventArgs e) {
            int id = this.deck.id;
            Deck deckBase;

            if (this.tipoDeck == DeckSource.Banco)
                deckBase = DeckDAO.getAllBlocksbyID(id);
            else
                deckBase = controllerCarrega.lerDeck(this.arquivo);

            if (_controlador.RVX(deckBase, this.caminho))
                this.showWarning("Deck gerado com sucesso.");
            else
                this.showError("Problemas na geração do deck.");
        }

        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoDeck == DeckSource.Banco) {
                this.dgvDeck.Visible = true;
                this.txtArquivo.Visible = false;
            } else if (this.tipoDeck == DeckSource.Arquivo) {
                this.dgvDeck.Visible = false;
                this.txtArquivo.Visible = true;
            }
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

        public String arquivo {
            get { return txtArquivo.Text; }
            set { txtArquivo.Text = value; }
        }

        public DeckSource tipoDeck {
            // Deck DB = 1, Pasta = 2
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

        #endregion
    }
}