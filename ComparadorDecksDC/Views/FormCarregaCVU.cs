using ComparadorDecksDC.Modelagem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolBox.Forms;

namespace ComparadorDecksDC.Views {
    public partial class FormCarregaCVU : FormBasic {

        /// <summary>
        /// Test only.
        /// </summary>
        public static RelatorioCVU TestCvu = null;

        public string CVUFilePath {
            get {
                return selectCVU.Text;
            }
        }

        public FormCarregaCVU() {
            InitializeComponent();
            selectCVU.Enabled = true;
        }

        private void btnCarregar_Click(object sender, EventArgs e) {
            try {
                IsLoading = true;

                if (!File.Exists(this.CVUFilePath) || !(new string[] { ".xls", ".xlsx" }).Contains(Path.GetExtension(this.CVUFilePath))) {
                    showError("Selecione um relatório válido.");
                } else {
                    //to do controller..

                    RelatorioCVU cvu = Controller.controllerCVU.LerCVU(this.CVUFilePath);

                    TestCvu = cvu;

                    showWarning("Carregado com sucesso.\r\n\r\n[" + cvu.ToString());                    
                }
            } finally {
                IsLoading = false;
            }

        }
    }
}
