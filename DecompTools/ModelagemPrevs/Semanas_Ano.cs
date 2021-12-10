using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;
using DecompTools.FactoryPrevs;

namespace DecompTools.ModelagemPrevs {
    public class Semanas_Ano : Model {
        public Semanas_Ano() {

        }
        public Semanas_Ano(int ano, int mes, int rev, int semana) {
            this.ano = ano;
            this.mes = mes;
            this.rev = rev;
            this.semana = semana;
        }

        public virtual int id { get; set; }
        public virtual DateTime dtInicio { get; set; }
        public virtual int semana { get; set; }
        public virtual int rev { get; set; }
        public virtual int ano { get; set; }
        public virtual int mes { get; set; }

        /// <summary>
        /// Retorna a semana imediatamente seguinte
        /// </summary>
        /// <returns>Semana_Ano seguinte a atual</returns>
        public virtual Semanas_Ano semanaProxima() {
            return SemanasAnoDAO.GetNextWeek(this);
        }

        /// <summary>
        /// A partir da semana atual, avança x semanas e retorna esta semana. (Não é a função mais eficiente, porem é a mais pratica)
        /// </summary>
        /// <param name="x">Numero de semanas a avançar</param>
        /// <returns>semana_ano daqui a x semanas</returns>
        public virtual Semanas_Ano semanaProxima(int x) {
            Semanas_Ano s = this;

            while (x > 0) {
                s = s.semanaProxima();
                x -= 1;
            }
            return s;
        }

        /// <summary>
        /// Retorna a semana imediatamente anterior
        /// </summary>
        /// <returns></returns>
        public virtual Semanas_Ano semanaAnterior() {
            return SemanasAnoDAO.GetPrevWeek(this);
        }

        /// <summary>
        /// A partir da semana atual, retrocede x semanas e retorna esta semana.
        /// </summary>
        /// <param name="x">Numero de semanas a retorceder</param>
        /// <returns></returns>
        public virtual Semanas_Ano semanaAnterior(int x) {
            Semanas_Ano s = this;

            while (x > 0) {
                s = s.semanaAnterior();
                x -= 1;
            }
            return s;
        }
    }
}
