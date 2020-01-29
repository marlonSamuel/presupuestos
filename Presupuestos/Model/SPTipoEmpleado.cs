namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPTipoEmpleado")]
    public partial class SPTipoEmpleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPTipoEmpleado()
        {
            SPEmpleado = new HashSet<SPEmpleado>();
        }

        [Key]
        public int tipoId { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPEmpleado> SPEmpleado { get; set; }

        //listamos todos los tipos empleados activos
        public List<SPTipoEmpleado> GetAll()
        {
            var list = new List<SPTipoEmpleado>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPTipoEmpleado.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPTipoEmpleado GetById(int id)
        {
            var tipo = new SPTipoEmpleado();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    tipo = ctx.SPTipoEmpleado.Where(x => x.tipoId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return tipo;
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
                    if (this.tipoId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Tipo empleado registrada con exito";
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
                    var tipo = this.GetById(id);

                    if (tipo != null)
                    {
                        tipo.estado = "I";
                        ctx.Entry(tipo).State = EntityState.Modified;
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
