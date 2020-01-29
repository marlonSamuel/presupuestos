namespace Model
{
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPPermiso")]
    public partial class SPPermiso : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPPermiso()
        {
            SPUsuarioPermiso = new HashSet<SPUsuarioPermiso>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string codigo { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUsuarioPermiso> SPUsuarioPermiso { get; set; }

        //metodo para listar permisot
        public List<SPPermiso> GetAll()
        {
            var list = new List<SPPermiso>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.SPPermiso.ToList();
                }
            }
            catch
            {

            }

            return list;
        }
    }
}
