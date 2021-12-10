using NHibernate;
using NHibernate.Cfg;
using DecompTools.ModelagemNW;

namespace DecompTools.FactoryNW {
    public class NHibernateHelper {
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Inicia a sessão com o banco de dados (NW) ou retorna a já existente
        /// </summary>
        private static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();
                    configuration.Configure("NhibernateConfigs\\hibernateNW.cfg.xml");

                    configuration.AddAssembly(typeof(DeckNW).Assembly);

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

