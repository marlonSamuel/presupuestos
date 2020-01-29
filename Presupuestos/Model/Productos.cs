namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Productos
    {
        [Key]
        [StringLength(15)]
        public string Codigo { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(2)]
        public string Tipo { get; set; }

        [StringLength(2)]
        public string Variedad { get; set; }

        [StringLength(2)]
        public string Presentacion { get; set; }

        [StringLength(2)]
        public string Marca { get; set; }

        [StringLength(1)]
        public string Proveedor { get; set; }
    }
}
