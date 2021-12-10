namespace ComparadorDecksDC.Views
{
    partial class FormComparar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComparar));
            this.txtDeck1 = new ToolBox.Componentes.SelectFileTextBox();
            this.txtDeck2 = new ToolBox.Componentes.SelectFileTextBox();
            this.btnCompara = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDeck1
            // 
            this.txtDeck1.AcceptedExtensions = null;
            this.txtDeck1.AllowDrop = true;
            this.txtDeck1.DialogTitle = "";
            this.txtDeck1.Location = new System.Drawing.Point(15, 31);
            this.txtDeck1.Name = "txtDeck1";
            this.txtDeck1.OwnerIWin32Window = null;
            this.txtDeck1.RootFolder = "";
            this.txtDeck1.Size = new System.Drawing.Size(868, 29);
            this.txtDeck1.TabIndex = 0;
            this.txtDeck1.Title = "Deck 1";
            // 
            // txtDeck2
            // 
            this.txtDeck2.AcceptedExtensions = null;
            this.txtDeck2.AllowDrop = true;
            this.txtDeck2.DialogTitle = "";
            this.txtDeck2.Location = new System.Drawing.Point(16, 30);
            this.txtDeck2.Name = "txtDeck2";
            this.txtDeck2.OwnerIWin32Window = null;
            this.txtDeck2.RootFolder = "";
            this.txtDeck2.Size = new System.Drawing.Size(867, 30);
            this.txtDeck2.TabIndex = 1;
            this.txtDeck2.Title = "Deck 2";
            // 
            // btnCompara
            // 
            this.btnCompara.Location = new System.Drawing.Point(784, 150);
            this.btnCompara.Name = "btnCompara";
            this.btnCompara.Size = new System.Drawing.Size(99, 27);
            this.btnCompara.TabIndex = 2;
            this.btnCompara.Text = "Comparar";
            this.btnCompara.UseVisualStyleBackColor = true;
            this.btnCompara.Click += new System.EventHandler(this.btnCompara_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtDeck1);
            this.panel2.Location = new System.Drawing.Point(0, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(891, 63);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDeck2);
            this.panel3.Location = new System.Drawing.Point(0, 81);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(891, 63);
            this.panel3.TabIndex = 4;
            // 
            // FormComparar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 189);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCompara);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComparar";
            this.ShowIcon = false;
            this.Text = "Comparador Dadger";
            this.Controls.SetChildIndex(this.btnCompara, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ToolBox.Componentes.SelectFileTextBox txtDeck1;
        private ToolBox.Componentes.SelectFileTextBox txtDeck2;
        private System.Windows.Forms.Button btnCompara;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}