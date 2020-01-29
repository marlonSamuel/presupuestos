using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class GrupoProducto
    {
        public string categoria { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }

        public List<GrupoProducto> GetAll(string categoriaId)
        {
            var list = new List<GrupoProducto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<GrupoProducto>("Select * from Grupo_Producto WHERE categoria IN (@p0)", categoriaId)
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
