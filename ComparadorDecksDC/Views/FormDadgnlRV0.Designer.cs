namespace ComparadorDecksDC.Views {
    partial class FormDadgnlRV0 {
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
            this.txtAno = new System.Windows.Forms.MaskedTextBox();
            this.txtMes = new System.Windows.Forms.MaskedTextBox();
            this.selectInputFile = new ToolBox.Componentes.SelectFileTextBox();
            this.selectOutputFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // txtAno
            // 
            this.txtAno.Location = new System.Drawing.Point(675, 129);
            this.txtAno.Mask = "0000";
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(53, 20);
            this.txtAno.TabIndex = 4;
            this.txtAno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMes
            // 
            this.txtMes.Location = new System.Drawing.Point(634, 129);
            this.txtMes.Mask = "00";
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(35, 20);
            this.txtMes.TabIndex = 3;
            this.txtMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(631, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(672, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ano";
            // 
            // FormDadgnlRV0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 195);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectOutputFolder);
            this.Controls.Add(this.selectInputFile);
            this.Controls.Add(this.txtMes);
            this.Controls.Add(this.txtAno);
            this.Controls.Add(this.btnGerar);
            this.Name = "FormDadgnlRV0";
            this.Text = "Dadgnl - RV0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.MaskedTextBox txtAno;
        private System.Windows.Forms.MaskedTextBox txtMes;
        private ToolBox.Componentes.SelectFileTextBox selectInputFile;
        private ToolBox.Componentes.SelectFolderTextBox selectOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}