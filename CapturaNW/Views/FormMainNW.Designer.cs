namespace CapturaNW.Views
{
    partial class FormMainNW
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
            this.btnCarregar = new System.Windows.Forms.Button();
            this.txtDescricao = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.ckbOficial = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(466, 199);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(119, 39);
            this.btnCarregar.TabIndex = 11;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(12, 97);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(573, 96);
            this.txtDescricao.TabIndex = 10;
            this.txtDescricao.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Descrição";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 56);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(501, 20);
            this.txtNome.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nome";
            // 
            // txtFolder
            // 
            this.txtFolder.AllowDrop = true;
            this.txtFolder.Description = "";
            this.txtFolder.Location = new System.Drawing.Point(12, 12);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.OwnerIWin32Window = null;
            this.txtFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtFolder.ShowNewFolderButton = true;
            this.txtFolder.Size = new System.Drawing.Size(573, 25);
            this.txtFolder.TabIndex = 12;
            this.txtFolder.Title = "DeckNW";
            // 
            // ckbOficial
            // 
            this.ckbOficial.AutoSize = true;
            this.ckbOficial.Location = new System.Drawing.Point(530, 59);
            this.ckbOficial.Name = "ckbOficial";
            this.ckbOficial.Size = new System.Drawing.Size(55, 17);
            this.ckbOficial.TabIndex = 13;
            this.ckbOficial.Text = "Oficial";
            this.ckbOficial.UseVisualStyleBackColor = true;
            // 
            // FormMainNW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 248);
            this.Controls.Add(this.ckbOficial);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormMainNW";
            this.ShowIcon = false;
            this.Text = "Carregar Deck Newave";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.RichTextBox txtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label1;
        private ToolBox.Componentes.SelectFolderTextBox txtFolder;
        private System.Windows.Forms.CheckBox ckbOficial;
    }
}