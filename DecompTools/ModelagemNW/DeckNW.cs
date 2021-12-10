using System;

using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;


namespace DecompTools.ModelagemNW {
    public class DeckNW : Model {
        public virtual int id { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual string nome { get; set; }
        public virtual string descricao { get; set; }
        public virtual int ano { get; set; }
        public virtual int mes { get; set; }
        public virtual int oficial { get; set; }

        #region Blocos
        public virtual IList<C_ADIC> c_adic { get; set; }
        public virtual IList<CLAST_1> clast_1 { get; set; }
        public virtual IList<CLAST_2> clast_2 { get; set; }
        public virtual IList<CUSTO_DEF> custo_def { get; set; }
        public virtual IList<DESVAGUA> desvagua { get; set; }
        public virtual IList<DGER> dger { get; set; }
        //public virtual IList<EAFPAST> eafpast { get; set; }
        public virtual IList<EXPH> exph { get; set; }
        public virtual IList<EXPT> expt { get; set; }
        public virtual IList<INTERCAMBIO> intercambio { get; set; }
        public virtual IList<MANUTT> manutt { get; set; }
        public virtual IList<MERCADO> mercado { get; set; }
        public virtual IList<PEQUENAS> pequenas { get; set; }
        public virtual IList<TERM> term { get; set; }
        public virtual IList<CONFT> conft { get; set; }
        public virtual IList<PAT_CARGA> pat_carga { get; set; }
        public virtual IList<PAT_INTERCAMBIO> pat_intercambio { get; set; }
        public virtual IList<PAT_NAO_SIMULADAS> pat_nao_simuladas { get; set; }
        public virtual IList<MODIF> modif { get; set; }
        #endregion

        public virtual string[] blocos { get; set; }
        public virtual string[] blocos2 { get; set; }

        public DeckNW() {
            blocos = new string[] { "DGER.DAT", "EXPH.DAT", "EXPT.DAT", "C_ADIC.DAT", "CLAST.DAT", "MANUTT.DAT", "TERM.DAT", "SISTEMA.DAT", "CONFT.DAT", "PATAMAR.DAT", "MODIF.DAT" };
            blocos2 = new string[] { "C_ADIC", "CLAST_1", "CLAST_2", "CONFT", "CUSTO_DEF", "DGER", "EXPH", "EXPT", "INTERCAMBIO", "MANUTT", "MERCADO", "PAT_CARGA", "PAT_INTERCAMBIO","PAT_NAO_SIMULADAS", "PEQUENA", "TERM", "MODIF" };
        }
    }
}
