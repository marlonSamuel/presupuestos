namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Insumos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Correlativo { get; set; }

        [StringLength(10)]
        public string Codigo { get; set; }

        [StringLength(80)]
        public string Descripcion { get; set; }

        [StringLength(10)]
        public string UnidadMed { get; set; }

        [StringLength(7)]
        public string TipoDosis { get; set; }

        public decimal? DosisBaja { get; set; }

        public decimal? DosisNormal { get; set; }

        public decimal? DosisAlta { get; set; }

        public decimal? CostoUnitario { get; set; }

        [StringLength(15)]
        public string DDosisBaja { get; set; }

        [StringLength(15)]
        public string DDosisNormal { get; set; }

        [StringLength(15)]
        public string DDosisAlta { get; set; }

        public int TipoInsumo { get; set; }
    }
}
