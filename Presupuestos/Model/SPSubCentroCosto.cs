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

    [Table("SPSubCentroCosto")]
    public partial class SPSubCentroCosto : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPSubCentroCosto()
        {
            SPCCostoActividad = new HashSet<SPCCostoActividad>();
            SPDetallePresupuesto = new HashSet<SPDetallePresupuesto>();
            SPPresupuestoContable = new HashSet<SPPresupuestoContable>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string codigo { get; set; }

        [StringLength(5)]
        public string fincaId { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(25)]
        public string codigo_nav { get; set; }

        [StringLength(1)]
        public string tipo_valor { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        public virtual CentrodeCosto CentrodeCosto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPCCostoActividad> SPCCostoActividad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPDetallePresupuesto> SPDetallePresupuesto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPresupuestoContable> SPPresupuestoContable { get; set; }

        public List<SPSubCentroCosto> GetAllSubCentros()
        {
            var list = new List<SPSubCentroCosto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.SPSubCentroCosto.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public List<SPSubCentroCosto> GetAll(string codigo)
        {
            var list = new List<SPSubCentroCosto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.SPSubCentroCosto.Where(x => x.fincaId == codigo).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        //obtener acatividades por subcentro de costo
        public List<SPCCostoActividad> GetDetalle(int subcentroId)
        {
            var list = new List<SPCCostoActividad>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPCCostoActividad.Where(x => x.fincaId == subcentroId).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        //obtener por id
        public SPSubCentroCosto GetById(int id)
        {
            var subcentro = new SPSubCentroCosto();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    subcentro = ctx.SPSubCentroCosto.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return subcentro;
        }

        //metodo para guardar lote y detalle lote
        public string Save(CreateSubcentroDto model)
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
                            var subCentroCosto = new SPSubCentroCosto
                            {
                               Id = model.Id,
                               fincaId = model.fincaId,
                               nombre = model.nombre,
                               codigo_nav = model.codigo_nav,
                               codigo = model.codigo,
                               estado = "A"
                            };

                            if (subCentroCosto.Id > 0) ctx.Entry(subCentroCosto).State = EntityState.Modified;
                            else ctx.SPSubCentroCosto.Add(subCentroCosto);
                            ctx.SaveChanges();

                            int fincaId = subCentroCosto.Id;

                            //eliminamos los existentes para sustituirlos
                            var detalles = ctx.SPCCostoActividad.Where(x => x.fincaId == fincaId).ToList();
                            ctx.SPCCostoActividad.RemoveRange(detalles);
                            ctx.SaveChanges();

                            if(model.actividades != null)
                            {

                                foreach (var d in model.actividades)
                                {
                                    var detalle = new SPCCostoActividad
                                    {
                                        fincaId = fincaId,
                                        actividadId = d
                                    };

                                    ctx.SPCCostoActividad.Add(detalle);
                                    ctx.SaveChanges();
                                }
                            }
                            transaction.Commit();
                            rpta = "sub centro de costo registado correctamente";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return rpta;
        }


        //metodo para eliminar a nivel logico una actividad
        public bool Delete(int id)
        {
            var rpta = false;
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var subcentro = this.GetById(id);

                    if (subcentro != null)
                    {
                        subcentro.estado = "I";
                        ctx.Entry(subcentro).State = EntityState.Modified;
                        ctx.SaveChanges();
                        rpta = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            return rpta;
        }
    }
}
