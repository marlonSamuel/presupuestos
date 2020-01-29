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

    [Table("SPActividad")]
    public partial class SPActividad : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPActividad()
        {
            SPCCostoActividad = new HashSet<SPCCostoActividad>();
        }

        public int Id { get; set; }
      
        public int grupoId { get; set; }

        [StringLength(10)]
        public string codigo { get; set; }

        [Required]
        [StringLength(250)]
        public string nombre { get; set; }

        public decimal? costo { get; set; }

        public int unidadId { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [StringLength(1)]
        public string tipo { get; set; }

        public virtual SPGrupoDimension SPGrupoDimension { get; set; }

        public virtual SPUnidadMedida SPUnidadMedida { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPCCostoActividad> SPCCostoActividad { get; set; }

        //listar actividades
        public List<ListActividadDto> GetAll()
        {
            var list = new List<ListActividadDto>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = (from a in ctx.SPActividad
                            join u in ctx.SPUnidadMedida on a.unidadId equals u.unidadId
                            join g in ctx.SPGrupoDimension on a.grupoId equals g.Id
                            where a.estado == "A"
                            select new {
                                Id = a.Id,
                                nombre = a.nombre,
                                estado = a.estado,
                                unidadId = a.unidadId,
                                costo = a.costo,
                                codigo = a.codigo,
                                unidadMedida = u.nombre,
                                grupoId = a.grupoId,
                                grupo = g.nombre
                            }).Select(x => new ListActividadDto{
                                Id = x.Id,
                                nombre = x.nombre,
                                estado = x.estado,
                                unidadId = x.unidadId,
                                costo = x.costo,
                                codigo = x.codigo,
                                unidadMedida = x.unidadMedida,
                                grupoId = x.grupoId,
                                grupo = x.grupo
                            }).ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<ListActividadFincaDto> GetByFinca(int fincaId, string tipo)
        {
            var list = new List<ListActividadFincaDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from ca in ctx.SPCCostoActividad
                            join a in ctx.SPActividad on ca.actividadId equals a.Id
                            join u in ctx.SPUnidadMedida on a.unidadId equals u.unidadId
                            where ca.fincaId == fincaId && a.tipo == tipo
                            select new
                            {
                                detalleID = ca.detalleId,
                                fincaId = ca.fincaId,
                                actividadId = ca.actividadId,
                                nombre = a.nombre,
                                costo = a.costo,
                                unidad_media = u.nombre,
                                codigo = a.codigo
                            }).Select(x => new ListActividadFincaDto
                            {
                                detalleId = x.detalleID,
                                fincaId = x.fincaId,
                                actividadId = x.actividadId,
                                nombre = x.nombre,
                                costo = x.costo,
                                unidad_medida = x.unidad_media,
                                codigo = x.codigo
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
        public SPActividad GetById(int id)
        {
            var actividad = new SPActividad();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    actividad = ctx.SPActividad.Where(x => x.Id == id).SingleOrDefault();
                }
            }catch(Exception e)
            {
                throw e;
            }
            return actividad;
        }

        //metodo para guardar actividads
        public string Save()
        {
            string rpta = "";
            this.estado = "A";

            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    if (this.Id > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Actividad registrada con exito";
                }
            }catch(Exception e)
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
              using(var ctx = new PresupuestoContext())
                {
                    var actividad = this.GetById(id);

                    if(actividad != null)
                    {
                        ctx.Entry(actividad).State = EntityState.Modified;
                        ctx.SaveChanges();
                        rpta = true;
                    }
                }   
            }catch(Exception e)
            {
                throw e;
            }


            return rpta;
        }
    }
}
