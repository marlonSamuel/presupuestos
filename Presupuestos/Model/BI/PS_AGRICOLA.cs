namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PS_AGRICOLA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string codigo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string nombre { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string unidad_medida { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string categoria { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal costo { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string codigo_grupo { get; set; }
    }
}
