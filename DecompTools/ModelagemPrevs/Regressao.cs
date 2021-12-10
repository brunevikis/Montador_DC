using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;
using DecompTools.FactoryPrevs;

namespace DecompTools.ModelagemPrevs {
    public class Regressao : Model {
        public virtual int id { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual int ano { get; set; }
        public virtual int ativo { get; set; }
        public virtual IList<RegressaoDados> dados { get; set; }
        public virtual IList<Estudos> estudo_dependentes { get; set; }


        public static void zeraRegressaoOficial() {
            Regressao r = RegressaoDAO.getRegressaoOficial();

            if (r != null) {
                r.ativo = 0;
                r.save();
            }
        }
    }
}
