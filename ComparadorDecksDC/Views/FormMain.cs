using CapturaNW.Views;
using AutoPrevs.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComparadorDecksDC.Views {
    public partial class FormMain : Form {
        
        public FormMain() {
            InitializeComponent();
            //calendar1.TodayDate = DateTime.Now;
        }

        #region ShowForm
        public void showFormComparar() {
            FormComparar m_objFormComparar = new FormComparar();
            m_objFormComparar.MdiParent = this;
            m_objFormComparar.Show();
            if (m_objFormComparar.WindowState == FormWindowState.Minimized) {
                m_objFormComparar.WindowState = FormWindowState.Normal;
            }
        }

        private void carregarDeckToolStripMenuItem_Click(object sender, EventArgs e) {
            FormMainNW m_objFormCarregaNW = new FormMainNW();
            m_objFormCarregaNW.MdiParent = this;
            m_objFormCarregaNW.Show();
            if (m_objFormCarregaNW.WindowState == FormWindowState.Minimized) {
                m_objFormCarregaNW.WindowState = FormWindowState.Normal;
            }
        }

        private void carregarDeckToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormCarrega m_objFormCarrega = new FormCarrega();
            m_objFormCarrega.MdiParent = this;
            m_objFormCarrega.Show();
            if (m_objFormCarrega.WindowState == FormWindowState.Minimized) {
                m_objFormCarrega.WindowState = FormWindowState.Normal;
            }
        }

        private void rVX1ToolStripMenuItem_Click(object sender, EventArgs e) {
            FormRVX m_objFormRVX = new FormRVX();
            m_objFormRVX.MdiParent = this;
            m_objFormRVX.Show();
            if (m_objFormRVX.WindowState == FormWindowState.Minimized) {
                m_objFormRVX.WindowState = FormWindowState.Normal;
            }
        }

        private void rV0UnicaToolStripMenuItem_Click(object sender, EventArgs e) {
            FormRV0 m_objFormRV0 = new FormRV0();
            m_objFormRV0.MdiParent = this;
            m_objFormRV0.Show();
            if (m_objFormRV0.WindowState == FormWindowState.Minimized) {
                m_objFormRV0.WindowState = FormWindowState.Normal;
            }
        }

        private void multiRV0ToolStripMenuItem_Click(object sender, EventArgs e) {
            FormMultiRV0 m_objFormMultiRV0 = new FormMultiRV0();
            m_objFormMultiRV0.MdiParent = this;
            m_objFormMultiRV0.Show();
            if (m_objFormMultiRV0.WindowState == FormWindowState.Minimized) {
                m_objFormMultiRV0.WindowState = FormWindowState.Normal;
            }
        }

        private void escreverDadgerToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormEscreve m_objFormEscrever = new FormEscreve();
            m_objFormEscrever.MdiParent = this;
            m_objFormEscrever.Show();
            if (m_objFormEscrever.WindowState == FormWindowState.Minimized) {
                m_objFormEscrever.WindowState = FormWindowState.Normal;
            }
        }

        private void carregarPrevsToolStripMenuItem_Click(object sender, EventArgs e) {
            FormCarregaPrevs m_objFormPrevs = new FormCarregaPrevs();
            m_objFormPrevs.MdiParent = this;
            m_objFormPrevs.Show();
        }

        private void escreverPrevsToolStripMenuItem_Click(object sender, EventArgs e) {
            FormEscrevePrevs m_objFormEscPrevs = new FormEscrevePrevs();
            m_objFormEscPrevs.MdiParent = this;
            m_objFormEscPrevs.Show();
        }

        private void atualizarDadosToolStripMenuItem_Click(object sender, EventArgs e) {
            FormAtualizaDados m_objFormDados = new FormAtualizaDados();
            m_objFormDados.MdiParent = this;
            m_objFormDados.Show();
        }

        private void novoPrevsToolStripMenuItem_Click(object sender, EventArgs e) {
            //FormNovoPrevs m_objNewPrevs = new FormNovoPrevs();
            FormNovoPrevsSensib m_objNewPrevs = new FormNovoPrevsSensib();
            m_objNewPrevs.MdiParent = this;
            m_objNewPrevs.Show();
        }

        private void multiBlockToolStripMenuItem_Click(object sender, EventArgs e) {
            FormMultiBlock m_objFormDados = new FormMultiBlock();
            m_objFormDados.MdiParent = this;
            m_objFormDados.Show();
        }

        private void atualizarDadosToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormAtualizaDados m_objForm = new FormAtualizaDados();
            m_objForm.MdiParent = this;
            m_objForm.Show();
        }

        private void carregarDadgerToolStripMenuItem_Click(object sender, EventArgs e) {
            FormCarrega m_objFormCarrega = new FormCarrega();
            m_objFormCarrega.MdiParent = this;
            m_objFormCarrega.Show();
            if (m_objFormCarrega.WindowState == FormWindowState.Minimized) {
                m_objFormCarrega.WindowState = FormWindowState.Normal;
            }
        }

        private void carregarPrevsToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormCarregaPrevs m_objFormPrevs = new FormCarregaPrevs();
            m_objFormPrevs.MdiParent = this;
            m_objFormPrevs.Show();
        }

        private void reservatorioToolStripMenuItem_Click(object sender, EventArgs e) {
            FormReservatorio m_objForm = new FormReservatorio();
            m_objForm.MdiParent = this;
            m_objForm.Show();
        }


        private void compararDadgerToolStripMenuItem_Click(object sender, EventArgs e) {
            FormComparar m_objForm = new FormComparar();
            m_objForm.MdiParent = this;
            m_objForm.Show();
        }

        private void mensalToolStripMenuItem_Click(object sender, EventArgs e) {
            FormDCMensal m_objForm = new FormDCMensal();
            m_objForm.MdiParent = this;
            m_objForm.Show();
        }


        private void rV0ToolStripMenuItem_Click(object sender, EventArgs e) {
            FormDadgnlRV0 form = new FormDadgnlRV0();
            form.MdiParent = this;

            form.Show();
        }

  
        private void rVX1ToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormDadgnlRVX form = new FormDadgnlRVX();
            form.MdiParent = this;

            form.Show();
        }

      #endregion

        private void carregaRelatórioToolStripMenuItem_Click(object sender, EventArgs e) {
            FormCarregaCVU m_objForm = new FormCarregaCVU();
            m_objForm.MdiParent = this;
            m_objForm.Show();
            if (m_objForm.WindowState == FormWindowState.Minimized) {
                m_objForm.WindowState = FormWindowState.Normal;
            }
        }

        private void alterarBlocoCTToolStripMenuItem_Click(object sender, EventArgs e) {
            FormAtualizaCVU m_objForm = new FormAtualizaCVU();
            m_objForm.MdiParent = this;
            m_objForm.Show();
            if (m_objForm.WindowState == FormWindowState.Minimized) {
                m_objForm.WindowState = FormWindowState.Normal;
            }
        }

        private void vazoesCToolStripMenuItem_Click(object sender, EventArgs e) {
            FormVazoesC m_objForm = new FormVazoesC();
            m_objForm.MdiParent = this;
            m_objForm.Show();
            if (m_objForm.WindowState == FormWindowState.Minimized) {
                m_objForm.WindowState = FormWindowState.Normal;
            }
        }

        private void mensalToolStripMenuItem1_Click(object sender, EventArgs e) {
            FormDadgnlMensal form = new FormDadgnlMensal();
            form.MdiParent = this;

            form.Show();
        }
    }
}
