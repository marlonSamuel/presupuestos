namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP_GroupByGrupo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int presupuestoId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fincaId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string grupo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string categoria { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string nombre_categoria { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string nombre_grupo { get; set; }

        public decimal? total_referencia { get; set; }

    }
}
