using NHibernate;
using NHibernate.Cfg;
using ComparadorDecksDC.Modelagem;

namespace ComparadorDecksDC.Factory
{
    public class NHibernateGeralHelper
    {
        private static ISessionFactory _sessionFactory;

        /// <summary>
        /// Inicia a sessão com o banco de dados ou retorna a já existente
        /// </summary>
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure("hibernateGERAL.cfg.xml");

                    configuration.AddAssembly(typeof(Feriado).Assembly);
                    
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
