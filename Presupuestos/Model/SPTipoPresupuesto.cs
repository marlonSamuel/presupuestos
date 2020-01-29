namespace Model
{
    using Model.DTO;
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPTipoPresupuesto")]
    public partial class SPTipoPresupuesto : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPTipoPresupuesto()
        {
            SPPresupuesto = new HashSet<SPPresupuesto>();
        }

        [Key]
        public int tipoId { get; set; }

        [StringLength(10)]
        public string codigo { get; set; }

        [StringLength(25)]
        public string nombre { get; set; }

        [StringLength(20)]
        public string icon { get; set; }

        [StringLength(20)]
        public string clase { get; set; }

        [StringLength(20)]
        public string url { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPresupuesto> SPPresupuesto { get; set; }

        public List<SPTipoPresupuesto> GetAll()
        {
            var list = new List<SPTipoPresupuesto>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.SPTipoPresupuesto.Where(x=>x.estado == "A").ToList();
                }
            }
            catch(Exception)
            {

            }

            return list;
        }

        public List<ListTipoPresupuestoDto> GetByUser(int userId)
        {
            var list = new List<ListTipoPresupuestoDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from up in ctx.SPUsuarioPresupuesto
                            join p in ctx.SPTipoPresupuesto on up.tipoId equals p.tipoId
                            where up.usuarioId == userId
                            select new ListTipoPresupuestoDto
                            {
                                Id = up.Id,
                                tipoId = up.tipoId,
                                icon = p.icon,
                                nombre = p.nombre,
                                clase = p.clase,
                                url = p.url
                            }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        public SPTipoPresupuesto GetById(int id)
        {
            var tipo = new SPTipoPresupuesto();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    tipo = ctx.SPTipoPresupuesto.Where(x => x.tipoId == id).SingleOrDefault();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return tipo;
        }
    }
}
