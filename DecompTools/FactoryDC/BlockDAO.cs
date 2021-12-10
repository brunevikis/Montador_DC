using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DecompTools.ModelagemDC;
using NHibernate;
using NHibernate.Criterion;

namespace DecompTools.FactoryDC {
    class BlockDAO {
        /// <summary>
        /// Dado um deck e um bloco, retorna todas as informações deste bloco daquele deck(sem uso ate o momento)
        /// </summary>
        /// <param name="deck">Deck base</param>
        /// <param name="bloco">Bloco requerido</param>
        /// <returns>List do tipo do bloco requerido</returns>
        public static IList<blockModel> GetAllByDeck(Deck deck, string bloco) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var decks = (IList<blockModel>)session.CreateCriteria(Type.GetType("DecompTools.ModelagemDC." + bloco))
                    .Add(Expression.Eq("id_deck", deck.id))
                    .AddOrder(Order.Asc("ordem"))
                    .List();
                return decks;
            }
        }
    }
}
