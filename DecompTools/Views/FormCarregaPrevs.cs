using DecompTools.ControllerPrevs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToolBox.Forms;
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormCarregaPrevs : FormBasic {
        private controllerCarregaPrevs _controlador;

        public FormCarregaPrevs() {
            InitializeComponent();
            txtPrevs.Enabled = true;
            _controlador = new controllerCarregaPrevs();
        }

        private void btnCarregar_Click(object sender, EventArgs e) {
            string r;

            if (this.Oficial)
                showWarning("Caso já exista algum prevs oficial para este mês, o atual passará a ser o oficial, deixando o anterior como não-oficial.");

            r = _controlador.carregarPrevs(this.Prevs, this.Nome, this.Descricao, this.Oficial, this.ano, this.mes);
            int x;
            if (int.TryParse(r, out x))
                this.showWarning("Leitura terminada com sucesso!");
            else
                this.showWarning(r);
        }

        #region Variaveis Formulario

        public string Nome {
            get { return txtNome.Text; }
            set { txtNome.Text = value; }
        }

        public string Descricao {
            get { return txtDescricao.Text; }
            set { txtDescricao.Text = value; }
        }

        public string Prevs {
            get { return txtPrevs.Text; }
            set { txtPrevs.Text = value; }
        }

        public int ano {
            get { return int.Parse(txtAno.Text); }
            set { txtAno.Text = value.ToString(); }
        }

        public int mes {
            get { return int.Parse(txtMes.Text); }
            set { txtMes.Text = value.ToString(); }
        }


        public bool Oficial {
            get { return ckbOficial.Checked; }
            set { ckbOficial.Checked = value; }
        }
        #endregion
    }
}