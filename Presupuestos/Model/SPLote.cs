namespace Model
{
    using Model.DTO;
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPLote")]
    public partial class SPLote : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPLote()
        {
            SPPresupuestoContable = new HashSet<SPPresupuestoContable>();
            SPSubLote = new HashSet<SPSubLote>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string codigo { get; set; }

        [Required]
        [StringLength(25)]
        public string nombre { get; set; }

        [Required]
        [StringLength(5)]
        public string fincaId { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion
        
        public virtual CentrodeCosto CentrodeCosto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPSubLote> SPSubLote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPresupuestoContable> SPPresupuestoContable { get; set; }

        //metodo para listar lotes
        public List<ListLoteDto> GetAll(int paisId)
        {
            var list = new List<ListLoteDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from l in ctx.SPLote
                            join cc in ctx.CentrodeCosto on l.fincaId equals cc.codigo
                            where cc.paisId == paisId
                            select new {
                                Id = l.Id,
                                codigo = l.codigo,
                                fincaId = l.fincaId,
                                nombre = l.nombre,
                                centroCosto = cc.descripcion,
                                estado = l.estado,
                                descripcion = l.descripcion
                            }).Select(x => new ListLoteDto
                            {
                                Id = x.Id,
                                codigo = x.codigo,
                                fincaId = x.fincaId,
                                nombre = x.nombre,
                                centroCosto = x.centroCosto,
                                estado = x.estado,
                                descripcion = x.descripcion
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<SPLote> GetByFinca(string fincaId)
        {
            var list = new List<SPLote>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.SPLote.Where(x => x.fincaId == fincaId).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para guardar lote y detalle lote
        public string Save()
        {
            var rpta = "";
            try
            {
                using (var ctx = new PresupuestoContext())
                {

                    using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            this.estado = "A";
                            if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                            else ctx.SPLote.Add(this);
                            ctx.SaveChanges();

                            transaction.Commit();
                            rpta = "Lote registado correctamente";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return rpta;
        }

        public string ActivarODesactivarLote(int loteId, string estado)
        {
            var rpta = "";
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    var lote = ctx.SPLote.Where(x => x.Id == loteId).SingleOrDefault();
                    lote.estado = estado;
                    ctx.Entry(lote).State = EntityState.Modified;
                    ctx.SaveChanges();

                    if (estado == "A") rpta = "Lote ah sido activado";
                    else rpta = "Lote ha sido desactivado";
                }
            }
            catch
            {
                throw;
            }

            return rpta;
        }
    }
}
