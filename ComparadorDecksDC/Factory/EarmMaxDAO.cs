using ComparadorDecksDC.Modelagem;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Factory
{
    class EarmMaxDAO
    {
        /// <summary>
        /// lista o EARMAX dos submercados
        /// </summary>
        /// <returns></returns>
        public static String GetAlltoString()
        {
            var list = GetAll();

            StringBuilder sb = new StringBuilder();
            if (list != null)
                foreach (EarmMax ea in list)
                    sb.Append(ea.valor.ToString()).Append("\n");

            return sb.ToString();
        }

        public static List<EarmMax> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (List<EarmMax>)session.CreateCriteria(typeof(EarmMax))
                     .List<EarmMax>();
            }
        }
    }
}
