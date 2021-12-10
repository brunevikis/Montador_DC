using DecompTools.FactoryNW;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DecompTools.Views {
    public partial class FormNWSistemaDatNew : Form {
        public FormNWSistemaDatNew() {
            InitializeComponent();
        }

        private void FormNWSistemaDatNew_Load(object sender, EventArgs e) {
            dgvNW.DataSource = DeckNWDAO.GetAll();
        }
        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoDeckNW == DeckSource.Banco) {
                this.dgvNW.Visible = true;
                this.txtNWFolder.Visible = false;
            } else if (this.tipoDeckNW == DeckSource.Arquivo) {
                this.dgvNW.Visible = false;
                this.txtNWFolder.Visible = true;
            }
        }

        public DeckSource tipoDeckNW {
            get { return rbBanco.Checked ? DeckSource.Banco : DeckSource.Arquivo; }
            set {
                switch (value) {
                    case DeckSource.Banco:
                        rbBanco.Checked = true;
                        break;
                    case DeckSource.Arquivo:
                        rbPasta.Checked = true;
                        break;
                }
            }
        }
    }
}
