using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC {
    public class Dadgnl {
        public int Ano { get { return SemanaInicial.Value.AddDays(6).Year; } }
        public int Mes { get { return SemanaInicial.Value.AddDays(6).Month; } }
        public int Rev { get; set; }

        public DateTime? SemanaInicial {
            get {
                if (GL != null && GL.Count > 0) {
                    var semana = GL.Select(x => new {
                        Semana = int.Parse(x.Semana),
                        DataInicio = new DateTime(int.Parse(x.AnoInicio), int.Parse(x.MesInicio), int.Parse(x.DiaInicio))
                    }).First();


                    return semana.DataInicio.AddDays(7 * (1 - semana.Semana));
                } else
                    return null;
            }
        }

        public BlocoTG TG { get; set; }
        public BlocoGS GS { get; set; }
        public BlocoNL NL { get; set; }
        public BlocoGL GL { get; set; }

        public override string ToString() {

            var result = new StringBuilder();

            result.Append(TG.ToString());
            result.Append(GS.ToString());
            result.Append(NL.ToString());
            result.Append(GL.ToString());


            return result.ToString();
        }

        public Dadgnl CreateNewRV0(int ano, int mes) {

            var dataAtual = new DateTime(ano, mes, 1);
            var dataSeguinte = dataAtual.AddMonths(1);

            Dadgnl result = new Dadgnl();

            var semanas = FactoryDC.SemanasDAO.GetByMesAno(mes, ano);

            var semanasPatamates = FactoryDC.SemanasPatamaresDAO.GetByMonth(mes, ano);
            var semanasSeguintes = FactoryDC.SemanasPatamaresDAO.GetByMonth(dataSeguinte.Month, dataSeguinte.Year);

            if (semanas.primeiraSemana.Month != semanas.mes) {
                var dataAnterior = dataAtual.AddMonths(-1);
                semanasPatamates[0] = Semanas_Patamares.somaSemanas(semanasPatamates[0], FactoryDC.SemanasPatamaresDAO.GetLastOrFirstByMonth(dataAnterior.Month, dataAnterior.Year, 0));
            }
            if (semanas.diasMes2 > 0) {
                semanasPatamates[semanas.semanas - 1] =
                    Semanas_Patamares.somaSemanas(semanasPatamates[semanas.semanas - 1], semanasSeguintes[0]);
                semanasSeguintes.RemoveAt(0);
            }

            var intervalosMesSeguinte = (9 - semanas.semanas);

            result.NL = this.NL.CloneToRV0();

            result.GS = this.GS.CloneToRV0();
            result.GS[0].Intervalos = result.GS[2].Intervalos = semanas.semanas.ToString();
            result.GS[1].Intervalos = intervalosMesSeguinte.ToString();

            result.GL = this.GL.CloneToRV0(semanasPatamates.Concat(semanasSeguintes).ToList(), semanas.primeiraSemana);

            result.TG = this.TG.CloneToRV0(this.SemanaInicial.Value, semanas.primeiraSemana);

            return result;
        }


        public void StepRev() {

            //Bloco TG
            this.TG.StepRev();

            //Bloco GS
            this.GS.StepRev();

            //Bloco GL           
            this.GL.StepRev();

            this.Rev++;
        }

        public Dadgnl CreateNewMensal(int ano, int mes) {
            var dataAtual = new DateTime(ano, mes, 1);
            var dataSeguinte = dataAtual.AddMonths(1);

            Dadgnl result = new Dadgnl();


            result.NL = this.NL.CloneToMensal();

            result.GS = this.GS.CloneToMensal();

            Semanas_Patamares patamar = FactoryDC.SemanasPatamaresDAO.GetByPeriod(dataAtual, dataAtual.AddMonths(1).AddDays(-1));
            Semanas_Patamares patamar2 = FactoryDC.SemanasPatamaresDAO.GetByPeriod(dataAtual.AddMonths(1), dataAtual.AddMonths(1).AddMonths(1).AddDays(-1));
            result.GL = this.GL.CloneToMensal(new Semanas_Patamares[] { patamar, patamar2 }, dataAtual);

            result.TG = this.TG.CloneToMensal(this.SemanaInicial.Value, dataAtual);

            return result;


        }
    }

    public class DadgnlFactory {
        public static Dadgnl CreatFromText(string texto, int rev) {

            var result = new Dadgnl() {
                Rev = rev,
                GL = new BlocoGL(),
                GS = new BlocoGS(),
                NL = new BlocoNL(),
                TG = new BlocoTG()
            };

            foreach (var line in texto.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)) {
                AddLine(result, line);
            }

            return result;
        }

        private static void AddLine(Dadgnl dadgnl, string line) {

            var identificacao = line.PadRight(2).Substring(0, 2);

            switch (identificacao) {
                case "GL":
                    AddGLLine(dadgnl, line);
                    break;
                case "NL":
                    AddNLLine(dadgnl, line);
                    break;
                case "GS":
                    AddGSLine(dadgnl, line);
                    break;
                case "TG":
                    AddTGLine(dadgnl, line);
                    break;
                default:
                    break;
            }
        }

        private static void AddTGLine(Dadgnl dadgnl, string line) {
            TG ln = new TG(line);

            dadgnl.TG.Add(ln);
        }

        private static void AddGSLine(Dadgnl dadgnl, string line) {
            GS ln = new GS(line);

            dadgnl.GS.Add(ln);
            //throw new NotImplementedException();
        }

        private static void AddNLLine(Dadgnl dadgnl, string line) {
            NL ln = new NL(line);

            dadgnl.NL.Add(ln);
        }

        private static void AddGLLine(Dadgnl dadgnl, string line) {
            GL ln = new GL(line);

            dadgnl.GL.Add(ln);
            // throw new NotImplementedException();
        }
    }

    public class BlocoGL : List<GL> {
        public BlocoGL CloneToRV0() {
            return (BlocoGL)this.MemberwiseClone();
        }

        public BlocoGL CloneToRV0(List<Semanas_Patamares> semanasPat, DateTime primeiraSemana) {

            var newBlock = new BlocoGL();

            var usinas = this.Select(x => new { x.NumeroUsina, x.Subsistema }).Distinct();

            foreach (var usina in usinas) {

                var geracao = this.Where(x => x.NumeroUsina == usina.NumeroUsina).Select(x => new {
                    x.GeracaoPat1,
                    x.GeracaoPat2,
                    x.GeracaoPat3,
                    DataInicio = new DateTime(int.Parse(x.AnoInicio), int.Parse(x.MesInicio), int.Parse(x.DiaInicio))
                });
                var geracaoMax = new {
                    GeracaoPat1 = geracao.Max(x => x.GeracaoPat1),
                    GeracaoPat2 = geracao.Max(x => x.GeracaoPat2),
                    GeracaoPat3 = geracao.Max(x => x.GeracaoPat3),
                };

                var dataInicio = primeiraSemana;
                for (int i = 0; i < 9; i++) {
                    var gl = new GL() {
                        Identificacao = "GL",
                        DiaInicio = dataInicio.Day.ToString("00"),
                        MesInicio = dataInicio.Month.ToString("00"),
                        AnoInicio = dataInicio.Year.ToString("0000"),
                        NumeroUsina = usina.NumeroUsina,
                        Semana = (i + 1).ToString(),
                        Subsistema = usina.Subsistema,
                        DuracaoPat1 = semanasPat[i].pesado.ToString(),
                        DuracaoPat2 = semanasPat[i].medio.ToString(),
                        DuracaoPat3 = semanasPat[i].leve.ToString(),
                    };

                    if (geracao.Any(x => x.DataInicio == dataInicio)) {
                        var geracaoDaSemana = geracao.First(x => x.DataInicio == dataInicio);
                        gl.GeracaoPat1 = geracaoDaSemana.GeracaoPat1;
                        gl.GeracaoPat2 = geracaoDaSemana.GeracaoPat2;
                        gl.GeracaoPat3 = geracaoDaSemana.GeracaoPat3;
                    } else {
                        gl.GeracaoPat1 = geracaoMax.GeracaoPat1;
                        gl.GeracaoPat2 = geracaoMax.GeracaoPat2;
                        gl.GeracaoPat3 = geracaoMax.GeracaoPat3;
                    }

                    newBlock.Add(gl);
                    dataInicio = dataInicio.AddDays(7);
                }
            }
            return newBlock;
        }

        public override string ToString() {
            var result = new StringBuilder();

            foreach (var item in this) {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }

        internal void StepRev() {
            foreach (var usina in this.ToList().GroupBy(x => new { x.NumeroUsina, x.Subsistema })) {

                this.Remove(usina.First());
                foreach (var gl in usina) {
                    gl.Semana = (int.Parse(gl.Semana) - 1).ToString();
                }

                var lastGl = usina.Last();
                var newGl = new GL() {
                    Subsistema = usina.Key.Subsistema,
                    Identificacao = "GL",
                    Semana = "9",
                    NumeroUsina = usina.Key.NumeroUsina,
                    DataInicio = lastGl.DataInicio.AddDays(7),
                    DuracaoPat1 = "",
                    DuracaoPat2 = "",
                    DuracaoPat3 = "",
                    GeracaoPat1 = lastGl.GeracaoPat1,
                    GeracaoPat2 = lastGl.GeracaoPat2,
                    GeracaoPat3 = lastGl.GeracaoPat3,
                };

                this.Insert(this.IndexOf(lastGl) + 1, newGl);
            }
        }

        public BlocoGL CloneToMensal(Semanas_Patamares[] semanasPat, DateTime primeiraSemana) {
            var newBlock = new BlocoGL();

            var usinas = this.Select(x => new { x.NumeroUsina, x.Subsistema }).Distinct();

            foreach (var usina in usinas) {

                var geracao = this.Where(x => x.NumeroUsina == usina.NumeroUsina).Select(x => new {
                    GeracaoPat1 = float.Parse(x.GeracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoPat2 = float.Parse(x.GeracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoPat3 = float.Parse(x.GeracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    //DuracaoPat1 = int.Parse(x.DuracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    //DuracaoPat2 = int.Parse(x.DuracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    //DuracaoPat3 = int.Parse(x.DuracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),

                    DataInicio = new DateTime(int.Parse(x.AnoInicio), int.Parse(x.MesInicio), int.Parse(x.DiaInicio))
                });
                var geracaoMax = new {
                    GeracaoPat1 = geracao.Max(x => x.GeracaoPat1),
                    GeracaoPat2 = geracao.Max(x => x.GeracaoPat2),
                    GeracaoPat3 = geracao.Max(x => x.GeracaoPat3),
                };

                var dataInicio = primeiraSemana;
                for (int i = 0; i < 2; i++) {
                    var gl = new GL() {
                        Identificacao = "GL",
                        DiaInicio = dataInicio.Day.ToString("00"),
                        MesInicio = dataInicio.Month.ToString("00"),
                        AnoInicio = dataInicio.Year.ToString("0000"),
                        NumeroUsina = usina.NumeroUsina,
                        Semana = (i + 1).ToString(),
                        Subsistema = usina.Subsistema,
                        DuracaoPat1 = semanasPat[i].pesado.ToString(),
                        DuracaoPat2 = semanasPat[i].medio.ToString(),
                        DuracaoPat3 = semanasPat[i].leve.ToString(),
                    };

                    //if (geracao.Any(x => x.DataInicio >= dataInicio)) {
                    //    var geracaoDoMes = geracao.Where(x => x.DataInicio >= dataInicio && x.DataInicio < dataInicio.AddMonths(1));

                    //    gl.GeracaoPat1 = (geracaoDoMes.Sum(x => x.GeracaoPat1 * x.DuracaoPat1) / geracaoDoMes.Sum(x => x.DuracaoPat1)).ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    //    gl.GeracaoPat2 = (geracaoDoMes.Sum(x => x.GeracaoPat2 * x.DuracaoPat2) / geracaoDoMes.Sum(x => x.DuracaoPat2)).ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    //    gl.GeracaoPat3 = (geracaoDoMes.Sum(x => x.GeracaoPat3 * x.DuracaoPat3) / geracaoDoMes.Sum(x => x.DuracaoPat3)).ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    //} else {
                    gl.GeracaoPat1 = geracaoMax.GeracaoPat1.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    gl.GeracaoPat2 = geracaoMax.GeracaoPat2.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    gl.GeracaoPat3 = geracaoMax.GeracaoPat3.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo);
                    //}

                    newBlock.Add(gl);
                    dataInicio = dataInicio.AddMonths(1);
                }
            }
            return newBlock;
        }
    }

    public class BlocoNL : List<NL> {
        public BlocoNL CloneToRV0() {
            return (BlocoNL)this.MemberwiseClone();
        }

        public BlocoNL CloneToMensal() {
            return (BlocoNL)this.MemberwiseClone();
        }

        public override string ToString() {
            var result = new StringBuilder();

            foreach (var item in this) {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }
    }

    public class BlocoGS : List<GS> {
        public BlocoGS CloneToRV0() {
            return (BlocoGS)this.MemberwiseClone();
        }

        public override string ToString() {
            var result = new StringBuilder();

            foreach (var item in this) {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }

        public void StepRev() {
            this[0].Intervalos = (int.Parse(this[0].Intervalos) - 1).ToString();
        }

        internal BlocoGS CloneToMensal() {

            var result = (BlocoGS)this.MemberwiseClone();
            result.ForEach(x => x.Intervalos = "1");

            return result;
        }
    }

    public class BlocoTG : List<TG> {
        public BlocoTG CloneToRV0() {
            return (BlocoTG)this.MemberwiseClone();
        }

        public BlocoTG CloneToRV0(DateTime semanaIncialBase, DateTime semanaInicial) {

            var newBlock = new BlocoTG();

            var usinas = this.Select(x => new { Numero = x.Numero.Trim() }).Distinct();

            foreach (var usina in usinas) {

                var geracao = this.Where(x => x.Numero.Trim() == usina.Numero).Select(x => new {
                    CapacidadeGeracaoPat1 = float.Parse(x.CapacidadeGeracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CapacidadeGeracaoPat2 = float.Parse(x.CapacidadeGeracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CapacidadeGeracaoPat3 = float.Parse(x.CapacidadeGeracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat1 = float.Parse(x.CustoGeracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat2 = float.Parse(x.CustoGeracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat3 = float.Parse(x.CustoGeracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat1 = float.Parse(x.GeracaoMinPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat2 = float.Parse(x.GeracaoMinPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat3 = float.Parse(x.GeracaoMinPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    DataInicio = semanaIncialBase.AddDays(7 * (int.Parse(x.Estagio) - 1)),
                    Estagio = int.Parse(x.Estagio)
                });
                var geracaoMax = geracao.OrderByDescending(x => x.CapacidadeGeracaoPat1).First();


                if (geracao.Any(x => x.DataInicio >= semanaInicial)) {
                    var gers = geracao.OrderByDescending(x => x.DataInicio).ToList();


                    var difRevs = (int)(semanaInicial - semanaIncialBase).TotalDays / 7;

                    var tempList = new BlocoTG();
                    foreach (var ger in gers) {

                        var estagio = ger.Estagio - difRevs;
                        if (estagio < 1) estagio = 1;

                        var tg = new TG() {
                            Identificacao = "TG",
                            Estagio = estagio.ToString(),
                            Nome = this.Where(x => x.Numero == usina.Numero).First().Nome,
                            SubSistema = this.Where(x => x.Numero == usina.Numero).First().SubSistema,
                            Numero = usina.Numero,
                            GeracaoMinPat1 = geracaoMax.GeracaoMinPat1.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            GeracaoMinPat2 = geracaoMax.GeracaoMinPat2.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            GeracaoMinPat3 = geracaoMax.GeracaoMinPat3.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CapacidadeGeracaoPat1 = geracaoMax.CapacidadeGeracaoPat1.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CapacidadeGeracaoPat2 = geracaoMax.CapacidadeGeracaoPat2.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CapacidadeGeracaoPat3 = geracaoMax.CapacidadeGeracaoPat3.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CustoGeracaoPat1 = geracaoMax.CustoGeracaoPat1.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CustoGeracaoPat2 = geracaoMax.CustoGeracaoPat2.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                            CustoGeracaoPat3 = geracaoMax.CustoGeracaoPat3.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                        };
                        tempList.Add(tg);
                        if (estagio == 1) break;
                    }

                    newBlock.AddRange(tempList.OrderBy(x => x.Estagio));

                } else {
                    var tg = new TG() {
                        Identificacao = "TG",
                        Estagio = "1",
                        Nome = this.Where(x => x.Numero == usina.Numero).First().Nome,
                        SubSistema = this.Where(x => x.Numero == usina.Numero).First().SubSistema,
                        Numero = usina.Numero,
                        GeracaoMinPat1 = geracaoMax.GeracaoMinPat1.ToString("N0"),
                        GeracaoMinPat2 = geracaoMax.GeracaoMinPat2.ToString("N0"),
                        GeracaoMinPat3 = geracaoMax.GeracaoMinPat3.ToString("N0"),
                        CapacidadeGeracaoPat1 = geracaoMax.CapacidadeGeracaoPat1.ToString("N0"),
                        CapacidadeGeracaoPat2 = geracaoMax.CapacidadeGeracaoPat2.ToString("N0"),
                        CapacidadeGeracaoPat3 = geracaoMax.CapacidadeGeracaoPat3.ToString("N0"),
                        CustoGeracaoPat1 = geracaoMax.CustoGeracaoPat1.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                        CustoGeracaoPat2 = geracaoMax.CustoGeracaoPat2.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                        CustoGeracaoPat3 = geracaoMax.CustoGeracaoPat3.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                    };
                    newBlock.Add(tg);
                }
            }
            return newBlock;
        }

        public override string ToString() {
            var result = new StringBuilder();

            foreach (var item in this) {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }

        public void StepRev() {

            foreach (var item in this.ToList().OrderByDescending(x => int.Parse(x.Estagio)).GroupBy(x => x.Numero)) {
                var removeLeft = false;
                foreach (var tg in item) {

                    if (removeLeft) {
                        this.Remove(tg);

                    } else {
                        var estagio = int.Parse(tg.Estagio);
                        if (estagio > 1)
                            estagio--;

                        tg.Estagio = estagio.ToString();

                        if (estagio == 1)
                            removeLeft = true;
                    }
                }
            }
        }

        internal BlocoTG CloneToMensal(DateTime semanaIncialBase, DateTime semanaInicial) {
            var newBlock = new BlocoTG();

            var usinas = this.Select(x => new { Numero = x.Numero.Trim() }).Distinct();

            foreach (var usina in usinas) {

                var geracao = this.Where(x => x.Numero.Trim() == usina.Numero).Select(x => new {
                    CapacidadeGeracaoPat1 = float.Parse(x.CapacidadeGeracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CapacidadeGeracaoPat2 = float.Parse(x.CapacidadeGeracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CapacidadeGeracaoPat3 = float.Parse(x.CapacidadeGeracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat1 = float.Parse(x.CustoGeracaoPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat2 = float.Parse(x.CustoGeracaoPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat3 = float.Parse(x.CustoGeracaoPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat1 = float.Parse(x.GeracaoMinPat1, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat2 = float.Parse(x.GeracaoMinPat2, System.Globalization.NumberFormatInfo.InvariantInfo),
                    GeracaoMinPat3 = float.Parse(x.GeracaoMinPat3, System.Globalization.NumberFormatInfo.InvariantInfo),
                    DataInicio = semanaIncialBase.AddDays(7 * (int.Parse(x.Estagio) - 1)),
                    Estagio = int.Parse(x.Estagio)
                });
                var geracaoMax = geracao.OrderByDescending(x => x.CapacidadeGeracaoPat1).First();


                //if (geracao.Count() > 1) {

                //    //1o mes
                //    var gers = geracao.OrderByDescending(x => x.DataInicio).ToList();


                //    var difRevs = (int)(semanaInicial - semanaIncialBase).TotalDays / 7;


                //    // var estagio = ger.Estagio - difRevs;

                //    var tg = new TG() {
                //        Identificacao = "TG",
                //        Estagio = "1",
                //        Nome = this.Where(x => x.Numero == usina.Numero).First().Nome,
                //        SubSistema = this.Where(x => x.Numero == usina.Numero).First().SubSistema,
                //        Numero = usina.Numero,
                //        GeracaoMinPat1 = geracaoMax.GeracaoMinPat1.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        GeracaoMinPat2 = geracaoMax.GeracaoMinPat2.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        GeracaoMinPat3 = geracaoMax.GeracaoMinPat3.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CapacidadeGeracaoPat1 = geracaoMax.CapacidadeGeracaoPat1.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CapacidadeGeracaoPat2 = geracaoMax.CapacidadeGeracaoPat2.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CapacidadeGeracaoPat3 = geracaoMax.CapacidadeGeracaoPat3.ToString("0.#", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CustoGeracaoPat1 = geracaoMax.CustoGeracaoPat1.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CustoGeracaoPat2 = geracaoMax.CustoGeracaoPat2.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CustoGeracaoPat3 = geracaoMax.CustoGeracaoPat3.ToString("0.##", System.Globalization.NumberFormatInfo.InvariantInfo),
                //    };
                //    newBlock.Add(tg);

                //    //2o mes
                //    tg = new TG() {
                //        Identificacao = "TG",
                //        Estagio = "2",
                //        Nome = this.Where(x => x.Numero == usina.Numero).First().Nome,
                //        SubSistema = this.Where(x => x.Numero == usina.Numero).First().SubSistema,
                //        Numero = usina.Numero,
                //        GeracaoMinPat1 = geracaoMax.GeracaoMinPat1.ToString("N0"),
                //        GeracaoMinPat2 = geracaoMax.GeracaoMinPat2.ToString("N0"),
                //        GeracaoMinPat3 = geracaoMax.GeracaoMinPat3.ToString("N0"),
                //        CapacidadeGeracaoPat1 = geracaoMax.CapacidadeGeracaoPat1.ToString("N0"),
                //        CapacidadeGeracaoPat2 = geracaoMax.CapacidadeGeracaoPat2.ToString("N0"),
                //        CapacidadeGeracaoPat3 = geracaoMax.CapacidadeGeracaoPat3.ToString("N0"),
                //        CustoGeracaoPat1 = geracaoMax.CustoGeracaoPat1.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CustoGeracaoPat2 = geracaoMax.CustoGeracaoPat2.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                //        CustoGeracaoPat3 = geracaoMax.CustoGeracaoPat3.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                //    };
                //    newBlock.Add(tg);

                //} else {
                var tg = new TG() {
                    Identificacao = "TG",
                    Estagio = "1",
                    Nome = this.Where(x => x.Numero == usina.Numero).First().Nome,
                    SubSistema = this.Where(x => x.Numero == usina.Numero).First().SubSistema,
                    Numero = usina.Numero,
                    GeracaoMinPat1 = geracaoMax.GeracaoMinPat1.ToString("N0"),
                    GeracaoMinPat2 = geracaoMax.GeracaoMinPat2.ToString("N0"),
                    GeracaoMinPat3 = geracaoMax.GeracaoMinPat3.ToString("N0"),
                    CapacidadeGeracaoPat1 = geracaoMax.CapacidadeGeracaoPat1.ToString("N0"),
                    CapacidadeGeracaoPat2 = geracaoMax.CapacidadeGeracaoPat2.ToString("N0"),
                    CapacidadeGeracaoPat3 = geracaoMax.CapacidadeGeracaoPat3.ToString("N0"),
                    CustoGeracaoPat1 = geracaoMax.CustoGeracaoPat1.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat2 = geracaoMax.CustoGeracaoPat2.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                    CustoGeracaoPat3 = geracaoMax.CustoGeracaoPat3.ToString("N2", System.Globalization.NumberFormatInfo.InvariantInfo),
                };
                newBlock.Add(tg);
                //}
            }
            return newBlock;
        }
    }

    public class GL : Line {
        public override string Identificacao { get { return Campos[0].Valor; } set { Campos[0].Valor = value; } }
        public string NumeroUsina { get { return Campos[1].Valor; } set { Campos[1].Valor = value; } }
        public string Subsistema { get { return Campos[2].Valor; } set { Campos[2].Valor = value; } }
        public string Semana { get { return Campos[3].Valor; } set { Campos[3].Valor = value; } }
        public string GeracaoPat1 { get { return Campos[4].Valor; } set { Campos[4].Valor = value; } }
        public string DuracaoPat1 { get { return Campos[5].Valor; } set { Campos[5].Valor = value; } }
        public string GeracaoPat2 { get { return Campos[6].Valor; } set { Campos[6].Valor = value; } }
        public string DuracaoPat2 { get { return Campos[7].Valor; } set { Campos[7].Valor = value; } }
        public string GeracaoPat3 { get { return Campos[8].Valor; } set { Campos[8].Valor = value; } }
        public string DuracaoPat3 { get { return Campos[9].Valor; } set { Campos[9].Valor = value; } }
        public string DiaInicio { get { return Campos[10].Valor; } set { Campos[10].Valor = value; } }
        public string MesInicio { get { return Campos[11].Valor; } set { Campos[11].Valor = value; } }
        public string AnoInicio { get { return Campos[12].Valor; } set { Campos[12].Valor = value; } }

        public DateTime DataInicio {
            get { return new DateTime(int.Parse(AnoInicio), int.Parse(MesInicio), int.Parse(DiaInicio)); }
            set { DiaInicio = value.Day.ToString("00"); MesInicio = value.Month.ToString("00"); AnoInicio = value.Year.ToString("0000"); }
        }
        //public int NumeroUsina { get; set; }
        //public int Subsistema { get; set; }
        //public int Semana { get; set; }
        //public float GeracaoPat1 { get; set; }
        //public float DuracaoPat1 { get; set; }
        //public float GeracaoPat2 { get; set; }
        //public float DuracaoPat2 { get; set; }
        //public float GeracaoPat3 { get; set; }
        //public float DuracaoPat3 { get; set; }
        //public int DiaInicio { get; set; }
        //public int MesInicio { get; set; }
        //public int AnoInicio { get; set; }

        List<Line.Campo> _campos = new List<Campo>() {
                     new Campo(){ Inicio = 0, Tamanho = 2}, 
                    new Campo(){ Inicio = 4, Tamanho = 3}, 
                    new Campo(){ Inicio = 9, Tamanho = 2}, 
                    new Campo(){ Inicio = 14, Tamanho= 2}, 
                    new Campo(){ Inicio = 19, Tamanho= 10}, 
                    new Campo(){ Inicio = 29, Tamanho= 5}, 
                    new Campo(){ Inicio = 34, Tamanho= 10},                     
                    new Campo(){ Inicio = 44, Tamanho= 5}, 
                    new Campo(){ Inicio = 49, Tamanho= 10},                     
                    new Campo(){ Inicio = 59, Tamanho= 5}, 
                    new Campo(){ Inicio = 65, Tamanho= 2}, 
                    new Campo(){ Inicio = 67, Tamanho= 2}, 
                    new Campo(){ Inicio = 69, Tamanho = 4}     
                };
        public override List<Line.Campo> Campos {
            get {
                return _campos;
            }
        }

        public GL() { }
        public GL(string line)
            : base(line) {
        }
    }

    public class NL : Line {
        public override string Identificacao { get { return Campos[0].Valor; } set { Campos[0].Valor = value; } }
        public string NumeroUsina { get { return Campos[1].Valor; } set { Campos[1].Valor = value; } }
        public string SubSistema { get { return Campos[2].Valor; } set { Campos[2].Valor = value; } }
        public string Lag { get { return Campos[3].Valor; } set { Campos[3].Valor = value; } }
        //public int NumeroUsina { get; set; }
        //public int SubSistema { get; set; }
        //public int Lag { get; set; }

        List<Line.Campo> _campos = new List<Campo>() {
                     new Campo(){ Inicio = 0, Tamanho = 2}, 
                    new Campo(){ Inicio = 4, Tamanho = 3}, 
                    new Campo(){ Inicio = 9, Tamanho = 2}, 
                    new Campo(){ Inicio = 14, Tamanho= 1}, 
                };
        public override List<Line.Campo> Campos {
            get {
                return _campos;
            }
        }


        public NL(string line)
            : base(line) {

        }

    }

    public class GS : Line {
        public override string Identificacao { get { return Campos[0].Valor; } set { Campos[0].Valor = value; } }
        public string Mes { get { return Campos[1].Valor; } set { Campos[1].Valor = value; } }
        public string Intervalos { get { return Campos[2].Valor; } set { Campos[2].Valor = value; } }
        //public int Mes { get; set; }
        //public int Intervalos { get; set; }

        List<Line.Campo> _campos = new List<Campo>() {
                     new Campo(){ Inicio = 0, Tamanho = 2}, 
                    new Campo(){ Inicio = 4, Tamanho = 2}, 
                    new Campo(){ Inicio = 9, Tamanho = 1}, 
                    };
        public override List<Line.Campo> Campos {
            get {
                return _campos;
            }
        }

        public GS() { }
        public GS(string line) : base(line) { }
    }

    public class TG : Line {

        public override string Identificacao { get { return Campos[0].Valor; } set { Campos[0].Valor = value; } }
        public string Numero { get { return Campos[1].Valor; } set { Campos[1].Valor = value; } }
        public string SubSistema { get { return Campos[2].Valor; } set { Campos[2].Valor = value; } }
        public string Nome { get { return Campos[3].Valor; } set { Campos[3].Valor = value; } }
        public string Estagio { get { return Campos[4].Valor; } set { Campos[4].Valor = value; } }
        public string GeracaoMinPat1 { get { return Campos[5].Valor; } set { Campos[5].Valor = value; } }
        public string CapacidadeGeracaoPat1 { get { return Campos[6].Valor; } set { Campos[6].Valor = value; } }
        public string CustoGeracaoPat1 { get { return Campos[7].Valor; } set { Campos[7].Valor = value; } }
        public string GeracaoMinPat2 { get { return Campos[8].Valor; } set { Campos[8].Valor = value; } }
        public string CapacidadeGeracaoPat2 { get { return Campos[9].Valor; } set { Campos[9].Valor = value; } }
        public string CustoGeracaoPat2 { get { return Campos[10].Valor; } set { Campos[10].Valor = value; } }
        public string GeracaoMinPat3 { get { return Campos[11].Valor; } set { Campos[11].Valor = value; } }
        public string CapacidadeGeracaoPat3 { get { return Campos[12].Valor; } set { Campos[12].Valor = value; } }
        public string CustoGeracaoPat3 { get { return Campos[13].Valor; } set { Campos[13].Valor = value; } }


        //public int Numero { get; set; }
        //public int SubSistema { get; set; }
        //public string Nome { get; set; }
        //public int Estagio { get; set; }
        //public float GeracaoMinPat1 { get; set; }
        //public float CapacidadeGeracaoPat1 { get; set; }
        //public float CustoGeracaoPat1 { get; set; }
        //public float GeracaoMinPat2 { get; set; }
        //public float CapacidadeGeracaoPat2 { get; set; }
        //public float CustoGeracaoPat2 { get; set; }
        //public float GeracaoMinPat3 { get; set; }
        //public float CapacidadeGeracaoPat3 { get; set; }
        //public float CustoGeracaoPat3 { get; set; }


        List<Line.Campo> _campos = new List<Campo>() {
                     new Campo(){ Inicio = 0, Tamanho = 2}, 
                    new Campo(){ Inicio = 4, Tamanho = 3}, 
                    new Campo(){ Inicio = 9, Tamanho = 2}, 
                    new Campo(){ Inicio = 14, Tamanho= 10}, 
                    new Campo(){ Inicio = 24, Tamanho= 2}, 
                    new Campo(){ Inicio = 29, Tamanho= 5}, 
                    new Campo(){ Inicio = 34, Tamanho= 5}, 
                    new Campo(){ Inicio = 39, Tamanho= 10}, 
                    new Campo(){ Inicio = 49, Tamanho= 5}, 
                    new Campo(){ Inicio = 54, Tamanho= 5}, 
                    new Campo(){ Inicio = 59, Tamanho= 10}, 
                    new Campo(){ Inicio = 69, Tamanho= 5}, 
                    new Campo(){ Inicio = 74, Tamanho= 5}, 
                    new Campo(){ Inicio = 79, Tamanho = 10}     
                };
        public override List<Line.Campo> Campos {
            get {
                return _campos;
            }
        }

        public TG() { }
        public TG(string line) : base(line) { }
    }

    public abstract class Line {

        public abstract string Identificacao { get; set; }

        public Line() { }
        public Line(string line) {
            foreach (var campo in Campos) {
                campo.Valor = Slice(line, campo.Inicio, campo.Tamanho).Trim();
            }
        }

        public static string Slice(string text, int inicio, int tamanho) {
            return text.PadRight(inicio + tamanho).Substring(inicio, tamanho);
        }

        public abstract List<Campo> Campos { get; }

        public override string ToString() {
            var result = new StringBuilder();

            int pos = 0;
            foreach (var campo in Campos) {
                result.Append(new string(' ', campo.Inicio - pos));
                result.Append(campo.Valor.PadLeft(campo.Tamanho));
                pos = (campo.Tamanho + campo.Inicio);
            }

            return result.ToString();
        }

        public class Campo {
            string _valor;
            public string Valor { get { return _valor; } set { _valor = value; } }
            public int Inicio { get; set; }
            public int Tamanho { get; set; }
        }
    }
}
