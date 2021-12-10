using NHibernate;
using NHibernate.Criterion;
using CapturaNW.Factory;
using System;

namespace CapturaNW.Modelagem
{
    public class Model
    {
        public virtual void delete()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                try{
                    session.Delete(this);
                    tx.Commit();
                }catch(Exception e ){
                    if( tx != null ){
                        try{
                            tx.Rollback();
                        }catch (Exception){
                            //Implemetar LOG
                        };
                        throw e;
                    }
                }
            }
        }

        public virtual void save()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(this);
                    tx.Commit();
                }
                catch (Exception)
                {
                    if (tx != null)
                    {
                        try
                        {
                            tx.Rollback();
                        }
                        catch (Exception)
                        {
                            //Implemetar LOG
                        };
                        //throw e;
                    }
                }
            }
        }
    }
}
