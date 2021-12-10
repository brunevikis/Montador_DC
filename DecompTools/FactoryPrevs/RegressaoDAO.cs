using DecompTools.ModelagemPrevs;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;


namespace DecompTools.FactoryPrevs {
    public class RegressaoDAO {
        /// <summary>
        /// Retorna as informações mais atuais das regressões lidas da planilha "Apoio"
        /// </summary>
        /// <returns></returns>
        public static Regressao getRegressaoOficial() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                return (Regressao)session.CreateCriteria(typeof(Regressao))
                    .Add(Expression.Eq("ativo", 1))
                    .SetMaxResults(1)
                    .UniqueResult();
            }
        }
    }
}

