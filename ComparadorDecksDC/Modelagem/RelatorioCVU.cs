using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Modelagem {
    public class RelatorioCVU : Model{
        public virtual int Id { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Arquivo { get; set; }
        public virtual DateTime DataAtualização { get; set; }

        public virtual IList<RelatorioCVUDetalhe> Detalhes { get; set; }

        public override string ToString() {
            return this.DataAtualização.ToString("dd/MM/yyy HH:mm:ss") + "] " + this.Titulo;
        }
    }
}
