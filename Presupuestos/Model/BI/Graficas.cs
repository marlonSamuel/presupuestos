using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BI
{
    public class Graficas
    {
        public  GraficasListDto GetPresupuestos()
        {
            var list = new GraficasListDto();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = (from t in ctx.SPTipoPresupuesto
                            join p in ctx.SPPresupuesto on t.tipoId equals p.tipoId
                            where t.tipoId == 1
                            select new
                            {
                                tipo = t.nombre,
                                labels = (from dp in ctx.SPPresupuesto
                                          join ti in ctx.SPTipoPresupuesto on dp.tipoId equals ti.tipoId
                                          where dp.tipoId == p.tipoId
                                          select new Labels {
                                              label = "Semana "+dp.semana
                                          }).ToList(),
                                data = (from dpt in ctx.SPPresupuesto
                                          join tit in ctx.SPTipoPresupuesto on dpt.tipoId equals tit.tipoId
                                          where dpt.tipoId == p.tipoId
                                          select new data
                                          {
                                              total = dpt.total_estimado
                                          }).ToList(),
                            }).Select(x => new GraficasListDto
                           {
                               tipo = x.tipo,
                               labels = x.labels,
                               data = x.data
                           }).SingleOrDefault();

                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return list;
        }
    }
}
