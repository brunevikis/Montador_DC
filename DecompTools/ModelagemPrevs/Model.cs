using NHibernate;
using NHibernate.Criterion;
using DecompTools.FactoryPrevs;
using System;

namespace DecompTools.ModelagemPrevs {
    public class Model {
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

        public virtual void save() {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction tx = session.BeginTransaction()) {
                try {
                    session.SaveOrUpdate(this);
                    tx.Commit();
                } catch {
                    if (tx != null) {
                        try {
                            tx.Rollback();
                        } catch {
                            //Implemetar LOG
                        };
                        //throw e;
                    }
                }
            }
        }
    }
}
