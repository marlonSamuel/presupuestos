namespace Model
{
    using Model.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using Model.Shared;

    [Table("SPSubLote")]
    public partial class SPSubLote : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPSubLote()
        {
            SPDetallePresupuesto = new HashSet<SPDetallePresupuesto>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string codigo { get; set; }

        public int loteId { get; set; }

        [Required]
        [StringLength(5)]
        public string cultivoId { get; set; }

        [Required]
        [StringLength(6)]
        public string variedadId { get; set; }

        [StringLength(25)]
        public string nombre { get; set; }

        public decimal? metros { get; set; }

        public decimal? ancho { get; set; }

        public decimal? altura { get; set; }

        public decimal? densidad { get; set; }

        [StringLength(1)]
        public string techado { get; set; }

        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public decimal? factor_manzana { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(20)]
        public string region { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        public virtual Gruposcultivos Gruposcultivos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPDetallePresupuesto> SPDetallePresupuesto { get; set; }

        public virtual SPLote SPLote { get; set; }

        public virtual Variedades Variedades { get; set; }

        //obtener todos los sublotes
        public List<SPSubLote> GetAllSublotes()
        {
            var list = new List<SPSubLote>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.SPSubLote.Where(x => x.estado == "A").ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para listar lotes
        public List<ListLoteDto> GetAll(int loteId)
        {
            var list = new List<ListLoteDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from sl in ctx.SPSubLote
                            join l in ctx.SPLote on sl.loteId equals l.Id
                            where sl.loteId == loteId
                            select new
                            {
                                Id = sl.Id,
                                codigo = sl.codigo,
                                fincaId = l.fincaId,
                                loteId = l.Id,
                                lote = l.nombre,
                                techado = sl.techado,
                                fecha_inicio = sl.fecha_inicio,
                                fecha_fin = sl.fecha_fin,
                                metros = sl.metros,
                                altura = sl.altura,
                                ancho = sl.ancho,
                                factor_manzana = sl.factor_manzana,
                                densidad = sl.densidad,
                                nombre = sl.nombre,
                                descripcion = sl.descripcion,
                                estado = sl.estado,
                            }).Select(x => new ListLoteDto
                            {
                                Id = x.Id,
                                codigo = x.codigo,
                                fincaId = x.fincaId,
                                loteId = x.loteId,
                                lote = x.lote,
                                techado = x.techado,
                                fecha_inicio = x.fecha_inicio,
                                fecha_fin = x.fecha_fin,
                                metros = x.metros,
                                altura = x.altura,
                                ancho = x.ancho,
                                factor_manzana = x.factor_manzana,
                                densidad = x.densidad,
                                nombre = x.nombre,
                                descripcion = x.descripcion,
                                estado = x.estado
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListLoteDto> GetByFinca(string fincaId)
        {
            var list = new List<ListLoteDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from sl in ctx.SPSubLote
                            join l in ctx.SPLote on sl.loteId equals l.Id
                            join g in ctx.Gruposcultivos on sl.cultivoId equals g.Codigo
                            join v in ctx.Variedades on sl.variedadId equals v.Codigo
                            where l.fincaId == fincaId
                            select new ListLoteDto
                            {
                                Id = sl.Id,
                                nombre = sl.nombre,
                                lote = l.nombre,
                                loteId = l.Id,
                                cultivo = g.Descripcion.Trim() +" "+ v.Descripcion.Trim(),
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para guardar lote y detalle lote
        public string Save(CreateSubLoteDto model)
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
                            var sublote = new SPSubLote
                            {
                                Id = model.Id,
                                codigo = model.codigo,
                                nombre = model.nombre,
                                loteId = model.loteId,
                                cultivoId = model.cultivoId,
                                variedadId = model.variedadId,
                                metros = model.metros,
                                altura = model.altura,
                                ancho = model.ancho,
                                densidad = model.densidad,
                                factor_manzana = model.factor_manzana,
                                techado = model.techado,
                                fecha_inicio = model.fecha_inicio,
                                fecha_fin = model.fecha_fin,
                                estado = "A"

                            };
                            if (sublote.Id > 0) ctx.Entry(sublote).State = EntityState.Modified;
                            else ctx.SPSubLote.Add(sublote);
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
                using (var ctx = new PresupuestoContext())
                {
                    var lote = ctx.SPSubLote.Where(x => x.Id == loteId).SingleOrDefault();
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
