namespace DecompTools.Views
{
    partial class FormEscreve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEscreve));
            this.btnEscrever = new System.Windows.Forms.Button();
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.txtFolder = new ToolBox.Componentes.SelectFolderTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
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
            // dgvDeck
            // 
            this.dgvDeck.AllowUserToAddRows = false;
            this.dgvDeck.AllowUserToDeleteRows = false;
            this.dgvDeck.AllowUserToOrderColumns = true;
            this.dgvDeck.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDeck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeck.Location = new System.Drawing.Point(12, 12);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.ReadOnly = true;
            this.dgvDeck.RowHeadersVisible = false;
            this.dgvDeck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeck.Size = new System.Drawing.Size(560, 449);
            this.dgvDeck.TabIndex = 2;
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
            this.txtFolder.Title = "Destino Deck";
            // 
            // FormEscreve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 543);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.dgvDeck);
            this.Controls.Add(this.btnEscrever);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEscreve";
            this.ShowIcon = false;
            this.Text = "Escrever Deck";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEscrever;
        private System.Windows.Forms.DataGridView dgvDeck;
        private ToolBox.Componentes.SelectFolderTextBox txtFolder;
    }
}