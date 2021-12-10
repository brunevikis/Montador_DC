namespace DecompTools.Views
{
    partial class FormMultiBlock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.btnGerar = new System.Windows.Forms.Button();
            this.pgbProcessoOperacao = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new ToolBox.Componentes.SelectFolderTextBox();
            this.txtDeckPreliminar = new ToolBox.Componentes.SelectFileTextBox();
            this.txtDeckOficial = new ToolBox.Componentes.SelectFileTextBox();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Dadger Preliminar";
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerar.Location = new System.Drawing.Point(739, 176);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(103, 26);
            this.btnGerar.TabIndex = 14;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // pgbProcessoOperacao
            // 
            this.pgbProcessoOperacao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pgbProcessoOperacao.Location = new System.Drawing.Point(709, 178);
            this.pgbProcessoOperacao.Name = "pgbProcessoOperacao";
            this.pgbProcessoOperacao.Size = new System.Drawing.Size(135, 23);
            this.pgbProcessoOperacao.TabIndex = 21;
            this.pgbProcessoOperacao.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Dadger Oficial";
            // 
            // txtPath
            // 
            this.txtPath.AllowDrop = true;
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Description = "";
            this.txtPath.Location = new System.Drawing.Point(57, 135);
            this.txtPath.Name = "txtPath";
            this.txtPath.OwnerIWin32Window = null;
            this.txtPath.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtPath.ShowNewFolderButton = true;
            this.txtPath.Size = new System.Drawing.Size(788, 29);
            this.txtPath.TabIndex = 25;
            this.txtPath.Title = "Caminho Saida";
            // 
            // txtDeckPreliminar
            // 
            this.txtDeckPreliminar.AcceptedExtensions = null;
            this.txtDeckPreliminar.AllowDrop = true;
            this.txtDeckPreliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeckPreliminar.DialogTitle = "";
            this.txtDeckPreliminar.Location = new System.Drawing.Point(13, 28);
            this.txtDeckPreliminar.Name = "txtDeckPreliminar";
            this.txtDeckPreliminar.OwnerIWin32Window = null;
            this.txtDeckPreliminar.RootFolder = "";
            this.txtDeckPreliminar.Size = new System.Drawing.Size(832, 29);
            this.txtDeckPreliminar.TabIndex = 26;
            this.txtDeckPreliminar.Title = "Caminho deck preliminar";
            // 
            // txtDeckOficial
            // 
            this.txtDeckOficial.AcceptedExtensions = null;
            this.txtDeckOficial.AllowDrop = true;
            this.txtDeckOficial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeckOficial.DialogTitle = "";
            this.txtDeckOficial.Location = new System.Drawing.Point(26, 86);
            this.txtDeckOficial.Name = "txtDeckOficial";
            this.txtDeckOficial.OwnerIWin32Window = null;
            this.txtDeckOficial.RootFolder = "";
            this.txtDeckOficial.Size = new System.Drawing.Size(817, 29);
            this.txtDeckOficial.TabIndex = 27;
            this.txtDeckOficial.Title = "Caminho Deck Oficial";
            // 
            // FormMultiBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 214);
            this.Controls.Add(this.txtDeckOficial);
            this.Controls.Add(this.txtDeckPreliminar);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pgbProcessoOperacao);
            this.Name = "FormMultiBlock";
            this.ShowIcon = false;
            this.Text = "Multi Block";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.ProgressBar pgbProcessoOperacao;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private ToolBox.Componentes.SelectFolderTextBox txtPath;
        private ToolBox.Componentes.SelectFileTextBox txtDeckPreliminar;
        private ToolBox.Componentes.SelectFileTextBox txtDeckOficial;
    }
}