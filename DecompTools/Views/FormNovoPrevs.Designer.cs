namespace DecompTools.Views
{
    partial class FormNovoPrevs {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtImpFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.cmbRDH = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAno = new System.Windows.Forms.TextBox();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.txtRev = new System.Windows.Forms.TextBox();
            this.txtEna = new System.Windows.Forms.RichTextBox();
            this.txtPathExit = new ToolBox.Componentes.SelectFolderTextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.rbBanco = new System.Windows.Forms.RadioButton();
            this.rbPasta = new System.Windows.Forms.RadioButton();
            this.dgvPrevs = new System.Windows.Forms.DataGridView();
            this.txtPrevsPath = new ToolBox.Componentes.SelectFileTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSemana = new System.Windows.Forms.TextBox();
            this.lblInfoData = new System.Windows.Forms.Label();
            this.rbRDH = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkSensBidirecional = new System.Windows.Forms.CheckBox();
            this.numPassosSens = new System.Windows.Forms.NumericUpDown();
            this.chkCriarSens = new System.Windows.Forms.CheckBox();
            this.txtEnaSens = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrevs)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPassosSens)).BeginInit();
            this.SuspendLayout();
            // 
            // txtImpFolder
            // 
            this.txtImpFolder.AllowDrop = true;
            this.txtImpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImpFolder.Description = "";
            this.txtImpFolder.Location = new System.Drawing.Point(12, 288);
            this.txtImpFolder.Name = "txtImpFolder";
            this.txtImpFolder.OwnerIWin32Window = null;
            this.txtImpFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtImpFolder.ShowNewFolderButton = true;
            this.txtImpFolder.Size = new System.Drawing.Size(1044, 31);
            this.txtImpFolder.TabIndex = 14;
            this.txtImpFolder.Title = "Pasta inp/dat";
            // 
            // cmbRDH
            // 
            this.cmbRDH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbRDH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRDH.FormattingEnabled = true;
            this.cmbRDH.Location = new System.Drawing.Point(149, 324);
            this.cmbRDH.Name = "cmbRDH";
            this.cmbRDH.Size = new System.Drawing.Size(205, 21);
            this.cmbRDH.TabIndex = 15;
            this.cmbRDH.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Ano";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Mes";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(789, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "REV";
            // 
            // txtAno
            // 
            this.txtAno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAno.Location = new System.Drawing.Point(584, 325);
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(74, 20);
            this.txtAno.TabIndex = 20;
            this.txtAno.TextChanged += new System.EventHandler(this.txtAno_TextChanged);
            // 
            // txtMes
            // 
            this.txtMes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMes.Location = new System.Drawing.Point(697, 325);
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(74, 20);
            this.txtMes.TabIndex = 21;
            this.txtMes.TextChanged += new System.EventHandler(this.txtMes_TextChanged);
            // 
            // txtRev
            // 
            this.txtRev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRev.Location = new System.Drawing.Point(824, 325);
            this.txtRev.Name = "txtRev";
            this.txtRev.Size = new System.Drawing.Size(74, 20);
            this.txtRev.TabIndex = 22;
            this.txtRev.TextChanged += new System.EventHandler(this.txtRev_TextChanged);
            // 
            // txtEna
            // 
            this.txtEna.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEna.Location = new System.Drawing.Point(77, 0);
            this.txtEna.Name = "txtEna";
            this.txtEna.Size = new System.Drawing.Size(577, 121);
            this.txtEna.TabIndex = 25;
            this.txtEna.Text = "";
            // 
            // txtPathExit
            // 
            this.txtPathExit.AllowDrop = true;
            this.txtPathExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathExit.Description = "";
            this.txtPathExit.Location = new System.Drawing.Point(12, 500);
            this.txtPathExit.Name = "txtPathExit";
            this.txtPathExit.OwnerIWin32Window = null;
            this.txtPathExit.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtPathExit.ShowNewFolderButton = true;
            this.txtPathExit.Size = new System.Drawing.Size(1044, 33);
            this.txtPathExit.TabIndex = 26;
            this.txtPathExit.Title = "Pasta de saída";
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerar.Location = new System.Drawing.Point(925, 540);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(131, 28);
            this.btnGerar.TabIndex = 27;
            this.btnGerar.Text = "Gerar Prevs";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // rbBanco
            // 
            this.rbBanco.AutoSize = true;
            this.rbBanco.Checked = true;
            this.rbBanco.Location = new System.Drawing.Point(16, 29);
            this.rbBanco.Name = "rbBanco";
            this.rbBanco.Size = new System.Drawing.Size(56, 17);
            this.rbBanco.TabIndex = 0;
            this.rbBanco.TabStop = true;
            this.rbBanco.Text = "Banco";
            this.rbBanco.UseVisualStyleBackColor = true;
            this.rbBanco.CheckedChanged += new System.EventHandler(this.rbBanco_CheckedChanged);
            // 
            // rbPasta
            // 
            this.rbPasta.AutoSize = true;
            this.rbPasta.Location = new System.Drawing.Point(95, 29);
            this.rbPasta.Name = "rbPasta";
            this.rbPasta.Size = new System.Drawing.Size(61, 17);
            this.rbPasta.TabIndex = 1;
            this.rbPasta.Text = "Arquivo";
            this.rbPasta.UseVisualStyleBackColor = true;
            // 
            // dgvPrevs
            // 
            this.dgvPrevs.AllowUserToAddRows = false;
            this.dgvPrevs.AllowUserToDeleteRows = false;
            this.dgvPrevs.AllowUserToOrderColumns = true;
            this.dgvPrevs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrevs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrevs.Location = new System.Drawing.Point(6, 59);
            this.dgvPrevs.Name = "dgvPrevs";
            this.dgvPrevs.ReadOnly = true;
            this.dgvPrevs.RowHeadersVisible = false;
            this.dgvPrevs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrevs.Size = new System.Drawing.Size(1032, 211);
            this.dgvPrevs.TabIndex = 3;
            // 
            // txtPrevsPath
            // 
            this.txtPrevsPath.AcceptedExtensions = null;
            this.txtPrevsPath.AllowDrop = true;
            this.txtPrevsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrevsPath.DialogTitle = "";
            this.txtPrevsPath.Location = new System.Drawing.Point(6, 59);
            this.txtPrevsPath.Name = "txtPrevsPath";
            this.txtPrevsPath.OwnerIWin32Window = null;
            this.txtPrevsPath.RootFolder = "";
            this.txtPrevsPath.Size = new System.Drawing.Size(1031, 32);
            this.txtPrevsPath.TabIndex = 4;
            this.txtPrevsPath.Title = "Prevs Base";
            this.txtPrevsPath.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtPrevsPath);
            this.groupBox3.Controls.Add(this.dgvPrevs);
            this.groupBox3.Controls.Add(this.rbPasta);
            this.groupBox3.Controls.Add(this.rbBanco);
            this.groupBox3.Location = new System.Drawing.Point(12, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1044, 276);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Prevs Base";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(741, 401);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(315, 93);
            this.txtDesc.TabIndex = 29;
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.Location = new System.Drawing.Point(741, 377);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(316, 20);
            this.txtNome.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(699, 380);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Nome";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(680, 431);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Descrição";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(904, 328);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 36;
            this.label10.Text = "Semana Previsão";
            // 
            // txtSemana
            // 
            this.txtSemana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSemana.Enabled = false;
            this.txtSemana.Location = new System.Drawing.Point(1000, 325);
            this.txtSemana.Name = "txtSemana";
            this.txtSemana.Size = new System.Drawing.Size(56, 20);
            this.txtSemana.TabIndex = 35;
            // 
            // lblInfoData
            // 
            this.lblInfoData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoData.AutoSize = true;
            this.lblInfoData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoData.Location = new System.Drawing.Point(26, 548);
            this.lblInfoData.Name = "lblInfoData";
            this.lblInfoData.Size = new System.Drawing.Size(0, 17);
            this.lblInfoData.TabIndex = 37;
            // 
            // rbRDH
            // 
            this.rbRDH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbRDH.AutoSize = true;
            this.rbRDH.Location = new System.Drawing.Point(93, 328);
            this.rbRDH.Name = "rbRDH";
            this.rbRDH.Size = new System.Drawing.Size(50, 17);
            this.rbRDH.TabIndex = 38;
            this.rbRDH.Text = "RDH";
            this.rbRDH.UseVisualStyleBackColor = true;
            this.rbRDH.CheckedChanged += new System.EventHandler(this.rbRDH_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 351);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(662, 147);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtEna);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(654, 121);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ENA";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkSensBidirecional);
            this.tabPage2.Controls.Add(this.numPassosSens);
            this.tabPage2.Controls.Add(this.chkCriarSens);
            this.tabPage2.Controls.Add(this.txtEnaSens);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(654, 121);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sensibilidade";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkSensBidirecional
            // 
            this.chkSensBidirecional.AutoSize = true;
            this.chkSensBidirecional.Checked = true;
            this.chkSensBidirecional.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSensBidirecional.Location = new System.Drawing.Point(6, 72);
            this.chkSensBidirecional.Name = "chkSensBidirecional";
            this.chkSensBidirecional.Size = new System.Drawing.Size(40, 17);
            this.chkSensBidirecional.TabIndex = 29;
            this.chkSensBidirecional.Text = "+/-";
            this.chkSensBidirecional.UseVisualStyleBackColor = true;
            // 
            // numPassosSens
            // 
            this.numPassosSens.Location = new System.Drawing.Point(6, 95);
            this.numPassosSens.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numPassosSens.Name = "numPassosSens";
            this.numPassosSens.Size = new System.Drawing.Size(65, 20);
            this.numPassosSens.TabIndex = 28;
            this.numPassosSens.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkCriarSens
            // 
            this.chkCriarSens.AutoSize = true;
            this.chkCriarSens.Location = new System.Drawing.Point(6, 7);
            this.chkCriarSens.Name = "chkCriarSens";
            this.chkCriarSens.Size = new System.Drawing.Size(47, 17);
            this.chkCriarSens.TabIndex = 27;
            this.chkCriarSens.Text = "Criar";
            this.chkCriarSens.UseVisualStyleBackColor = true;
            // 
            // txtEnaSens
            // 
            this.txtEnaSens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnaSens.Location = new System.Drawing.Point(77, 0);
            this.txtEnaSens.Name = "txtEnaSens";
            this.txtEnaSens.Size = new System.Drawing.Size(577, 121);
            this.txtEnaSens.TabIndex = 26;
            this.txtEnaSens.Text = "";
            // 
            // FormNovoPrevs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 577);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rbRDH);
            this.Controls.Add(this.lblInfoData);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSemana);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtPathExit);
            this.Controls.Add(this.txtRev);
            this.Controls.Add(this.txtMes);
            this.Controls.Add(this.txtAno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRDH);
            this.Controls.Add(this.txtImpFolder);
            this.Controls.Add(this.groupBox3);
            this.Name = "FormNovoPrevs";
            this.ShowIcon = false;
            this.Text = "Novo Prevs";
            this.Load += new System.EventHandler(this.FormNovoPrevs_Load);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.txtImpFolder, 0);
            this.Controls.SetChildIndex(this.cmbRDH, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtAno, 0);
            this.Controls.SetChildIndex(this.txtMes, 0);
            this.Controls.SetChildIndex(this.txtRev, 0);
            this.Controls.SetChildIndex(this.txtPathExit, 0);
            this.Controls.SetChildIndex(this.btnGerar, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.txtDesc, 0);
            this.Controls.SetChildIndex(this.txtSemana, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.lblInfoData, 0);
            this.Controls.SetChildIndex(this.rbRDH, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrevs)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPassosSens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolBox.Componentes.SelectFolderTextBox txtImpFolder;
        private System.Windows.Forms.ComboBox cmbRDH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAno;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.TextBox txtRev;
        private System.Windows.Forms.RichTextBox txtEna;
        private ToolBox.Componentes.SelectFolderTextBox txtPathExit;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.RadioButton rbPasta;
        private System.Windows.Forms.DataGridView dgvPrevs;
        private ToolBox.Componentes.SelectFileTextBox txtPrevsPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSemana;
        private System.Windows.Forms.Label lblInfoData;
        private System.Windows.Forms.CheckBox rbRDH;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkSensBidirecional;
        private System.Windows.Forms.NumericUpDown numPassosSens;
        private System.Windows.Forms.CheckBox chkCriarSens;
        private System.Windows.Forms.RichTextBox txtEnaSens;
    }
}