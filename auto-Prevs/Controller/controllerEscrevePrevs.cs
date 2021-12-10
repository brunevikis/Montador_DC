using AutoPrevs.Factory;
using AutoPrevs.Modelagem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPrevs.Controller
{
    public class controllerEscrevePrevs
    {
        /// <summary>
        /// Escreve um prevs do banco no HD
        /// </summary>
        /// <param name="prevs">Prevs a ser escrito</param>
        /// <param name="caminho">Caminho do HD onde o prevs será escrito</param>
        /// <returns>Mensagem de sucesso ou de falha</returns>
        public string escreverPrevs( Prevs prevs, String caminho)
        {
            Prevs prevsFull = PrevsDAO.getDataById(prevs.id);
            prevsFull.escreverPrevs( caminho );

            return "Prevs escrito com sucesso";
        }
    }
}
