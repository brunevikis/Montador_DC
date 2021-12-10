using AutoPrevs.Controller;
using AutoPrevs.Factory;
using AutoPrevs.Modelagem;
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

namespace AutoPrevs.Views {
    public partial class FormNovoPrevsSensib : FormBasic {
        private controllerNovoPrevs _controlador;

        public FormNovoPrevsSensib() {
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

                string r;
                int idPrevsBase;


                Prevs prevsBase;
                if (this.tipoPrevs == DeckSource.Arquivo) {
                    controllerCarregaPrevs _carrega = new controllerCarregaPrevs();
                    prevsBase = _carrega.lePrevs(this.caminhoPrevs, this.ano, this.mes);

                } else {
                    idPrevsBase = this.prevs.id;
                    prevsBase = await PrevsDAO.getDataByIdAsync(idPrevsBase);
                }

                Semanas_Ano s = new Semanas_Ano(this.ano, this.mes, this.rev, this.semana);

                RDH rdhBase = null;
                if (this.usarRdh) {
                    rdhBase = await RDHDAO.getDataByIdAsync(this.RDH);
                }

                var caminhoImp = this.caminhoImp;
                var ENA = this.ENA;
                var nome = this.nome;
                var descricao = this.descricao;
                var caminhoSaida = this.caminhoSaida;
                var usarRdh = this.usarRdh;

                if (chkCriarSens.Checked) {
                    caminhoSaida = System.IO.Path.Combine(this.caminhoSaida, "Sensibilidade_0");
                    if (!System.IO.Directory.Exists(caminhoSaida)) System.IO.Directory.CreateDirectory(caminhoSaida);
                }

                r = await Task.Factory.StartNew(() => _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, ENA, nome, descricao, caminhoSaida, usarRdh));


                if (chkCriarSens.Checked) {

                    for (int p = chkSensBidirecional.Checked ? -(int)numPassosSens.Value : 1
                        ; p <= (int)numPassosSens.Value; p++) {

                        if (p == 0) continue;

                        var caminhoSaidaSens = System.IO.Path.Combine(this.caminhoSaida, "Sensibilidade_" + p.ToString("+#;-#"));


                        string[] ENAlinhas = ENA.Split('\n');
                        string[] ENASenslinhas = txtEnaSens.Text.Split('\n');

                        int[,] ENAbase = new int[4, 6];

                        int _sub = 0;
                        foreach (string ENAsubmercado in ENAlinhas) {
                            int _sem = 0;
                            var enaSens = ENASenslinhas[_sub].Replace(".", String.Empty).Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string ENAsemana in ENAsubmercado.Replace("\t", " ").Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)) {

                                int sens;

                                if (!int.TryParse(enaSens[_sem], out sens)) {
                                    sens = 0;
                                }

                                ENAbase[_sub, _sem] = int.Parse(ENAsemana.Replace(".", String.Empty)) + sens * p;
                                _sem++;
                            }
                            _sub++;

                            if (_sub > 3)
                                break;
                        }

                        if (!System.IO.Directory.Exists(caminhoSaidaSens)) System.IO.Directory.CreateDirectory(caminhoSaidaSens);

                        r = await Task.Factory.StartNew(() => _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, ENAbase, nome, descricao, caminhoSaidaSens, usarRdh));


                    }
                }


                //r = _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, ENA, nome, descricao, caminhoSaida, tipoBase);
                int x;
                if (int.TryParse(r, out x))
                    this.showWarning("Leitura terminada com sucesso!");
                else
                    this.showWarning(r);
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

        public string nome {
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
