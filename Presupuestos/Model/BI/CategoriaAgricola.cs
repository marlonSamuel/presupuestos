using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class CategoriaAgricola
    {
        public string codigo { get; set; }
        public string nombre { get; set; }

        public List<CategoriaAgricola> GetAll()
        {
            var list = new List<CategoriaAgricola>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<CategoriaAgricola>("Select * from CATEGORIA where codigo != 12")
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }
    }
}
