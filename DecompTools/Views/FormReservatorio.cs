using DecompTools.ControllerDC;
using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolBox.Forms;
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormReservatorio : FormBasic {
        public FormReservatorio() {
            InitializeComponent();
            txtExitPath.Enabled = true;

            //Inicializando o DataGrid
            this.dgvDeck.ApplyDefaults();
            this.dgvDeck.ApplyFilters();
            var ds = DeckDAO.GetAll().ToFilteredBindingList();
            dgvDeck.DataSource = new BindingSource(ds, null);

            txtEarm.Text = EarmMaxDAO.GetAlltoString();
        }

        private async void btnGerar_Click(object sender, EventArgs e) {
            btnGerar.Enabled = false;
            IsLoading = true;
            string msg;
            decimal[] target = new decimal[4] { reservSE, reservS, reservNE, reservN };

            var deck = this.deck;
            var earmmax = this.earmmax;
            var exitPath = this.exitPath;

            msg = await Task.Factory.StartNew(() => controllerReservatorio.createReserv(deck, target, earmmax, exitPath));
            showWarning(msg);
            IsLoading = false;
            btnGerar.Enabled = true;
        }

        #region Variaveis Formulario

        public Deck deck {
            get {
                if (dgvDeck.SelectedRows.Count == 0 || dgvDeck.SelectedRows.Count > 1)
                    return null;
                else {
                    return (Deck)dgvDeck.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }

        public String exitPath {
            get { return txtExitPath.Text; }
            set { txtExitPath.Text = value; }
        }

        public decimal reservSE {
            get { return decimal.Parse(txtReservSE.Text.Replace(".", ","), NumberStyles.Float); }
            set { txtReservSE.Text = value.ToString(); }
        }

        public decimal reservS {
            get { return decimal.Parse(txtReservS.Text.Replace(".", ","), NumberStyles.Float); }
            set { txtReservS.Text = value.ToString(); }
        }

        public decimal reservNE {
            get { return decimal.Parse(txtReservNE.Text.Replace(".", ","), NumberStyles.Float); }
            set { txtReservNE.Text = value.ToString(); }
        }

        public decimal reservN {
            get { return decimal.Parse(txtReservN.Text.Replace(".", ","), NumberStyles.Float); }
            set { txtReservN.Text = value.ToString(); }
        }

        public string earmmax {
            get { return txtEarm.Text; }
            set { txtEarm.Text = value.ToString(); }
        }

        #endregion
    }
}
