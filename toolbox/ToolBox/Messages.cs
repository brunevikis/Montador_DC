using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ToolBox
{
	public static class Messages
	{
		#region Information

		public static void Info(IWin32Window p_objOwner, string p_strMessage)
		{
			MessageBox.Show(
				p_objOwner,
				p_strMessage,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
				);
		}

		#endregion

		#region Erro

		public static void Error(IWin32Window p_objOwner, string p_strMessage)
		{
			MessageBox.Show(
				p_objOwner,
				p_strMessage,
				Application.ProductName,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
		}

		public static void Error(IWin32Window p_objOwner, string p_strMessage, Exception ex)
		{
			Error(p_objOwner, p_strMessage, ex, false);
		}

		public static void Error(IWin32Window p_objOwner, string p_strMessage, Exception ex, bool p_blnFullDetails)
		{

			string strMensagem;

			if (p_blnFullDetails)
			{
				strMensagem = String.Format("{0}\n\n{1} disparou {2} com a seguinte mensagem:\n{3}\n\nStack trace:\n{4}", p_strMessage, ex.Source, ex.GetType().Name, ex.Message, ex.StackTrace);
			}
			else
			{
				strMensagem = String.Format("Ocorreu um erro do tipo {0}\nMensagem: {1}", ex.GetType().Name, ex.Message);
			}

			MessageBox.Show(
				p_objOwner,
				strMensagem,
				Application.ProductName + " [" + ex.GetType().Name + "]",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);
		}

		#endregion

	}
}
