namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPCargo")]
    public partial class SPCargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPCargo()
        {
            SPEmpleado = new HashSet<SPEmpleado>();
        }

        [Key]
        public int cargoId { get; set; }

        [Required]
        [StringLength(25)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPEmpleado> SPEmpleado { get; set; }

        public List<SPCargo> GetAll()
        {
            var list = new List<SPCargo>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPCargo.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPCargo GetById(int id)
        {
            var cargo = new SPCargo();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    cargo = ctx.SPCargo.Where(x => x.cargoId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return cargo;
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
                    if (this.cargoId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "cargo registrada con exito";
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
                    var cargo = this.GetById(id);

                    if (cargo != null)
                    {
                        cargo.estado = "I";
                        ctx.Entry(cargo).State = EntityState.Modified;
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
