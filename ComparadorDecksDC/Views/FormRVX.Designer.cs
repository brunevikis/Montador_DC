namespace ComparadorDecksDC.Views
{
    partial class FormRVX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRVX));
            this.btnEscrever = new System.Windows.Forms.Button();
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.txtFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.rbPasta = new System.Windows.Forms.RadioButton();
            this.rbBanco = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtArquivo = new ToolBox.Componentes.SelectFileTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEscrever
            // 
            this.btnEscrever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEscrever.Location = new System.Drawing.Point(523, 548);
            this.btnEscrever.Name = "btnEscrever";
            this.btnEscrever.Size = new System.Drawing.Size(109, 30);
            this.btnEscrever.TabIndex = 1;
            this.btnEscrever.Text = "Gerar";
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
            this.dgvDeck.Location = new System.Drawing.Point(8, 47);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.ReadOnly = true;
            this.dgvDeck.RowHeadersVisible = false;
            this.dgvDeck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeck.Size = new System.Drawing.Size(624, 459);
            this.dgvDeck.TabIndex = 2;
            // 
            // txtFolder
            // 
            this.txtFolder.AllowDrop = true;
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Description = "";
            this.txtFolder.Location = new System.Drawing.Point(8, 512);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.OwnerIWin32Window = null;
            this.txtFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtFolder.ShowNewFolderButton = true;
            this.txtFolder.Size = new System.Drawing.Size(624, 30);
            this.txtFolder.TabIndex = 3;
            this.txtFolder.Title = "Destino Deck";
            // 
            // rbPasta
            // 
            this.rbPasta.AutoSize = true;
            this.rbPasta.Location = new System.Drawing.Point(152, 15);
            this.rbPasta.Name = "rbPasta";
            this.rbPasta.Size = new System.Drawing.Size(61, 17);
            this.rbPasta.TabIndex = 5;
            this.rbPasta.Text = "Arquivo";
            this.rbPasta.UseVisualStyleBackColor = true;
            // 
            // rbBanco
            // 
            this.rbBanco.AutoSize = true;
            this.rbBanco.Checked = true;
            this.rbBanco.Location = new System.Drawing.Point(90, 15);
            this.rbBanco.Name = "rbBanco";
            this.rbBanco.Size = new System.Drawing.Size(56, 17);
            this.rbBanco.TabIndex = 4;
            this.rbBanco.TabStop = true;
            this.rbBanco.Text = "Banco";
            this.rbBanco.UseVisualStyleBackColor = true;
            this.rbBanco.CheckedChanged += new System.EventHandler(this.rbBanco_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dadger Base:";
            // 
            // txtArquivo
            // 
            this.txtArquivo.AcceptedExtensions = null;
            this.txtArquivo.AllowDrop = true;
            this.txtArquivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArquivo.DialogTitle = "";
            this.txtArquivo.Location = new System.Drawing.Point(8, 49);
            this.txtArquivo.Name = "txtArquivo";
            this.txtArquivo.OwnerIWin32Window = null;
            this.txtArquivo.RootFolder = "";
            this.txtArquivo.Size = new System.Drawing.Size(624, 30);
            this.txtArquivo.TabIndex = 7;
            this.txtArquivo.Title = "Arquivo";
            this.txtArquivo.Visible = false;
            // 
            // FormRVX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 583);
            this.Controls.Add(this.txtArquivo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbPasta);
            this.Controls.Add(this.rbBanco);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.dgvDeck);
            this.Controls.Add(this.btnEscrever);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRVX";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.Text = "Gerar RVx+1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEscrever;
        private System.Windows.Forms.DataGridView dgvDeck;
        private ToolBox.Componentes.SelectFolderTextBox txtFolder;
        private System.Windows.Forms.RadioButton rbPasta;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.Label label1;
        private ToolBox.Componentes.SelectFileTextBox txtArquivo;
    }
}