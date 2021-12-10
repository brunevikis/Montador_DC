namespace ToolBox.Forms
{
	partial class FormAbout
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
			this.lblTituloSombra = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtVersao = new System.Windows.Forms.TextBox();
			this.txtCompilado = new System.Windows.Forms.TextBox();
			this.txtAmbiente = new System.Windows.Forms.TextBox();
			this.txtLocal = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTituloSombra
			// 
			this.lblTituloSombra.AutoSize = true;
			this.lblTituloSombra.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTituloSombra.ForeColor = System.Drawing.Color.SteelBlue;
			this.lblTituloSombra.Location = new System.Drawing.Point(-4, 6);
			this.lblTituloSombra.Name = "lblTituloSombra";
			this.lblTituloSombra.Size = new System.Drawing.Size(261, 73);
			this.lblTituloSombra.TabIndex = 0;
			this.lblTituloSombra.Text = "Captura";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Versão local:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "Data da versão:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(12, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.TabIndex = 6;
			this.label5.Text = "Caminho:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(12, 182);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 8;
			this.label9.Text = "Ambiente atual:";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.DimGray;
			this.panel2.Location = new System.Drawing.Point(8, 90);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(494, 2);
			this.panel2.TabIndex = 9;
			// 
			// txtVersao
			// 
			this.txtVersao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtVersao.BackColor = System.Drawing.Color.White;
			this.txtVersao.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtVersao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtVersao.Location = new System.Drawing.Point(121, 112);
			this.txtVersao.Name = "txtVersao";
			this.txtVersao.ReadOnly = true;
			this.txtVersao.Size = new System.Drawing.Size(381, 15);
			this.txtVersao.TabIndex = 1;
			// 
			// txtCompilado
			// 
			this.txtCompilado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCompilado.BackColor = System.Drawing.Color.White;
			this.txtCompilado.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtCompilado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCompilado.Location = new System.Drawing.Point(121, 132);
			this.txtCompilado.Name = "txtCompilado";
			this.txtCompilado.ReadOnly = true;
			this.txtCompilado.Size = new System.Drawing.Size(381, 15);
			this.txtCompilado.TabIndex = 2;
			// 
			// txtAmbiente
			// 
			this.txtAmbiente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAmbiente.BackColor = System.Drawing.Color.White;
			this.txtAmbiente.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtAmbiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAmbiente.Location = new System.Drawing.Point(121, 182);
			this.txtAmbiente.Name = "txtAmbiente";
			this.txtAmbiente.ReadOnly = true;
			this.txtAmbiente.Size = new System.Drawing.Size(381, 15);
			this.txtAmbiente.TabIndex = 4;
			// 
			// txtLocal
			// 
			this.txtLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLocal.BackColor = System.Drawing.Color.White;
			this.txtLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLocal.Location = new System.Drawing.Point(121, 152);
			this.txtLocal.Name = "txtLocal";
			this.txtLocal.ReadOnly = true;
			this.txtLocal.Size = new System.Drawing.Size(381, 15);
			this.txtLocal.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(396, 211);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(106, 41);
			this.button1.TabIndex = 5;
			this.button1.Text = "Fechar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(518, 264);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtLocal);
			this.Controls.Add(this.txtAmbiente);
			this.Controls.Add(this.txtCompilado);
			this.Controls.Add(this.txtVersao);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblTituloSombra);
			this.KeyPreview = true;
			this.MinimizeBox = false;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.Text = "FormAbout";
			this.Load += new System.EventHandler(this.FormAbout_Load);
			this.Shown += new System.EventHandler(this.FormAbout_Shown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormAbout_KeyUp);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTituloSombra;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox txtVersao;
		private System.Windows.Forms.TextBox txtCompilado;
		private System.Windows.Forms.TextBox txtAmbiente;
		private System.Windows.Forms.TextBox txtLocal;
		private System.Windows.Forms.Button button1;
	}
}