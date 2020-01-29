namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPDepartamento")]
    public partial class SPDepartamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPDepartamento()
        {
            SPEmpleado = new HashSet<SPEmpleado>();
        }

        [Key]
        public int departamentoId { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPEmpleado> SPEmpleado { get; set; }

        public List<SPDepartamento> GetAll()
        {
            var list = new List<SPDepartamento>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPDepartamento.Where(x=>x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPDepartamento GetById(int id)
        {
            var departamento = new SPDepartamento();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    departamento = ctx.SPDepartamento.Where(x => x.departamentoId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return departamento;
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
                    if (this.departamentoId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Actividad registrada con exito";
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
                    var departamento = this.GetById(id);

                    if (departamento != null)
                    {
                        departamento.estado = "I";
                        ctx.Entry(departamento).State = EntityState.Modified;
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
