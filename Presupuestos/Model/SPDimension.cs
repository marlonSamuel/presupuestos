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

    [Table("SPDimension")]
    public partial class SPDimension : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPDimension()
        {
            SPCostoByDimension = new HashSet<SPCostoByDimension>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string cuenta_contable { get; set; }

        [StringLength(150)]
        public string nombre_cuenta { get; set; }

        [StringLength(150)]
        public string nombre { get; set; }

        public double? factor { get; set; }

        public int grupoId { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPCostoByDimension> SPCostoByDimension { get; set; }

        public virtual SPGrupoDimension SPGrupoDimension { get; set; }

        public List<ListDimensionesDto> GetAll()
        {
            var list = new List<ListDimensionesDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    //list = ctx.SPDimension.Where(x => x.estado == "A").ToList();

                    list = (from d in ctx.SPDimension
                            join g in ctx.SPGrupoDimension on d.grupoId equals g.Id
                            where d.estado == "A"
                            select new ListDimensionesDto
                            {
                                Id = d.Id,
                                grupoId = d.grupoId,
                                factor = d.factor,
                                estado = d.estado,
                                nombre = d.nombre,
                                nombre_cuenta = d.cuenta_contable + "-"+d.nombre_cuenta,
                                agrupador = g.nombre,
                                cuenta_contable = d.cuenta_contable
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPDimension GetById(int id)
        {
            var dimension = new SPDimension();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    dimension = ctx.SPDimension.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return dimension;
        }

        public List<ListDimensionActividad> GetDimensionesbyActividad(string actividad)
        {
            var list = new List<ListDimensionActividad>();

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = (from a in ctx.SPActividad
                            join g in ctx.SPGrupoDimension on a.grupoId equals g.Id
                            join d in ctx.SPDimension on g.Id equals d.grupoId
                            where a.codigo == actividad
                            select new ListDimensionActividad
                            {
                                dimensionId = d.Id,
                                factor = d.factor,
                                grupoId = g.Id
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //metodo para guardar actividads
        public string Save()
        {
            string rpta = "";
            this.estado = "A";

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Dimension registrada con exito";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return rpta;
        }

        //metodo para eliminar a nivel logico una actividad
        public bool Delete(int id)
        {
            var rpta = false;
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var dimension = this.GetById(id);

                    if (dimension != null)
                    {
                        dimension.estado = "I";
                        ctx.Entry(dimension).State = EntityState.Modified;
                        ctx.SaveChanges();
                        rpta = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            return rpta;
        }
    }
}
