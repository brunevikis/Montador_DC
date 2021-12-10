namespace ComparadorDecksDC.Views {
    partial class FormDadgnlRVX {
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
            this.SuspendLayout();
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(653, 160);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(75, 23);
            this.btnGerar.TabIndex = 0;
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
            this.selectInputFile.TabIndex = 5;
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
            this.selectOutputFolder.TabIndex = 6;
            this.selectOutputFolder.Title = "Pasta de Saida";
            // 
            // FormDadgnlRVX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 195);
            this.Controls.Add(this.selectOutputFolder);
            this.Controls.Add(this.selectInputFile);
            this.Controls.Add(this.btnGerar);
            this.Name = "FormDadgnlRVX";
            this.Text = "Dadgnl RVX + 1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGerar;
        private ToolBox.Componentes.SelectFileTextBox selectInputFile;
        private ToolBox.Componentes.SelectFolderTextBox selectOutputFolder;
    }
}