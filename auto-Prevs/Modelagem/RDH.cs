using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace AutoPrevs.Modelagem
{
    public class RDH
    {
        public virtual DateTime dt_rdh { get; set; }
        public virtual IList<RDHDados> dados{ get; set; }
    }
}
