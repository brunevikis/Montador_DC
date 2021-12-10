namespace DecompTools.Views {
    partial class FormCarregaCVU {
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
            this.selectCVU = new ToolBox.Componentes.SelectFileTextBox();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // selectCVU
            // 
            this.selectCVU.AcceptedExtensions = null;
            this.selectCVU.AllowDrop = true;
            this.selectCVU.DialogTitle = "";
            this.selectCVU.Location = new System.Drawing.Point(12, 12);
            this.selectCVU.Name = "selectCVU";
            this.selectCVU.OwnerIWin32Window = null;
            this.selectCVU.RootFolder = "";
            this.selectCVU.Size = new System.Drawing.Size(636, 28);
            this.selectCVU.TabIndex = 2;
            this.selectCVU.Title = "CVU (xls, xlsx)";
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(573, 89);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(75, 23);
            this.btnCarregar.TabIndex = 3;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // FormCarregaCVU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 126);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.selectCVU);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormCarregaCVU";
            this.Text = "FormCarregaCVU";
            this.Controls.SetChildIndex(this.selectCVU, 0);
            this.Controls.SetChildIndex(this.btnCarregar, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolBox.Componentes.SelectFileTextBox selectCVU;
        private System.Windows.Forms.Button btnCarregar;
    }
}