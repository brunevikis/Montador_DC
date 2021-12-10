using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace AutoPrevs.Modelagem
{
    public class CalculadosDados : Model
    {
        public virtual int id { get; set; }
        public virtual Calculados calculados { get; set; }
        public virtual int posto { get; set; }
        public virtual string formula { get; set; }
    }
}
