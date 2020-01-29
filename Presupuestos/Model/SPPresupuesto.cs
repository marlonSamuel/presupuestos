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

    [Table("SPPresupuesto")]
    public partial class SPPresupuesto : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPPresupuesto()
        {
            SPDetallePresupuesto = new HashSet<SPDetallePresupuesto>();
            SPPresupuestoContable = new HashSet<SPPresupuestoContable>();
        }

        public int Id { get; set; }

        public int tipoId { get; set; }

        public int? paisId { get; set; }

        [StringLength(1)]
        public string tipo { get; set; }

        public int? año { get; set; }

        public int? mes { get; set; }

        public int? semana { get; set; }

        public int? quincena { get; set; }

        public decimal? total_estimado { get; set; }

        public DateTime? fecha { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPDetallePresupuesto> SPDetallePresupuesto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPresupuestoContable> SPPresupuestoContable { get; set; }

        public virtual SPPais SPPais { get; set; }

        public virtual SPTipoPresupuesto SPTipoPresupuesto { get; set; }


        public List<SPPresupuesto> GetAll(int tipoId, string opcion, int paisId)
        {
            var list = new List<SPPresupuesto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPPresupuesto.Where(x => x.tipoId == tipoId && x.tipo == opcion && x.estado != "A" && x.paisId == paisId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPPresupuesto GetById(int id)
        {
            var presupuesto = new SPPresupuesto();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    presupuesto = ctx.SPPresupuesto.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return presupuesto;
        }

        //metodo para guardar actividads
        public string Save()
        {
            string rpta = "";
            this.estado = "I";
            this.fecha = DateTime.Today;

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "presupuesto ah sido registrado con exito";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return rpta;
        }

        //metodo para eliminar a nivel logico una actividad o finalizar presupuesto
        public bool Delete(int id, string estado)
        {
            var rpta = false;
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var presupuesto = this.GetById(id);

                    if (presupuesto != null)
                    {
                        presupuesto.estado = estado;
                        ctx.Entry(presupuesto).State = EntityState.Modified;
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
