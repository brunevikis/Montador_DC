using ComparadorDecksDC.Modelagem;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComparadorDecksDC.Factory
{
    public class CadUshDAO
    {
        /// <summary>
        /// lista todas as informações de todas as termicas do banco
        /// </summary>
        /// <returns></returns>
        public static List<CadUsh> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
               return (List<CadUsh>)session.CreateCriteria(typeof(CadUsh))
                    .List<CadUsh>();
            }
        }
    }
}
