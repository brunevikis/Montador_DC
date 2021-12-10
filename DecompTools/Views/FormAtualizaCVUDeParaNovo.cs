using DecompTools.ModelagemDC;
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
    public partial class FormAtualizaCVUDeParaNovo : FormBasic {

        public DeParaNomePosto DePara { get { return new DeParaNomePosto { De = txtDe.Text, Para = txtPara.Text, DataAtualizacao = DateTime.Now }; } }

        public FormAtualizaCVUDeParaNovo() {
            InitializeComponent();
        }

        public FormAtualizaCVUDeParaNovo(string de)
            : this() {
            txtDe.Text = de;
            txtPara.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e) {

            if (!string.IsNullOrWhiteSpace(txtDe.Text) || !string.IsNullOrWhiteSpace(txtPara.Text)) {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            } else {
                showError("Preencha todos os campos");
            }


        }

        private void btnIgnorar_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.Close();
        }


    }
}
