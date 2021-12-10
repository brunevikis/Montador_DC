using ComparadorDecksDC.Modelagem;
using ComparadorDecksDC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Controller {
    public class controllerDadgnlRV0 {
        DadgnlRV0 view;

        public controllerDadgnlRV0(DadgnlRV0 view) {
            this.view = view;
        }

        public void CreateRV0() {
            var fileContent = System.IO.File.ReadAllText(view.InputFile);
            Dadgnl dadgnl;
            try {
                dadgnl = DadgnlFactory.CreatFromText(fileContent, 2014, 1, 0);
            } catch {
                view.ReturnMessage = "Não foi possivel ler o arquivo de entrada.";
                return;
            }

            var result = dadgnl.CreateNewRV0(view.Ano, view.Mes);

            using (var sw = System.IO.File.CreateText(
                System.IO.Path.Combine(view.OutputFolder, "DADGNL.RV0")
                )) {

                sw.Write(result.ToString());
                sw.Close();
                view.ReturnMessage = "DADGNL criado com sucesso.";

            }


        }
    }
}
