using DecompTools.ControllerDC;
using DecompTools.ControllerPrevs;
using DecompTools.FactoryPrevs;
using DecompTools.ModelagemDC;
using DecompTools.ModelagemPrevs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test {
    class Program {
        static void Main(string[] args) {


            Deck deckBase;

           
                deckBase = controllerCarrega.lerDeck(@"L:\10_estudos\19_PATAMAR_CPAMP\DC201807-sem1\dadger.rv0");

            //deckBase.escreveDeck(@"C:\Users\douglas.canducci\Desktop\PMO_deck_preliminar");




            return;


            var _controlador = new controllerNovoPrevs();


            var caminhoPrevs = @"H:\Middle - Preço\Acompanhamento de vazões\02_2017\Dados_de_Entrada_e_Saida_201702_RV0\Gevazp\PREVS.RV0";
            var ano = 2017;
            var mes = 02;
            var rev = 1;
            var semana = 6;

            var caminhoImp = @"H:\Middle - Preço\Acompanhamento de vazões\02_2017\Dados_de_Entrada_e_Saida_201702_RV0\Previvaz\Arq_Entrada";
            var ENA = 
@"56372	57138	
8836	9453	
4370	3441	3318
6724			
";
            var descricao = @"T5";
            //var caminhoSaidaEstudo = @"C:\Users\douglas.canducci\Desktop\Nova pasta (2)";
            var caminhoSaidaEstudo = @"L:\10_Estudos\10_Estudo_ModeloNW_douglas\pr";


            
            var usarRdh = false;
            var nome = descricao;


            DateTime dtInicio = DateTime.Now;

         
            controllerCarregaPrevs _carrega = new controllerCarregaPrevs();
            Prevs prevsBase;

            prevsBase = _carrega.lePrevs(caminhoPrevs, ano, mes);

            Semanas_Ano s = new Semanas_Ano(ano, mes, rev, semana);

            RDH rdhBase = null;
            
            int revsToRun;
            Dictionary<int, Dictionary<string, string>> strDat = null;

            var matrizENA = _controlador.CheckEnaEntrada(s, ENA, out revsToRun);

            
            if (!System.IO.Directory.Exists(caminhoSaidaEstudo)) System.IO.Directory.CreateDirectory(caminhoSaidaEstudo);
            var caminhoSaidaSensBase = caminhoSaidaEstudo;
            if (!string.IsNullOrWhiteSpace(nome)) {
                caminhoSaidaSensBase = System.IO.Path.Combine(caminhoSaidaEstudo, nome + "_0");
            }
            if (!System.IO.Directory.Exists(caminhoSaidaSensBase)) System.IO.Directory.CreateDirectory(caminhoSaidaSensBase);

            try {

                _controlador.prevsSemana(prevsBase, rdhBase, s, caminhoImp, matrizENA, descricao, caminhoSaidaSensBase, usarRdh, strDat: strDat);
            } finally { }

            DateTime dtfim = DateTime.Now;

            Console.WriteLine((dtfim - dtInicio).TotalSeconds);
            Console.ReadKey();
        }
    }
}
