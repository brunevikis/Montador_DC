using DecompTools.ControllerPrevs;
using DecompTools.FactoryPrevs;
using DecompTools.ModelagemPrevs;
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
using ToolBox.Helpers;

namespace DecompTools.Views {
    public partial class FormNovoPrevs : FormBasic {
        private controllerNovoPrevs _controlador;

        public FormNovoPrevs() {
            _controlador = new controllerNovoPrevs();
            InitializeComponent();
            this.dgvPrevs.ApplyDefaults();
            this.dgvPrevs.ApplyFilters();

        }

        private void rbBanco_CheckedChanged(object sender, EventArgs e) {
            if (this.tipoPrevs == DeckSource.Banco) {
                this.dgvPrevs.Visible = true;
                this.txtPrevsPath.Visible = false;
            } else if (this.tipoPrevs == DeckSource.Arquivo) {
                this.dgvPrevs.Visible = false;
                this.txtPrevsPath.Visible = true;
            }
        }

        private async void rbRDH_CheckedChanged(object sender, EventArgs e) {

            if (cmbRDH.Items.Count == 0) {
                await LoadRDHAsync();
            }

            this.cmbRDH.Visible = rbRDH.Checked;
        }

        private async void btnGerar_Click(object sender, EventArgs e) {
            try {
                btnGerar.EnterLoadingState();
                IsLoading = true;

                DateTime dtInicio = DateTime.Now;

                string r = "";
                int idPrevsBase;

                controllerCarregaPrevs _carrega = new controllerCarregaPrevs();
                Prevs prevsBase;
                if (this.tipoPrevs == DeckSource.Arquivo) {
                    prevsBase = _carrega.lePrevs(this.caminhoPrevs, this.ano, this.mes);

                } else {
                    idPrevsBase = this.prevs.id;
                    prevsBase = await PrevsDAO.getDataByIdAsync(idPrevsBase);
                }

                Semanas_Ano s = new Semanas_Ano(this.ano, this.mes, this.rev, this.semana);

                RDH rdhBase = null;
                if (this.usarRdh) {
                    rdhBase = await RDHDAO.getDataByIdAsync(this.RDH);

                    if (rdhBase.dados.Count < 140) throw new Exception("RDH não carregado corretamente");
                }

                var caminhoImp = this.caminhoImp;
                var ENA = this.ENA;
                var descricao = this.descricao;
                var caminhoSaidaEstudo = this.caminhoSaida;
                var usarRdh = this.usarRdh;

                int revsToRun;

                //                       //<posto, <strDat Hash, caminho>> 
                Dictionary<int, Dictionary<string, string>> strDat = null;

                var matrizENA = _controlador.CheckEnaEntrada(s, ENA, out revsToRun);

                if (revsToRun > 1 &&
                    MessageBox.Show("Serão processadas " + revsToRun.ToString() + " semanas. Continuar?", "Decomp Tools", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes
                    ) return;

                for (int revI = 0; revI < revsToRun; revI++) {

                    if (revsToRun > 1) caminhoSaidaEstudo = System.IO.Path.Combine(this.caminhoSaida, "RV" + s.rev.ToString());
                    if (!System.IO.Directory.Exists(caminhoSaidaEstudo)) System.IO.Directory.CreateDirectory(caminhoSaidaEstudo);
                    var caminhoSaidaSensBase = caminhoSaidaEstudo;
                    if (chkCriarSens.Checked) {
                        caminhoSaidaSensBase = System.IO.Path.Combine(caminhoSaidaEstudo,
                            (string.IsNullOrWhiteSpace(this.Nome) ? "Sensibilidade" : this.Nome) + "_0"
                        );
                        strDat = new Dictionary<int, Dictionary<string, string>>();
                    } else if (!string.IsNullOrWhiteSpace(this.Nome)) {
                        caminhoSaidaSensBase = System.IO.Path.Combine(caminhoSaidaEstudo, this.Nome + "_0");
                    }
                    if (!System.IO.Directory.Exists(caminhoSaidaSensBase)) System.IO.Directory.CreateDirectory(caminhoSaidaSensBase);

                    r = await Task.Factory.StartNew(() => _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, matrizENA, descricao, caminhoSaidaSensBase, usarRdh, strDat: strDat));

                    if (chkCriarSens.Checked) for (int p = chkSensBidirecional.Checked ? -(int)numPassosSens.Value : 1; p <= (int)numPassosSens.Value; p++) {
                            if (p == 0) continue;

                            var caminhoSaidaSens = System.IO.Path.Combine(caminhoSaidaEstudo,
                            (string.IsNullOrWhiteSpace(this.Nome) ? "Sensibilidade" : this.Nome) + "_" + p.ToString("+#;-#"));
                            if (!System.IO.Directory.Exists(caminhoSaidaSens)) System.IO.Directory.CreateDirectory(caminhoSaidaSens);

                            var somaSens = revI == 0 ? ModoSensibilidade.total : ModoSensibilidade.somentePosteriorPrevisao;
                            int[,] ENAbase = _controlador.SomaSensibilidade(s, matrizENA, txtEnaSens.Text, p, somaSens);

                            r = await Task.Factory.StartNew(() => _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, ENAbase, descricao, caminhoSaidaSens, usarRdh, strDat: strDat));

                        }

                    caminhoImp = System.IO.Path.Combine(caminhoSaidaSensBase, "arq_previvaz");
                    prevsBase = _carrega.lePrevs(
                        System.IO.Path.Combine(caminhoSaidaSensBase, "PREVS.RV" + s.rev), this.ano, this.mes);

                    s = s.semanaProxima();

                    //não utilizar rdh nas revisões posteriores
                    rdhBase = null;
                    usarRdh = false;
                }


                DateTime dtFim = DateTime.Now;

                int x;
                if (int.TryParse(r, out x))
                    this.showWarning("Leitura terminada com sucesso!");
                else if (!string.IsNullOrEmpty(r))
                    this.showWarning(r);

                this.showWarning(String.Concat("Processo terminado em :", (dtFim - dtInicio).ToString()));
            } catch (Exception ex) {

                ToolBox.Messages.Error(this, "", ex, true);
                btnGerar.SetErrorState(ex);
                //this.showError(ex.Message);
            } finally {

                btnGerar.ExitLoadingState();
                IsLoading = false;
            }
        }

