﻿using DecompTools.ModelagemPrevs;
using System;
using System.Globalization;

namespace DecompTools.Util {
    public class UtilitarioDeData {
        private const string FormatoBrasileiro = "{0:MM/dd/yyyy}";
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Função não utilizada ate o momento.
        /// </summary>
        public static DateTime ConvertaParaBanco(string dataString) {
            return Convert.ToDateTime(dataString);
        }

        /// <summary>
        /// Função não utilizada ate o momento.
        /// </summary>
        public static string ConvertaParaFormatoBrasileiro(DateTime data) {
            return String.Format(FormatoBrasileiro, data);
        }

        /// <summary>
        /// Função não utilizada ate o momento.
        /// </summary>
        public static string ConvertaParaBanco(DateTime data) {
            return data.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// Retorna o nome do mes.
        /// </summary>
        /// <param name="i">numero do mes, entre 1 e 12</param>
        /// <returns>String com nome do mes</returns>
        public static string NomeMes(int i) {
            switch (i) {
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

        /// <summary>
        /// Retorna o mes pelo nome.
        /// </summary>
        /// <param name="mes">nome do mes</param>
        /// <returns>int com o numero do mes</returns>
        public static int MesNome(string mes) {
            switch (mes) {
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
        /// Retorna a estrutura de dados para o comentario final do dadger.
        /// </summary>
        /// <param name="mes">numero do mes</param>
        /// <returns>Estrutura de dados para o mes requerido em int</returns>
        public static int estruturaArvore(int mes) {
            int a = 0;
            switch (mes) {
                case 1:
                    return 116;
                case 2:
                    return 143;
                case 3:
                    return 143;
                case 4:
                    return 193;
                case 5:
                    return 267;
                case 6:
                    return 513;
                case 7:
                    return 353;
                case 8:
                    return 303;
                case 9:
                    return 259;
                case 10:
                    return 228;
                case 11:
                    return 153;
                case 12:
                    return 136;
            }

            return a;
        }

        /// <summary>
        /// Subtrai um mes e retorna o valor correto.
        /// </summary>
        /// <param name="mes">Mes a ser decrencrementado</param>
        /// <returns>Numero do novo mes</returns>
        public static int mesInicialReal(int mes) {
            int mesReal = mes;

            mesReal--;
            if (mesReal != 0)
                return mesReal;
            else
                return 12;
        }

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
        /// Soma um mes e retorna o valor correto.
        /// </summary>
        /// <param name="mes">Mes a ser incrementado</param>
        /// <returns></returns>
        public static int mesFinalReal(int mes) {
            int mesReal = mes;

            mesReal++;
            if (mesReal != 13)
                return mesReal;
            else
                return 1;
        }

        /// <summary>
        /// Verifica se a mudança de mes gerou uma mudança de ano, e caso afirmativo, retorna o ano correto
        /// </summary>
        /// <param name="anoBase"> Ano a ser avaliado </param>
        /// <param name="mesBase"> O mes que sofreu a alteração. </param>
        /// <param name="mesFinal"> O mes Alterado </param>
        /// <returns>numero do ano apos atualizar o mes.</returns>
        public static int anoInicialReal(int anoBase, int mesBase, int mesFinal) {
            int anoReal = anoBase;

            if (mesBase == 12 && mesFinal == 1)
                anoReal++;
            else if (mesBase == 1 && mesFinal == 12)
                anoReal--;
            return anoReal;
        }

        /// <summary>
        /// Retorna o numero do mes para o calendario.
        /// </summary>
        /// <param name="ano"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static int diasMes(int ano, int mes) {
            Calendar c = new GregorianCalendar();
            int dias = c.GetDaysInMonth(ano, mes);

            return dias;
        }

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

        
        public static double[][] diasMeses(Semanas_Ano semanaProj) {
            double[][] diasMes = new double[3][];
            for (var i = 0; i < diasMes.Length; i++)
                diasMes[i] = new double[6];

            Semanas_Ano semana0;
            //if (semanaProj.rev != 0)
            semana0 = DecompTools.FactoryPrevs.SemanasAnoDAO.GetByMesAno(semanaProj.mes, semanaProj.ano, 0);
            //else
            //semana0 = SemanasAnoDAO.GetByMesAno(semanaProj.semanaAnterior().mes, semanaProj.semanaAnterior().ano, 0);

            DateTime dataInicio = semana0.dtInicio;
            DateTime inicioMes = new DateTime(semana0.ano, semana0.mes, 1);
            DateTime fimMes = new DateTime(inicioMes.Year, inicioMes.Month, 1).AddMonths(1);

            for (int x = 0; x < 6; x++) {
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