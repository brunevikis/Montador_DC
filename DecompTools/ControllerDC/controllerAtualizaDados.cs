using DecompTools.ModelagemPrevs;
using DecompTools.ModelagemDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DecompTools.ControllerDC {
    public class controllerAtualizaDados {
        /// <summary>
        /// Atualiza o banco de dados a partir da planilha "Apoio"
        /// </summary>
        /// <param name="caminho">Caminho para a planilha "Apoio"</param>
        /// <returns>Mensagem de sucesso ou de erro</returns>
        public string atualizaDados(string caminho) {
            string result = "";
            CadUsh.zerarDados();

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + caminho + ";Extended Properties=Excel 8.0")) {
                //Tratar Postos
                result = atualizarPostos(con);
                if (result != "true")
                    return result;

                //Tratar Regressoes
                result = atualizarRegressoes(con);
                if (result != "true")
                    return result;

                //Trata MLT
                result = atualizarMLT(con);
                if (result != "true")
                    return result;

                result = atualizarMLTPost(con);
                if (result != "true")
                    return result;

                //Trata cadUsh
                result = atualizarCadUsh(con);
                if (result != "true")
                    return result;

            }

            return "Dados atualizados com sucesso!";
        }

        public static string atualizarPostos(OleDbConnection con) {
            try {
                DataSet postosDataSet = new DataSet();
                OleDbDataAdapter postosCommand = new OleDbDataAdapter(" SELECT * FROM [postos$]", con);
                postosCommand.Fill(postosDataSet);

                foreach (DataRow myDataRow in postosDataSet.Tables[0].Rows) {
                    Postos p = new Postos();
                    int sub;
                    double prod;
                    Object[] cells = myDataRow.ItemArray;

                    p.numero = int.Parse(cells[0].ToString());
                    p.nome = cells[1].ToString();
                    p.bacia = cells[2].ToString();
                    p.observacao = cells[5].ToString();

                    if (int.TryParse(cells[3].ToString(), out sub))
                        p.submercado = sub;
                    else
                        p.submercado = 0;

                    if (double.TryParse(cells[4].ToString(), out prod))
                        p.produtibilidade = prod;
                    else
                        p.produtibilidade = 0;

                    p.save();
                }
            } catch (Exception ex) {
                return ex.ToString();
            }
            return "true";
        }

        public static string atualizarRegressoes(OleDbConnection con) {
            Regressao.zeraRegressaoOficial();
            Regressao reg = new Regressao();
            List<RegressaoDados> listRegDados = new List<RegressaoDados>();
            reg.ativo = 1;

            try {
                DataSet regressoesDataSet = new DataSet();
                OleDbDataAdapter regressoesCommand = new OleDbDataAdapter(" SELECT * FROM [regressoes$]", con);
                regressoesCommand.Fill(regressoesDataSet);

                regressoesDataSet.Tables[0].Rows.RemoveAt(0);
                regressoesDataSet.Tables[0].Rows.RemoveAt(0);
                regressoesDataSet.Tables[0].Rows.RemoveAt(0);
                regressoesDataSet.Tables[0].Rows.RemoveAt(0);
                foreach (DataRow myDataRow in regressoesDataSet.Tables[0].Rows) {
                    Object[] cells = myDataRow.ItemArray;
                    if (!String.Equals(cells[0].ToString(), "A0"))
                        break;
                    RegressaoDados r = new RegressaoDados();

                    r.regressao = reg;
                    r.postoBase = int.Parse(cells[3].ToString());
                    r.postoRegredido = int.Parse(cells[1].ToString());

                    for (int x = 1; x < 13; x++) {
                        PropertyInfo block1 = r.GetType().GetProperty(String.Concat("a0_", x.ToString()));
                        PropertyInfo block2 = r.GetType().GetProperty(String.Concat("a1_", x.ToString()));
                        block1.SetValue(r, double.Parse(cells[x + 4].ToString()), null);
                        block2.SetValue(r, double.Parse(cells[x + 22].ToString()), null);
                    }

                    listRegDados.Add(r);
                }

                reg.dados = listRegDados;
                reg.save();
            } catch (Exception ex) {
                return ex.ToString();
            }
            return "true";
        }

        public static string atualizarMLT(OleDbConnection con) {
            try {
                DataSet mltDataSet = new DataSet();
                OleDbDataAdapter mltCommand = new OleDbDataAdapter(" SELECT * FROM [mlt$]", con);
                mltCommand.Fill(mltDataSet);

                int i = 1;
                MltSub m1 = new MltSub();
                MltSub m2 = new MltSub();
                MltSub m3 = new MltSub();
                MltSub m4 = new MltSub();

                m1.submercado = 1;
                m2.submercado = 2;
                m3.submercado = 3;
                m4.submercado = 4;

                foreach (DataRow myDataRow in mltDataSet.Tables[0].Rows) {
                    Object[] cells = myDataRow.ItemArray;

                    PropertyInfo block = m1.GetType().GetProperty(String.Concat("mes", i));
                    block.SetValue(m1, int.Parse(cells[1].ToString()), null);
                    block.SetValue(m2, int.Parse(cells[2].ToString()), null);
                    block.SetValue(m3, int.Parse(cells[3].ToString()), null);
                    block.SetValue(m4, int.Parse(cells[4].ToString()), null);

                    i += 1;
                }

                m1.save();
                m2.save();
                m3.save();
                m4.save();
            } catch (Exception ex) {
                return ex.ToString();
            }
            return "true";
        }

        public static string atualizarCadUsh(OleDbConnection con) {
            try {
                DataSet cadUshDataSet = new DataSet();
                OleDbDataAdapter cadUshCommand = new OleDbDataAdapter(" SELECT * FROM [cadush$]", con);
                cadUshCommand.Fill(cadUshDataSet);

                foreach (DataRow myDataRow in cadUshDataSet.Tables[0].Rows) {
                    CadUsh cad = new CadUsh();

                    Object[] cells = myDataRow.ItemArray;
                    if (cells[0].ToString() == "") {
                        break;
                    }

                    NumberStyles style = style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands; ;

                    cad.codUsina = int.Parse(cells[0].ToString());
                    cad.nomeUsina = cells[1].ToString();
                    cad.sistema = int.Parse(cells[2].ToString().Substring(0, 1));
                    cad.posto = int.Parse(cells[4].ToString());
                    cad.jusante = int.Parse(cells[6].ToString().Substring(0, cells[6].ToString().IndexOf(' ')));
                    cad.volMax = decimal.Parse(cells[8].ToString());
                    cad.volMin = decimal.Parse(cells[9].ToString());
                    cad.cotaMax = decimal.Parse(cells[10].ToString());
                    cad.cotaMin = decimal.Parse(cells[11].ToString());
                    cad.PCV0 = decimal.Parse(cells[14].ToString(), NumberStyles.Float);
                    cad.PCV1 = decimal.Parse(cells[15].ToString(), NumberStyles.Float);
                    cad.PCV2 = decimal.Parse(cells[16].ToString(), NumberStyles.Float);
                    cad.PCV3 = decimal.Parse(cells[17].ToString(), NumberStyles.Float);
                    cad.PCV4 = decimal.Parse(cells[18].ToString(), NumberStyles.Float);
                    cad.prodEsp = decimal.Parse(cells[36].ToString());
                    cad.canalFugaMed = decimal.Parse(cells[37].ToString());
                    cad.perdaTipo = int.Parse(cells[45].ToString());
                    cad.perdaVal = decimal.Parse(cells[46].ToString());
                    cad.numUnidBase = int.Parse(cells[48].ToString());
                    cad.reg = cells[179].ToString();

                    cad.numConjMaq = int.Parse(cells[41].ToString());


                    cad.numMaqConj1 = int.Parse(cells[51].ToString());
                    cad.potEfConj1 = float.Parse(cells[52].ToString());
                    cad.qEfConj1 = float.Parse(cells[53].ToString());
                    cad.hEfConj1 = float.Parse(cells[54].ToString());
                    cad.numMaqConj2 = int.Parse(cells[55].ToString());
                    cad.potEfConj2 = float.Parse(cells[56].ToString());
                    cad.qEfConj2 = float.Parse(cells[57].ToString());
                    cad.hEfConj2 = float.Parse(cells[58].ToString());
                    cad.numMaqConj3 = int.Parse(cells[59].ToString());
                    cad.potEfConj3 = float.Parse(cells[60].ToString());
                    cad.qEfConj3 = float.Parse(cells[61].ToString());
                    cad.hEfConj3 = float.Parse(cells[62].ToString());
                    cad.numMaqConj4 = int.Parse(cells[63].ToString());
                    cad.potEfConj4 = float.Parse(cells[64].ToString());
                    cad.qEfConj4 = float.Parse(cells[65].ToString());
                    cad.hEfConj4 = float.Parse(cells[66].ToString());
                    cad.numMaqConj5 = int.Parse(cells[67].ToString());
                    cad.potEfConj5 = float.Parse(cells[68].ToString());
                    cad.qEfConj5 = float.Parse(cells[69].ToString());
                    cad.hEfConj5 = float.Parse(cells[70].ToString());

                    cad.save();
                }
            } catch (Exception ex) {
                return ex.ToString();
            }
            return "true";
        }

        public static string atualizarMLTPost(OleDbConnection con) {
            try {
                DataSet mltDataSet = new DataSet();
                OleDbDataAdapter mltCommand = new OleDbDataAdapter(" SELECT * FROM [mlt_earm$]", con);
                mltCommand.Fill(mltDataSet);

                foreach (DataRow myDataRow in mltDataSet.Tables[0].Rows) {
                    Object[] cells = myDataRow.ItemArray;
                    MltPosto p = new MltPosto();

                    if (cells[0] == null || cells[0].ToString() == "")
                        break;
                    p.numPosto = int.Parse(cells[0].ToString());
                    p.submercado = int.Parse(cells[1].ToString());

                    for (int x = 1; x < 13; x++) {
                        PropertyInfo block = typeof(MltPosto).GetProperty(String.Concat("mes", x));
                        block.SetValue(p, double.Parse(cells[x + 1].ToString().Replace(".", ",")));
                    }

                    p.save();
                }
            } catch (Exception ex) {
                return ex.ToString();
            }
            return "true";
        }
    }
}
