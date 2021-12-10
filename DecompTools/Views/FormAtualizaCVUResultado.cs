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

namespace DecompTools.Views {
    public partial class FormAtualizaCVUResultado : FormBasic {

        public string Resultado { get { return txtResultado.Text; } set { txtResultado.Text = value; } }

        public FormAtualizaCVUResultado() {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnAceitar_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }
    }
}
