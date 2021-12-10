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

namespace DecompTools.Views
{
    public partial class FormEscreve : FormBasic {
        private controllerEscreve _controlador;

        public FormEscreve() {
            InitializeComponent();

            _controlador = new controllerEscreve(this);

            //Inicializando o DataGrid
            DataGridViewHelper.ApplyDefaults(this.dgvDeck);
            dgvDeck.ApplyFilters();
            txtFolder.Enabled = true;
            txtFolder.Text = "C:\\Decomp_Semanal\\";
            dgvDeck.DataSource = DeckDAO.GetAll().ToBindingSource();
        }

        private void btnEscrever_Click(object sender, EventArgs e) {
            _controlador.escreverDeck();
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

        #endregion
    }
}