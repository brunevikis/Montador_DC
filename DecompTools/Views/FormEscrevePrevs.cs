using DecompTools.ControllerPrevs;
using DecompTools.FactoryPrevs;
using DecompTools.ModelagemPrevs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolBox.Helpers;
using ToolBox;
using ToolBox.Forms;

namespace DecompTools.Views {
    public partial class FormEscrevePrevs : FormBasic {
        private controllerEscrevePrevs _controlador;

        public FormEscrevePrevs() {
            InitializeComponent();

            _controlador = new controllerEscrevePrevs();

            //Inicializando o DataGrid
            this.dgvPrevs.ApplyDefaults();
            this.dgvPrevs.ApplyFilters();
            txtFolder.Enabled = true;
            txtFolder.Text = "C:\\Decomp_Semanal\\";



            dgvPrevs.DataSource = PrevsDAO.GetAll().ToBindingSource();
        }

        private void btnEscrever_Click(object sender, EventArgs e) {
            this.showWarning(_controlador.escreverPrevs(this.prevs, this.caminho));
        }

        #region Variaveis Formulario

        public Prevs prevs {
            get {
                if (dgvPrevs.SelectedRows.Count == 0 || dgvPrevs.SelectedRows.Count > 1)
                    return null;
                else {
                    return (Prevs)dgvPrevs.CurrentRow.DataBoundItem;
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