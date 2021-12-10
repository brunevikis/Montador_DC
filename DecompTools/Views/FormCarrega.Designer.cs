namespace DecompTools.Views
{
    partial class FormCarrega
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCarrega));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.RichTextBox();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.txtDadger = new ToolBox.Componentes.SelectFileTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtNWFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.rbPasta = new System.Windows.Forms.RadioButton();
            this.rbBanco = new System.Windows.Forms.RadioButton();
            this.dgvNW = new System.Windows.Forms.DataGridView();
            this.ckbOficial = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 73);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(614, 20);
            this.txtNome.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescricao.Location = new System.Drawing.Point(12, 123);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(697, 96);
            this.txtDescricao.TabIndex = 5;
            this.txtDescricao.Text = "";
            // 
            // btnCarregar
            // 
            this.btnCarregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCarregar.Location = new System.Drawing.Point(584, 551);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(119, 39);
            this.btnCarregar.TabIndex = 6;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // txtDadger
            // 
            this.txtDadger.AcceptedExtensions = null;
            this.txtDadger.AllowDrop = true;
            this.txtDadger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDadger.DialogTitle = "";
            this.txtDadger.Location = new System.Drawing.Point(12, 19);
            this.txtDadger.Name = "txtDadger";
            this.txtDadger.OwnerIWin32Window = null;
            this.txtDadger.RootFolder = "";
            this.txtDadger.Size = new System.Drawing.Size(697, 35);
            this.txtDadger.TabIndex = 7;
            this.txtDadger.Title = "Dadger";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtNWFolder);
            this.groupBox3.Controls.Add(this.rbPasta);
            this.groupBox3.Controls.Add(this.rbBanco);
            this.groupBox3.Controls.Add(this.dgvNW);
            this.groupBox3.Location = new System.Drawing.Point(12, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(697, 320);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Deck NEWAVE";
            // 
            // txtNWFolder
            // 
            this.txtNWFolder.AllowDrop = true;
            this.txtNWFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNWFolder.Description = "";
            this.txtNWFolder.Location = new System.Drawing.Point(11, 57);
            this.txtNWFolder.Name = "txtNWFolder";
            this.txtNWFolder.OwnerIWin32Window = null;
            this.txtNWFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtNWFolder.ShowNewFolderButton = true;
            this.txtNWFolder.Size = new System.Drawing.Size(680, 28);
            this.txtNWFolder.TabIndex = 2;
            this.txtNWFolder.Title = "Pasta Deck";
            this.txtNWFolder.Visible = false;
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
            // dgvNW
            // 
            this.dgvNW.AllowUserToAddRows = false;
            this.dgvNW.AllowUserToDeleteRows = false;
            this.dgvNW.AllowUserToOrderColumns = true;
            this.dgvNW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNW.Location = new System.Drawing.Point(6, 56);
            this.dgvNW.Name = "dgvNW";
            this.dgvNW.ReadOnly = true;
            this.dgvNW.RowHeadersVisible = false;
            this.dgvNW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNW.Size = new System.Drawing.Size(685, 258);
            this.dgvNW.TabIndex = 3;
            // 
            // ckbOficial
            // 
            this.ckbOficial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbOficial.AutoSize = true;
            this.ckbOficial.Location = new System.Drawing.Point(648, 75);
            this.ckbOficial.Name = "ckbOficial";
            this.ckbOficial.Size = new System.Drawing.Size(55, 17);
            this.ckbOficial.TabIndex = 15;
            this.ckbOficial.Text = "Oficial";
            this.ckbOficial.UseVisualStyleBackColor = true;
            // 
            // FormCarrega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 602);
            this.Controls.Add(this.ckbOficial);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtDadger);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCarrega";
            this.ShowIcon = false;
            this.Text = "Carregar Deck";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtDescricao;
        private System.Windows.Forms.Button btnCarregar;
        private ToolBox.Componentes.SelectFileTextBox txtDadger;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvNW;
        private ToolBox.Componentes.SelectFolderTextBox txtNWFolder;
        private System.Windows.Forms.RadioButton rbPasta;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.CheckBox ckbOficial;
    }
}