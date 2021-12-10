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
using System.Windows.Forms;
using ToolBox.Forms;
using ToolBox.Helpers;

namespace ComparadorDecksDC.Views
{
    public partial class FormMultiRV0 : FormBasic
    {
        private controllerRV0 _controlador;
        private string resp;

        public FormMultiRV0()
        {
            InitializeComponent();

            _controlador = new controllerRV0();
            txtNWFolder.Enabled = true;

            //Inicializando o DataGrid
            DataGridViewHelper.ApplyDefaults(this.dgvDeck);
            dgvDeck.ApplyFilters();
            dgvDeck.DataSource = DeckDAO.GetAll().ToBindingSource();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            pgbProcessoOperacao.Visible = true;
            btnGerar.Visible = false;
            backgroundWorker1.RunWorkerAsync();
        }

        #region Variaveis Formulario

        public Deck deck
        {
            get
            {
                if (dgvDeck.SelectedRows.Count == 0 || dgvDeck.SelectedRows.Count > 1)
                    return null;
                else
                {
                    return (Deck)dgvDeck.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }
        
        public String caminhoNW
        {
            get { return txtNWFolder.Text; }
            set { txtNWFolder.Text = value; }
        }

        #endregion

        #region bgworker
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgbProcessoOperacao.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pgbProcessoOperacao.Visible = false;
            btnGerar.Visible = true;
            this.showWarning(resp);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            resp = _controlador.gerarMultiRV0( this.deck.id, this.caminhoNW, backgroundWorker1);
        }
        #endregion
    }
}
