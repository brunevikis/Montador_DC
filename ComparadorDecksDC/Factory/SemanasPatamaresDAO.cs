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
    class SemanasPatamaresDAO
    {
        /// <summary>
        /// Retorna uma lista com cada semana de um mes.
        /// </summary>
        /// <param name="mes">mes requerido</param>
        /// <param name="ano">ano requerido</param>
        /// <returns>List com todas as semanas_patamares do periodo escolhido</returns>
        public static List<Semanas_Patamares> GetByMonth(int mes, int ano)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                String s = String.Concat(ano.ToString(), UtilitarioDeTexto.zeroEsq( mes, 2), "%");

                ICriteria crit = session.CreateCriteria(typeof(Semanas_Patamares))
                    .Add(Expression.Like("Semana", s));
                return (List<Semanas_Patamares>)crit.List<Semanas_Patamares>();
            }
        }

        /// <summary>
        /// Retorna o valor da primeira semana do mes ou da ultima, dependendo do parametro ordem.
        /// </summary>
        /// <param name="mes">Mes escolhido</param>
        /// <param name="ano">Ano escolhido</param>
        /// <param name="ordem">Se ordem = 1, pega do começo do mes, senao pega do fim.</param>
        /// <returns>A semana_patamar do periodo escolhido</returns>
        public static Semanas_Patamares GetLastOrFirstByMonth(int mes, int ano, int ordem)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                String s = String.Concat(ano.ToString(), UtilitarioDeTexto.zeroEsq(mes, 2), "%");

                ICriteria crit = session.CreateCriteria(typeof(Semanas_Patamares))
                    .Add(Expression.Like("Semana", s))
                    .SetMaxResults(1);

                if (ordem == 1)
                    crit.AddOrder(Order.Asc("Semana"));
                else
                    crit.AddOrder(Order.Desc("Semana"));
                    
                return (Semanas_Patamares)crit.UniqueResult();
            }
        }

        /// <summary>
        /// Retorna os patamares de carga para o periodo de dias entre inicio e fim.
        /// </summary>
        /// <param name="inicio">Data de inicio do periodo</param>
        /// <param name="fim">Data final do periodo</param>
        /// <returns></returns>
        public static Semanas_Patamares GetByPeriod(DateTime inicio, DateTime fim)
        {
            DateTime veraoInicio, veraoFim, verao2Inicio, verao2Fim;
            int diasTotal, tipo1, tipo2;
            diasTotal = (fim - inicio).Days + 1;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                StringBuilder query = new StringBuilder("SELECT [COMPARADOR_DC].[dbo].[CalculaDiaTipo2] ( '");
                query.Append(inicio.ToString("yyyy-MM-dd"));
                query.Append("' , '");
                query.Append(fim.ToString("yyyy-MM-dd"));
                query.Append("')");

                ISQLQuery q = session.CreateSQLQuery(query.ToString());

                tipo2 = (int)q.UniqueResult();

                veraoInicio = (DateTime)session.CreateSQLQuery("SELECT [GERAL].[dbo].[InicioHorarioVerao]("+ inicio.ToString("yyyy") + ")").UniqueResult();
                veraoFim = (DateTime)session.CreateSQLQuery("SELECT [GERAL].[dbo].[TerminoHorarioVerao](" + inicio.AddYears(-1).ToString("yyyy") + ")").UniqueResult();
                verao2Inicio = (DateTime)session.CreateSQLQuery("SELECT [GERAL].[dbo].[InicioHorarioVerao](" + fim.ToString("yyyy") + ")").UniqueResult();
                verao2Fim = (DateTime)session.CreateSQLQuery("SELECT [GERAL].[dbo].[TerminoHorarioVerao](" + fim.AddYears(-1).ToString("yyyy") + ")").UniqueResult();
            }
            tipo1 = diasTotal - tipo2;

            Semanas_Patamares semana = new Semanas_Patamares(tipo1, tipo2);
            if ((inicio <= veraoInicio && veraoInicio <= fim) || (inicio <= verao2Inicio && verao2Inicio <= fim))
                semana.leve = semana.leve -1;
            if( (inicio <= veraoFim  && veraoFim <= fim)  || (inicio <= verao2Fim && verao2Fim <= fim)  )
                semana.medio = semana.medio + 1;

            return semana;
        }
    }
}
