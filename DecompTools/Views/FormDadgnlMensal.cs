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
    public partial class FormDadgnlMensal : FormBasic, DadgnlMensal {

        ControllerDC.controllerDadgnlMensal presenter;
        public FormDadgnlMensal()
            : base() {
            InitializeComponent();
            presenter = new ControllerDC.controllerDadgnlMensal(this);
            selectInputFile.Enabled = true;
            selectOutputFolder.Enabled = true;
        }

        private void btnGerar_Click(object sender, EventArgs e) {

            if (System.IO.File.Exists(InputFile) && System.IO.Directory.Exists(OutputFolder)) {


                //var ext = DecompTools.Util.UtilitarioDeData.NomeMes(dt.Month);
                //if (!System.IO.File.Exists(System.IO.Path.Combine(OutputFolder, "\\" + dt.ToString("yyyyMM") + "\\DADGNL." + ext))
                //    || DialogResult.Yes == MessageBox.Show("Sobreescrever arquivo existente?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                //    )
                presenter.CreateMensal();
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

        public int MesInicial {
            get {
                return int.Parse(txtMesIni.Text);
            }
        }

        public int AnoInicial {
            get {
                return int.Parse(txtAnoIni.Text);
            }
        }

        public int MesFinal {
            get {
                return int.Parse(txtMesFin.Text);
            }
        }

        public int AnoFinal {
            get {
                return int.Parse(txtAnoFin.Text);
            }
        }

        public string ReturnMessage {
            set { base.showWarning(value); }
        }
    }
}
