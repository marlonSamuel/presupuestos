namespace Model
{
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPUnidadMedida")]
    public partial class SPUnidadMedida : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPUnidadMedida()
        {
            SPActividad = new HashSet<SPActividad>();
        }

        [Key]
        public int unidadId { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPActividad> SPActividad { get; set; }

        //listar actividades
        public List<SPUnidadMedida> GetAll()
        {
            var list = new List<SPUnidadMedida>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPUnidadMedida.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPUnidadMedida GetById(int id)
        {
            var actividad = new SPUnidadMedida();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    actividad = ctx.SPUnidadMedida.Where(x => x.unidadId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return actividad;
        }

        //metodo para guardar actividads
        public string Save()
        {
            string rpta = "";
            this.estado = "A";

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    if (this.unidadId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Unidad de media registrada con exito";
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
                    var unidad = this.GetById(id);

                    if (unidad != null)
                    {
                        ctx.Entry(unidad).State = EntityState.Modified;
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
