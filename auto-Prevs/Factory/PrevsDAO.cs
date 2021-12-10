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
    public class PrevsDAO
    {
        /// <summary>
        /// Dado o id do prevs, carrega todas as informações dele e retorna o prevs completo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Prevs getDataById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (Prevs)session.CreateCriteria(typeof(Prevs))
                    .Add(Expression.Eq("id", id))
                    .UniqueResult();
            }
        }
        public static async Task<Prevs> getDataByIdAsync(int id) {

            var tsk = Task.Factory.StartNew(()=>getDataById(id));
            return await tsk;

        }


        /// <summary>
        /// lista todas as informações de todos os prevs do banco, sem trazer as informações junto
        /// </summary>
        /// <returns></returns>
        public static IList<Prevs> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ISQLQuery q = session.CreateSQLQuery("SELECT * FROM Prevs order by dt_entrada desc");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(Prevs))).SetCacheable(true).List<Prevs>();
            }
        }

        public static async Task<IList<Prevs>> GetAllAsync() {
            var tsk = Task<IList<Prevs>>.Factory.StartNew(GetAll);

            return await tsk;
        }

        /// <summary>
        /// Dado uma data, retorna o prevs oficial desta data (SEM USO)
        /// </summary>
        /// <param name="mes">Mes escolhido</param>
        /// <param name="ano">Ano escolhido</param>
        /// <returns>Prevs oficial da data escolhida</returns>
        public static Prevs getPrevsOficialByMonth(int mes, int ano)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return (Prevs)session.CreateCriteria(typeof(Prevs))
                    .Add(Expression.Eq("ano", ano))
                    .Add(Expression.Eq("mes", mes))
                    .Add(Expression.Eq("oficial", 1))
                    .SetMaxResults(1)
                    .SetCacheable(true)
                    .UniqueResult();
            }
        }
    }
}
