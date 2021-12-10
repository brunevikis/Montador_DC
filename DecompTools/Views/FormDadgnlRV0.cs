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
    public partial class FormDadgnlRV0 : FormBasic, DadgnlRV0 {

        ControllerDC.controllerDadgnlRV0 presenter;
        public FormDadgnlRV0() {
            InitializeComponent();
            presenter = new ControllerDC.controllerDadgnlRV0(this);
            selectInputFile.Enabled = true;
            selectOutputFolder.Enabled = true;
        }

        private void btnGerar_Click(object sender, EventArgs e) {

            if (System.IO.File.Exists(InputFile) && System.IO.Directory.Exists(OutputFolder)) {


                if (!System.IO.File.Exists(System.IO.Path.Combine(OutputFolder, "DADGNL.RV0"))
                    || DialogResult.Yes == MessageBox.Show("Sobreescrever arquivo existente?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    )
                    presenter.CreateRV0();

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

        public int Mes {
            get {
                return int.Parse(txtMes.Text);
            }
        }

        public int Ano {
            get {
                return int.Parse(txtAno.Text);
            }
        }

        public string ReturnMessage {
            set { base.showWarning(value); }
        }
    }
}
