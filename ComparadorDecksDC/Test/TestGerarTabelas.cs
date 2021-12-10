//using NHibernate.Cfg;
//using NHibernate.Tool.hbm2ddl;
//using NUnit.Framework;
//using ComparadorDecksDC.Modelagem;

//namespace PrimeraAplicacaoNHibernate
//{
//    [TestFixture]
//    public class TesteParaGerarTabelas
//    {
//        [Test]
//        public void testeIniciacao()
//        {
//            var cfg = new Configuration();
//            cfg.Configure();

//            cfg.AddAssembly(typeof(Deck).Assembly);

//            new SchemaExport(cfg).Execute(false, true, false);
//        }
//    }
//}

// CLASSE USADA PARA GERAR AS TABELAS DO BANCO DE DADOS, A PARTIR DOS XMLS DO nHIBERNATE