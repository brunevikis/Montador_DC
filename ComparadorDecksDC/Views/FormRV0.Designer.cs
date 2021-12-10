namespace ComparadorDecksDC.Views
{
    partial class FormRV0
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRV0));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvNW = new System.Windows.Forms.DataGridView();
            this.txtNWFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.rbPasta = new System.Windows.Forms.RadioButton();
            this.rbBanco = new System.Windows.Forms.RadioButton();
            this.btnEscrever = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAno = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtTE = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolder = new ToolBox.Componentes.SelectFolderTextBox();
            this.dgvDeck = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAddExc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvOutro = new System.Windows.Forms.DataGridView();
            this.rbOutro = new System.Windows.Forms.RadioButton();
            this.txtManual = new System.Windows.Forms.RichTextBox();
            this.rbManual = new System.Windows.Forms.RadioButton();
            this.cbExcept = new System.Windows.Forms.ComboBox();
            this.lstExcept = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutro)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1026, 692);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.btnEscrever);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgvDeck);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1018, 666);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Informações Obrigatorias";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvNW);
            this.groupBox3.Controls.Add(this.txtNWFolder);
            this.groupBox3.Controls.Add(this.rbPasta);
            this.groupBox3.Controls.Add(this.rbBanco);
            this.groupBox3.Location = new System.Drawing.Point(6, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1006, 233);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Deck NEWAVE";
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
            this.dgvNW.Location = new System.Drawing.Point(11, 57);
            this.dgvNW.Name = "dgvNW";
            this.dgvNW.ReadOnly = true;
            this.dgvNW.RowHeadersVisible = false;
            this.dgvNW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNW.Size = new System.Drawing.Size(989, 170);
            this.dgvNW.TabIndex = 3;
            // 
            // txtNWFolder
            // 
            this.txtNWFolder.AllowDrop = true;
            this.txtNWFolder.Description = "";
            this.txtNWFolder.Location = new System.Drawing.Point(11, 57);
            this.txtNWFolder.Name = "txtNWFolder";
            this.txtNWFolder.OwnerIWin32Window = null;
            this.txtNWFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtNWFolder.ShowNewFolderButton = true;
            this.txtNWFolder.Size = new System.Drawing.Size(989, 28);
            this.txtNWFolder.TabIndex = 2;
            this.txtNWFolder.Title = "Pasta Deck";
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
            // btnEscrever
            // 
            this.btnEscrever.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEscrever.Location = new System.Drawing.Point(924, 629);
            this.btnEscrever.Name = "btnEscrever";
            this.btnEscrever.Size = new System.Drawing.Size(88, 30);
            this.btnEscrever.TabIndex = 11;
            this.btnEscrever.Text = "Gerar";
            this.btnEscrever.UseVisualStyleBackColor = true;
            this.btnEscrever.Click += new System.EventHandler(this.btnEscrever_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 15);
            this.label7.TabIndex = 10;
            this.label7.Text = "Deck Decomp Base";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAno);
            this.groupBox1.Controls.Add(this.txtDesc);
            this.groupBox1.Controls.Add(this.txtTE);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFolder);
            this.groupBox1.Location = new System.Drawing.Point(6, 503);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1006, 120);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados da Rv";
            // 
            // txtAno
            // 
            this.txtAno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAno.Location = new System.Drawing.Point(917, 84);
            this.txtAno.Name = "txtAno";
            this.txtAno.Size = new System.Drawing.Size(83, 20);
            this.txtAno.TabIndex = 7;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.Location = new System.Drawing.Point(83, 84);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(417, 29);
            this.txtDesc.TabIndex = 4;
            // 
            // txtTE
            // 
            this.txtTE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTE.Location = new System.Drawing.Point(566, 58);
            this.txtTE.Name = "txtTE";
            this.txtTE.Size = new System.Drawing.Size(434, 20);
            this.txtTE.TabIndex = 5;
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.Location = new System.Drawing.Point(82, 58);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(418, 20);
            this.txtNome.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(539, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "TE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nome";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Descrição";
            // 
            // txtMes
            // 
            this.txtMes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMes.Location = new System.Drawing.Point(840, 84);
            this.txtMes.Name = "txtMes";
            this.txtMes.Size = new System.Drawing.Size(39, 20);
            this.txtMes.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(807, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mês";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(885, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ano";
            // 
            // txtFolder
            // 
            this.txtFolder.AllowDrop = true;
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Description = "";
            this.txtFolder.Location = new System.Drawing.Point(6, 19);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.OwnerIWin32Window = null;
            this.txtFolder.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.txtFolder.ShowNewFolderButton = true;
            this.txtFolder.Size = new System.Drawing.Size(994, 30);
            this.txtFolder.TabIndex = 2;
            this.txtFolder.Title = "Destino do deck";
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
            this.dgvDeck.Location = new System.Drawing.Point(6, 29);
            this.dgvDeck.Name = "dgvDeck";
            this.dgvDeck.ReadOnly = true;
            this.dgvDeck.RowHeadersVisible = false;
            this.dgvDeck.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeck.Size = new System.Drawing.Size(1006, 229);
            this.dgvDeck.TabIndex = 8;
            this.dgvDeck.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeck_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1018, 666);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Opcionais";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnAddExc);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dgvOutro);
            this.groupBox2.Controls.Add(this.rbOutro);
            this.groupBox2.Controls.Add(this.txtManual);
            this.groupBox2.Controls.Add(this.rbManual);
            this.groupBox2.Controls.Add(this.cbExcept);
            this.groupBox2.Controls.Add(this.lstExcept);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1006, 653);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exceções";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Origem";
            // 
            // btnAddExc
            // 
            this.btnAddExc.Location = new System.Drawing.Point(869, 19);
            this.btnAddExc.Name = "btnAddExc";
            this.btnAddExc.Size = new System.Drawing.Size(131, 28);
            this.btnAddExc.TabIndex = 13;
            this.btnAddExc.Text = "Adicionar Exceçao";
            this.btnAddExc.UseVisualStyleBackColor = true;
            this.btnAddExc.Click += new System.EventHandler(this.btnAddExc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bloco";
            // 
            // dgvOutro
            // 
            this.dgvOutro.AllowUserToAddRows = false;
            this.dgvOutro.AllowUserToDeleteRows = false;
            this.dgvOutro.AllowUserToOrderColumns = true;
            this.dgvOutro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutro.Location = new System.Drawing.Point(125, 62);
            this.dgvOutro.Name = "dgvOutro";
            this.dgvOutro.ReadOnly = true;
            this.dgvOutro.RowHeadersVisible = false;
            this.dgvOutro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutro.Size = new System.Drawing.Size(738, 585);
            this.dgvOutro.TabIndex = 12;
            // 
            // rbOutro
            // 
            this.rbOutro.AutoSize = true;
            this.rbOutro.Checked = true;
            this.rbOutro.Location = new System.Drawing.Point(16, 90);
            this.rbOutro.Name = "rbOutro";
            this.rbOutro.Size = new System.Drawing.Size(80, 17);
            this.rbOutro.TabIndex = 10;
            this.rbOutro.TabStop = true;
            this.rbOutro.Text = "Outro Deck";
            this.rbOutro.UseVisualStyleBackColor = true;
            this.rbOutro.CheckedChanged += new System.EventHandler(this.rbOutro_CheckedChanged);
            // 
            // txtManual
            // 
            this.txtManual.AcceptsTab = true;
            this.txtManual.Location = new System.Drawing.Point(125, 62);
            this.txtManual.Name = "txtManual";
            this.txtManual.Size = new System.Drawing.Size(738, 167);
            this.txtManual.TabIndex = 3;
            this.txtManual.Text = "";
            this.txtManual.Visible = false;
            // 
            // rbManual
            // 
            this.rbManual.AutoSize = true;
            this.rbManual.Location = new System.Drawing.Point(16, 113);
            this.rbManual.Name = "rbManual";
            this.rbManual.Size = new System.Drawing.Size(60, 17);
            this.rbManual.TabIndex = 11;
            this.rbManual.Text = "Manual";
            this.rbManual.UseVisualStyleBackColor = true;
            this.rbManual.CheckedChanged += new System.EventHandler(this.rbManual_CheckedChanged);
            // 
            // cbExcept
            // 
            this.cbExcept.FormattingEnabled = true;
            this.cbExcept.Items.AddRange(new object[] {
            "UH",
            "CT",
            "UE",
            "DP",
            "CD",
            "PQ",
            "IT",
            "IA",
            "TX",
            "GP",
            "NI",
            "MP",
            "MT",
            "FD",
            "VE",
            "RHE",
            "VI",
            "QI",
            "AC",
            "CI",
            "FC ",
            "EA",
            "ES",
            "TI",
            "RQ",
            "EZ",
            "RHA",
            "RHV",
            "RHQ"});
            this.cbExcept.Location = new System.Drawing.Point(125, 25);
            this.cbExcept.Name = "cbExcept";
            this.cbExcept.Size = new System.Drawing.Size(158, 21);
            this.cbExcept.TabIndex = 9;
            // 
            // lstExcept
            // 
            this.lstExcept.FormattingEnabled = true;
            this.lstExcept.IntegralHeight = false;
            this.lstExcept.Location = new System.Drawing.Point(869, 62);
            this.lstExcept.Name = "lstExcept";
            this.lstExcept.Size = new System.Drawing.Size(131, 219);
            this.lstExcept.TabIndex = 14;
            this.lstExcept.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstExcept_KeyDown);
            // 
            // FormRV0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 699);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRV0";
            this.ShowIcon = false;
            this.Text = "RV0";
            this.Load += new System.EventHandler(this.FormRV0_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNW)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeck)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddExc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvOutro;
        private System.Windows.Forms.RadioButton rbOutro;
        private System.Windows.Forms.RichTextBox txtManual;
        private System.Windows.Forms.RadioButton rbManual;
        private System.Windows.Forms.ComboBox cbExcept;
        private System.Windows.Forms.ListBox lstExcept;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEscrever;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAno;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtTE;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private ToolBox.Componentes.SelectFolderTextBox txtFolder;
        private System.Windows.Forms.DataGridView dgvDeck;
        private System.Windows.Forms.RadioButton rbPasta;
        private System.Windows.Forms.RadioButton rbBanco;
        private System.Windows.Forms.DataGridView dgvNW;
        private ToolBox.Componentes.SelectFolderTextBox txtNWFolder;

    }
}