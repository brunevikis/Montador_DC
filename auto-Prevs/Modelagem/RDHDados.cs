using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace AutoPrevs.Modelagem
{
    [Serializable]
    public class RDHDadosIdentifier{
        public virtual DateTime dt_rdh { get; set; }
        public virtual int id_posto { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as RDHDadosIdentifier;
            if (t == null)
                return false;
            if (this.dt_rdh == t.dt_rdh && this.id_posto == t.id_posto)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return (this.dt_rdh.ToString() + "|" + this.id_posto.ToString()).GetHashCode();
        }
    }

    public class RDHDados
    {
        private RDHDadosIdentifier _RDHDadosIdentifier = new RDHDadosIdentifier();
        public virtual RDHDadosIdentifier RDHDadosIdentifier{
            get { return _RDHDadosIdentifier; }
            set { _RDHDadosIdentifier = value; }
        }

        private RDH _rdh;
        public virtual RDH rdh{ 
            get{ return _rdh; }
            set { _rdh = value; } 
        }

        private int _posto;
        public virtual int posto
        {
            get { return _posto; }
            set { _posto = value; }
        }

        public virtual int vazaoDia { get; set; }
        public virtual int vazaoUltMax { get; set; }
        public virtual int vazaoUltMin { get; set; }
        public virtual double earm { get; set; }
        public virtual double volEspera { get; set; }
        public virtual int vazaoDefluente { get; set; }
        public virtual int vazaoIncremental { get; set; }


    }
}
