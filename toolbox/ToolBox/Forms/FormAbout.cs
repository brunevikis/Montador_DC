using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ToolBox.IniFileHandling;

namespace ToolBox.Forms
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
		}

		private void FormAbout_Load(object sender, EventArgs e)
		{
			this.Text = "Sobre o " + RunInfo.Titulo + " ...";
			this.txtVersao.Text = RunInfo.Versao.ToString();
			this.txtCompilado.Text = Ambiente.ApplicationBuildDate.ToString("dddd, dd/MM/yyyy - HH:mm:ss");
			this.txtLocal.Text = Ambiente.ApplicationExePath;

			switch (RunInfo.Ambiente)
			{
				case RunInfo.NivelAmbiente.DEV:
					this.txtAmbiente.Text = "Desenvolvimento";
					this.txtAmbiente.ForeColor = Color.DarkGreen;
					break;

				case RunInfo.NivelAmbiente.QA:
					this.txtAmbiente.Text = "Qualidade & aprovação";
					this.txtAmbiente.ForeColor = Color.OrangeRed;
					break;

				case RunInfo.NivelAmbiente.PROD:
					this.txtAmbiente.Text = "Produção";
					this.txtAmbiente.ForeColor = Color.Firebrick;
					break;

				default:
					this.txtAmbiente.Text = "Indefinido (?)";
					this.txtAmbiente.ForeColor = Color.DarkGray;
					break;

			}
		}


		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FormAbout_Shown(object sender, EventArgs e)
		{
			txtLocal.SelectionStart = 0;
			txtLocal.SelectionLength = 0;
			button1.Select();
		}

		private void FormAbout_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

	}
}
