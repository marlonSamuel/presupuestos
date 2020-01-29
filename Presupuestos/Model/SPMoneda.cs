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

    [Table("SPMoneda")]
    public partial class SPMoneda : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPMoneda()
        {
            SPPais = new HashSet<SPPais>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string moneda { get; set; }

        [Required]
        [StringLength(25)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string iso { get; set; }

        [Required]
        [StringLength(5)]
        public string simbolo { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPais> SPPais { get; set; }
        

        public List<SPMoneda> GetAll()
        {
            var list = new List<SPMoneda>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPMoneda.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPMoneda GetById(int id)
        {
            var moneda = new SPMoneda();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    moneda = ctx.SPMoneda.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return moneda;
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
                    rpta = "moneda registrada con exito";
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
                    var moneda = this.GetById(id);

                    if (moneda != null)
                    {
                        moneda.estado = "I";
                        ctx.Entry(moneda).State = EntityState.Modified;
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
