namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPCostoByDimension")]
    public partial class SPCostoByDimension
    {
        public int Id { get; set; }

        public int detalle_presupuesdoId { get; set; }

        public int dimensionId { get; set; }

        public double? costo { get; set; }

        public virtual SPDetallePresupuesto SPDetallePresupuesto { get; set; }

        public virtual SPDimension SPDimension { get; set; }
    }
}
