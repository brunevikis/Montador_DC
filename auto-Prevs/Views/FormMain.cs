using AutoPrevs.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPrevs.Views
{
    public partial class FormMain : Form
    {
        private readonly controllerMain _controlador;

        public FormMain()
        {
            InitializeComponent();

            _controlador = new controllerMain(this);
            calendar1.TodayDate = DateTime.Now;
        }

        #region ShowForm
        public void showFormCarregar()
        {
            new FormCarrega().ShowDialog();
        }

        public void showFormCarregarNW()
        {
            new FormMainNW().ShowDialog();
        }

        public void showFormRV0()
        {
            new FormRV0().ShowDialog();
        }

        public void showFormMultiRV0()
        {
            new FormMultiRV0().ShowDialog();
        }

        public void showFormRVx()
        {
            new FormRVX().ShowDialog();
        }

        public void showFormComparar()
        {
            new FormComparar().ShowDialog();
        }

        public void showFormEscrever()
        {
            new FormEscreve().ShowDialog();
        }

        #endregion  

        #region botoes

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            _controlador.clickCarregar();
        }

        private void btnCarregarNW_Click(object sender, EventArgs e)
        {
            _controlador.clickCarregarNW();
        }

        private void btnRv0_Click(object sender, EventArgs e)
        {
            _controlador.clickRV0();
        }

        private void btnMultiRV0_Click(object sender, EventArgs e)
        {
            _controlador.clickMultiRV0();
        }

        private void btnRVx_Click(object sender, EventArgs e)
        {
            _controlador.clickRVx();
        }

        private void btnEscrever_Click(object sender, EventArgs e)
        {
            _controlador.clickEscrever();
        }
        #endregion
    }
}
