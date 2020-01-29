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

    [Table("SPGrupoDimension")]
    public partial class SPGrupoDimension : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPGrupoDimension()
        {
            SPActividad = new HashSet<SPActividad>();
            SPDimension = new HashSet<SPDimension>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string codigo { get; set; }

        [StringLength(150)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPDimension> SPDimension { get; set; }

        public List<SPGrupoDimension> GetAll()
        {
            var list = new List<SPGrupoDimension>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPGrupoDimension.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPGrupoDimension GetById(int id)
        {
            var grupo = new SPGrupoDimension();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    grupo = ctx.SPGrupoDimension.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return grupo;
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
                    if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "grupo dimension registrada con exito";
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
                    var grupo = this.GetById(id);

                    if (grupo != null)
                    {
                        grupo.estado = "I";
                        ctx.Entry(grupo).State = EntityState.Modified;
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
