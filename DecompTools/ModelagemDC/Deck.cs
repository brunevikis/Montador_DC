using System;
using DecompTools.Util;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Linq;

using DecompTools.FactoryDC;

namespace DecompTools.ModelagemDC
{
    public class Deck : Model
    {
        public virtual int id { get; set; }
        public virtual int id_deckNW { get; set; }
        public virtual int id_deckDC_base { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual string nome { get; set; }
        public virtual string descricao { get; set; }
        public virtual string caminho { get; set; }
        public virtual string te { get; set; }
        public virtual int oficial { get; set; }
        public virtual int rev { get; set; }
        public virtual int ano { get; set; }
        public virtual int mes { get; set; }
        public virtual int dia { get; set; }

        #region Blocos
        public virtual IList<UH> uh { get; set; }
        public virtual IList<CT> ct { get; set; }
        public virtual IList<UE> ue { get; set; }
        public virtual IList<VR> vr { get; set; }
        public virtual IList<DP> dp { get; set; }
        public virtual IList<CD> cd { get; set; }
        public virtual IList<PQ> pq { get; set; }
        public virtual IList<IT> it { get; set; }

        public virtual IList<RI> ri { get; set; }
        public virtual IList<VL> vl { get; set; }
        public virtual IList<VU> vu { get; set; }

        public virtual IList<IA> ia { get; set; }
        public virtual IList<TX> tx { get; set; }
        public virtual IList<GP> gp { get; set; }
        public virtual IList<NI> ni { get; set; }
        public virtual IList<MP> mp { get; set; }
        public virtual IList<MT> mt { get; set; }
        public virtual IList<FD> fd { get; set; }
        public virtual IList<VE> ve { get; set; }
        public virtual IList<VI> vi { get; set; }
        public virtual IList<QI> qi { get; set; }
        public virtual IList<AC> ac { get; set; }
        public virtual IList<PI> pi { get; set; }
        public virtual IList<FP> fp { get; set; }
        //public virtual IList<IR> ir { get; set; }
        public virtual IList<CX> cx { get; set; }
        public virtual IList<FJ> fj { get; set; }
        public virtual IList<FA> fa { get; set; }
        public virtual IList<CI> ci { get; set; }
        public virtual IList<FC> fc { get; set; }
        //public virtual IList<EA> ea { get; set; }
        //public virtual IList<ES> es { get; set; }
        public virtual IList<TI> ti { get; set; }
        public virtual IList<RQ> rq { get; set; }
        public virtual IList<EZ> ez { get; set; }
        public virtual IList<HE> he { get; set; }
        public virtual IList<CM> cm { get; set; }

        //RESTRIÇÔES
        public virtual IList<RHA> rha { get; set; }
        public virtual IList<RHE> rhe { get; set; }
        public virtual IList<RHQ> rhq { get; set; }
        public virtual IList<RHV> rhv { get; set; }
        #endregion

        public virtual string[] blocos { get; set; }

        public Deck()
        {
            //blocos = new string[] { "UH", "CT", "CI", "UE", "VR", "DP", "CD", "PQ", "IT", "IA", "TX", "GP", "NI", "DT", "MP", "MT", "FD", "VE", "RHE", "VI", "QI", "AC", "PI", "FP", "IR", "FC", "TI", "RQ", "EZ", "RHA", "RHV", "RHQ" };
            blocos = new string[] { "UH", "CT", "UE", "VR", "DP", "CD", "PQ", "RI", "VL", "VU", "IA", "TX", "GP", "NI", "DT", "MP", "MT", "FD", "VE", "RHE", "VI", "QI", "AC", "PI", "FP", "IR", "CI", "FC", "CX", "TI", "RQ", "EZ", "RHA", "RHV", "RHQ", "HE", "FJ", "FA" };
        }


        /// <summary>
        /// Instancia uma IList do tipo passado por parametro. 
        /// PS: Nao deveria estar nessa classe.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList GetList(Type type)
        {
            return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
        }

        /// <summary>
        /// Dado os dois primeiros algarismos da linha define o nome do bloco.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public virtual string escolheBloco(string s)
        {
            string[] vRHA = { "HA", "CA", "LA" };
            string[] vRHV = { "HV", "CV", "LV" };
            string[] vRHQ = { "HQ", "CQ", "LQ" };
            string[] vRHE = { "FI", "FT", "LU", "FU", "RE", "FE" };
            string[] vRHECM = { "HE", "CM" };
            string[] vRVLVU = { "VL", "VU", "VA" };

            string blocoAtual;

            if (vRHA.Contains(s))
                blocoAtual = "RHA";
            else if (vRHE.Contains(s))
                blocoAtual = "RHE";
            else if (vRHQ.Contains(s))
                blocoAtual = "RHQ";
            else if (vRHV.Contains(s))
                blocoAtual = "RHV";
            else if (vRHECM.Contains(s))
                blocoAtual = "HE";
            else if (vRVLVU.Contains(s))
                blocoAtual = "VL";
            else
                blocoAtual = s;

            return blocoAtual;
        }

        /// <summary>
        /// Formata e adiciona todas as linhas do bloco em questao no seu respectivo deck.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="blocoAtual"></param>
        /// <param name="numLinha"></param>
        public virtual void add(ArrayList list, String blocoAtual, int numLinha)
        {
            int numBloco = 1;
            IList blockList;

            if (blocoAtual != "")
            {
                Type t = Type.GetType("DecompTools.ModelagemDC." + blocoAtual);
                if (t != null)
                {
                    blockList = GetList(t);

                    foreach (string linha in list)
                    {
                        var linhaBloco = (blockModel)Activator.CreateInstance(t);
                        //if (linhaBloco is Restricoes)
                        //{

                        //    //linhaBloco.bloco = linha.Substring(0, 2);
                        //    linhaBloco.definePos();
                        //}

                        linhaBloco.leLinha(linha);
                        linhaBloco.deck = this;
                        linhaBloco.linha = numLinha;
                        linhaBloco.ordem = numBloco;

                        blockList.Add(linhaBloco);

                        numLinha++;
                        numBloco++;
                    }

                    PropertyInfo block = this.GetType().GetProperty(blocoAtual.ToLower());
                    block.SetValue(this, blockList, null);

                }

            }
        }

        /// <summary>
        /// Copiar todos os blocos do deckBase.
        /// </summary>
        /// <param name="deckBase"></param>
        public virtual void clone(Deck deckBase)
        {
            foreach (string b in blocos)
            {
                if (!String.Equals(b, "DT") && !String.Equals(b, "IR"))
                {
                    this.clone(deckBase, b);
                }
            }
        }

        /// <summary>
        /// Copiar apenas o bloco b do deckBase
        /// </summary>
        /// <param name="deckBase"></param>
        /// <param name="b"></param>
        public virtual void clone(Deck deckBase, string b)
        {
            IList blockListBase;
            IList blockListNew;
            blockModel linhaBloco;

            Type t = Type.GetType("DecompTools.ModelagemDC." + b);
            if (t != null)
            {
                blockListNew = GetList(t);

                PropertyInfo block = this.GetType().GetProperty(b.ToLower());
                blockListBase = (IList)block.GetValue(deckBase, null);

                if (blockListBase != null)
                {
                    foreach (blockModel linha in blockListBase)
                    {
                        linhaBloco = (blockModel)Activator.CreateInstance(t);

                        foreach (PropertyInfo pi in t.GetProperties())
                            if (!String.Equals(pi.Name, "id"))
                                pi.SetValue(linhaBloco, pi.GetValue(linha, null), null);

                        linhaBloco.deck = this;
                        blockListNew.Add(linhaBloco);
                    }
                    block.SetValue(this, blockListNew, null);
                }
            }

        }

        /// <summary>
        /// Exporta o deck para um arquivo no local pre-definido
        /// </summary>
        /// <param name="caminho"></param>
        public virtual void escreveDeck(string caminho, string nomeArquivo = null, string nomeMensal = null)
        {
            IList blockList;
            if (nomeArquivo == null)
                caminho = Path.Combine(caminho, String.Concat("DADGER.RV", this.rev.ToString()));
            else
                caminho = Path.Combine(caminho, nomeArquivo);

            if (File.Exists(caminho))
                File.Delete(caminho);
            File.Create(caminho).Close();
            using (TextWriter arquivo = File.CreateText(caminho))
            {

                arquivo.WriteLine(this.te);
                arquivo.WriteLine("SB   1   SE");
                arquivo.WriteLine("SB   2   S ");
                arquivo.WriteLine("SB   3   NE");
                arquivo.WriteLine("SB   4   N ");
                arquivo.WriteLine("SB  11   FC");

                foreach (string b in blocos)
                {
                    if (String.Equals(b, "DT"))
                    {
                        StringBuilder sb = new StringBuilder("DT  ");
                        sb.Append(UtilitarioDeTexto.zeroEsq(this.dia, 2));
                        sb.Append("   ");
                        sb.Append(UtilitarioDeTexto.zeroEsq(this.mes, 2));
                        sb.Append("   ");
                        sb.Append(UtilitarioDeTexto.zeroEsq(this.ano, 2));

                        arquivo.WriteLine(sb);
                    }
                    else if (String.Equals(b, "IR"))
                    {
                        arquivo.WriteLine("IR  GRAFICO");
                        arquivo.WriteLine("IR  CUSTOS ");
                        arquivo.WriteLine("IR  AVALIA ");
                        arquivo.WriteLine("IR  NORMAL    17   61");
                        arquivo.WriteLine("IR  ACOPLA    01");
                        arquivo.WriteLine("IR  ARQFPHA");
                        arquivo.WriteLine("IR  ARQCSV ");
                        arquivo.WriteLine("IR  ARQLIBS");
                    }
                    else
                    {
                        Type t = Type.GetType("DecompTools.ModelagemDC." + b);
                        if (t != null)
                        {
                            blockList = GetList(t);

                            PropertyInfo block = this.GetType().GetProperty(b.ToLower());
                            blockList = (IList)block.GetValue(this, null);

                            if (blockList != null)
                            {
                                foreach (blockModel linha in blockList)
                                {
                                    if (String.Equals(b, "RHA") || String.Equals(b, "RHE") || String.Equals(b, "RHQ") || String.Equals(b, "RHV"))
                                        linha.definePos();
                                    arquivo.WriteLine(linha.escreveLinha());
                                }
                            }
                        }

                    }
                    arquivo.Flush();
                }

                Semanas s = new Semanas();
                if (this.rev != 0)
                    s = SemanasDAO.GetByMesAno(this.mes, this.ano);
                else
                    s = SemanasDAO.GetBySemanaInicial(this.ano, this.mes, this.dia);

                arquivo.WriteLine("&--------------------- CVAR DECOMP ---------------");
                arquivo.WriteLine("&    CEN   LBD.X ALF.X");
                arquivo.WriteLine("AR     1              ");
                arquivo.WriteLine("& ---------------------  EVAPORAÇÃO LINEAR -------------------------------");
                arquivo.WriteLine("&x  x    xxx");
                arquivo.WriteLine("EV  1    INI ");
                //FA  indices.csv arquivo.WriteLine("FJ  polinjus.dat ");
                //arquivo.WriteLine("FA  indices.csv ");
                arquivo.WriteLine("& --------------------------------------------------------------------------------------------");
                arquivo.WriteLine("&   DADOS PARA O PROGRAMA CONFIGURADOR DO ARQUIVO DE CENARIOS DE VAZOES:");
                arquivo.WriteLine("&   -  IDENTIFICADOR \"& VAZOES\" NO INICIO DO REGISTROS;");
                arquivo.WriteLine("&   -  DEVE SER MANTIDA A ORDEM DOS REGISTROS DE DADOS A SEGUIR:");
                arquivo.WriteLine("&");
                arquivo.WriteLine("& VAZOES                              (COLUNAS: 40 A 70)");
                arquivo.WriteLine("& ARQ. DE VAZOES PREVISTAS - HIDROL => {NOME_ARQ_PREVS}".Replace("{NOME_ARQ_PREVS}", (nomeMensal == null) ? String.Concat("PREVS.RV", this.rev.ToString()) : String.Concat("PREVS.", nomeMensal)));
                arquivo.WriteLine("& HISTORICO DE VAZOES      - HIDROL => {NOME_ARQ_VAZOES}".Replace("{NOME_ARQ_VAZOES}", String.Concat((s.ano - 2).ToString(), " VAZOES.DAT")));
                arquivo.WriteLine("& ARQ. DE POSTOS           - HIDROL => {NOME_ARQ_POSTOS}".Replace("{NOME_ARQ_POSTOS}", "POSTOS.DAT"));
                arquivo.WriteLine(String.Concat("& MES INICIAL DO ESTUDO             => ", UtilitarioDeTexto.zeroEsq(s.mes, 2)));
                arquivo.WriteLine(String.Concat("& MES FINAL DO ESTUDO               => ", UtilitarioDeTexto.zeroEsq(UtilitarioDeData.mesFinalReal(s.mes), 2)));
                arquivo.WriteLine(String.Concat("& ANO INICIAL DO ESTUDO             => ", UtilitarioDeData.anoInicialReal(this.ano, this.mes, s.mes)));
                arquivo.WriteLine(String.Concat("& NO. SEMANAS NO MES INIC. DO ESTUDO=> ", ((nomeMensal == null) ? s.semanas - this.rev : 0)) + "    " + this.rev);
                arquivo.WriteLine(String.Concat("& NO. DIAS DO MES 2 NA ULT. SEMANA  => ", (nomeMensal == null) ? s.diasMes2 : 0));
                arquivo.WriteLine(String.Concat("& ESTRUTURA DA ARVORE               => ", UtilitarioDeData.estruturaArvore(s.mes)));
                arquivo.WriteLine("& UTILIZA AGREGACAO                 => 6");
                arquivo.WriteLine("& ORDEM MAXIMA PARP                 => 11");
                arquivo.WriteLine("& UTILIZAR REGRA DE PROP. LINEAR    => 2");

                arquivo.WriteLine("& MATRIZ DE CARGA MENSAL            => 0001");
                arquivo.WriteLine("& ORDENACAO AUTOMATICA              => 0001");
                arquivo.WriteLine("& REPRES. AGREGACAO                 => 0001");
                arquivo.WriteLine("& AJUSTE PARP A TODAS UHES          => 0001");
                arquivo.WriteLine("& UTILIZA INFORMACAO DE ENOS        => 0000");
                arquivo.WriteLine("& ESTUDO PROSPECTIVO DECOMP         => 0000 prospec.dat");
                arquivo.WriteLine("& IMPRIME PREVS                     => 0001 prevs12.rv0");
                arquivo.WriteLine("& IMPRIME VAZPAST                   => 0000 vazpast2.dat");
                arquivo.WriteLine("& TENDENCIA HIDROLOGICA P/ VAZPAST  => 0000");
                arquivo.WriteLine("& MODELO PAR-A                      => 0001");



                arquivo.Flush();
            }
        }
    }
}
