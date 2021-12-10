using AutoPrevs.Modelagem;
using ComparadorDecksDC.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Factory
{
    public class MltPostoDAO
    {
        /// <summary>
        /// retorna a mlt de todos os submercados.
        /// </summary>
        /// <returns></returns>
        public static IList<MltPosto> getAll()
        {
            using (ISession session = AutoPrevs.Factory.NHibernateHelper.OpenSession())
            {
                return (IList<MltPosto>)session.CreateCriteria(typeof(MltPosto))
                    .List<MltPosto>();
            }
        }

        public static IList<UH> getUhListByMonth(int mes)
        {
            List<UH> lstUH = new List<UH>();
            IList<MltPosto> lstAll = getAll();

            foreach (MltPosto mlt in lstAll)
            {
                PropertyInfo block = typeof(MltPosto).GetProperty(String.Concat("mes", mes.ToString()));

                UH uh = new UH();
                uh.campo1 = mlt.numPosto.ToString();
                uh.campo2 = mlt.submercado.ToString();
                uh.campo3 = block.GetValue(mlt).ToString();
                uh.campo4 = "1";

                lstUH.Add(uh);
            }

            return lstUH;
        }
    }
}
