namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPProductoPresentacion")]
    public partial class SPProductoPresentacion
    {
        public int Id { get; set; }

        public int productoId { get; set; }

        [Required]
        [StringLength(6)]
        public string presentacionId { get; set; }

        public virtual Presentacion Presentacion { get; set; }

        public virtual SPProducto SPProducto { get; set; }
    }
}
