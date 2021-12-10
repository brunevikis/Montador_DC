using DecompTools.FactoryDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecompTools.ModelagemDC {
    class EarmMax : Model {
        public virtual string id { get; set; }
        public virtual string sub { get; set; }
        public virtual decimal valor { get; set; }

        public static void confereNovoEarm(decimal[] subTotal) {
            List<EarmMax> list = EarmMaxDAO.GetAll();
            int x = 0;

            foreach (EarmMax ea in list) {
                var subValor = (subTotal[x] / 730.5m);
                if (Math.Abs(ea.valor - subValor) > 1m) {
                    ea.valor = subValor;
                    ea.save();
                }
                x++;
            }
        }
    }
}
