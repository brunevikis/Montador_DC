namespace ComparadorDecksDC.Views
{
    partial class FormReservatorio
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
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExitPath = new ToolBox.Componentes.SelectFileTextBox();
            this.btnGerar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReservSE = new System.Windows.Forms.TextBox();
            this.txtReservS = new System.Windows.Forms.TextBox();
            this.txtReservNE = new System.Windows.Forms.TextBox();
            this.txtReservN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEarm = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
            this.SuspendLayout();
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
            this.dgvDeck.Location = new System.Drawing.Point(15, 31);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.ReadOnly = true;
            this.dgvDeck.RowHeadersVisible = false;
            this.dgvDeck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeck.Size = new System.Drawing.Size(991, 327);
            this.dgvDeck.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Deck Oficial Base";
            // 
            // txtExitPath
            // 
            this.txtExitPath.AcceptedExtensions = null;
            this.txtExitPath.AllowDrop = true;
            this.txtExitPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExitPath.DialogTitle = "";
            this.txtExitPath.Location = new System.Drawing.Point(15, 500);
            this.txtExitPath.Name = "txtExitPath";
            this.txtExitPath.OwnerIWin32Window = null;
            this.txtExitPath.RootFolder = "";
            this.txtExitPath.Size = new System.Drawing.Size(991, 26);
            this.txtExitPath.TabIndex = 14;
            this.txtExitPath.Title = "Arquivo de saida";
            // 
            // btnGerar
            // 
            this.btnGerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerar.Location = new System.Drawing.Point(904, 532);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(102, 28);
            this.btnGerar.TabIndex = 15;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Sudeste";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 415);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Sul";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 443);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Nordeste";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 469);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Norte";
            // 
            // txtReservSE
            // 
            this.txtReservSE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReservSE.Location = new System.Drawing.Point(108, 386);
            this.txtReservSE.Name = "txtReservSE";
            this.txtReservSE.Size = new System.Drawing.Size(319, 20);
            this.txtReservSE.TabIndex = 21;
            // 
            // txtReservS
            // 
            this.txtReservS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReservS.Location = new System.Drawing.Point(108, 412);
            this.txtReservS.Name = "txtReservS";
            this.txtReservS.Size = new System.Drawing.Size(319, 20);
            this.txtReservS.TabIndex = 22;
            // 
            // txtReservNE
            // 
            this.txtReservNE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReservNE.Location = new System.Drawing.Point(108, 440);
            this.txtReservNE.Name = "txtReservNE";
            this.txtReservNE.Size = new System.Drawing.Size(319, 20);
            this.txtReservNE.TabIndex = 23;
            // 
            // txtReservN
            // 
            this.txtReservN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReservN.Location = new System.Drawing.Point(108, 466);
            this.txtReservN.Name = "txtReservN";
            this.txtReservN.Size = new System.Drawing.Size(319, 20);
            this.txtReservN.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(108, 369);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Meta (%)";
            // 
            // txtEarm
            // 
            this.txtEarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEarm.Location = new System.Drawing.Point(784, 394);
            this.txtEarm.Name = "txtEarm";
            this.txtEarm.Size = new System.Drawing.Size(222, 100);
            this.txtEarm.TabIndex = 26;
            this.txtEarm.Text = "";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(876, 376);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "EARM MAX";
            // 
            // FormReservatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 572);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEarm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReservN);
            this.Controls.Add(this.txtReservNE);
            this.Controls.Add(this.txtReservS);
            this.Controls.Add(this.txtReservSE);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGerar);
            this.Controls.Add(this.txtExitPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDeck);
            this.Name = "FormReservatorio";
            this.Text = "FormReservatorio";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDeck;
        private System.Windows.Forms.Label label1;
        private ToolBox.Componentes.SelectFileTextBox txtExitPath;
        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReservSE;
        private System.Windows.Forms.TextBox txtReservS;
        private System.Windows.Forms.TextBox txtReservNE;
        private System.Windows.Forms.TextBox txtReservN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtEarm;
        private System.Windows.Forms.Label label7;
    }
}