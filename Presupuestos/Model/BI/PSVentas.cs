using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class PSVentas
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal costo { get; set; }
        public string unidad_medida { get; set; }
        public string categoria { get; set; }

        public List<PSVentas> GetAllProductos()
        {
            var list = new List<PSVentas>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSVentas>("Select * from PS_VENTA")
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<PSVentas> GetAll(string grupoId)
        {
            var list = new List<PSVentas>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSVentas>("Select * from PS_VENTA WHERE codigo_grupo IN (@P0) ",grupoId)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<PSVentas> GetByCategoria()
        {
            var list = new List<PSVentas>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSVentas>("Select * from PS_VENTA")
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListDetallDto> GetDetalleVentas(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<ListDetallDto>("select * from DETALLE_P_VENTAS WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId, fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListDetallDto> GetDetalleGroupByGroupo(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<ListDetallDto>("select * from SP_GroupByGrupoVenta WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId, fincaId).ToList();
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
