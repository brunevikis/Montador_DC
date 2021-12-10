using NHibernate;
using NHibernate.Cfg;
using DecompTools.ModelagemPrevs;

namespace DecompTools.FactoryPrevs {
    public class NHibernateHelperRDH {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();
                    configuration.Configure("NhibernateConfigs\\hibernateRDH.cfg.xml");
                    configuration.AddAssembly(typeof(RDH).Assembly);

                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }
    }
}
