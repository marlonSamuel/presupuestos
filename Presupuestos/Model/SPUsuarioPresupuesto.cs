namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPUsuarioPresupuesto")]
    public partial class SPUsuarioPresupuesto
    {
        [Key]
        public int Id { get; set; }

        public int? usuarioId { get; set; }

        public int? tipoId { get; set; }

        public virtual SPTipoPresupuesto SPTipoPresupuesto { get; set; }

        public virtual SPUsuario SPUsuario { get; set; }
    }
}
