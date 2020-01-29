using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class CuentasContables
    {
        public string numero { get; set; }
        public string nombre { get; set; }


        public List<CuentasContables> GetAll()
        {
            var list = new List<CuentasContables>();

            try
            {
                using(var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<CuentasContables>("Select * from cuentas_contables")
                        .ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return list;
        }
    }
}
