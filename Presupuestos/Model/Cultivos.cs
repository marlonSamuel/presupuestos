namespace Model
{
    using Model.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Cultivos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cultivos()
        {
            SPProducto = new HashSet<SPProducto>();
        }

        [Key]
        [StringLength(6)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(5)]
        public string GrupoCultivo { get; set; }

        public short Activo { get; set; }

        [StringLength(15)]
        public string CodigoNAV { get; set; }

        public double FactorCrecimiento { get; set; }

        public double FactorDesarrollo { get; set; }

        public double FactorProduccion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPProducto> SPProducto { get; set; }

        public List<ListCultivoDto> GetAll()
        {
            var list = new List<ListCultivoDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    list = (from c in ctx.Cultivos 
                                 join g in ctx.Gruposcultivos on c.GrupoCultivo equals g.Codigo
                                 select new
                                 {
                                    codigo = c.Codigo,
                                    descripcion = c.Descripcion,
                                    grupo = g.Descripcion,
                                    codigoGrupo = g.Codigo,
                                    codigoNav = c.CodigoNAV,
                                    activo = c.Activo
                                 }).Select(x=> new ListCultivoDto {
                                    codigo = x.codigo,
                                    descripcion = x.descripcion,
                                    grupo = x.grupo,
                                    codigoGrupo = x.codigoGrupo,
                                    codigoNav = x.codigoNav,
                                    activo = x.activo
                                 }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        public List<Gruposcultivos>GetActivos()
        {
            var list = new List<Gruposcultivos>();

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.Gruposcultivos.Where(x => x.Activo == 1).ToList();
                }
            }
            catch
            {
                throw;
            }

            return list;
        }
    }
}
