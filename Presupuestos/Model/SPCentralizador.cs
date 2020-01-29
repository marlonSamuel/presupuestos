namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPCentralizador")]
    public partial class SPCentralizador
    {
        [Key]
        public int centralizadorId { get; set; }

        [Required]
        [StringLength(5)]
        public string fincaId { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        public int? estado { get; set; }

        public virtual CentrodeCosto CentrodeCosto { get; set; }
    }
}
