namespace DecompTools.Views
{
    partial class FormNWSistemaDatNew
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvNW = new System.Windows.Forms.DataGridView();
            this.txtNWFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.rbPasta = new System.Windows.Forms.RadioButton();
            this.rbBanco = new System.Windows.Forms.RadioButton();
            this.btnEscrever = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvNW);
            this.groupBox3.Controls.Add(this.txtNWFolder);
            this.groupBox3.Controls.Add(this.rbPasta);
            this.groupBox3.Controls.Add(this.rbBanco);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(936, 335);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Deck NEWAVE";
            // 
            // dgvNW
            // 
            this.dgvNW.AllowUserToAddRows = false;
            this.dgvNW.AllowUserToDeleteRows = false;
            this.dgvNW.AllowUserToOrderColumns = true;
            this.dgvNW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNW.Location = new System.Drawing.Point(11, 52);
            this.dgvNW.Name = "dgvNW";
            this.dgvNW.ReadOnly = true;
            this.dgvNW.RowHeadersVisible = false;
            this.dgvNW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNW.Size = new System.Drawing.Size(919, 272);
            this.dgvNW.TabIndex = 3;
            // 
            // txtNWFolder
            // 
            this.txtNWFolder.AllowDrop = true;
            this.txtNWFolder.Description = "";
            this.txtNWFolder.Location = new System.Drawing.Point(6, 52);
            this.txtNWFolder.Name = "txtNWFolder";
            this.txtNWFolder.OwnerIWin32Window = null;
            this.txtNWFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtNWFolder.ShowNewFolderButton = true;
            this.txtNWFolder.Size = new System.Drawing.Size(924, 28);
            this.txtNWFolder.TabIndex = 2;
            this.txtNWFolder.Title = "Pasta Deck";
            // 
            // rbPasta
            // 
            this.rbPasta.AutoSize = true;
            this.rbPasta.Location = new System.Drawing.Point(95, 29);
            this.rbPasta.Name = "rbPasta";
            this.rbPasta.Size = new System.Drawing.Size(52, 17);
            this.rbPasta.TabIndex = 1;
            this.rbPasta.Text = "Pasta";
            this.rbPasta.UseVisualStyleBackColor = true;
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
            // btnEscrever
            // 
            this.btnEscrever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEscrever.Location = new System.Drawing.Point(860, 353);
            this.btnEscrever.Name = "btnEscrever";
            this.btnEscrever.Size = new System.Drawing.Size(88, 30);
            this.btnEscrever.TabIndex = 12;
            this.btnEscrever.Text = "Gerar";
            this.btnEscrever.UseVisualStyleBackColor = true;
            // 
            // FormNWSistemaDatNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 395);
            this.Controls.Add(this.btnEscrever);
            this.Controls.Add(this.groupBox3);
            this.Name = "FormNWSistemaDatNew";
            this.Text = "Novo Sistema.dat";
            this.Load += new System.EventHandler(this.FormNWSistemaDatNew_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvNW;
        private ToolBox.Componentes.SelectFolderTextBox txtNWFolder;
        private System.Windows.Forms.RadioButton rbPasta;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.Button btnEscrever;
    }
}