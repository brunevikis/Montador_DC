using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Strings
{
	public class StringNavigator
	{

		#region Construção

		public StringNavigator(string Text)
		{
			SetText(Text);
		}

		#endregion

		#region Atributos privados

		private string m_strText;
		private int m_intPosition = 0;
        //private int m_intLineIndex = 0;
		private int m_intLength = 0;
		//private string[][] m_arrTextAsArray;
        private int m_intRowCount = 0;
        private List<int> m_arrBreakMap = new List<int>();

		#endregion

		#region Propriedades

		/// <summary>
		/// Ler / definir a posição do ponteiro na string. 0 = primeiro caractere, Length-1 = caractere máximo permitido (último).
		/// </summary>
		public int Position
		{
			get { return m_intPosition; }
			set
			{
				SetPosition(value);
			}
		}

		/// <summary>
		/// Quantidade de caracteres
		/// </summary>
		public int Length
		{
			get { return m_intLength; }
		}

		/// <summary>
		/// Ler / definir o texto a ser navegado
		/// </summary>
		public string Text
		{
			get { return m_strText; }
			set
			{
				SetText(value);
			}
		}

        /// <summary>
        /// Quantidade de linhas (conta as quebras através dos "\n")
        /// </summary>
        public int RowCount
        {
            get { return m_intRowCount; }
            set { m_intRowCount = value; }
        }

		#endregion

		#region Métodos privados

        /// <summary>
        /// Cria o "BreakMap", que é um índice de onde estão as quebras de linhas. Isso permite
        /// navegar entre linhas com mais agilidade.
        /// </summary>
        private void CreateBreakMap()
        {
            m_arrBreakMap.Clear();
            int intPos = 0;

            m_arrBreakMap.Add(intPos);
            intPos = m_strText.IndexOf("\n", intPos);
            while (intPos != -1)
            {
                m_arrBreakMap.Add(++intPos);
                intPos = m_strText.IndexOf("\n", intPos);
            }          

        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Seta a posição do ponteiro de navegação
        /// </summary>
        /// <param name="p_intPosicao"></param>
        public void SetPosition(int p_intPosicao)
		{
            if (m_intPosition != p_intPosicao)
            {
                m_intPosition = Math.Max(Math.Min(p_intPosicao, this.Length - 1), 0);
            }
		}

        /// <summary>
        /// Definir o texto relacionado
        /// </summary>
        /// <param name="p_strTexto">String com o conteúdo do texto a navegar</param>
        public void SetText(string p_strTexto)
        {
            p_strTexto = p_strTexto.Replace("\r", "");

            m_strText = p_strTexto;
            m_intLength = p_strTexto.Length;
            m_intPosition = 0;
            m_intRowCount = m_intLength - p_strTexto.Replace("\n", "").Length + 1;

            CreateBreakMap();
        }

        /// <summary>
        /// Navegar até uma determinada string
        /// </summary>
        /// <param name="p_strTexto">String procurada</param>
        /// <param name="p_intCount">Ponto de partida</param>
        /// <returns>Posição encontrada</returns>
        public bool SeekTo(string p_strTexto, int p_intCount)
        {

            int intPosicao = (p_intCount <= 0) ?
                m_strText.IndexOf(p_strTexto, m_intPosition, StringComparison.InvariantCultureIgnoreCase) :
                m_strText.IndexOf(p_strTexto, m_intPosition, p_intCount, StringComparison.InvariantCultureIgnoreCase);
            if (intPosicao == -1)
            {
                return false;
            }
            m_intPosition = intPosicao;
            return true;
        }

        /// <summary>
        /// Navegar até uma determinada string, a partir do início do texto
        /// </summary>
        /// <param name="p_strTexto">String procurada</param>
        /// <param name="p_intCount">Ponto de partida</param>
        /// <returns>Posição encontrada</returns>
        public bool SeekTo(string p_strTexto)
        {
            return SeekTo(p_strTexto, 0);
        }

        /// <summary>
        /// Linha atual do ponteiro
        /// </summary>
        /// <returns>Índice da linha onde o ponteiro está</returns>
        public int GetActualLine()
        {
            return 0;
            //return (from n in m_arrBreakMap
            //               orderby n ascending
            //               where n < this.Position
            //               select n).Count();
        }

        /// <summary>
        /// Ler um conjunto de linhas fixas
        /// </summary>
        /// <param name="p_intFirstLine">Índice da primeira linha</param>
        /// <param name="p_intLastLine">Índice da ultima linha</param>
        /// <returns>Texto do trecho definido</returns>
        public string GetLines(int p_intFirstLine, int p_intLastLine)
        {
            return m_strText.Substring(
                    m_arrBreakMap[p_intFirstLine-1],
                    m_arrBreakMap[p_intLastLine] - m_arrBreakMap[p_intFirstLine-1] - 1);
        }

        /// <summary>
        /// Faz a leitura de um trecho quadrado da string. Leva em consideração as quebras de linha.
        /// </summary>
        /// <param name="p_intFirstLine">Primeira linha</param>
        /// <param name="p_intLastLine">Última linha</param>
        /// <param name="p_intFirstCol">Primeira coluna</param>
        /// <param name="p_intLastCol">Última coluna</param>
        /// <returns></returns>
        public string GetSquare(int p_intFirstLine, int p_intLastLine, int p_intFirstCol, int p_intLastCol)
        {

            p_intFirstCol--;
            p_intLastCol--;

            string[] arrTrecho = GetLines(p_intFirstLine, p_intLastLine).Split('\n');
            for (int i = 0; i < arrTrecho.Length; i++)
            {
                arrTrecho[i] = arrTrecho[i].Substring(p_intFirstCol, Math.Min(p_intLastCol - p_intFirstCol + 1, arrTrecho[i].Length - p_intFirstCol));
            }
            return String.Join("\n", arrTrecho);
        }

		#endregion

	}
}
