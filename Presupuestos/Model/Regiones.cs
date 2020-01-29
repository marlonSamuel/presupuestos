namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Regiones
    {
        [Key]
        [StringLength(5)]
        public string Codigo { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public List<Regiones> GetAll()
        {
            var list = new List<Regiones>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.Regiones.ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }
    }
}
