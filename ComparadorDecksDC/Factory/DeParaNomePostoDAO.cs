using ComparadorDecksDC.Modelagem;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparadorDecksDC.Factory {
    public class DeParaNomePostoDAO {

        public static List<DeParaNomePosto> GetAll() {
            using (ISession session = NHibernateHelper.OpenSession()) {
                return (List<DeParaNomePosto>)session.CreateCriteria(typeof(DeParaNomePosto))
                     .List<DeParaNomePosto>();
            }
        }

        public static async Task<List<DeParaNomePosto>> GetAllAsync() {
            return await Task.Factory.StartNew(() => GetAll());            
        }
    }
}
