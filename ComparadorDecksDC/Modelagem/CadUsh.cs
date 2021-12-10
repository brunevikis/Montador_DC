using ComparadorDecksDC.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ComparadorDecksDC.Modelagem {
    public class CadUsh : Model {
        public virtual int id { get; set; }
        public virtual DateTime dt_Entrada { get; set; }
        public virtual int codUsina { get; set; }
        public virtual string nomeUsina { get; set; }
        public virtual int sistema { get; set; }
        public virtual int posto { get; set; }
        public virtual int jusante { get; set; }
        public virtual decimal volMax { get; set; }
        public virtual decimal volMin { get; set; }
        public virtual decimal cotaMax { get; set; }
        public virtual decimal cotaMin { get; set; }
        public virtual decimal PCV0 { get; set; }
        public virtual decimal PCV1 { get; set; }
        public virtual decimal PCV2 { get; set; }
        public virtual decimal PCV3 { get; set; }
        public virtual decimal PCV4 { get; set; }
        public virtual decimal prodEsp { get; set; }
        public virtual decimal canalFugaMed { get; set; }
        public virtual int perdaTipo { get; set; }
        public virtual decimal perdaVal { get; set; }
        public virtual int numUnidBase { get; set; }

        public virtual int numConjMaq { get; set; }

        public virtual int numMaqConj1 { get; set; }
        public virtual float potEfConj1 { get; set; }
        public virtual float qEfConj1 { get; set; }
        public virtual float hEfConj1 { get; set; }

        public virtual int numMaqConj2 { get; set; }
        public virtual float potEfConj2 { get; set; }
        public virtual float qEfConj2 { get; set; }
        public virtual float hEfConj2 { get; set; }

        public virtual int numMaqConj3 { get; set; }
        public virtual float potEfConj3 { get; set; }
        public virtual float qEfConj3 { get; set; }
        public virtual float hEfConj3 { get; set; }

        public virtual int numMaqConj4 { get; set; }
        public virtual float potEfConj4 { get; set; }
        public virtual float qEfConj4 { get; set; }
        public virtual float hEfConj4 { get; set; }

        public virtual int numMaqConj5 { get; set; }
        public virtual float potEfConj5 { get; set; }
        public virtual float qEfConj5 { get; set; }
        public virtual float hEfConj5 { get; set; }

        /// <summary>
        /// Regulamentação do posto. Caso D, é fio dagua. Caso M, o posto tem reservatorio.
        /// </summary>
        public virtual string reg { get; set; }

        //Variaveis sem persistencia em banco
        public virtual decimal uhBase { get; set; }
        public virtual decimal ficEZ { get; set; }
        public virtual decimal ficSub { get; set; } //Para as postos ficticias, qual o submercado da usina "Base"
        public virtual decimal ficBaseSub { get; set; } //Para as postos com ficticias, qual o submercado da usina "Ficticia"
        public virtual decimal quedaCalc { get; set; }
        public virtual decimal prodCalc {
            get { return quedaCalc * prodEsp; }
            set { Math.Pow(1, 1); }
        }
        public virtual decimal sumProd { get; set; }
        public const decimal c = 277.777778m;        //Ajustar esta constante
        public virtual bool inDadger { get; set; }
        public virtual decimal energia {
            get { return c * uhBase * (volMax - volMin) * sumProd / 100m; }
            set { Math.Pow(1, 1); }
        }

        public virtual decimal? restricaoVolMin { get; set; }
        public virtual decimal? restricaoVolMax { get; set; }


        public CadUsh() { }

        public CadUsh(CadUsh c) {
            this.id = c.id;
            this.dt_Entrada = c.dt_Entrada;
            this.codUsina = c.codUsina;
            this.nomeUsina = c.nomeUsina;
            this.sistema = c.sistema;
            this.posto = c.posto;
            this.jusante = c.jusante;
            this.volMax = c.volMax;
            this.volMin = c.volMin;
            this.cotaMax = c.cotaMax;
            this.cotaMin = c.cotaMin;
            this.PCV0 = c.PCV0;
            this.PCV1 = c.PCV1;
            this.PCV2 = c.PCV2;
            this.PCV3 = c.PCV3;
            this.PCV4 = c.PCV4;
            this.prodEsp = c.prodEsp;
            this.canalFugaMed = c.canalFugaMed;
            this.perdaTipo = c.perdaTipo;
            this.perdaVal = c.perdaVal;
            this.numUnidBase = c.numUnidBase;
            this.reg = c.reg;
            this.inDadger = c.inDadger;
            this.uhBase = c.uhBase;
            this.ficEZ = c.ficEZ;
            this.ficSub = c.ficSub;

            this.numConjMaq = c.numConjMaq;

            this.numMaqConj1 = c.numMaqConj1;
            this.numMaqConj2 = c.numMaqConj2;
            this.numMaqConj3 = c.numMaqConj3;
            this.numMaqConj4 = c.numMaqConj4;
            this.numMaqConj5 = c.numMaqConj5;

            this.potEfConj1 = c.potEfConj1;
            this.potEfConj2 = c.potEfConj2;
            this.potEfConj3 = c.potEfConj3;
            this.potEfConj4 = c.potEfConj4;
            this.potEfConj5 = c.potEfConj5;

            this.qEfConj1 = c.qEfConj1;
            this.qEfConj2 = c.qEfConj2;
            this.qEfConj3 = c.qEfConj3;
            this.qEfConj4 = c.qEfConj4;
            this.qEfConj5 = c.qEfConj5;

            this.hEfConj1 = c.hEfConj1;
            this.hEfConj2 = c.hEfConj2;
            this.hEfConj3 = c.hEfConj3;
            this.hEfConj4 = c.hEfConj4;
            this.hEfConj5 = c.hEfConj5;

            this.restricaoVolMax = c.restricaoVolMax;
            this.restricaoVolMin = c.restricaoVolMin;

        }

        public static void zerarDados() {
            List<CadUsh> cad = CadUshDAO.GetAll();

            foreach (CadUsh c in cad)
                c.delete();
        }

        static IEnumerable<Tuple<CadUsh, CadUsh>> usinasTemp;
        public static void calculaSomaProd(List<CadUsh> cadUsinas) {
            usinasTemp = cadUsinas.Select(c => new Tuple<CadUsh, CadUsh>(c, cadUsinas.FirstOrDefault(j => j.codUsina == c.jusante))).ToList();
            calculaSomaProd(cadUsinas, 0, null);
            usinasTemp = null;
        }

        public static void calculaSomaProd(List<CadUsh> cadUsinas, decimal prodSom, CadUsh jusante) {
                        
            var usinas = usinasTemp.Where(c => {
                if (jusante == null) {
                    return c.Item2 == null || (c.Item2.sistema != c.Item1.sistema);
                } else {
                    return c.Item2 != null && c.Item2.codUsina == jusante.codUsina && c.Item1.sistema == jusante.sistema;
                }

            }).Select(c => c.Item1).ToList();

            foreach (CadUsh usina in usinas) { //  cadUsinas.Where(p => p.jusante == codUsina)) {

                usina.sumProd = usina.prodCalc + prodSom;
                calculaSomaProd(cadUsinas, usina.sumProd, usina);
                System.Diagnostics.Debug.WriteLine(usina.nomeUsina + "\t" + usina.codUsina.ToString() + "\t" + usina.sumProd + "\t" + usina.energia);
            }
        }

        public static decimal[] calculaTotalSub(List<CadUsh> cadUsinas) {
            decimal[] total = new decimal[4];

            total[0] = calculaTotalSub(cadUsinas, 1);
            total[1] = calculaTotalSub(cadUsinas, 2);
            total[2] = calculaTotalSub(cadUsinas, 3);
            total[3] = calculaTotalSub(cadUsinas, 4);

            return total;
        }

        public static decimal calculaTotalSub(List<CadUsh> cadUsinas, int sub) {
            decimal total = 0m;

            foreach (CadUsh usina in cadUsinas.Where(p => p.sistema == sub))
                total = total + usina.energia;

            return total;
        }

    }
}