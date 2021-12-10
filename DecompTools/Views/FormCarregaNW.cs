using DecompTools.ControllerNW;
using DecompTools.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ToolBox.Forms;

namespace DecompTools.Views {
    public partial class FormCarregaNW : FormBasic {
        public FormCarregaNW() {
            InitializeComponent();
            txtFolder.Enabled = true;
        }

        private void btnCarregar_Click(object sender, EventArgs e) {
            if (this.Oficial)
                showWarning("Caso já exista algum deck oficial para este mês, o atual passará a ser o oficial, deixando o anterior como não-oficial.");

            string r;
            int id;
            r = controllerCarregaNW.CarregaDeckNW(this.Folder, this.Nome, this.Descricao, this.Oficial);
            if (!int.TryParse(r, out id))
                this.showError(r);
            else
                this.showWarning("Deck adicionado com sucesso");

            //Função para carregar massa de dados
            //this.carregarTodos();
        }

        private void carregarTodos() {
            string nome;
            string pasta;

            foreach (var dir in Directory.GetDirectories(this.Folder)) {
                pasta = dir.Substring(dir.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                nome = String.Concat(UtilitarioDeData.NomeMes(int.Parse(pasta.Substring(6))), " ", pasta.Substring(2, 4), " - Oficial");
                var r = controllerCarregaNW.CarregaDeckNW(dir, nome, String.Concat("Pasta - ", pasta), true);
            }
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

        public string Folder {
            get { return txtFolder.Text; }
            set { txtFolder.Text = value; }
        }

        public bool Oficial {
            get { return ckbOficial.Checked; }
            set { ckbOficial.Checked = value; }
        }
        #endregion
    }
}
