namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPCCostoActividad")]
    public partial class SPCCostoActividad
    {
        [Key]
        public int detalleId { get; set; }

        public int fincaId { get; set; }

        public int actividadId { get; set; }

        public virtual SPActividad SPActividad { get; set; }

        public virtual SPSubCentroCosto SPSubCentroCosto { get; set; }
    }
}
