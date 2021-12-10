namespace ComparadorDecksDC.Views {
    partial class FormAtualizaCVU {
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCVUs = new System.Windows.Forms.ComboBox();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtDeck = new ToolBox.Componentes.SelectFileTextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Relatório CVU";
            // 
            // cbxCVUs
            // 
            this.cbxCVUs.FormattingEnabled = true;
            this.cbxCVUs.Location = new System.Drawing.Point(104, 100);
            this.cbxCVUs.Name = "cbxCVUs";
            this.cbxCVUs.Size = new System.Drawing.Size(791, 21);
            this.cbxCVUs.TabIndex = 7;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(820, 127);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAtualizar.TabIndex = 9;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtDeck);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(891, 63);
            this.panel2.TabIndex = 10;
            // 
            // txtDeck
            // 
            this.txtDeck.AcceptedExtensions = null;
            this.txtDeck.AllowDrop = true;
            this.txtDeck.DialogTitle = "";
            this.txtDeck.Location = new System.Drawing.Point(15, 31);
            this.txtDeck.Name = "txtDeck";
            this.txtDeck.OwnerIWin32Window = null;
            this.txtDeck.RootFolder = "";
            this.txtDeck.Size = new System.Drawing.Size(868, 29);
            this.txtDeck.TabIndex = 0;
            this.txtDeck.Title = "Deck";
            // 
            // FormAtualizaCVU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 164);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.cbxCVUs);
            this.Controls.Add(this.label1);
            this.Name = "FormAtualizaCVU";
            this.Text = "FormAtualizaCVU";
            this.Load += new System.EventHandler(this.FormAtualizaCVU_Load);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbxCVUs, 0);
            this.Controls.SetChildIndex(this.btnAtualizar, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCVUs;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Panel panel2;
        private ToolBox.Componentes.SelectFileTextBox txtDeck;
    }
}