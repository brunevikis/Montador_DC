using AutoPrevs.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace AutoPrevs.Factory {
    public class RDHDAO {
        /// <summary>
        /// Dado a data do RDH, carrega todas as informações dele e retorna completo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RDH getDataById(DateTime id) {
            using (ISession session = NHibernateHelperRDH.OpenSession()) {
                return (RDH)session.CreateCriteria(typeof(RDH))
                    .Add(Expression.Eq("dt_rdh", id))
                    .UniqueResult();
            }
        }

        public static async Task<RDH> getDataByIdAsync(DateTime id) {
            var tsk = Task<RDH>.Factory.StartNew(() => getDataById(id));
            return await tsk;
        }

        /// <summary>
        /// lista todas as informações de todos os prevs do banco, sem trazer as informações junto
        /// </summary>
        /// <returns></returns>
        public static IList<RDH> GetAll() {
            using (ISession session = NHibernateHelperRDH.OpenSession()) {
                ISQLQuery q = session.CreateSQLQuery("SELECT dt_rdh FROM fat_rdh_carga order by dt_rdh desc limit 150");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(RDH))).SetCacheable(true).List<RDH>();
            }
        }

        public static async Task<IList<RDH>> GetAllAsync() {

            var tsk = Task<IList<RDH>>.Factory.StartNew(GetAll);
            return await tsk;
        }
    }
}
