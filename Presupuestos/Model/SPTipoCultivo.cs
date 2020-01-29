namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPTipoCultivo")]
    public partial class SPTipoCultivo
    {
        public int Id { get; set; }

        [StringLength(25)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public List<SPTipoCultivo> GetAll()
        {
            var list = new List<SPTipoCultivo>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPTipoCultivo.ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }
    }
}
