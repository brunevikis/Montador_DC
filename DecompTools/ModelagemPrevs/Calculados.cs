using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DecompTools.ModelagemPrevs
{
    public class Calculados : Model
    {
        public virtual int id { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual bool ativo { get; set; }
        public virtual int ano { get; set; }
        public virtual IList<CalculadosDados> dados { get; set; }
        public virtual IList<Estudos> estudo_dependentes { get; set; }

        public virtual int[] postos { get; set; }

        public Calculados()
        {
            postos = new int[] { 154, 240, ///distribuicao incremental   238 (regredido)
                104, 109, 116, 169, 171, 172, 173, 175, 176, 178, 244, 37, 38, 39, 40, 42, 43, 45, 46, 66, 298, 75, 318, 317, 315, 316, 131, 132, 304, 299, 127, 126, 303, 306, 314, 319, 292, 302

                , 81

            };
        }

        /// <summary>
        /// Função para atualizar os postos calculados. É nesta função que as contas destes postos estão definidas.
        /// </summary>
        /// <param name="postosList">lista com os postos projetados</param>
        /// <param name="semanaProjecao">semana do novo prevs</param>
        public static void atualizar(IList<Postos> postosList, Semanas_Ano semanaProjecao, bool manterEntradas = false)
        {
            Postos[] postosReorg = Postos.reorganizaPostos(postosList);
            Calculados c = new Calculados();
            foreach (int postoId in c.postos)
            {
                Postos p = postosReorg[postoId];

                if (p == null)
                    continue; //Pula esta execução;

                if (p.prevs_saida != null && manterEntradas)
                {
                    continue;
                } 

                if (p.prevs_saida == null)
                {
                    p.prevs_saida = new PrevsDados();
                    p.prevs_saida.posto = p.numero;

                }

                switch (p.numero)
                {
                    case 81:
                        p.prevs_saida = postosReorg[222].prevs_saida + 5;
                        break;

                    case 55:
                        p.prevs_saida = postosReorg[57].prevs_saida *0.66;
                        break;

                    case 37:
                        p.prevs_saida = postosReorg[237].prevs_saida - (postosReorg[161].prevs_saida * 0.1) - (postosReorg[117].prevs_saida * 0.9) - (postosReorg[118].prevs_saida * 0.9);
                        break;

                    case 38:
                        p.prevs_saida = postosReorg[238].prevs_saida - (postosReorg[161].prevs_saida * 0.1) - (postosReorg[117].prevs_saida * 0.9) - (postosReorg[118].prevs_saida * 0.9);
                        break;

                    case 39:
                        p.prevs_saida = postosReorg[239].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 40:
                        p.prevs_saida = postosReorg[240].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 42:
                        p.prevs_saida = postosReorg[242].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 43:
                        p.prevs_saida = postosReorg[243].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 45:
                        p.prevs_saida = postosReorg[245].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 46:
                        p.prevs_saida = postosReorg[246].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 66:
                        p.prevs_saida = postosReorg[266].prevs_saida - 0.1 * postosReorg[161].prevs_saida - 0.9 * postosReorg[117].prevs_saida - 0.9 * postosReorg[118].prevs_saida;
                        break;

                    case 75:
                        p.prevs_saida = postosReorg[76].prevs_saida + PrevsDados.minValue(postosReorg[73].prevs_saida - 10.0, 173.5);
                        break;

                    case 104:
                        p.prevs_saida = postosReorg[117].prevs_saida + postosReorg[118].prevs_saida;
                        break;

                    case 109:
                        p.prevs_saida = postosReorg[118].prevs_saida * 1.0;
                        break;

                    case 116:
                        p.prevs_saida = postosReorg[119].prevs_saida - postosReorg[118].prevs_saida;
                        break;

                    case 169:
                        p.prevs_saida.sem1 = p.prevs_base.sem1 - postosReorg[168].prevs_base.sem1 + postosReorg[168].prevs_saida.sem1;
                        p.prevs_saida.sem2 = p.prevs_base.sem2 - postosReorg[168].prevs_base.sem2 + postosReorg[168].prevs_saida.sem2;// postosReorg[168].prevs_saida.sem2;//postosReorg[168].prevs_saida.sem4 + postosReorg[156].prevs_saida.sem4 + postosReorg[158].prevs_saida.sem4;
                        p.prevs_saida.sem3 = postosReorg[168].prevs_saida.sem3 + postosReorg[156].prevs_saida.sem1 + postosReorg[158].prevs_saida.sem1;
                        p.prevs_saida.sem4 = postosReorg[168].prevs_saida.sem4 + postosReorg[156].prevs_saida.sem2 + postosReorg[158].prevs_saida.sem2;
                        p.prevs_saida.sem5 = postosReorg[168].prevs_saida.sem5 + postosReorg[156].prevs_saida.sem3 + postosReorg[158].prevs_saida.sem3;
                        p.prevs_saida.sem6 = postosReorg[168].prevs_saida.sem6 + postosReorg[156].prevs_saida.sem4 + postosReorg[158].prevs_saida.sem4;
                        break;

                    case 171:
                        p.prevs_saida = new PrevsDados();
                        break;

                    case 172:
                        p.prevs_saida = postosReorg[169].prevs_saida * 1.0;
                        break;

                    case 173:
                        p.prevs_saida = postosReorg[172].prevs_saida * 1.0;
                        break;

                    case 175:
                        p.prevs_saida = postosReorg[172].prevs_saida * 1.0;
                        break;

                    case 176:
                        p.prevs_saida = postosReorg[172].prevs_saida * 1.0;
                        break;

                    case 178:
                        p.prevs_saida = postosReorg[172].prevs_saida * 1.0;
                        break;

                    case 244:
                        p.prevs_saida = postosReorg[34].prevs_saida + postosReorg[243].prevs_saida;
                        break;

                    case 298:
                        for (int x = 1; x < 7; x++)
                        {
                            PropertyInfo semanaPrevs = p.prevs_saida.GetType().GetProperty(String.Concat("sem", (x).ToString()));

                            double valPosto125 = (double)semanaPrevs.GetValue(postosReorg[125].prevs_saida, null);

                            if (valPosto125 <= 190)
                                semanaPrevs.SetValue(p.prevs_saida, valPosto125 * 119 / 190, null);
                            else if (valPosto125 <= 209)
                                semanaPrevs.SetValue(p.prevs_saida, 119, null);
                            else if (valPosto125 <= 250)
                                semanaPrevs.SetValue(p.prevs_saida, valPosto125 - 90, null);
                            else
                                semanaPrevs.SetValue(p.prevs_saida, 160, null);
                        }
                        break;

                    case 318:
                        p.prevs_saida = postosReorg[116].prevs_saida + 0.9 * postosReorg[117].prevs_saida + 0.9 * postosReorg[118].prevs_saida + 0.1 * postosReorg[161].prevs_saida;
                        break;

                    case 317:
                        p.prevs_saida = PrevsDados.maxValue((postosReorg[201].prevs_saida - 25.0), 0.0);
                        break;

                    case 315:
                        p.prevs_saida = postosReorg[203].prevs_saida - postosReorg[201].prevs_saida + postosReorg[317].prevs_saida + postosReorg[298].prevs_saida;
                        break;

                    case 316:
                        p.prevs_saida = PrevsDados.minValue(postosReorg[315].prevs_saida, 190.0);
                        break;

                    case 131:
                        p.prevs_saida = PrevsDados.minValue(postosReorg[316].prevs_saida, 144.0);
                        break;
                    case 132:
                        p.prevs_saida = postosReorg[202].prevs_saida + PrevsDados.minValue(postosReorg[201].prevs_saida, 25.0);
                        break;
                    case 304:
                        p.prevs_saida = postosReorg[315].prevs_saida - postosReorg[316].prevs_saida;
                        break;

                    case 299:
                        p.prevs_saida = postosReorg[130].prevs_saida - postosReorg[298].prevs_saida - postosReorg[203].prevs_saida + postosReorg[304].prevs_saida;
                        break;

                    case 127:
                        p.prevs_saida = postosReorg[129].prevs_saida - postosReorg[298].prevs_saida - postosReorg[203].prevs_saida + postosReorg[304].prevs_saida;
                        break;

                    case 126:
                        p.prevs_saida.sem1 = postosReorg[127].prevs_saida.sem1 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem1 : 340;
                        p.prevs_saida.sem2 = postosReorg[127].prevs_saida.sem2 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem2 : 340;
                        p.prevs_saida.sem3 = postosReorg[127].prevs_saida.sem3 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem3 : 340;
                        p.prevs_saida.sem4 = postosReorg[127].prevs_saida.sem4 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem4 : 340;
                        p.prevs_saida.sem5 = postosReorg[127].prevs_saida.sem5 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem5 : 340;
                        p.prevs_saida.sem6 = postosReorg[127].prevs_saida.sem6 <= 430 ? PrevsDados.maxValue(postosReorg[127].prevs_saida - 90.0, 0.0).sem6 : 340;
                        break;

                    case 285:
                        p.prevs_saida = 0.985 * postosReorg[287].prevs_saida;
                        break;
                    case 303:
                        p.prevs_saida = postosReorg[132].prevs_saida + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0);

                        //p.prevs_saida.sem1 = postosReorg[132].prevs_saida.sem1 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem1;
                        //p.prevs_saida.sem2 = postosReorg[132].prevs_saida.sem2 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem2;
                        //p.prevs_saida.sem3 = postosReorg[132].prevs_saida.sem3 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem3;
                        //p.prevs_saida.sem4 = postosReorg[132].prevs_saida.sem4 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem4;
                        //p.prevs_saida.sem5 = postosReorg[132].prevs_saida.sem5 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem5;
                        //p.prevs_saida.sem6 = postosReorg[132].prevs_saida.sem6 + PrevsDados.minValue(postosReorg[316].prevs_saida - postosReorg[131].prevs_saida, 51.0).sem6;
                        // = vaz(132) + min(vaz(316) – vaz(131) ; 51 m³/s])
                        break;

                    case 306:
                        p.prevs_saida = postosReorg[303].prevs_saida + postosReorg[131].prevs_saida;
                        break;
                    case 314:
                        p.prevs_saida = postosReorg[199].prevs_saida - postosReorg[298].prevs_saida - postosReorg[203].prevs_saida + postosReorg[304].prevs_saida;
                        break;
                    case 319:
                        p.prevs_saida = 0.9 * postosReorg[117].prevs_saida + 0.9 * postosReorg[118].prevs_saida + 0.1 * postosReorg[161].prevs_saida;
                        break;
                    case 292:

                        int[] tvr = new int[] { 900, 1100, 1600, 4000, 8000, 4000, 2000, 1200, 900, 750, 700, 800, 900, 1100 };


                        var tvr1Sem = FactoryPrevs.SemanasAnoDAO.GetByMesAno(semanaProjecao.mes, semanaProjecao.ano, 0);
                        var dataFimSem = tvr1Sem.dtInicio.AddDays(6);



                        var tvr1 = (dataFimSem.Day * tvr[semanaProjecao.mes] + (7 - dataFimSem.Day) * tvr[semanaProjecao.mes - 1]) / 7;
                        var tvr2 = tvr[semanaProjecao.mes];
                        var tvr3 = tvr[semanaProjecao.mes];
                        var tvr4 = dataFimSem.AddDays(7 * 3).Month == dataFimSem.AddDays(7 * 2).Month ? tvr[dataFimSem.AddDays(7 * 2).Month] :
                            (dataFimSem.AddDays(7 * 3).Day * tvr[dataFimSem.AddDays(7 * 3).Month] + (7 - dataFimSem.AddDays(7 * 3).Day) * tvr[dataFimSem.AddDays(7 * 2).Month]) / 7;

                        var tvr5 = dataFimSem.AddDays(7 * 4).Month == dataFimSem.AddDays(7 * 3).Month ? tvr[dataFimSem.AddDays(7 * 3).Month] :
                            (dataFimSem.AddDays(7 * 4).Day * tvr[dataFimSem.AddDays(7 * 4).Month] + (7 - dataFimSem.AddDays(7 * 4).Day) * tvr[dataFimSem.AddDays(7 * 3).Month]) / 7;
                                                
                        var tvr6 = dataFimSem.AddDays(7 * 5).Month == dataFimSem.AddDays(7 * 4).Month ? tvr[dataFimSem.AddDays(7 * 4).Month] :
                            (dataFimSem.AddDays(7 * 5).Day * tvr[dataFimSem.AddDays(7 * 5).Month] + (7 - dataFimSem.AddDays(7 * 5).Day) * tvr[dataFimSem.AddDays(7 * 4).Month]) / 7;


                        p.prevs_saida.sem1 = postosReorg[288].prevs_saida.sem1 <= tvr1 ? 0 : (postosReorg[288].prevs_saida.sem1 <= (tvr1 + 13900) ? postosReorg[288].prevs_saida.sem1 - tvr1 : 13900);
                        p.prevs_saida.sem2 = postosReorg[288].prevs_saida.sem2 <= tvr2 ? 0 : (postosReorg[288].prevs_saida.sem2 <= (tvr2 + 13900) ? postosReorg[288].prevs_saida.sem2 - tvr2 : 13900);
                        p.prevs_saida.sem3 = postosReorg[288].prevs_saida.sem3 <= tvr3 ? 0 : (postosReorg[288].prevs_saida.sem3 <= (tvr3 + 13900) ? postosReorg[288].prevs_saida.sem3 - tvr3 : 13900);
                        p.prevs_saida.sem4 = postosReorg[288].prevs_saida.sem4 <= tvr4 ? 0 : (postosReorg[288].prevs_saida.sem4 <= (tvr4 + 13900) ? postosReorg[288].prevs_saida.sem4 - tvr4 : 13900);
                        p.prevs_saida.sem5 = postosReorg[288].prevs_saida.sem5 <= tvr5 ? 0 : (postosReorg[288].prevs_saida.sem5 <= (tvr5 + 13900) ? postosReorg[288].prevs_saida.sem5 - tvr5 : 13900);
                        p.prevs_saida.sem6 = postosReorg[288].prevs_saida.sem6 <= tvr6 ? 0 : (postosReorg[288].prevs_saida.sem6 <= (tvr6 + 13900) ? postosReorg[288].prevs_saida.sem6 - tvr6 : 13900);


                        break;
                    case 302:
                        p.prevs_saida = postosReorg[288].prevs_saida - postosReorg[292].prevs_saida;
                        break;


                    case 154:
                        p.prevs_saida = (postosReorg[246].prevs_saida - postosReorg[245].prevs_saida) * 0.152;
                        break;
                    case 238:
                        p.prevs_saida = (postosReorg[239].prevs_saida - postosReorg[237].prevs_saida) * 0.342 + postosReorg[237].prevs_saida;
                        break;
                    case 240:
                        p.prevs_saida = (postosReorg[242].prevs_saida - postosReorg[239].prevs_saida) * 0.717 + postosReorg[239].prevs_saida;
                        break;
                }
                p.prevs_saida.posto = p.numero;
            }

            foreach (Postos p in postosList.Where(p => p.tipo == 4))
                p.prevs_saida = postosReorg[p.numero].prevs_saida;
        }
    }
}
