namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SPUsuarioPermiso")]
    public partial class SPUsuarioPermiso
    {
        public int Id { get; set; }

        public int? usuarioId { get; set; }

        public int? permisoId { get; set; }

        public virtual SPPermiso SPPermiso { get; set; }

        public virtual SPUsuario SPUsuario { get; set; }
    }
}
