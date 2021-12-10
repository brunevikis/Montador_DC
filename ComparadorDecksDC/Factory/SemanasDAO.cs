using ComparadorDecksDC.Modelagem;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComparadorDecksDC.Util;

namespace ComparadorDecksDC.Factory
{
    class SemanasDAO
    {
        /// <summary>
        /// Seleciona as informações da primeira semana operativa do mes e ano em questao
        /// </summary>
        /// <param name="mes">Mes em questao</param>
        /// <param name="ano">Ano em questão</param>
        /// <returns>Informações da primeira semana em questão</returns>
        public static Semanas GetByMesAno(int mes, int ano)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var semana = (Semanas)session.CreateCriteria(typeof(Semanas))
                    .Add(Expression.Eq("mes", mes))
                    .Add(Expression.Eq("ano", ano))
                    .UniqueResult();
                return semana;
            }
        }

        /// <summary>
        /// Seleciona as informações da primeiras semana operativa pela data da mesma
        /// </summary>
        /// <param name="ano">ano da semana</param>
        /// <param name="mes">mes da semana</param>
        /// <param name="dia">dia da semana</param>
        /// <returns>A semana do periodo em questão</returns>
        public static Semanas GetBySemanaInicial(int ano, int mes, int dia)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                DateTime d = new DateTime(ano, mes, dia);
                
                var semana = (Semanas)session.CreateCriteria(typeof(Semanas))
                    .Add(Expression.Le("primeiraSemana", d))
                    .AddOrder( Order.Desc("primeiraSemana"))
                    .SetMaxResults(1)
                    .UniqueResult();

                return semana;
            }
        }
    }
}
