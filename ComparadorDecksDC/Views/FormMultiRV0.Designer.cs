namespace ComparadorDecksDC.Views
{
    partial class FormMultiRV0
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
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.txtNWFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.pgbProcessoOperacao = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Dadger Base";
            // 
            // dgvDeck
            // 
            this.dgvDeck.AllowUserToAddRows = false;
            this.dgvDeck.AllowUserToDeleteRows = false;
            this.dgvDeck.AllowUserToOrderColumns = true;
            this.dgvDeck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDeck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeck.Location = new System.Drawing.Point(11, 26);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.ReadOnly = true;
            this.dgvDeck.RowHeadersVisible = false;
            this.dgvDeck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeck.Size = new System.Drawing.Size(832, 161);
            this.dgvDeck.TabIndex = 11;
            // 
            // txtNWFolder
            // 
            this.txtNWFolder.AllowDrop = true;
            this.txtNWFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNWFolder.Description = "";
            this.txtNWFolder.Location = new System.Drawing.Point(13, 203);
            this.txtNWFolder.Name = "txtNWFolder";
            this.txtNWFolder.OwnerIWin32Window = null;
            this.txtNWFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtNWFolder.ShowNewFolderButton = true;
            this.txtNWFolder.Size = new System.Drawing.Size(832, 28);
            this.txtNWFolder.TabIndex = 13;
            this.txtNWFolder.Title = "Pasta estudo Encadeado";
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerar.Location = new System.Drawing.Point(740, 241);
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
            this.pgbProcessoOperacao.Location = new System.Drawing.Point(710, 243);
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
            // FormMultiRV0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 301);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtNWFolder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvDeck);
            this.Controls.Add(this.pgbProcessoOperacao);
            this.Name = "FormMultiRV0";
            this.ShowIcon = false;
            this.Text = "FormMultiRV0";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvDeck;
        private ToolBox.Componentes.SelectFolderTextBox txtNWFolder;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.ProgressBar pgbProcessoOperacao;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}