using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Modelagem {
    public class RelatorioCVUDetalhe : Model {
        public virtual int Id { get; set; }
        public virtual RelatorioCVU RelatorioCVU { get; set; }
        public virtual string Empreendimento { get; set; }
        public virtual string Combustivel { get; set; }
        public virtual string Leilao { get; set; }
        public virtual string Produto { get; set; }
        public virtual string CVU_PMO { get; set; }
    }
}
