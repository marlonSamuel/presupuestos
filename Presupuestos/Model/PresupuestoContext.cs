namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Model.Shared;
    using Helpers;

    public partial class PresupuestoContext : DbContext
    {
        public PresupuestoContext()
            : base("name=PresupuestoContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public override int SaveChanges()
        {
            Auditar();
            return base.SaveChanges();
        }

        public void Auditar()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(
                x => x.Entity is IAuditable && (x.State == System.Data.Entity.EntityState.Added
                     || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditable entity = entry.Entity as IAuditable;
                if (entity != null)
                {
                    var fecha = DateTime.Now;
                    var usuario = SessionHelper.GetUser();

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreadoFecha = fecha;
                        entity.CreadoPor = usuario;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreadoFecha).IsModified = false;
                        base.Entry(entity).Property(x => x.CreadoPor).IsModified = false;
                    }

                    entity.ActualizadoFecha = fecha;
                    entity.ActualizadoPor = usuario;
                }
            }
        }

        public virtual DbSet<CentrodeCosto> CentrodeCosto { get; set; }
        public virtual DbSet<Cultivos> Cultivos { get; set; }
        public virtual DbSet<Gruposcultivos> Gruposcultivos { get; set; }
        public virtual DbSet<Presentacion> Presentacion { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Regiones> Regiones { get; set; }
        public virtual DbSet<SPActividad> SPActividad { get; set; }
        public virtual DbSet<SPCargo> SPCargo { get; set; }
        public virtual DbSet<SPCategoriaProducto> SPCategoriaProducto { get; set; }
        public virtual DbSet<SPCCostoActividad> SPCCostoActividad { get; set; }
        public virtual DbSet<SPCentralizador> SPCentralizador { get; set; }
        public virtual DbSet<SPCostoByDimension> SPCostoByDimension { get; set; }
        public virtual DbSet<SPDepartamento> SPDepartamento { get; set; }
        public virtual DbSet<SPDetallePresupuesto> SPDetallePresupuesto { get; set; }
        public virtual DbSet<SPDimension> SPDimension { get; set; }
        public virtual DbSet<SPEmpleado> SPEmpleado { get; set; }
        public virtual DbSet<SPGrupoDimension> SPGrupoDimension { get; set; }
        public virtual DbSet<SPLote> SPLote { get; set; }
        public virtual DbSet<SPMoneda> SPMoneda { get; set; }
        public virtual DbSet<SPPais> SPPais { get; set; }
        public virtual DbSet<SPPermiso> SPPermiso { get; set; }
        public virtual DbSet<SPPresupuesto> SPPresupuesto { get; set; }
        public virtual DbSet<SPProducto> SPProducto { get; set; }
        public virtual DbSet<SPProductoPresentacion> SPProductoPresentacion { get; set; }
        public virtual DbSet<SPSubCentroCosto> SPSubCentroCosto { get; set; }
        public virtual DbSet<SPSubLote> SPSubLote { get; set; }
        public virtual DbSet<SPTipoCambio> SPTipoCambio { get; set; }
        public virtual DbSet<SPTipoCultivo> SPTipoCultivo { get; set; }
        public virtual DbSet<SPTipoEmpleado> SPTipoEmpleado { get; set; }
        public virtual DbSet<SPTipoPresupuesto> SPTipoPresupuesto { get; set; }
        public virtual DbSet<SPUnidadMedida> SPUnidadMedida { get; set; }
        public virtual DbSet<SPUsuario> SPUsuario { get; set; }
        public virtual DbSet<SPUsuarioPais> SPUsuarioPais { get; set; }
        public virtual DbSet<SPUsuarioPermiso> SPUsuarioPermiso { get; set; }
        public virtual DbSet<SPUsuarioPresupuesto> SPUsuarioPresupuesto { get; set; }
        public virtual DbSet<Variedades> Variedades { get; set; }
        public virtual DbSet<SPPresupuestoContable> SPPresupuestoContable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CentrodeCosto>()
                .Property(e => e.codigo)
                .IsFixedLength();

            modelBuilder.Entity<CentrodeCosto>()
                .Property(e => e.descripcion)
                .IsFixedLength();

            modelBuilder.Entity<CentrodeCosto>()
                .Property(e => e.Region)
                .IsFixedLength();

            modelBuilder.Entity<CentrodeCosto>()
                .Property(e => e.CodigoNAV)
                .IsFixedLength();

            modelBuilder.Entity<CentrodeCosto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CentrodeCosto>()
                .HasMany(e => e.SPLote)
                .WithRequired(e => e.CentrodeCosto)
                .HasForeignKey(e => e.fincaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CentrodeCosto>()
                .HasMany(e => e.SPSubCentroCosto)
                .WithOptional(e => e.CentrodeCosto)
                .HasForeignKey(e => e.fincaId);

            modelBuilder.Entity<Presentacion>()
                .Property(e => e.Codigo)
                .IsFixedLength();

            modelBuilder.Entity<Presentacion>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Presentacion>()
                .Property(e => e.Tipo)
                .IsFixedLength();

            modelBuilder.Entity<Presentacion>()
                .HasMany(e => e.SPProductoPresentacion)
                .WithRequired(e => e.Presentacion)
                .HasForeignKey(e => e.presentacionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.Codigo)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Nombre)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Tipo)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Variedad)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Presentacion)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Marca)
                .IsFixedLength();

            modelBuilder.Entity<Productos>()
                .Property(e => e.Proveedor)
                .IsFixedLength();

            modelBuilder.Entity<Regiones>()
                .Property(e => e.Codigo)
                .IsFixedLength();

            modelBuilder.Entity<Regiones>()
                .Property(e => e.Nombre)
                .IsFixedLength();

            modelBuilder.Entity<SPActividad>()
                .Property(e => e.codigo)
                .IsUnicode(false);


            modelBuilder.Entity<SPActividad>()
                .Property(e => e.costo)
                .HasPrecision(11, 2);

            modelBuilder.Entity<SPActividad>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPActividad>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPActividad>()
                .HasMany(e => e.SPCCostoActividad)
                .WithRequired(e => e.SPActividad)
                .HasForeignKey(e => e.actividadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPCategoriaProducto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPCategoriaProducto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPCategoriaProducto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPCategoriaProducto>()
                .HasMany(e => e.SPProducto)
                .WithRequired(e => e.SPCategoriaProducto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPDetallePresupuesto>()
                .Property(e => e.referencia)
                .IsUnicode(false);

            modelBuilder.Entity<SPDetallePresupuesto>()
                .Property(e => e.total_referencia)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPDetallePresupuesto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPDetallePresupuesto>()
                .HasMany(e => e.SPCostoByDimension)
                .WithRequired(e => e.SPDetallePresupuesto)
                .HasForeignKey(e => e.detalle_presupuesdoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPDimension>()
                .Property(e => e.cuenta_contable)
                .IsUnicode(false);

            modelBuilder.Entity<SPDimension>()
                .Property(e => e.nombre_cuenta)
                .IsUnicode(false);

            modelBuilder.Entity<SPDimension>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPDimension>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPDimension>()
                .HasMany(e => e.SPCostoByDimension)
                .WithRequired(e => e.SPDimension)
                .HasForeignKey(e => e.dimensionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .HasMany(e => e.SPActividad)
                .WithRequired(e => e.SPGrupoDimension)
                .HasForeignKey(e => e.grupoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPGrupoDimension>()
                .HasMany(e => e.SPDimension)
                .WithRequired(e => e.SPGrupoDimension)
                .HasForeignKey(e => e.grupoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPLote>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPLote>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPLote>()
                .Property(e => e.fincaId)
                .IsFixedLength();

            modelBuilder.Entity<SPLote>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPLote>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPLote>()
                .HasMany(e => e.SPPresupuestoContable)
                .WithOptional(e => e.SPLote)
                .HasForeignKey(e => e.loteId);

            modelBuilder.Entity<SPLote>()
                .HasMany(e => e.SPSubLote)
                .WithRequired(e => e.SPLote)
                .HasForeignKey(e => e.loteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPMoneda>()
                .Property(e => e.moneda)
                .IsUnicode(false);

            modelBuilder.Entity<SPMoneda>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPMoneda>()
                .Property(e => e.iso)
                .IsUnicode(false);

            modelBuilder.Entity<SPMoneda>()
                .Property(e => e.simbolo)
                .IsUnicode(false);

            modelBuilder.Entity<SPMoneda>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPMoneda>()
                .HasMany(e => e.SPPais)
                .WithRequired(e => e.SPMoneda)
                .HasForeignKey(e => e.monedaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPPais>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPPais>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPPais>()
                .HasMany(e => e.CentrodeCosto)
                .WithOptional(e => e.SPPais)
                .HasForeignKey(e => e.paisId);

            modelBuilder.Entity<SPPais>()
                .HasMany(e => e.SPPresupuesto)
                .WithOptional(e => e.SPPais)
                .HasForeignKey(e => e.paisId);

            modelBuilder.Entity<SPPais>()
                .HasMany(e => e.SPTipoCambio)
                .WithRequired(e => e.SPPais)
                .HasForeignKey(e => e.paisId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPPais>()
                .HasMany(e => e.SPUsuarioPais)
                .WithRequired(e => e.SPPais)
                .HasForeignKey(e => e.paisId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPPermiso>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPPermiso>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPPermiso>()
                .HasMany(e => e.SPUsuarioPermiso)
                .WithOptional(e => e.SPPermiso)
                .HasForeignKey(e => e.permisoId);

            modelBuilder.Entity<SPPresupuesto>()
                .Property(e => e.tipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPPresupuesto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPPresupuesto>()
                .HasMany(e => e.SPDetallePresupuesto)
                .WithRequired(e => e.SPPresupuesto)
                .HasForeignKey(e => e.presupuestoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPPresupuesto>()
                .HasMany(e => e.SPPresupuestoContable)
                .WithOptional(e => e.SPPresupuesto)
                .HasForeignKey(e => e.presupuestoId);

            modelBuilder.Entity<SPPresupuestoContable>()
                .Property(e => e.codigo_cuenta)
                .IsUnicode(false);

            modelBuilder.Entity<SPPresupuestoContable>()
                .Property(e => e.cuenta_contable)
                .IsUnicode(false);

            modelBuilder.Entity<SPProducto>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPProducto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPProducto>()
                .Property(e => e.tipo)
                .IsUnicode(false);

            modelBuilder.Entity<SPProducto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPProducto>()
                .Property(e => e.cultivoId)
                .IsFixedLength();

            modelBuilder.Entity<SPProducto>()
                .HasMany(e => e.SPProductoPresentacion)
                .WithRequired(e => e.SPProducto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPProductoPresentacion>()
                .Property(e => e.presentacionId)
                .IsFixedLength();

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.fincaId)
                .IsFixedLength();

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.codigo_nav)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.tipo_valor)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .HasMany(e => e.SPCCostoActividad)
                .WithRequired(e => e.SPSubCentroCosto)
                .HasForeignKey(e => e.fincaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .HasMany(e => e.SPDetallePresupuesto)
                .WithRequired(e => e.SPSubCentroCosto)
                .HasForeignKey(e => e.fincaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPSubCentroCosto>()
                .HasMany(e => e.SPPresupuestoContable)
                .WithOptional(e => e.SPSubCentroCosto)
                .HasForeignKey(e => e.subcentroId);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.cultivoId)
                .IsFixedLength();

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.variedadId)
                .IsFixedLength();

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.metros)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.ancho)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.altura)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.densidad)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.techado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.factor_manzana)
                .HasPrecision(11, 2);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPSubLote>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPSubLote>()
                .HasMany(e => e.SPDetallePresupuesto)
                .WithRequired(e => e.SPSubLote)
                .HasForeignKey(e => e.subloteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPTipoCambio>()
                .Property(e => e.tipo_cambio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SPTipoCambio>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoCultivo>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoCultivo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.codigo)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.clase)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<SPTipoPresupuesto>()
                .Property(e => e.estado)
                .IsFixedLength();

            modelBuilder.Entity<SPTipoPresupuesto>()
                .HasMany(e => e.SPPresupuesto)
                .WithRequired(e => e.SPTipoPresupuesto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPUnidadMedida>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<SPUnidadMedida>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SPUnidadMedida>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPUnidadMedida>()
                .HasMany(e => e.SPActividad)
                .WithRequired(e => e.SPUnidadMedida)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPUsuario>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<SPUsuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<SPUsuario>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<SPUsuario>()
                .Property(e => e.estado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SPUsuario>()
                .HasMany(e => e.SPUsuarioPais)
                .WithRequired(e => e.SPUsuario)
                .HasForeignKey(e => e.usuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPUsuario>()
                .HasMany(e => e.SPUsuarioPermiso)
                .WithOptional(e => e.SPUsuario)
                .HasForeignKey(e => e.usuarioId);

            modelBuilder.Entity<SPUsuario>()
                .HasMany(e => e.SPUsuarioPresupuesto)
                .WithOptional(e => e.SPUsuario)
                .HasForeignKey(e => e.usuarioId);

            modelBuilder.Entity<Variedades>()
                .Property(e => e.Codigo)
                .IsFixedLength();

            modelBuilder.Entity<Variedades>()
                .Property(e => e.Descripcion)
                .IsFixedLength();

            modelBuilder.Entity<Variedades>()
                .Property(e => e.GrupoCultivo)
                .IsFixedLength();

            modelBuilder.Entity<Variedades>()
                .HasMany(e => e.SPSubLote)
                .WithRequired(e => e.Variedades)
                .HasForeignKey(e => e.variedadId)
                .WillCascadeOnDelete(false);
        }
    }
}
