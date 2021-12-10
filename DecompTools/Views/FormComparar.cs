using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DecompTools.ControllerDC;
using System.IO;
using ToolBox.Forms;
using System.Threading.Tasks;

namespace DecompTools.Views {
    public partial class FormComparar : FormBasic {
        public FormComparar() {
            InitializeComponent();
            txtDeck1.Enabled = true;
            txtDeck2.Enabled = true;
        }

        private async void btnCompara_Click(object sender, EventArgs e) {
            try {
                btnCompara.EnterLoadingState();
                IsLoading = true;

                if (File.Exists(this.pathDeck1) && File.Exists(this.pathDeck2)) {

                    var result = await controllerCompara.compararAsync(this.pathDeck1, this.pathDeck2);

                    this.showWarning(result);

                } else {
                    this.showWarning("Selecione os dois decks para comparação");
                }
            } catch (Exception ex) {
                this.showError(ex.Message);
            } finally {
                IsLoading = false;
                btnCompara.ExitLoadingState();
            }
        }

        #region Variaveis Formulario
        public String pathDeck1 {
            get { return txtDeck1.Text; }
            set { txtDeck1.Text = value; }
        }

        public String pathDeck2 {
            get { return txtDeck2.Text; }
            set { txtDeck2.Text = value; }
        }
        #endregion
    }
}
