using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class Reportes
    {

        //agrupar detalles por lote
        public List<ReporteAgricolaDto> GetAgriculaGroup(int presupuestoId)
        {
            var list = new List<ReporteAgricolaDto>();
            try
            {
                using (var ctx = new BIContext())
                {
                    list = (from g in ctx.SP_GroupByGrupo
                            join c in ctx.SP_GroupByCategoria on g.categoria equals c.categoria
                            where g.presupuestoId == presupuestoId
                            group c by new { c.categoria, c.nombre_categoria, c.total_referencia } into hh
                            select new
                            {
                                nombre_categoria = hh.Key.nombre_categoria,
                                total_categoria = hh.Key.total_referencia,
                                total = hh.Sum(x => x.total_referencia),
                                grupos = ctx.SP_GroupByGrupo.Where(x => x.categoria == hh.Key.categoria).ToList()
                            }).Select(x => new ReporteAgricolaDto
                            {
                                nombre_categoria = x.nombre_categoria,
                                total_categoria = x.total_categoria,
                                total = x.total,
                                grupos = x.grupos
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public reporteHeader GetHeader(int presupuestoId)
        {
            var header = new reporteHeader();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    header = (from p in ctx.SPPresupuesto
                              join t in ctx.SPTipoPresupuesto on p.tipoId equals t.tipoId
                              where p.Id == presupuestoId
                              select new reporteHeader
                              {
                                  nombre_presupuesto = "Presupuesto "+t.nombre,
                                  total = p.total_estimado,
                                  año = p.año,
                                  semana = p.semana,
                                  mes = p.mes
                              }).SingleOrDefault();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return header;
        }
    }
}
