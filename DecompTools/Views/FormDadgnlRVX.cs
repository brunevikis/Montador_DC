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
    public partial class FormDadgnlRVX : FormBasic, DadgnlRVX {

        ControllerDC.controllerDadgnlRVX presenter;
        public FormDadgnlRVX() {
            InitializeComponent();
            presenter = new ControllerDC.controllerDadgnlRVX(this);
            selectInputFile.Enabled = true;
            selectOutputFolder.Enabled = true;
        }

        private void btnGerar_Click(object sender, EventArgs e) {

            if (System.IO.File.Exists(InputFile) && System.IO.Directory.Exists(OutputFolder)) {

                presenter.CreateRVX();

            }
        }

        public string InputFile {
            get {
                return selectInputFile.Text;
            }
        }

        public string OutputFolder {
            get {
                return selectOutputFolder.Text;
            }
        }

        public string ReturnMessage {
            set { base.showWarning(value); }
        }
    }
}
