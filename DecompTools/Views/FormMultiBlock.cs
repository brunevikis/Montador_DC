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
using System.Windows.Forms;
using ToolBox.Forms;
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormMultiBlock : FormBasic {
        private controllerMultiBlock _controlador;
        private string resp;

        public FormMultiBlock() {
            InitializeComponent();

            _controlador = new controllerMultiBlock();
            txtDeckPreliminar.Enabled = true;
            txtDeckOficial.Enabled = true;
            txtPath.Enabled = true;
        }

        private void btnGerar_Click(object sender, EventArgs e) {
            pgbProcessoOperacao.Visible = true;
            btnGerar.Visible = false;
            backgroundWorker1.RunWorkerAsync();
        }

        #region Variaveis Formulario

        public string caminhoDeckPrelim {
            get { return txtDeckPreliminar.Text; }
            set { txtDeckPreliminar.Text = value; }
        }

        public string caminhoDeckOficial {
            get { return txtDeckOficial.Text; }
            set { txtDeckOficial.Text = value; }
        }

        public string caminhoSaida {
            get { return txtPath.Text; }
            set { txtPath.Text = value; }
        }


        #endregion

        #region bgworker
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            pgbProcessoOperacao.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            pgbProcessoOperacao.Visible = false;
            btnGerar.Visible = true;
            this.showWarning(resp);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            resp = _controlador.MultiBlock(this.caminhoDeckPrelim, this.caminhoDeckOficial, this.caminhoSaida, backgroundWorker1);
        }
        #endregion
    }
}
