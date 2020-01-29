namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP_GroupByCategoria
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string categoria { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int presupuestoId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fincaId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string nombre_categoria { get; set; }

        public decimal? total_referencia { get; set; }
    }
}
