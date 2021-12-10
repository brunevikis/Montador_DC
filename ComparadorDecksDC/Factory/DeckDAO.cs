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
    public class DeckDAO
    {
        /// <summary>
        /// lista todas as informações de todos os decks do banco, sem trazer os blocos junto
        /// </summary>
        /// <returns>lista com todos os decks</returns>
        public static IList<Deck> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ISQLQuery q = session.CreateSQLQuery("SELECT nome, descricao, te, caminho, id, id_deckNW, dt_entrada AS dt_Entrada, ano, mes, dia, rev, id_deckDC_base, oficial  FROM deck order by dt_entrada desc");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(Deck))).SetCacheable(true).List<Deck>();
            }
        }

        public static async Task<IList<Deck>> GetAllAsync() {
            var tsk = Task<IList<Deck>>.Factory.StartNew(GetAll);

            return await tsk;            

        }

        /// <summary>
        /// lista todas as informações de todos os decks oficiais do banco, sem trazer os blocos junto
        /// </summary>
        /// <returns>lista com todos os decks</returns>
        public static IList<Deck> GetAllOficial()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ISQLQuery q = session.CreateSQLQuery("SELECT nome, descricao, te, caminho, id, id_deckNW, dt_entrada AS dt_Entrada, ano, mes, dia, rev, id_deckDC_base, oficial  FROM deck Where oficial = 1 order by dt_entrada desc ");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(Deck))).SetCacheable(true).List<Deck>();
            }
        }

        /// <summary>
        /// Dado o id do deck, carrega todos os blocos dele e retorna o deck com os mesmos atrelados.
        /// </summary>
        /// <param name="id">id do deck requerido</param>
        /// <returns>O deck com seus blocos carregados</returns>
        public static Deck getAllBlocksbyID(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Deck));
                crit.Add( Expression.Eq( "id", id ) );
                var deckNew = (Deck)crit.UniqueResult();
                return deckNew;
            }
        }

        /// <summary>
        /// Função que retorna o deck oficial para o mes, ano e revisão dados.
        /// </summary>
        /// <param name="mes">mes do deck oficial requerido</param>
        /// <param name="ano">ano do deck oficial requerido</param>
        /// <returns>Deck oficial do mes e ano requerido.</returns>
        public static Deck getDeckOficialByMonth(Deck deck)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Deck))
                    .Add(Expression.Eq("oficial", 1))
                    .Add(Expression.Eq("rev", deck.rev))
                    .Add(Expression.Eq("ano", deck.ano))
                    .Add(Expression.Eq("mes", deck.mes))
                    .Add(Expression.Eq("dia", deck.dia))
                    .SetCacheable(true)
                    .SetMaxResults(1);

                var deckXXX = (Deck)crit.UniqueResult();

                return deckXXX;
            }
        }

        /// <summary>
        /// Função que rertona o deck do mes, ano e rev passados como parametro. OBS: sempre retorna um deck oficial
        /// </summary>
        /// <param name="semanaInicial">Semana Inicial do Deck</param>
        /// <param name="rev">Revisão do Deck</param>
        /// <returns></returns>
        public static Deck getDeckOficialByDate(Semanas semanaInicial, int rev)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriteria crit = session.CreateCriteria(typeof(Deck))
                    .Add(Expression.Eq("oficial", 1))
                    .Add(Expression.Eq("rev", rev))
                    .Add(Expression.Eq("ano", semanaInicial.primeiraSemana.Year ))
                    .Add(Expression.Eq("mes", semanaInicial.primeiraSemana.Month ))
                    .Add(Expression.Eq("dia", semanaInicial.primeiraSemana.Day ))
                    .SetCacheable(true)
                    .SetMaxResults(1);

                return (Deck)crit.UniqueResult();
            }
        }
    }
}
