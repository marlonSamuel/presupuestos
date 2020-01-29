namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLE_P_AGRICOLA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fincaId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int presupuestoId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string referencia { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string nombre_referencia { get; set; }

        [StringLength(25)]
        public string lote { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int loteId { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal costo { get; set; }

        public decimal? total_referencia { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(10)]
        public string unidad_medida { get; set; }

        public int? cantidad { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(10)]
        public string categoria { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string grupo { get; set; }
    }
}
