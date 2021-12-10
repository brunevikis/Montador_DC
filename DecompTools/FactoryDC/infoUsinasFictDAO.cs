using DecompTools.ModelagemDC;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecompTools.FactoryDC {
    public class infoUsinasFictDAO {
        /// <summary>
        /// lista todas as informações de todas as termicas do banco
        /// </summary>
        /// <returns>lista com todos os decks</returns>
        public static List<infoUsinasFict> GetAll() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                return (List<infoUsinasFict>)session.CreateCriteria(typeof(infoUsinasFict))
                    .List<infoUsinasFict>();
            }
        }
    }
}
