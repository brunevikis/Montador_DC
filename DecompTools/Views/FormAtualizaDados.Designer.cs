namespace DecompTools.Views
{
    partial class FormAtualizaDados
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
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.txtDados = new ToolBox.Componentes.SelectFileTextBox();
            this.SuspendLayout();
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(471, 46);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(155, 35);
            this.btnAtualizar.TabIndex = 0;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // txtDados
            // 
            this.txtDados.AcceptedExtensions = null;
            this.txtDados.AllowDrop = true;
            this.txtDados.DialogTitle = "";
            this.txtDados.Location = new System.Drawing.Point(6, 11);
            this.txtDados.Name = "txtDados";
            this.txtDados.OwnerIWin32Window = null;
            this.txtDados.RootFolder = "";
            this.txtDados.Size = new System.Drawing.Size(620, 29);
            this.txtDados.TabIndex = 1;
            this.txtDados.Title = "Plan. Dados";
            // 
            // FormAtualizaDados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 88);
            this.Controls.Add(this.txtDados);
            this.Controls.Add(this.btnAtualizar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAtualizaDados";
            this.ShowIcon = false;
            this.Text = "FormAtualizaDados";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAtualizar;
        private ToolBox.Componentes.SelectFileTextBox txtDados;
    }
}