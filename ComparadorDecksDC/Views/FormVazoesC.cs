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
    public partial class FormVazoesC : FormBasic {

        VazoesC vazoes = null;

        public FormVazoesC() {
            InitializeComponent();
            btnMedia.BackColor = Color.Gray;
        }

        private void btnCarregar_Click(object sender, EventArgs e) {


            if (!File.Exists(selectVazoesCFile.Text)) {
                showError("Selecione um arquivo");
                return;
            }

            var filePathExt = Path.GetExtension(selectVazoesCFile.Text);
            var filePath = selectVazoesCFile.Text;

            dynamic content;

            switch (filePathExt.ToLower()) {
                case ".csv":
                    content = File.ReadAllText(filePath);
                    break;
                case ".dat":
                    content = File.ReadAllBytes(filePath);
                    break;
                default:
                    showError("Selecione um arquivo");
                    return;
            }


            vazoes = new VazoesC(content);
            btnMedia.BackColor = Color.Gray;

            tabControl1.Enabled = true;
            panel3.Visible = true;

            txtAnoInicial.Text = "Ano inicial: " + VazoesC.anoInicial.ToString();
            txtAnoFinal.Text = "Ano final: " + vazoes.AnoFinal.ToString();
            txtMesFinal.Text = "Mes final: " + vazoes.MesFinal.ToString();


            txtInAnoFinal.Text = (vazoes.AnoFinal - 1).ToString();
            txtInAnoInicial.Text = VazoesC.anoInicial.ToString();


            var dataAtual = new DateTime(vazoes.AnoFinal, vazoes.MesFinal, 1);
            var dataSeg = dataAtual.AddMonths(1);

            txtOutAnoInicial.Text = dataSeg.Year.ToString();
            txtOutAnoFinal.Text = dataSeg.Year.ToString();
            txtOutMesInicial.Text = dataSeg.Month.ToString();
            txtOutMesFinal.Text = dataSeg.Month.ToString();


            btnExportCsv.Enabled = true;
            btnExportDat.Enabled = true;
        }

        private void btnExportCsv_Click(object sender, EventArgs e) {
            SaveFileDialog diag = new SaveFileDialog() {
                Filter = "CSV|*.CSV",
                DefaultExt = ".CSV",
                OverwritePrompt = true,
                InitialDirectory = Path.GetDirectoryName(selectVazoesCFile.Text),
                AddExtension = true
            };

            diag.FileName = Path.ChangeExtension(selectVazoesCFile.Text, "CSV");

            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var content = vazoes.ToContentString();

                File.WriteAllText(diag.FileName, content);

                MessageBox.Show("Saved on " + diag.FileName);
            }

        }

        private void btnExportDat_Click(object sender, EventArgs e) {
            SaveFileDialog diag = new SaveFileDialog() {
                Filter = "DAT|*.DAT",
                DefaultExt = ".DAT",
                OverwritePrompt = true,
                InitialDirectory = Path.GetDirectoryName(selectVazoesCFile.Text),
                AddExtension = true
            };

            diag.FileName = Path.ChangeExtension(selectVazoesCFile.Text, "DAT");

            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var content = vazoes.ToBytes();

                File.WriteAllBytes(diag.FileName, content);

                MessageBox.Show("Saved on " + diag.FileName);
            }
        }

        private async void btnMedia_Click(object sender, EventArgs e) {
            try {
                IsLoading = true;
                btnMedia.EnterLoadingState();
                
                if (vazoes == null) {
                    showError("Carregar arquivo de entrada primeiro");
                    return;
                }


                int mesIni = int.Parse(txtOutMesInicial.Text);
                int mesFin = int.Parse(txtOutMesFinal.Text);
                int anoIni = int.Parse(txtOutAnoInicial.Text);
                int anoFin = int.Parse(txtOutAnoFinal.Text);


                int anoMedIni = int.Parse(txtInAnoInicial.Text);
                int anoMedFin = int.Parse(txtInAnoFinal.Text);


                await Task.Factory.StartNew(() => {
                    for (DateTime date = new DateTime(anoIni, mesIni, 1); date <= new DateTime(anoFin, mesFin, 1); date = date.AddMonths(1)) {
                        vazoes.ProjetarVazoesMedias(date.Month, date.Year, anoMedIni, anoMedFin);
                    }
                });

                showWarning("Completed");

                btnMedia.BackColor = Color.Green;
            } finally {
                IsLoading = false;
                btnMedia.ExitLoadingState();
            }
        }
    }
}
