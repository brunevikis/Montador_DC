namespace ComparadorDecksDC.Views {
    partial class FormVazoesC {
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
            this.selectVazoesCFile = new ToolBox.Componentes.SelectFileTextBox();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.btnExportDat = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.txtOutMesFinal = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtOutAnoInicial = new System.Windows.Forms.TextBox();
            this.txtOutAnoFinal = new System.Windows.Forms.TextBox();
            this.btnMedia = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutMesInicial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInAnoFinal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInAnoInicial = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtAnoInicial = new System.Windows.Forms.TextBox();
            this.txtAnoFinal = new System.Windows.Forms.TextBox();
            this.txtMesFinal = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectVazoesCFile
            // 
            this.selectVazoesCFile.AcceptedExtensions = null;
            this.selectVazoesCFile.AllowDrop = true;
            this.selectVazoesCFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectVazoesCFile.DialogTitle = "";
            this.selectVazoesCFile.Location = new System.Drawing.Point(24, 12);
            this.selectVazoesCFile.Name = "selectVazoesCFile";
            this.selectVazoesCFile.OwnerIWin32Window = null;
            this.selectVazoesCFile.RootFolder = "";
            this.selectVazoesCFile.Size = new System.Drawing.Size(584, 28);
            this.selectVazoesCFile.TabIndex = 2;
            this.selectVazoesCFile.Title = "VazoesC (DAT, CSV)";
            // 
            // btnCarregar
            // 
            this.btnCarregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarregar.Location = new System.Drawing.Point(614, 12);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(75, 28);
            this.btnCarregar.TabIndex = 3;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // btnExportDat
            // 
            this.btnExportDat.Enabled = false;
            this.btnExportDat.Location = new System.Drawing.Point(614, 261);
            this.btnExportDat.Name = "btnExportDat";
            this.btnExportDat.Size = new System.Drawing.Size(75, 25);
            this.btnExportDat.TabIndex = 4;
            this.btnExportDat.Text = "Gravar DAT";
            this.btnExportDat.UseVisualStyleBackColor = true;
            this.btnExportDat.Click += new System.EventHandler(this.btnExportDat_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(24, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 173);
            this.panel2.TabIndex = 5;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Enabled = false;
            this.btnExportCsv.Location = new System.Drawing.Point(614, 229);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(75, 23);
            this.btnExportCsv.TabIndex = 6;
            this.btnExportCsv.Text = "Salvar CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // txtOutMesFinal
            // 
            this.txtOutMesFinal.Location = new System.Drawing.Point(129, 40);
            this.txtOutMesFinal.Name = "txtOutMesFinal";
            this.txtOutMesFinal.Size = new System.Drawing.Size(53, 20);
            this.txtOutMesFinal.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtMesFinal);
            this.panel3.Controls.Add(this.txtAnoFinal);
            this.panel3.Controls.Add(this.txtAnoInicial);
            this.panel3.Location = new System.Drawing.Point(24, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 30);
            this.panel3.TabIndex = 2;
            this.panel3.Visible = false;
            // 
            // txtOutAnoInicial
            // 
            this.txtOutAnoInicial.Location = new System.Drawing.Point(70, 66);
            this.txtOutAnoInicial.Name = "txtOutAnoInicial";
            this.txtOutAnoInicial.Size = new System.Drawing.Size(53, 20);
            this.txtOutAnoInicial.TabIndex = 0;
            // 
            // txtOutAnoFinal
            // 
            this.txtOutAnoFinal.Location = new System.Drawing.Point(129, 66);
            this.txtOutAnoFinal.Name = "txtOutAnoFinal";
            this.txtOutAnoFinal.Size = new System.Drawing.Size(53, 20);
            this.txtOutAnoFinal.TabIndex = 2;
            // 
            // btnMedia
            // 
            this.btnMedia.Location = new System.Drawing.Point(418, 56);
            this.btnMedia.Name = "btnMedia";
            this.btnMedia.Size = new System.Drawing.Size(59, 50);
            this.btnMedia.TabIndex = 3;
            this.btnMedia.Text = "Projetar Média";
            this.btnMedia.UseVisualStyleBackColor = true;
            this.btnMedia.Click += new System.EventHandler(this.btnMedia_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Final";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ano";
            // 
            // txtOutMesInicial
            // 
            this.txtOutMesInicial.Location = new System.Drawing.Point(70, 40);
            this.txtOutMesInicial.Name = "txtOutMesInicial";
            this.txtOutMesInicial.Size = new System.Drawing.Size(53, 20);
            this.txtOutMesInicial.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Inicial";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOutAnoFinal);
            this.groupBox1.Controls.Add(this.txtOutMesInicial);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOutMesFinal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOutAnoInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(212, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saída";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInAnoFinal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtInAnoInicial);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entrada";
            // 
            // txtInAnoFinal
            // 
            this.txtInAnoFinal.Location = new System.Drawing.Point(129, 66);
            this.txtInAnoFinal.Name = "txtInAnoFinal";
            this.txtInAnoFinal.Size = new System.Drawing.Size(53, 20);
            this.txtInAnoFinal.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Ano";
            // 
            // txtInAnoInicial
            // 
            this.txtInAnoInicial.Location = new System.Drawing.Point(70, 66);
            this.txtInAnoInicial.Name = "txtInAnoInicial";
            this.txtInAnoInicial.Size = new System.Drawing.Size(53, 20);
            this.txtInAnoInicial.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(67, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Inicial";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(126, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Final";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(577, 166);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnMedia);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(569, 140);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Média Simples";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtAnoInicial
            // 
            this.txtAnoInicial.BackColor = System.Drawing.Color.LightGreen;
            this.txtAnoInicial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAnoInicial.Location = new System.Drawing.Point(8, 3);
            this.txtAnoInicial.Name = "txtAnoInicial";
            this.txtAnoInicial.ReadOnly = true;
            this.txtAnoInicial.Size = new System.Drawing.Size(143, 13);
            this.txtAnoInicial.TabIndex = 0;
            // 
            // txtAnoFinal
            // 
            this.txtAnoFinal.BackColor = System.Drawing.Color.LightGreen;
            this.txtAnoFinal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAnoFinal.Location = new System.Drawing.Point(157, 3);
            this.txtAnoFinal.Name = "txtAnoFinal";
            this.txtAnoFinal.ReadOnly = true;
            this.txtAnoFinal.Size = new System.Drawing.Size(143, 13);
            this.txtAnoFinal.TabIndex = 0;
            // 
            // txtMesFinal
            // 
            this.txtMesFinal.BackColor = System.Drawing.Color.LightGreen;
            this.txtMesFinal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMesFinal.Location = new System.Drawing.Point(306, 3);
            this.txtMesFinal.Name = "txtMesFinal";
            this.txtMesFinal.ReadOnly = true;
            this.txtMesFinal.Size = new System.Drawing.Size(143, 13);
            this.txtMesFinal.TabIndex = 0;
            // 
            // FormVazoesC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 300);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnExportDat);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.selectVazoesCFile);
            this.MinimumSize = new System.Drawing.Size(709, 327);
            this.Name = "FormVazoesC";
            this.Text = "FormVazoesC";
            this.Controls.SetChildIndex(this.selectVazoesCFile, 0);
            this.Controls.SetChildIndex(this.btnCarregar, 0);
            this.Controls.SetChildIndex(this.btnExportDat, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.btnExportCsv, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolBox.Componentes.SelectFileTextBox selectVazoesCFile;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.Button btnExportDat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMedia;
        private System.Windows.Forms.TextBox txtOutAnoFinal;
        private System.Windows.Forms.TextBox txtOutAnoInicial;
        private System.Windows.Forms.TextBox txtOutMesFinal;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOutMesInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInAnoFinal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInAnoInicial;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMesFinal;
        private System.Windows.Forms.TextBox txtAnoFinal;
        private System.Windows.Forms.TextBox txtAnoInicial;
    }
}