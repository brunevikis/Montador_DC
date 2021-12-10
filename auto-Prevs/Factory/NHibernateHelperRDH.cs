using NHibernate;
using NHibernate.Cfg;
using AutoPrevs.Modelagem;

namespace AutoPrevs.Factory
{
    public class NHibernateHelperRDH
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure("hibernateRDH.cfg.xml");
                    configuration.AddAssembly(typeof(RDH).Assembly);

                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
