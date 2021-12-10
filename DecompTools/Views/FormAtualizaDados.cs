using DecompTools.ControllerDC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolBox.Forms;

namespace DecompTools.Views {
    public partial class FormAtualizaDados : FormBasic {
        public FormAtualizaDados() {
            InitializeComponent();
            txtDados.Enabled = true;
            txtDados.Text = "H:\\TI - Sistemas\\PROD\\Decomp Tools\\apoio.xls";
        }

        private void btnAtualizar_Click(object sender, EventArgs e) {
            controllerAtualizaDados _controller = new controllerAtualizaDados();
            this.showWarning(_controller.atualizaDados(this.Caminho));
        }

        #region Variaveis Formulario

        public string Caminho {
            get { return txtDados.Text; }
            set { txtDados.Text = value; }
        }

        #endregion
    }
}
