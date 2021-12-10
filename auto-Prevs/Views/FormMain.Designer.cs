namespace ComparadorDecksDC.Views
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnRv0 = new System.Windows.Forms.Button();
            this.btnCarregar = new System.Windows.Forms.Button();
            this.btnRVx = new System.Windows.Forms.Button();
            this.btnMultiRV0 = new System.Windows.Forms.Button();
            this.btnEscrever = new System.Windows.Forms.Button();
            this.calendar1 = new System.Windows.Forms.MonthCalendar();
            this.btnCarregarNW = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRv0
            // 
            this.btnRv0.Location = new System.Drawing.Point(29, 145);
            this.btnRv0.Name = "btnRv0";
            this.btnRv0.Size = new System.Drawing.Size(89, 35);
            this.btnRv0.TabIndex = 0;
            this.btnRv0.Text = "RV0 Única";
            this.btnRv0.UseVisualStyleBackColor = true;
            this.btnRv0.Click += new System.EventHandler(this.btnRv0_Click);
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(151, 18);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(89, 35);
            this.btnCarregar.TabIndex = 1;
            this.btnCarregar.Text = "Carregar DADGER";
            this.btnCarregar.UseVisualStyleBackColor = true;
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // btnRVx
            // 
            this.btnRVx.Location = new System.Drawing.Point(151, 80);
            this.btnRVx.Name = "btnRVx";
            this.btnRVx.Size = new System.Drawing.Size(89, 35);
            this.btnRVx.TabIndex = 2;
            this.btnRVx.Text = "RV x+1";
            this.btnRVx.UseVisualStyleBackColor = true;
            this.btnRVx.Click += new System.EventHandler(this.btnRVx_Click);
            // 
            // btnMultiRV0
            // 
            this.btnMultiRV0.Location = new System.Drawing.Point(151, 145);
            this.btnMultiRV0.Name = "btnMultiRV0";
            this.btnMultiRV0.Size = new System.Drawing.Size(89, 35);
            this.btnMultiRV0.TabIndex = 3;
            this.btnMultiRV0.Text = "Multi-RV0 ";
            this.btnMultiRV0.UseVisualStyleBackColor = true;
            this.btnMultiRV0.Click += new System.EventHandler(this.btnMultiRV0_Click);
            // 
            // btnEscrever
            // 
            this.btnEscrever.Location = new System.Drawing.Point(29, 80);
            this.btnEscrever.Name = "btnEscrever";
            this.btnEscrever.Size = new System.Drawing.Size(89, 35);
            this.btnEscrever.TabIndex = 4;
            this.btnEscrever.Text = "Escrever Deck";
            this.btnEscrever.UseVisualStyleBackColor = true;
            this.btnEscrever.Click += new System.EventHandler(this.btnEscrever_Click);
            // 
            // calendar1
            // 
            this.calendar1.FirstDayOfWeek = System.Windows.Forms.Day.Saturday;
            this.calendar1.Location = new System.Drawing.Point(270, 18);
            this.calendar1.Name = "calendar1";
            this.calendar1.TabIndex = 5;
            // 
            // btnCarregarNW
            // 
            this.btnCarregarNW.Location = new System.Drawing.Point(29, 18);
            this.btnCarregarNW.Name = "btnCarregarNW";
            this.btnCarregarNW.Size = new System.Drawing.Size(89, 35);
            this.btnCarregarNW.TabIndex = 6;
            this.btnCarregarNW.Text = "Carregar Deck NEWAVE";
            this.btnCarregarNW.UseVisualStyleBackColor = true;
            this.btnCarregarNW.Click += new System.EventHandler(this.btnCarregarNW_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 198);
            this.Controls.Add(this.btnCarregarNW);
            this.Controls.Add(this.calendar1);
            this.Controls.Add(this.btnEscrever);
            this.Controls.Add(this.btnMultiRV0);
            this.Controls.Add(this.btnRVx);
            this.Controls.Add(this.btnCarregar);
            this.Controls.Add(this.btnRv0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Decomp Tools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRv0;
        private System.Windows.Forms.Button btnCarregar;
        private System.Windows.Forms.Button btnRVx;
        private System.Windows.Forms.Button btnMultiRV0;
        private System.Windows.Forms.Button btnEscrever;
        private System.Windows.Forms.MonthCalendar calendar1;
        private System.Windows.Forms.Button btnCarregarNW;
    }
}