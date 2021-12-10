using NHibernate;
using NHibernate.Cfg;
using DecompTools.ModelagemPrevs;

namespace DecompTools.FactoryPrevs {
    public class NHibernateHelper {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();
                    configuration.Configure("NhibernateConfigs\\hibernatePV.cfg.xml");
                    configuration.AddAssembly(typeof(Estudos).Assembly);

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
