using NHibernate;
using NHibernate.Criterion;
using DecompTools.FactoryDC;
using System;

namespace DecompTools.ModelagemDC {
    /// <summary>
    /// Classe que é extendida por todas as classes de modelagem e contem funções genericas de manipulação do banco.
    /// </summary>
    public class Model {
        /// <summary>
        /// Função generica que exclui o objeto do banco de dados.
        /// </summary>
        public virtual void delete() {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction tx = session.BeginTransaction()) {
                try {
                    session.Delete(this);
                    tx.Commit();
                } catch (Exception e) {
                    if (tx != null) {
                        try {
                            tx.Rollback();
                        } catch {
                            //Implemetar LOG
                        };
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// Função generica que insere o objeto no banco de dados
        /// </summary>
        public virtual void save() {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction tx = session.BeginTransaction()) {
                try {
                    session.SaveOrUpdate(this);
                    tx.Commit();
                } catch (Exception e) {
                    if (tx != null) {
                        try {
                            tx.Rollback();
                        } catch (Exception) {
                            //Implemetar LOG
                        };
                        throw e;
                    }
                }
            }
        }
    }
}
