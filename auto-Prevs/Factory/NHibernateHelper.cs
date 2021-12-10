using NHibernate;
using NHibernate.Cfg;
using AutoPrevs.Modelagem;

namespace AutoPrevs.Factory
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure("hibernatePV.cfg.xml");
                    configuration.AddAssembly(typeof(Estudos).Assembly);

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
