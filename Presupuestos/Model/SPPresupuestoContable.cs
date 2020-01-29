namespace Model
{
    using Model.DTO;
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPPresupuestoContable")]
    public partial class SPPresupuestoContable : IAuditable
    {
        public int Id { get; set; }

        public int? presupuestoId { get; set; }

        public int? subcentroId { get; set; }

        public int? loteId { get; set; }

        [StringLength(100)]
        public string codigo_cuenta { get; set; }

        [StringLength(100)]
        public string cuenta_contable { get; set; }

        public decimal? enero { get; set; }

        public decimal? febrero { get; set; }

        public decimal? marzo { get; set; }

        public decimal? abril { get; set; }

        public decimal? mayo { get; set; }

        public decimal? junio { get; set; }

        public decimal? julio { get; set; }

        public decimal? agosto { get; set; }

        public decimal? septiembre { get; set; }

        public decimal? octubre { get; set; }

        public decimal? noviembre { get; set; }

        public decimal? diciembre { get; set; }

        public decimal? total { get; set; }

        [StringLength(1)]
        public string estado { get; set; }
        
        [MaxLength]
        public string detalle { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        public virtual SPLote SPLote { get; set; }

        public virtual SPPresupuesto SPPresupuesto { get; set; }

        public virtual SPSubCentroCosto SPSubCentroCosto { get; set; }

        //get all presupuestos
        public List<SPPresupuestoContable> GetAll()
        {
            var list = new List<SPPresupuestoContable>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPPresupuestoContable.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<SPPresupuestoContable> GetByPresupuesto(int presupuestoId)
        {
            var list = new List<SPPresupuestoContable>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPPresupuestoContable.Where(x => x.presupuestoId == presupuestoId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<SPPresupuestoContable> GetByPresupuestoFinca(int presupuestoId, int fincaId)
        {
            var list = new List<SPPresupuestoContable>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPPresupuestoContable.Where(x => x.presupuestoId == presupuestoId && x.subcentroId == fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public SPPresupuestoContable GetById(int id)
        {
            var pre = new SPPresupuestoContable();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    pre = ctx.SPPresupuestoContable.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return pre;
        }

        public string Save()
        {
            var rpta = "";
            this.estado = "A";
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Presuppuesto registrada con exito";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return rpta;
        }

        public bool Delete(int Id)
        {
            var rpta = false;
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var dPresupuesto = ctx.SPPresupuestoContable.Where(x => x.Id == Id).SingleOrDefault();
                    if (dPresupuesto != null)
                    {
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

        public string SaveFile(CargaMasivaCt model)
        {
            string rpta = "";
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    using(var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach(var m in model.detalle)
                            {
                                var newPsContalle = new SPPresupuestoContable
                                {
                                    Id = model.Id,
                                    presupuestoId = model.presupuestoId,
                                    subcentroId = m.subcentroId,
                                    loteId = m.loteId,
                                    cuenta_contable = m.cuenta_contable,
                                    codigo_cuenta = m.codigo_cuenta,
                                    enero = m.enero,
                                    febrero = m.febrero,
                                    marzo = m.marzo,
                                    abril = m.abril,
                                    mayo = m.mayo,
                                    junio = m.junio,
                                    julio = m.julio,
                                    agosto = m.agosto,
                                    septiembre = m.septiembre,
                                    octubre = m.octubre,
                                    noviembre = m.noviembre,
                                    diciembre = m.diciembre,
                                    detalle = m.detalle,
                                    total = m.enero + m.febrero + m.marzo + m.abril + m.marzo + m.junio+
                                            m.julio + m.agosto + m.septiembre + m.octubre + m.noviembre + 
                                            m.diciembre
                                };

                                ctx.Configuration.AutoDetectChangesEnabled = false;
                                ctx.SPPresupuestoContable.Add(newPsContalle);
                                ctx.SaveChanges();

                            }


                            transaction.Commit();
                            rpta = "Carga de datos realizada correctamente";
                        }
                        catch(Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
            }catch(Exception e)
            {
                throw;
            }
            return rpta;
        }
    }
}
