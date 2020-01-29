namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Grupo_Producto
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string categoria { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string codigo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string nombre { get; set; }
    }
}
