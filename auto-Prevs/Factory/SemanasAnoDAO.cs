using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPrevs.Modelagem;
using AutoPrevs.Factory;

namespace AutoPrevs.Factory {
    class SemanasAnoDAO {


        private static IList<Semanas_Ano> semanas_ano_Cache = null;
        static IList<Semanas_Ano> Semanas_ano_Cache {
            get {
                if (semanas_ano_Cache == null) {
                    FillCache();
                }
                return semanas_ano_Cache;
            }
        }

        private static void FillCache() {

            using (ISession session = NHibernateHelper.OpenSession()) {
                var semanas = (IList<Semanas_Ano>)session.CreateCriteria(typeof(Semanas_Ano))
                    .AddOrder(Order.Asc("ano"))
                    .AddOrder(Order.Asc("semana"))
                .List<Semanas_Ano>();
                semanas_ano_Cache = semanas;
            }
        }

        /// <summary>
        /// Seleciona as informações da semana operativa do mes e ano na rev em questao
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <param name="rev"></param>
        /// <returns>Semana_Ano</returns>
        public static Semanas_Ano GetByMesAno(int mes, int ano, int rev) {

            //using (ISession session = NHibernateHelper.OpenSession()) {
            //    var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
            //        .Add(Expression.Eq("mes", mes))
            //        .Add(Expression.Eq("ano", ano))
            //        .Add(Expression.Eq("rev", rev))
            //        .SetCacheable(true)
            //        .SetMaxResults(1)
            //        .UniqueResult();
            //    return semana;
            //}

            return Semanas_ano_Cache.FirstOrDefault(x => x.mes == mes && x.ano == ano && x.rev == rev);

        }

        /// <summary>
        /// Dada um ano e uma semana, seleciona as outras informações
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static Semanas_Ano GetBySemanaAno(int ano, int rev) {
            //using (ISession session = NHibernateHelper.OpenSession()) {
            //    var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
            //        .Add(Expression.Eq("ano", ano))
            //        .Add(Expression.Eq("rev", rev))
            //        .SetCacheable(true)
            //        .SetMaxResults(1)
            //        .UniqueResult();
            //    return semana;
            //}


            return Semanas_ano_Cache.FirstOrDefault(x => x.ano == ano && x.rev == rev);
        }

        /// <summary>
        /// Dada uma semana, retorna a proxima
        /// </summary>
        /// <param name="s">Semana atual</param>
        /// <returns>Proxima semana</returns>
        public static Semanas_Ano GetNextWeek(Semanas_Ano s) {
            //using (ISession session = NHibernateHelper.OpenSession()) {
            //    var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
            //        .Add(Expression.Or(Expression.And(Expression.Eq("ano", s.ano), Expression.Gt("semana", s.semana)), Expression.Gt("ano", s.ano)))
            //        .AddOrder(Order.Asc("ano"))
            //        .AddOrder(Order.Asc("semana"))
            //        .SetCacheable(true)
            //        .SetMaxResults(1)
            //        .UniqueResult();
            //    return semana;
            //}

            if (Semanas_ano_Cache.Contains(s)) {

                var idx = Semanas_ano_Cache.IndexOf(s);
                return Semanas_ano_Cache[idx + 1];

            } else
                return Semanas_ano_Cache.FirstOrDefault(x => ((x.ano == s.ano) && (x.semana > s.semana)) || (x.ano > s.ano));



        }

        /// <summary>
        /// Dada uma semana, retorna a anterior
        /// </summary>
        /// <param name="s">Semana atual</param>
        /// <returns>Semana anterior</returns>
        public static Semanas_Ano GetPrevWeek(Semanas_Ano s) {
            //using (ISession session = NHibernateHelper.OpenSession()) {
            //    var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
            //        .Add(Expression.Or(Expression.And(Expression.Eq("ano", s.ano), Expression.Lt("semana", s.semana)), Expression.Lt("ano", s.ano)))
            //        .AddOrder(Order.Desc("ano"))
            //        .AddOrder(Order.Desc("semana"))
            //        .SetCacheable(true)
            //        .SetMaxResults(1)
            //        .UniqueResult();
            //    return semana;
            //}


            if (Semanas_ano_Cache.Contains(s)) {
                var idx = Semanas_ano_Cache.IndexOf(s);
                return Semanas_ano_Cache[idx - 1];
            } else
                return Semanas_ano_Cache.OrderByDescending(x => x.ano).ThenByDescending(x => x.semana)
                    .FirstOrDefault(x => ((x.ano == s.ano) && (x.semana < s.semana)) || (x.ano < s.ano));
        }
    }

    class SemanasAnoDAOUnCached {
        /// <summary>
        /// Seleciona as informações da semana operativa do mes e ano na rev em questao
        /// </summary>
        /// <param name="mes"></param>
        /// <param name="ano"></param>
        /// <param name="rev"></param>
        /// <returns>Semana_Ano</returns>
        public static Semanas_Ano GetByMesAno(int mes, int ano, int rev) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
                    .Add(Expression.Eq("mes", mes))
                    .Add(Expression.Eq("ano", ano))
                    .Add(Expression.Eq("rev", rev))
                    .SetCacheable(true)
                    .SetMaxResults(1)
                    .UniqueResult();
                return semana;
            }
        }

        /// <summary>
        /// Dada um ano e uma semana, seleciona as outras informações
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="rev"></param>
        /// <returns></returns>
        public static Semanas_Ano GetBySemanaAno(int ano, int rev) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
                    .Add(Expression.Eq("ano", ano))
                    .Add(Expression.Eq("rev", rev))
                    .SetCacheable(true)
                    .SetMaxResults(1)
                    .UniqueResult();
                return semana;
            }
        }

        /// <summary>
        /// Dada uma semana, retorna a proxima
        /// </summary>
        /// <param name="s">Semana atual</param>
        /// <returns>Proxima semana</returns>
        public static Semanas_Ano GetNextWeek(Semanas_Ano s) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
                    .Add(Expression.Or(Expression.And(Expression.Eq("ano", s.ano), Expression.Gt("semana", s.semana)), Expression.Gt("ano", s.ano)))
                    .AddOrder(Order.Asc("ano"))
                    .AddOrder(Order.Asc("semana"))
                    .SetCacheable(true)
                    .SetMaxResults(1)
                    .UniqueResult();
                return semana;
            }
        }

        /// <summary>
        /// Dada uma semana, retorna a anterior
        /// </summary>
        /// <param name="s">Semana atual</param>
        /// <returns>Semana anterior</returns>
        public static Semanas_Ano GetPrevWeek(Semanas_Ano s) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var semana = (Semanas_Ano)session.CreateCriteria(typeof(Semanas_Ano))
                    .Add(Expression.Or(Expression.And(Expression.Eq("ano", s.ano), Expression.Lt("semana", s.semana)), Expression.Lt("ano", s.ano)))
                    .AddOrder(Order.Desc("ano"))
                    .AddOrder(Order.Desc("semana"))
                    .SetCacheable(true)
                    .SetMaxResults(1)
                    .UniqueResult();
                return semana;
            }
        }
    }
}
