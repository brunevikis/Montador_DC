using AutoPrevs.Factory;
using AutoPrevs.Modelagem;
using System;

namespace AutoPrevs.Util
{
    public class UtilitarioDeData
    {
        private const string FormatoBrasileiro = "{0:MM/dd/yyyy}";
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public static DateTime ConvertaParaBanco(string dataString)
        {
            return Convert.ToDateTime(dataString);
        }

        public static string ConvertaParaFormatoBrasileiro(DateTime data)
        {
            return String.Format(FormatoBrasileiro, data);
        }

        public static string ConvertaParaBanco(DateTime data)
        {
            return data.ToString("yyyyMMddHHmmssfff");
        }

        public static string NomeMes(int i)
        {
            switch (i){
                case 1:
                    return "JAN";
                case 2:
                    return "FEV";
                case 3:
                    return "MAR";
                case 4:
                    return "ABR";
                case 5:
                    return "MAI";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AGO";
                case 9:
                    return "SET";
                case 10:
                    return "OUT";
                case 11:
                    return "NOV";
                case 12:
                    return "DEZ";

            }
            return null;
        }

        public static int NumeroMes(string s)
        {
            switch (s)
            {
                case "JAN":
                    return 1;
                case "FEV":
                    return 2;
                case "MAR":
                    return 3;
                case "ABR":
                    return 4;
                case "MAI":
                    return 5;
                case "JUN":
                    return 6;
                case "JUL":
                    return 7;
                case "AGO":
                    return 8;
                case "SET":
                    return 9;
                case "OUT":
                    return 10;
                case "NOV":
                    return 11;
                case "DEZ":
                    return 12;

            }
            return 0;
        }

        /// <summary>
        /// Retorna o mes anterior ao mes do parametro
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static int mesAnterior(int mes)
        {
            int mesReal = mes;

            mesReal--;
            if (mesReal != 0)
                return mesReal;
            else
                return 12;
        }

        /// <summary>
        /// Retorna o mes seguinte ao mes passado como parametro
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static int mesPosterior(int mes)
        {
            int mesReal = mes;

            mesReal++;
            if (mesReal != 13)
                return mesReal;
            else
                return 1;
        }

        /// <summary>
        /// dado 2 meses seguidos, verifica se houve uma mudança de ano
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <param name="direcao">Se direcao = 1, verifica o mes posterior, caso -1 verifica o mes anterior</param>
        /// <returns></returns>
        public static int calculaAno(int ano, int mes, int direcao)
        {
            if (direcao == 1 && mesPosterior(mes) == 1)
                ano++;
            else if (direcao == -1 && mesAnterior(mes) == 12)
                ano--;
            return ano;
        }

        public static double[][] diasMeses(Semanas_Ano semanaProj)
        {
            double[][] diasMes = new double[3][];
            for (var i = 0; i < diasMes.Length; i++)
                diasMes[i] = new double[6];

            Semanas_Ano semana0;
            //if (semanaProj.rev != 0)
                semana0 = SemanasAnoDAO.GetByMesAno(semanaProj.mes, semanaProj.ano, 0);
            //else
                //semana0 = SemanasAnoDAO.GetByMesAno(semanaProj.semanaAnterior().mes, semanaProj.semanaAnterior().ano, 0);

            DateTime dataInicio = semana0.dtInicio;
            DateTime inicioMes = new DateTime(semana0.ano, semana0.mes, 1);
            DateTime fimMes = new DateTime(inicioMes.Year, inicioMes.Month, 1).AddMonths(1);

            for (int x = 0; x < 6; x++)
            {
                diasMes[0][x] = (inicioMes - dataInicio).Days > 0 ? (inicioMes - dataInicio).Days : 0;
                if ((fimMes - dataInicio).Days < 0)
                    diasMes[2][x] = 7;
                else if ((fimMes - dataInicio).Days < 7)
                    diasMes[2][x] = 7 - (fimMes - dataInicio).Days;
                diasMes[1][x] = 7 - diasMes[0][x] - diasMes[2][x];
                dataInicio = dataInicio.AddDays(7);
            }

            return diasMes;
        }
    }
}