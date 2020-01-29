namespace Model.BI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BIContext : DbContext
    {
        public BIContext()
            : base("name=BIContext")
        {
        }

        public virtual DbSet<CATEGORIA> CATEGORIA { get; set; }
        public virtual DbSet<cuentas_contables> cuentas_contables { get; set; }
        public virtual DbSet<DETALLE_P_AGRICOLA> DETALLE_P_AGRICOLA { get; set; }
        public virtual DbSet<DETALLE_P_VENTAS> DETALLE_P_VENTAS { get; set; }
        public virtual DbSet<Grupo_Producto> Grupo_Producto { get; set; }
        public virtual DbSet<PS_AGRICOLA> PS_AGRICOLA { get; set; }
        public virtual DbSet<PS_VENTA> PS_VENTA { get; set; }
        public virtual DbSet<SP_GroupByCategoria> SP_GroupByCategoria { get; set; }
        public virtual DbSet<SP_GroupByGrupo> SP_GroupByGrupo { get; set; }
        public virtual DbSet<SP_GroupByGrupoVenta> SP_GroupByGrupoVenta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DETALLE_P_AGRICOLA>()
                .Property(e => e.referencia)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_P_AGRICOLA>()
                .Property(e => e.lote)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_P_AGRICOLA>()
                .Property(e => e.costo)
                .HasPrecision(38, 20);

            modelBuilder.Entity<DETALLE_P_AGRICOLA>()
                .Property(e => e.total_referencia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DETALLE_P_AGRICOLA>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_P_VENTAS>()
                .Property(e => e.referencia)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_P_VENTAS>()
                .Property(e => e.lote)
                .IsUnicode(false);

            modelBuilder.Entity<DETALLE_P_VENTAS>()
                .Property(e => e.costo)
                .HasPrecision(38, 20);

            modelBuilder.Entity<DETALLE_P_VENTAS>()
                .Property(e => e.total_referencia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DETALLE_P_VENTAS>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PS_AGRICOLA>()
                .Property(e => e.costo)
                .HasPrecision(38, 20);

            modelBuilder.Entity<PS_VENTA>()
                .Property(e => e.costo)
                .HasPrecision(38, 20);

            modelBuilder.Entity<SP_GroupByCategoria>()
                .Property(e => e.total_referencia)
                .HasPrecision(38, 2);

            modelBuilder.Entity<SP_GroupByGrupo>()
                .Property(e => e.total_referencia)
                .HasPrecision(38, 2);

            modelBuilder.Entity<SP_GroupByGrupoVenta>()
                .Property(e => e.total_referencia)
                .HasPrecision(38, 2);
        }
    }
}
