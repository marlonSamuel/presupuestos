namespace Model
{
    using Model.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Variedades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Variedades()
        {
            SPSubLote = new HashSet<SPSubLote>();
        }

        [Key]
        [StringLength(6)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(5)]
        public string GrupoCultivo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPSubLote> SPSubLote { get; set; }

        public List<ListVariedadesDto> GetAll()
        {
            var list = new List<ListVariedadesDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from v in ctx.Variedades
                            join g in ctx.Gruposcultivos on v.GrupoCultivo equals g.Codigo
                            select new
                            {
                                codigo = v.Codigo,
                                descripcion = v.Descripcion,
                                grupo = g.Descripcion,
                                codigoGrupo = g.Codigo
                            }).Select(x => new ListVariedadesDto
                            {
                                codigo = x.codigo,
                                descripcion = x.descripcion,
                                grupo = x.grupo,
                                codigoGrupo = x.codigoGrupo
                            }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        public List<Variedades> GetByCultivo(string cultivo)
        {
            var list = new List<Variedades>();

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.Variedades.Where(x => x.GrupoCultivo == cultivo).ToList();
                }
            }catch(Exception ex)
            {
                throw ex;
            }

            return list;
        }
    }
}
