using AutoPrevs.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace AutoPrevs.Factory
{
    public class MLTDAO
    {
        /// <summary>
        /// retorna a mlt de um submercado
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static MltSub getDataBySubmercado(int sub)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (MltSub)session.CreateCriteria(typeof(MltSub))
                    .Add(Expression.Eq("submercado", sub))
                    .SetMaxResults(1)
                    .UniqueResult();
            }
        }

        public static async Task<MltSub> getDataBySubmercadoAsync(int sub) {
            return await Task<MltSub>.Factory.StartNew(() => getDataBySubmercado(sub));
        }

        /// <summary>
        /// retorna a mlt de todos os submercados.
        /// </summary>
        /// <returns></returns>
        public static IList<MltSub> getAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<MltSub> foo = (IList<MltSub>)session.CreateCriteria(typeof(MltSub))
                    .AddOrder( Order.Asc( "submercado" ))
                    .List<MltSub>();
                return foo;
            }
        }
    }
}
