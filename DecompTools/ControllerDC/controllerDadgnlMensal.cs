using DecompTools.ModelagemDC;
using DecompTools.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ControllerDC {
    public class controllerDadgnlMensal {
        DadgnlMensal view;

        public controllerDadgnlMensal(DadgnlMensal view) {
            this.view = view;
        }

        public void CreateMensal() {
            var fileContent = System.IO.File.ReadAllText(view.InputFile);
            Dadgnl dadgnl;
            try {
                dadgnl = DadgnlFactory.CreatFromText(fileContent, 0);
            } catch {
                view.ReturnMessage = "Não foi possivel ler o arquivo de entrada.";
                return;
            }

            for (var dt = new DateTime(view.AnoInicial, view.MesInicial, 1);
                    dt <= new DateTime(view.AnoFinal, view.MesFinal, 1);
                    dt = dt.AddMonths(1)) {

                var result = dadgnl.CreateNewMensal(dt.Year, dt.Month);

                var ext = DecompTools.Util.UtilitarioDeData.NomeMes(dt.Month);

                if (!System.IO.Directory.Exists(System.IO.Path.Combine(view.OutputFolder, dt.ToString("yyyyMM")))) {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(view.OutputFolder, dt.ToString("yyyyMM")));
                }

                using (var sw = System.IO.File.CreateText(
                    System.IO.Path.Combine(view.OutputFolder, dt.ToString("yyyyMM"), "DADGNL." + ext)
                    )) {

                    sw.Write(result.ToString());
                    sw.Close();

                }
            }

            view.ReturnMessage = "DADGNL criado com sucesso.";

        }
    }
}
