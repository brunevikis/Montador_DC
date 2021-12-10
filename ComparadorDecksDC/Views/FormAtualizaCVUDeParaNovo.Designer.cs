namespace ComparadorDecksDC.Views {
    partial class FormAtualizaCVUDeParaNovo {
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
            this.txtDe = new System.Windows.Forms.TextBox();
            this.txtPara = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnIgnorar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDe
            // 
            this.txtDe.Location = new System.Drawing.Point(119, 17);
            this.txtDe.Name = "txtDe";
            this.txtDe.Size = new System.Drawing.Size(326, 20);
            this.txtDe.TabIndex = 2;
            // 
            // txtPara
            // 
            this.txtPara.Location = new System.Drawing.Point(119, 43);
            this.txtPara.Name = "txtPara";
            this.txtPara.Size = new System.Drawing.Size(326, 20);
            this.txtPara.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(370, 72);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Para (Codigo Usina)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "De";
            // 
            // btnIgnorar
            // 
            this.btnIgnorar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIgnorar.Location = new System.Drawing.Point(289, 72);
            this.btnIgnorar.Name = "btnIgnorar";
            this.btnIgnorar.Size = new System.Drawing.Size(75, 23);
            this.btnIgnorar.TabIndex = 7;
            this.btnIgnorar.Text = "Ignorar";
            this.btnIgnorar.UseVisualStyleBackColor = true;
            this.btnIgnorar.Click += new System.EventHandler(this.btnIgnorar_Click);
            // 
            // FormAtualizaCVUDeParaNovo
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIgnorar;
            this.ClientSize = new System.Drawing.Size(457, 107);
            this.Controls.Add(this.btnIgnorar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtPara);
            this.Controls.Add(this.txtDe);
            this.Name = "FormAtualizaCVUDeParaNovo";
            this.Text = "FormAtualizaCVUDeParaNovo";
            this.Controls.SetChildIndex(this.txtDe, 0);
            this.Controls.SetChildIndex(this.txtPara, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnIgnorar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDe;
        private System.Windows.Forms.TextBox txtPara;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIgnorar;
    }
}