namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Gruposcultivos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gruposcultivos()
        {
            SPSubLote = new HashSet<SPSubLote>();
        }

        [Key]
        [StringLength(5)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        public int Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPSubLote> SPSubLote { get; set; }

        public List<Gruposcultivos> GetAll()
        {
            var list = new List<Gruposcultivos>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.Gruposcultivos.ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }
    }
}
