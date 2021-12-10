using NHibernate;
using NHibernate.Cfg;
using DecompTools.ModelagemDC;

namespace DecompTools.FactoryDC {
    public class NHibernateHelper {
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Inicia a sessão com o banco de dados ou retorna a já existente
        /// </summary>
        private static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();
                    configuration.Configure("NhibernateConfigs\\hibernateDC.cfg.xml");

                    configuration.AddAssembly(typeof(Deck).Assembly);

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
