using AutoPrevs.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;


namespace AutoPrevs.Factory
{
    public class PostosDAO
    {
        /// <summary>
        /// Dado o id do posto, carrega todas as informações dele e retorna.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Postos getById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (Postos)session.CreateCriteria(typeof(Postos))
                    .Add(Expression.Eq("id", id))
                    .SetMaxResults(1)
                    .UniqueResult();
            }
        }

        /// <summary>
        /// lista todas as postos
        /// </summary>
        /// <returns></returns>
        public static IList<Postos> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (IList<Postos>)session.CreateCriteria(typeof(Postos))
                    .List<Postos>();
            }
        }

        /// <summary>
        /// Retorna todos os postos de um submercado
        /// </summary>
        /// <returns></returns>
        public static IList<Postos> GetAllBySub( int submercado )
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (IList<Postos>)session.CreateCriteria(typeof(Postos))
                    .Add(Expression.Eq("submercado", submercado))
                    .List();
            }
        }
    }
}
