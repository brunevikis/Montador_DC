using System;
using System.Collections.Generic;
using System.Text;
using DecompTools.ModelagemNW;
using NHibernate;
using NHibernate.Criterion;

namespace DecompTools.FactoryNW {
    public class BlockNWDAO {
        /// <summary>
        /// Dado um deck, carrega todas as informações de mercado deste deck
        /// </summary>
        /// <param name="id_deck">id do deck em questão</param>
        /// <returns>List com todas as informações de MERCADO do deck em questão</returns>
        public static List<MERCADO> GetAllMercByDeck(int id_deck) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var decks = (List<MERCADO>)session.CreateCriteria(typeof(MERCADO))
                    .Add(Expression.Eq("deckNW.id", id_deck))
                    .List<MERCADO>();
                return decks;
            }
        }

        /// <summary>
        /// Dado uma data, carrega todas as informações de manutenção do deck mais proximo desta data.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static MANUTT getManuttByDate(MANUTT m, int ano, int mes) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var decks = (MANUTT)session.CreateCriteria(typeof(MANUTT))
                    .CreateAlias("deckNW", "deckNW")
                    .Add(Expression.Or(Expression.And(Expression.Lt("deckNW.mes", mes), Expression.Eq("deckNW.ano", ano)), Expression.Lt("deckNW.ano", ano)))
                    //.Add(Expression.Eq("Oficial", 1))
                    .Add(Expression.Eq("Codigo", m.Codigo))
                    .Add(Expression.Eq("Unidade", m.Unidade))
                    .AddOrder(Order.Desc("deckNW.ano"))
                    .AddOrder(Order.Desc("deckNW.mes"))
                    .SetMaxResults(1)
                    .UniqueResult();

                if (decks == null)
                    return m;

                return decks;
            }
        }
    }
}
