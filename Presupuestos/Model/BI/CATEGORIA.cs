namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CATEGORIA")]
    public partial class CATEGORIA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string codigo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string nombre { get; set; }
    }
}