        #region Variaveis Formulario

        public DeckSource tipoPrevs {
            // Prevs DB = 1, Pasta = 2
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

        //public int tipoBase {
        //    // Prevs RDH = 1, Prevs = 2
        //    get { return rbRDH.Checked ? 1 : 2; }
        //    set {
        //        if (value == 1)
        //            rbRDH.Checked = true;
        //        else
        //            rbPrevs.Checked = true;
        //    }
        //}

        public bool usarRdh {
            get { return rbRDH.Checked; }
            set { rbRDH.Checked = value; }
        }

        public Prevs prevs {
            get {
                if (dgvPrevs.SelectedRows.Count == 0 || dgvPrevs.SelectedRows.Count > 1)
                    return null;
                else {
                    return (Prevs)dgvPrevs.CurrentRow.DataBoundItem;
                }
            }
            set { }
        }

        public String caminhoPrevs {
            get { return txtPrevsPath.Text; }
            set { txtPrevsPath.Text = value; }
        }

        public String caminhoImp {
            get { return txtImpFolder.Text; }
            set { txtImpFolder.Text = value; }
        }

        public string caminhoSaida {
            get { return txtPathExit.Text; }
            set { txtPathExit.Text = value; }
        }

        public int rev {
            get {
                if (String.Equals(txtRev.Text, String.Empty))
                    return 0;
                return int.Parse(txtRev.Text);
            }
            set { txtRev.Text = value.ToString(); }
        }

        public int mes {
            get {
                if (String.Equals(txtMes.Text, String.Empty))
                    return 0;
                return int.Parse(txtMes.Text);
            }
            set { txtMes.Text = value.ToString(); }
        }

        public int ano {
            get {
                if (String.Equals(txtAno.Text, String.Empty))
                    return 0;
                return int.Parse(txtAno.Text);
            }
            set { txtAno.Text = value.ToString(); }
        }

        public int semana {
            get {
                if (String.Equals(txtSemana.Text, String.Empty))
                    return 0;
                return int.Parse(txtSemana.Text);
            }
            set { txtSemana.Text = value.ToString(); }
        }

        public string Nome {
            get { return txtNome.Text; }
            set { txtNome.Text = value; }
        }

        public string descricao {
            get { return txtDesc.Text; }
            set { txtDesc.Text = value; }
        }

        public string ENA {
            get { return txtEna.Text; }
            set { txtEna.Text = value; }
        }

        public DateTime RDH {
            get { return Convert.ToDateTime(cmbRDH.Text); }
            set { cmbRDH.Text = value.ToString(); }
        }
        #endregion

        #region Calculo da Semana Formulario

        private void txtAno_TextChanged(object sender, EventArgs e) {
            int r;
            if (int.TryParse(txtAno.Text, out r)) atualizaSemana();
            else txtAno.Text = "";
        }

        private void txtMes_TextChanged(object sender, EventArgs e) {
            int r;
            if (int.TryParse(txtMes.Text, out r)) atualizaSemana();
            else txtMes.Text = "";
        }

        private void txtRev_TextChanged(object sender, EventArgs e) {
            int r;
            if (int.TryParse(txtRev.Text, out r)) atualizaSemana();
            else txtRev.Text = "";
        }

        public void atualizaSemana() {
            if (this.ano != 0 && this.mes != 0) {
                this.semana = 0;
                Semanas_Ano s = SemanasAnoDAO.GetByMesAno(this.mes, this.ano, this.rev);
                try {
                    this.semana = s.semana;
                } catch (Exception) {
                    this.semana = 0;
                }
            }
        }

        #endregion

        private async void FormNovoPrevs_Load(object sender, EventArgs e) {
            try {

                btnGerar.EnterLoadingState();
                IsLoading = true;
                txtPrevsPath.Enabled = true;
                //txtPrevsPath.Text = "C:\\Decomp_Semanal\\";
                txtPathExit.Enabled = true;
                //txtPathExit.Text = "C:\\Decomp_Semanal\\";
                txtImpFolder.Enabled = true;
                //txtImpFolder.Text = "P:\\Alexandre\\Previvaz\\Simulacao\\Arq_Previvaz\\";
                //ano = 2013;
                //mes = 11;
                //rev = 2;
                //ENA = "20573 19663 25000\n14222 6724 8000\n2585 2463 2500 3500\n1822 2500";

                await Task.WhenAll(
                    // LoadMLTAsync(),
                    LoadPrevsAsync()
                    );

                btnGerar.ExitLoadingState();

            } catch (Exception ex) {

                ToolBox.Messages.Error(this, "", ex, true);
            } finally {
                IsLoading = false;

            }
        }

        async Task LoadRDHAsync() {
            try {
                cmbRDH.EnterLoadingState();
                var rdhs = await RDHDAO.GetAllAsync();
                foreach (RDH r in rdhs) {
                    cmbRDH.Items.Add(r.dt_rdh);
                }
                cmbRDH.SelectedIndex = 0;
                cmbRDH.ExitLoadingState();
            } catch (Exception ex) {
                cmbRDH.SetErrorState(ex);
            }
        }

        async Task LoadMLTAsync() {
            lblInfoData.EnterLoadingState();
            var data = await MLTDAO.getDataBySubmercadoAsync(1);
            lblInfoData.Text = "Planilha Atualizada em: " + data.dt_atualizacao.ToString();
            lblInfoData.ExitLoadingState();
        }

        async Task LoadPrevsAsync() {
            dgvPrevs.EnterLoadingState();
            var prevs = await PrevsDAO.GetAllAsync();
            dgvPrevs.DataSource = prevs.ToBindingSource();
            dgvPrevs.ExitLoadingState();
        }
    }
}
