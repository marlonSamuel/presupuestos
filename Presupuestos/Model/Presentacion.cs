namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Presentacion")]
    public partial class Presentacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Presentacion()
        {
            SPProductoPresentacion = new HashSet<SPProductoPresentacion>();
        }

        [Key]
        [StringLength(6)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(2)]
        public string Tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPProductoPresentacion> SPProductoPresentacion { get; set; }


        public List<Presentacion> GetAll()
        {
            var list = new List<Presentacion>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.Presentacion.ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }
    }
}
