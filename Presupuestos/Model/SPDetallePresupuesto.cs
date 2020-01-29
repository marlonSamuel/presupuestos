namespace Model
{
    using Model.BI;
    using Model.DTO;
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPDetallePresupuesto")]
    public partial class SPDetallePresupuesto : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPDetallePresupuesto()
        {
            SPCostoByDimension = new HashSet<SPCostoByDimension>();
        }

        public int Id { get; set; }

        public int presupuestoId { get; set; }

        public int fincaId { get; set; }
        
        [StringLength(50)]
        public string referencia { get; set; }

        public int? cantidad { get; set; }

        public int subloteId { get; set; }

        public decimal? total_referencia { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public int unidad_by_p_cs { get; set; }
        public decimal? costo_by_p_cs { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPCostoByDimension> SPCostoByDimension { get; set; }

        public virtual SPPresupuesto SPPresupuesto { get; set; }

        public virtual SPSubCentroCosto SPSubCentroCosto { get; set; }

        public virtual SPSubLote SPSubLote { get; set; }

        //obtener detalles por id
        public List<SPDetallePresupuesto> GetByPresupuesto(int presupuestoId)
        {
            var list = new List<SPDetallePresupuesto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPDetallePresupuesto.Where(x => x.presupuestoId == presupuestoId).ToList();
                }
            }
            catch
            {

            }
            return list;
        }


        //obtener detalles por presupuest y centro de costo
        public List<ListDetallDto> GetByCCosto(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();
                    
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from dm in ctx.SPDetallePresupuesto
                            join l in ctx.SPSubLote on dm.subloteId equals l.Id
                            join a in ctx.SPActividad on dm.referencia equals a.codigo
                            join u in ctx.SPUnidadMedida on a.unidadId equals u.unidadId

                            where dm.presupuestoId == presupuestoId && dm.fincaId == fincaId
                            select new
                            {
                                Id = dm.Id,
                                fincaId = dm.fincaId,
                                presupuestoId = dm.presupuestoId,
                                referencia = dm.referencia,
                                actividad = a.nombre,
                                loteId = dm.subloteId,
                                lote = l.nombre,
                                cantidad = dm.cantidad,
                                costo = a.costo,
                                total_referencia = dm.total_referencia,
                                unidad_media = u.nombre,
                                total = ctx.SPDetallePresupuesto.Where(x => x.presupuestoId == presupuestoId && x.fincaId == fincaId).Sum(x => x.total_referencia),
                                estado = dm.estado,
                                tipo = a.tipo
                            }
                            ).Select(x => new ListDetallDto
                            {
                                id = x.Id,
                                nombre_referencia = x.actividad,
                                referencia = x.referencia,
                                loteId = x.loteId,
                                lote = x.lote,
                                cantidad = x.cantidad,
                                total_referencia = x.total_referencia,
                                costo = x.costo,
                                fincaId = x.fincaId,
                                presupuestoId = x.presupuestoId,
                                total = x.total,
                                unidad_medida = x.unidad_media,
                                estado = x.estado, 
                                tipo = x.tipo
                            }).ToList();
                    //list = ctx.SPDetallePreManoObra.Where(x => x.presupuestoId == presupuestoId && x.fincaId == fincaId).ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return list;
        }

        //obtener detalles por presupuest y centro de costo
        public List<ListDetallDto> GetByCCostoCosecha(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from dm in ctx.SPDetallePresupuesto
                            join l in ctx.SPSubLote on dm.subloteId equals l.Id
                            join g in ctx.Gruposcultivos on l.cultivoId equals g.Codigo
                            join v in ctx.Variedades on l.variedadId equals v.Codigo
                            join u in ctx.SPUnidadMedida on dm.unidad_by_p_cs equals u.unidadId
                            where dm.presupuestoId == presupuestoId && dm.fincaId == fincaId
                            select new
                            {
                                Id = dm.Id,
                                fincaId = dm.fincaId,
                                presupuestoId = dm.presupuestoId,
                                loteId = dm.subloteId,
                                lote = l.nombre,
                                cantidad = dm.cantidad,
                                total_referencia = dm.total_referencia,
                                unidad_media = u.nombre,
                                total = ctx.SPDetallePresupuesto.Where(x => x.presupuestoId == presupuestoId && x.fincaId == fincaId).Sum(x => x.total_referencia),
                                estado = dm.estado,
                                cultivo = g.Descripcion.Trim() + " " + v.Descripcion.Trim(),
                                unidad_by_p_cs = dm.unidad_by_p_cs,
                                costo_by_p_cs = dm.costo_by_p_cs,
                                costo = dm.costo_by_p_cs
                            }
                            ).Select(x => new ListDetallDto
                            {
                                id = x.Id,
                                loteId = x.loteId,
                                lote = x.lote,
                                cantidad = x.cantidad,
                                total_referencia = x.total_referencia,
                                fincaId = x.fincaId,
                                presupuestoId = x.presupuestoId,
                                total = x.total,
                                unidad_medida = x.unidad_media,
                                estado = x.estado,
                                cultivo = x.cultivo,
                                unidad_by_p_cs = x.unidad_by_p_cs,
                                costo_by_p_cs = x.costo_by_p_cs,
                                costo = x.costo
                            }).ToList();
                    //list = ctx.SPDetallePreManoObra.Where(x => x.presupuestoId == presupuestoId && x.fincaId == fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        //agrupar detalles por lote
        public List<ListDetallDto> GetByGroup(int presupuestoId, int fincaId)
        {
            var list = new List<ListDetallDto>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = (from dm in ctx.SPDetallePresupuesto
                            join l in ctx.SPSubLote on dm.subloteId equals l.Id
                            where dm.presupuestoId == presupuestoId && dm.fincaId == fincaId
                            group dm by new { dm.subloteId, l.nombre, dm.presupuestoId } into hh
                            select new
                                {
                                    presupuestoId = hh.Key.presupuestoId,
                                    loteId = hh.Key.subloteId,
                                    nombre = hh.Key.nombre,
                                    suma =  hh.Sum(x=>x.total_referencia),
                                }).Select(x=> new ListDetallDto {
                                    loteId = x.loteId,
                                    lote = x.nombre,
                                    total_lote = x.suma,
                                    presupuestoId = x.presupuestoId
                                }).ToList();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return list;
        }

        //obtener centros de costo asignados mas sus estados
        public List<ListPresupuestoDto> GetByAsignaciones(int presupuestoId)
        {
            var list = new List<ListPresupuestoDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from c in ctx.SPSubCentroCosto
                            from pm in ctx.SPDetallePresupuesto.Where(x => c.Id == x.fincaId).DefaultIfEmpty()
                            where pm.presupuestoId == presupuestoId || pm.presupuestoId.Equals(null)
                            group pm by new { c.nombre, pm.fincaId, pm.estado } into hh
                            select new {
                                centro_costo = hh.Key.nombre,
                                fincaId = hh.Key.fincaId,
                                estado = hh.Key.estado
                            }).Select(x => new ListPresupuestoDto
                            {
                                centro_costo = x.centro_costo,
                                estado = x.estado
                                
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        //guardar o actualizar lotes
        public string Save(CreateDetalleManoObraDto model)
        {
            var rpta = "";
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            if(model.referencia == null)
                            {
                                model.referencia = model.referencia_producto;
                            }

                            var d_manoObra = new SPDetallePresupuesto
                            {
                                Id = model.Id,
                                presupuestoId = model.presupuestoId,
                                fincaId = model.fincaId,
                                subloteId = model.loteId,
                                referencia = model.referencia,
                                total_referencia = model.total_referencia,
                                cantidad = model.cantidad,
                                unidad_by_p_cs = model.unidad_by_p_cs,
                                costo_by_p_cs = model.costo_by_p_cs,
                                estado = "I"
                            };

                            if (d_manoObra.Id > 0) ctx.Entry(d_manoObra).State = EntityState.Modified;
                            else ctx.Entry(d_manoObra).State = EntityState.Added;
                            ctx.SaveChanges();

                            ctx.Database.ExecuteSqlCommand("UPDATE SPDetallePresupuesto SET estado = 'I' WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", d_manoObra.presupuestoId, d_manoObra.fincaId);

                            var detalleId = d_manoObra.Id;

                            //obtener dimensiones por actividad
                            SPDimension _dimension = new SPDimension();
                            var dimensiones = _dimension.GetDimensionesbyActividad(d_manoObra.referencia);
                            if(dimensiones.Count > 0)
                            {

                                //eliminamos los existentes para sustituirlos
                                var costosdim = ctx.SPCostoByDimension.Where(x => x.detalle_presupuesdoId == detalleId).ToList();
                                ctx.SPCostoByDimension.RemoveRange(costosdim);
                                ctx.SaveChanges();

                                foreach (var ca in dimensiones)
                                {

                                    var costo_actividad = new SPCostoByDimension
                                    {
                                        dimensionId = ca.dimensionId,
                                        detalle_presupuesdoId = detalleId,
                                        costo = Convert.ToDouble(d_manoObra.total_referencia) * (ca.factor/100)
                                    };

                                    //costo_actividad.costo = Math.Round(Convert.ToDecimal(costo_actividad.costo), 2);

                                    ctx.SPCostoByDimension.Add(costo_actividad);
                                    ctx.SaveChanges();
                                }
                            }
                            
                            transaction.Commit();
                            rpta = "accion se agrego presupuesto correctamente";
                        }
                        catch(Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
            }
            catch
            {

            }
            return rpta;
        }

        //actualizar el total del presupuesto
        public bool updateTotal(int presupuestoId, decimal? total_estimado)
        {
            var rpta = false;

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    var presupuesto = ctx.SPPresupuesto.Where(x => x.Id == presupuestoId).SingleOrDefault();
                    if(presupuesto != null)
                    {
                        presupuesto.total_estimado = total_estimado;
                        ctx.Entry(presupuesto).State = EntityState.Modified;
                        ctx.SaveChanges();
                        rpta = true;
                    }
                }
            }
            catch
            {

            }

            return rpta;
        }

        //eliminar detalles de presupuesto
        public bool Delete(int Id)
        {
            var rpta = false;
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    var dPresupuesto = ctx.SPDetallePresupuesto.Where(x => x.Id == Id).SingleOrDefault();
                    if(dPresupuesto != null)
                    {
                        var dimensiones = ctx.SPCostoByDimension.Where(x => x.detalle_presupuesdoId == Id).ToList();

                        //eliminamos los existentes en el detalle
                        ctx.SPCostoByDimension.RemoveRange(dimensiones);
                        ctx.SaveChanges();

                        ctx.Entry(dPresupuesto).State = EntityState.Deleted;
                        ctx.SaveChanges();
                        rpta = true;
                    }
                }
            }
            catch
            {

            }
            return rpta;
        }

        //finalizar presupusto por centro de costo
        public bool Finalizar(int presupuestoId, string fincaId)
        {
            var rpta = false;
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    //var query = "UPDATE SPDetallePreManoObra SET estado = 'F' WHERE presupuestoId ="+presupuestoId+" and fincaId="+"'"+fincaId+"+";

                    ctx.Database.ExecuteSqlCommand("UPDATE SPDetallePresupuesto SET estado = 'F' WHERE presupuestoId IN (@p0) and fincaId IN (@p1)", presupuestoId, fincaId);
                    rpta = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return rpta;
        }

        //comprobar si un presupusto ya fue finalizado
        public bool IsFinished(int presupuestoId, int fincaId)
        {
            var rpta = true;

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    var list = ctx.SPDetallePresupuesto.
                              Where(x => x.presupuestoId == presupuestoId && x.fincaId == fincaId && x.estado == "I").ToList();

                    if(list.Count > 0) rpta = false;
                }
            }catch(Exception e)
            {
                throw e;
            }

            return rpta;
        }

        //guardar carga masiva
        public string saveFile(CargaMasivaCreateDto model)
        {
            var rpta = "";
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var d in model.detalle)
                            {
                                var detalle = new SPDetallePresupuesto
                                {
                                    Id = model.Id,
                                    presupuestoId = model.presupuestoId,
                                    referencia = d.referencia,
                                    cantidad = d.cantidad,
                                    total_referencia = d.total_referencia,
                                    fincaId = d.fincaId,
                                    subloteId = d.subloteId,
                                    unidad_by_p_cs = d.unidad_by_p_cs,
                                    costo_by_p_cs = d.costo_by_p_cs
                                };


                                ctx.Configuration.AutoDetectChangesEnabled = false;
                                ctx.SPDetallePresupuesto.Add(detalle);
                                ctx.SaveChanges();

                                var detalleId = detalle.Id;

                                if(model.codigoTipo == "P_MO")
                                {

                                    //obtener dimensiones por actividad
                                    SPDimension _dimension = new SPDimension();
                                    var dimensiones = _dimension.GetDimensionesbyActividad(detalle.referencia);
                                    if (dimensiones.Count > 0)
                                    {
                                        //eliminamos los existentes para sustituirlos
                                        var costosdim = ctx.SPCostoByDimension.Where(x => x.detalle_presupuesdoId == detalleId).ToList();
                                        ctx.SPCostoByDimension.RemoveRange(costosdim);
                                        ctx.SaveChanges();

                                        foreach (var ca in dimensiones)
                                        {

                                            var costo_actividad = new SPCostoByDimension
                                            {
                                                dimensionId = ca.dimensionId,
                                                detalle_presupuesdoId = detalleId,
                                                costo = Convert.ToDouble(detalle.total_referencia) * (ca.factor / 100)
                                            };

                                            //costo_actividad.costo = Math.Round(Convert.ToDecimal(costo_actividad.costo), 2);
                                            ctx.Configuration.AutoDetectChangesEnabled = false;
                                            ctx.SPCostoByDimension.Add(costo_actividad);
                                            ctx.SaveChanges();
                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                            rpta = "Carga de datos realizada correctamente";
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return rpta;
        }
    }
}
