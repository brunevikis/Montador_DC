using ComparadorDecksDC.Modelagem;
using ComparadorDecksDC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Controller {
    public class controllerDadgnlRVX {
        DadgnlRVX view;

        public controllerDadgnlRVX(DadgnlRVX view) {
            this.view = view;
        }

        public void CreateRVX() {
            var fileContent = System.IO.File.ReadAllText(view.InputFile);

            var rev = int.Parse(view.InputFile[view.InputFile.Length - 1].ToString());

            var dadgnl = DadgnlFactory.CreatFromText(fileContent, 2014, 1, rev);

            dadgnl.StepRev();

            using (var sw = System.IO.File.CreateText(
                System.IO.Path.Combine(view.OutputFolder, "DADGNL.RV" + dadgnl.Rev.ToString())
                )) {

                    sw.Write(dadgnl.ToString());
                sw.Close();
                view.ReturnMessage = "DADGNL criado com sucesso.";
                
            }

            
        }
    }
}
