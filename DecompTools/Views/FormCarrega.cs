using DecompTools.ControllerNW;
using DecompTools.FactoryNW;
using DecompTools.ModelagemNW;
using DecompTools.ControllerDC;
using DecompTools.Util;
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
    public partial class FormCarrega : FormBasic {
        private readonly controllerCarrega _controlador;

        public FormCarrega() {
            InitializeComponent();
            _controlador = new controllerCarrega();
            txtDadger.Enabled = true;
            dgvNW.DataSource = DeckNWDAO.GetAll().ToBindingSource();
            DataGridViewHelper.ApplyDefaults(this.dgvNW);
            this.dgvNW.ApplyFilters();
        }

        private void btnCarregar_Click(object sender, EventArgs e) {
            int idDeckNW;
            string r;

            if (this.Oficial)
                showWarning("Caso já exista algum deck oficial para este mês e revisão, o atual passará a ser o oficial, deixando o anterior como não-oficial.");


            if (this.tipoDeckNW == 1)
                idDeckNW = this.deckNW.id;
            else {
                string foo = controllerCarregaNW.CarregaDeckNW(this.caminhoNW, this.Nome, "DeckNW carregado automaticamente no processo do deck {0}".Replace("{0}", this.Nome), false);
                if (!int.TryParse(foo, out idDeckNW)) {
                    this.showError("Problema ao carregar deck NW {0}".Replace("{0}", foo));
                    return;
                }
            }

            r = _controlador.carregarDeck(this.Dadger, idDeckNW, this.Nome, this.Descricao, this.Oficial);
            int x;
            if (int.TryParse(r, out x))
                this.showWarning("Leitura terminada com sucesso!");
            else
                this.showWarning(r);

            //Carregar em massa.
            //this.carregarTodos();
            //this.showError("TERMINOU!");
        }

        /// <summary>
        /// Função para carregar decks oficiais em massa.
        /// </summary>
        private void carregarTodos() {
            string caminho = Path.GetDirectoryName(this.Dadger);
            foreach (var dir in Directory.GetDirectories(caminho)) {
                foreach (var dir2 in Directory.GetDirectories(dir)) {
                    foreach (var dir3 in Directory.GetDirectories(dir2)) {
                        string nome;
                        string pasta;
                        int mes, ano, idDeckNW;
                        pasta = dir3.Substring(dir3.LastIndexOf(Path.DirectorySeparatorChar) + 1);
                        mes = int.Parse(pasta.Substring(6, 2));
                        ano = int.Parse(pasta.Substring(2, 4));
                        nome = String.Concat(UtilitarioDeData.NomeMes(mes), " ", pasta.Substring(2, 4), " - Oficial");

                        DeckNW deckNW = DeckNWDAO.getDeckOficialByMonth(mes, ano);
                        idDeckNW = (deckNW == null ? 1 : deckNW.id);

                        var r = _controlador.carregarDeck(Path.Combine(dir3, "DADGER.RV0"), idDeckNW, nome, String.Concat("Pasta - ", pasta), true);
                    }
                }
            }
        }

        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoDeckNW == 1) {
                this.dgvNW.Visible = true;
                this.txtNWFolder.Visible = false;
            } else if (this.tipoDeckNW == 2) {
                this.dgvNW.Visible = false;
                this.txtNWFolder.Visible = true;
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

        public string Dadger {
            get { return txtDadger.Text; }
            set { txtDadger.Text = value; }
        }

        public bool Oficial {
            get { return ckbOficial.Checked; }
            set { ckbOficial.Checked = value; }
        }

        //Parte do NW
        public int tipoDeckNW {
            // Deck DB = 1, Pasta = 2
            get { return rbBanco.Checked ? 1 : 2; }
            set {
                if (value == 1)
                    rbBanco.Checked = true;
                else
                    rbBanco.Checked = true;
            }
        }

        public DeckNW deckNW {
            get {
                if (dgvNW.SelectedRows.Count == 0 || dgvNW.SelectedRows.Count > 1)
                    return null;
                else {
                    return (DeckNW)dgvNW.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }

        public String caminhoNW {
            get { return txtNWFolder.Text; }
            set { txtNWFolder.Text = value; }
        }

        #endregion
    }
}