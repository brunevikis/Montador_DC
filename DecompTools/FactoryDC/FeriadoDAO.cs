using DecompTools.ModelagemDC;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DecompTools.Util;

namespace DecompTools.FactoryDC {
    class FeriadoDAO {
        /// <summary>
        /// Busca feriados de uma semana a partir do parametro date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Feriado> GetBySemanaInicial(DateTime date) {
            using (ISession session = NHibernateGeralHelper.OpenSession()) {

                return (List<Feriado>)session.CreateCriteria(typeof(Feriado))
                    .Add(Expression.Between("DATA", date, date.AddDays(6)))
                    .List<Feriado>();

            }
        }

        /// <summary>
        /// Segunda-Feira = 1
        /// </summary>
        /// <param name="diaSemana"></param>
        /// <param name="subsistema"></param>
        /// <returns></returns>
        public static double GetImpactoFeriadoBy(Feriado feriado, int subsistema) {
            //DOMINGO	SEGUNDA-FEIRA	TERÇA-FEIRA	QUARTA-FEIRA	QUINTA-FEIRA	SEXTA-FEIRA	SÁBADO


            var fator = new double[,] {  
{0.35,	1.86,	2.33,	2.26,	1.84,	2.16,	1.12},
{0.58,	3.18,	3.40,	2.92,	2.51,	3.62,	1.29},
{0.39,	1.17,	1.56,	1.83,	1.21,	1.79,	0.72},
    {0.16,	0.98,	0.79,	1.05,	0.86,	0.94,	0.52}};

            int i = feriado.ID_SEMANA % 7;
            var f = fator[subsistema, i] / 100;

            if (feriado.DS_FERIADO == "CARNAVAL")
                f = f + fator[subsistema, i - 1] / 100;

            return fator[subsistema, i] / 100;
        }

        public static double GetImpactoSazonalBy(int mes, int subsistema) {
            var fator = new double[,] { 
            {0.26,	0.06,	-0.29,	-0.32,	-0.16,	-0.07,	0.08,	0.21,	0.15,	0.00,	-0.05,	0.12},
            {0.26,	-0.17,	-0.44,	-0.20,	-0.03,	0.04,	0.05,	-0.04,	0.07,	0.27,	0.08,	0.11},
            {-0.07,	0.08,	0.04,	-0.10,	-0.20,	-0.13,	0.06,	0.14,	0.14,	0.11,	0.03,	-0.11},
            {-0.22,	0.09,	0.07,	0.02,	-0.23,	0.06,	0.45,	0.14,	-0.04,	0.00,	-0.04,	-0.30}
            };

            return fator[subsistema, mes - 1] * 7 / 100;

        }
    }
}
