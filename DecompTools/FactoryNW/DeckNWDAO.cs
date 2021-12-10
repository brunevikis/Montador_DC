using DecompTools.ModelagemNW;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DecompTools.FactoryNW {
    public class DeckNWDAO {
        /// <summary>
        /// Dado o id do deck, carrega todos os blocos dele e retorna o deck com os mesmos.
        /// </summary>
        /// <param name="id">id do deck em questão</param>
        /// <returns>O deck em questão com seus respectivos blocos</returns>
        public static DeckNW getAllBlocksbyID(int id) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                ICriteria crit = session.CreateCriteria(typeof(DeckNW));
                crit.Add(Expression.Eq("id", id));
                var deckNew = (DeckNW)crit.UniqueResult();
                return deckNew;
            }
        }

        /// <summary>
        /// lista todas as informações de todos os decks do banco, sem trazer os blocos junto
        /// </summary>
        /// <returns>List com todos os decks do banco, sem seus blocos</returns>
        public static IList<DeckNW> GetAll() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                //DeckNW deck = new DeckNW();
                //ICriteria crit = session.CreateCriteria(typeof(DeckNW));
                ////Seta os blocos como lazy loading, para eles nao carregarem neste momento.
                //foreach (string s in deck.blocos2)
                //{
                //    crit.SetFetchMode(s, FetchMode.Lazy);
                //}
                //crit.AddOrder(Order.Desc("id"));
                //return crit.List<DeckNW>();

                ISQLQuery q = session.CreateSQLQuery("SELECT ID AS id,NOME AS nome,DESCRICAO AS descricao,ANO AS ano,MES AS mes,DATA_INSERCAO AS dt_Entrada, OFICIAL as oficial FROM DECKS order by dt_Entrada desc");
                return q.SetResultTransformer(Transformers.AliasToBean(typeof(DeckNW))).List<DeckNW>();
            }
        }

        public static async Task<IList<DeckNW>> GetAllAsync() {
            var tsk = Task<IList<DeckNW>>.Factory.StartNew(GetAll);
            return await tsk;
        }
        /// <summary>
        /// Retorna um deck do mes e ano escolhido
        /// </summary>
        /// <param name="mes">Mes escolhido</param>
        /// <param name="ano">Ano escolhido</param>
        /// <returns>Deck newave da data escolhida</returns>
        public static DeckNW getDeckByMonth(int mes, int ano) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var deckNew = (DeckNW)session.CreateCriteria(typeof(DeckNW))
                    .Add(Expression.Eq("ano", ano))
                    .Add(Expression.Eq("mes", mes))
                    .SetMaxResults(1)
                    .UniqueResult();

                if (deckNew == null) {
                    deckNew = (DeckNW)session.CreateCriteria(typeof(DeckNW))
                    .AddOrder(Order.Desc("ano"))
                    .AddOrder(Order.Desc("mes"))
                    .SetMaxResults(1)
                    .UniqueResult();
                }

                return deckNew;
            }
        }

        /// <summary>
        /// Dado uma data, retorna o mes oficial desta data.
        /// </summary>
        /// <param name="mes">Mes escolhido</param>
        /// <param name="ano">Ano escolhido</param>
        /// <returns>Deck Newave Oficial da data escolhida</returns>
        public static DeckNW getDeckOficialByMonth(int mes, int ano) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var deckNew = (DeckNW)session.CreateCriteria(typeof(DeckNW))
                    .Add(Expression.Eq("ano", ano))
                    .Add(Expression.Eq("mes", mes))
                    .Add(Expression.Eq("oficial", 1))
                    .SetMaxResults(1)
                    .UniqueResult();

                return deckNew;
            }
        }
    }
}
