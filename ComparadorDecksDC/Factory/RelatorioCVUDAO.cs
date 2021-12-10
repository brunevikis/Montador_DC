using ComparadorDecksDC.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComparadorDecksDC.Factory
{
    public class RelatorioCVUDAO
    {

        public static IList<RelatorioCVU> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ISQLQuery q = session.CreateSQLQuery("SELECT Id, Titulo, Arquivo, DataAtualização  FROM RelatorioCVU order by DataAtualização desc");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(RelatorioCVU))).SetCacheable(true).List<RelatorioCVU>();
            }
        }

        public static async Task<IList<RelatorioCVU>> GetAllAsync() {
            var tsk = Task<IList<RelatorioCVU>>.Factory.StartNew(GetAll);

            return await tsk;            

        }

        public static RelatorioCVU GetByID(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(RelatorioCVU));
                crit.Add(Expression.Eq("Id", id));
                var cvu = (RelatorioCVU)crit.UniqueResult();
                return cvu;
            }
        }

        public static async Task<RelatorioCVU> GetByIDAsync(int id) {
            var tsk = Task<RelatorioCVU>.Factory.StartNew(()=>GetByID(id));

            return await tsk;    
        }
    }
}
