using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class PSAgricola
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal costo { get; set; }
        public string unidad_medida { get; set; }
        public string categoria { get; set; }

        public List<PSAgricola> GetAllProductos()
        {
            var list = new List<PSAgricola>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSAgricola>("Select * from PS_AGRICOLA")
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<PSAgricola> GetAll(string grupoId)
        {
            var list = new List<PSAgricola>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSAgricola>("Select * from PS_AGRICOLA WHERE codigo_grupo IN (@p0)",grupoId)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<PSAgricola> GetByCategoria(string categoriaId)
        {
            var list = new List<PSAgricola>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<PSAgricola>("Select * from PS_AGRICOLA WHERE categoria IN (@p0)", categoriaId)
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListDetallDto> GetDetalleAgricola(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<ListDetallDto>("select * from DETALLE_P_AGRICOLA WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId,fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListDetallDto> GetDetalleGroupByGrupo(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<ListDetallDto>("select * from SP_GroupByGrupo WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId, fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListDetallDto> GetDetalleGroupByCategoria(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new BIContext())
                {
                    list = ctx.Database.SqlQuery<ListDetallDto>("select * from SP_GroupByCategoria WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId, fincaId).ToList();
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
