using System;
using System.Collections.Generic;

namespace ToolBox.Strings
{
    public class StringManipulator
    {
        
        private List<string> m_arrText = new List<string>();
        string m_strRawText;
      
        public StringManipulator(string p_strText)
        {
            m_strRawText = p_strText;
            m_strRawText = m_strRawText.Replace("\r", "");

            m_arrText.Clear();

            int posAntes = 0;
            int pos = 0;
            while (pos != -1)
            {
                posAntes = pos;
                pos = m_strRawText.IndexOf("\n");
                m_arrText.Add(m_strRawText.Substring(posAntes, pos - posAntes));
            }
            m_arrText.Add(m_strRawText.Substring(posAntes, pos - posAntes));
        }

        
    }
}
