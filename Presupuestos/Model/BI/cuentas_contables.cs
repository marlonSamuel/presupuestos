namespace Model.BI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cuentas_contables
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string numero { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string nombre { get; set; }
    }
}
