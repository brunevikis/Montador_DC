using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC {
    public class DeParaNomePosto : Model {
        public virtual int Id { get; set; }
        public virtual DateTime DataAtualizacao { get; set; }
        public virtual string De { get; set; }
        public virtual string Para { get; set; }
    }
}

