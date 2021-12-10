namespace ComparadorDecksDC.Views {
    partial class FormDadgnlMensal {
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
            this.btnGerar = new System.Windows.Forms.Button();
            this.selectInputFile = new ToolBox.Componentes.SelectFileTextBox();
            this.selectOutputFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMesIni = new System.Windows.Forms.TextBox();
            this.txtAnoIni = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAnoFin = new System.Windows.Forms.TextBox();
            this.txtMesFin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(653, 160);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(75, 23);
            this.btnGerar.TabIndex = 5;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // selectInputFile
            // 
            this.selectInputFile.AcceptedExtensions = null;
            this.selectInputFile.AllowDrop = true;
            this.selectInputFile.DialogTitle = "";
            this.selectInputFile.Location = new System.Drawing.Point(12, 23);
            this.selectInputFile.Name = "selectInputFile";
            this.selectInputFile.OwnerIWin32Window = null;
            this.selectInputFile.RootFolder = "";
            this.selectInputFile.Size = new System.Drawing.Size(716, 28);
            this.selectInputFile.TabIndex = 1;
            this.selectInputFile.Title = "Dadgnl Base";
            // 
            // selectOutputFolder
            // 
            this.selectOutputFolder.AllowDrop = true;
            this.selectOutputFolder.Description = "";
            this.selectOutputFolder.Location = new System.Drawing.Point(1, 68);
            this.selectOutputFolder.Name = "selectOutputFolder";
            this.selectOutputFolder.OwnerIWin32Window = null;
            this.selectOutputFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.selectOutputFolder.ShowNewFolderButton = true;
            this.selectOutputFolder.Size = new System.Drawing.Size(726, 28);
            this.selectOutputFolder.TabIndex = 2;
            this.selectOutputFolder.Title = "Pasta de Saida";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ano";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mes";
            // 
            // txtMesIni
            // 
            this.txtMesIni.Location = new System.Drawing.Point(91, 129);
            this.txtMesIni.Name = "txtMesIni";
            this.txtMesIni.Size = new System.Drawing.Size(37, 20);
            this.txtMesIni.TabIndex = 9;
            // 
            // txtAnoIni
            // 
            this.txtAnoIni.Location = new System.Drawing.Point(134, 129);
            this.txtAnoIni.Name = "txtAnoIni";
            this.txtAnoIni.Size = new System.Drawing.Size(67, 20);
            this.txtAnoIni.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Data Inicial";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Data Final";
            // 
            // txtAnoFin
            // 
            this.txtAnoFin.Location = new System.Drawing.Point(134, 155);
            this.txtAnoFin.Name = "txtAnoFin";
            this.txtAnoFin.Size = new System.Drawing.Size(67, 20);
            this.txtAnoFin.TabIndex = 13;
            // 
            // txtMesFin
            // 
            this.txtMesFin.Location = new System.Drawing.Point(91, 155);
            this.txtMesFin.Name = "txtMesFin";
            this.txtMesFin.Size = new System.Drawing.Size(37, 20);
            this.txtMesFin.TabIndex = 12;
            // 
            // FormDadgnlMensal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 195);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAnoFin);
            this.Controls.Add(this.txtMesFin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAnoIni);
            this.Controls.Add(this.txtMesIni);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectOutputFolder);
            this.Controls.Add(this.selectInputFile);
            this.Controls.Add(this.btnGerar);
            this.Name = "FormDadgnlMensal";
            this.Text = "Dadgnl - Mensal";
            this.Controls.SetChildIndex(this.btnGerar, 0);
            this.Controls.SetChildIndex(this.selectInputFile, 0);
            this.Controls.SetChildIndex(this.selectOutputFolder, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtMesIni, 0);
            this.Controls.SetChildIndex(this.txtAnoIni, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtMesFin, 0);
            this.Controls.SetChildIndex(this.txtAnoFin, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private ToolBox.Componentes.SelectFileTextBox selectInputFile;
        private ToolBox.Componentes.SelectFolderTextBox selectOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMesIni;
        private System.Windows.Forms.TextBox txtAnoIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAnoFin;
        private System.Windows.Forms.TextBox txtMesFin;
    }
}