using System;

namespace CapturaNW.Util
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

        public static int estruturaArvore(int mes)
        {
            int a = 0;
            switch (mes)
            {
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

        //public static int mesInicialReal()
        //{
        //    int mesReal = this.mes;
        //    if (this.rev == 0 && this.dia != 1)
        //    {
        //        mesReal++;
        //        if (mesReal == 13)
        //        {
        //            mesReal = 1;
        //        }
        //    }
        //    return mesReal;
        //}

        public static int mesFinalReal(int mes)
        {
            int mesReal = mes;

            mesReal++;
            if (mesReal != 13)
                return mesReal;
            else
                return 1;
        }

        public static int anoInicialReal(int anoDeck, int mes, int mesDeck)
        {
            int anoReal = anoDeck;

            if (mesDeck == 12 && mes == 1)
                anoReal++;
            return anoReal;
        }
    }
}