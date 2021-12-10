namespace AutoPrevs.Views
{
    partial class FormEscrevePrevs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEscrevePrevs));
            this.btnEscrever = new System.Windows.Forms.Button();
            this.dgvPrevs = new System.Windows.Forms.DataGridView();
            this.txtFolder = new ToolBox.Componentes.SelectFolderTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrevs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEscrever
            // 
            this.btnEscrever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEscrever.Location = new System.Drawing.Point(484, 501);
            this.btnEscrever.Name = "btnEscrever";
            this.btnEscrever.Size = new System.Drawing.Size(88, 30);
            this.btnEscrever.TabIndex = 1;
            this.btnEscrever.Text = "Escrever";
            this.btnEscrever.UseVisualStyleBackColor = true;
            this.btnEscrever.Click += new System.EventHandler(this.btnEscrever_Click);
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
            this.dgvPrevs.Location = new System.Drawing.Point(12, 12);
            this.dgvPrevs.Name = "dgvPrevs";
            this.dgvPrevs.ReadOnly = true;
            this.dgvPrevs.RowHeadersVisible = false;
            this.dgvPrevs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrevs.Size = new System.Drawing.Size(560, 449);
            this.dgvPrevs.TabIndex = 2;
            // 
            // txtFolder
            // 
            this.txtFolder.AllowDrop = true;
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Description = "";
            this.txtFolder.Location = new System.Drawing.Point(13, 467);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.OwnerIWin32Window = null;
            this.txtFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtFolder.ShowNewFolderButton = true;
            this.txtFolder.Size = new System.Drawing.Size(559, 30);
            this.txtFolder.TabIndex = 3;
            this.txtFolder.Title = "Destino Prevs";
            // 
            // FormEscrevePrevs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 543);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.dgvPrevs);
            this.Controls.Add(this.btnEscrever);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEscrevePrevs";
            this.ShowIcon = false;
            this.Text = "Escrever Prevs";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrevs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEscrever;
        private System.Windows.Forms.DataGridView dgvPrevs;
        private ToolBox.Componentes.SelectFolderTextBox txtFolder;
    }
}