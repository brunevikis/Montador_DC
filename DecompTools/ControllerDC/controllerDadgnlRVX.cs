using DecompTools.FactoryDC;
using DecompTools.ModelagemDC;
using DecompTools.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ControllerDC {
    public class controllerDadgnlRVX {
        DadgnlRVX view;

        public controllerDadgnlRVX(DadgnlRVX view) {
            this.view = view;
        }

        public void CreateRVX() {

            Semanas s;

            //if (deckBase.rev != 0)
            //    s = SemanasDAO.GetByMesAno(deckBase.mes, deckBase.ano);
            //else
            //    s = SemanasDAO.GetBySemanaInicial(deckBase.ano, deckBase.mes, deckBase.dia);


            var fileContent = System.IO.File.ReadAllText(view.InputFile);

            var rev = int.Parse(view.InputFile[view.InputFile.Length - 1].ToString());

            var dadgnl = DadgnlFactory.CreatFromText(fileContent, rev);

            //if (dadgnl.Rev != 0)
            s = SemanasDAO.GetByMesAno(dadgnl.Mes, dadgnl.Ano);

            var numSemanas = s.semanas - (s.diasMes2 != 0 ? 1 : 0);

            //for (; (dadgnl.Rev + 1) <= s.semanas; dadgnl.StepRev()) {
            do {

                // dadgnl.StepRev();
                var folder = System.IO.Path.Combine(view.OutputFolder, "RV" + dadgnl.Rev.ToString());
                if (!System.IO.Directory.Exists(folder)) System.IO.Directory.CreateDirectory(folder);

                
                using (var sw = System.IO.File.CreateText(
                    System.IO.Path.Combine(folder, "DADGNL.RV" + dadgnl.Rev.ToString())
                    )) {

                    sw.Write(dadgnl.ToString());
                    sw.Close();

                }
                dadgnl.StepRev();

            } while ((dadgnl.Rev + 1) <= numSemanas);
            view.ReturnMessage = "DADGNL criado com sucesso.";
        }
    }
}
