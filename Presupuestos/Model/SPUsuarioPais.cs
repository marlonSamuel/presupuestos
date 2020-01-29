namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SPUsuarioPais
    {
        public int Id { get; set; }

        public int usuarioId { get; set; }

        public int paisId { get; set; }

        public virtual SPPais SPPais { get; set; }

        public virtual SPUsuario SPUsuario { get; set; }
    }
}
