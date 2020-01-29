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

    public partial class SPPais: IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPPais()
        {
            CentrodeCosto = new HashSet<CentrodeCosto>();
            SPPresupuesto = new HashSet<SPPresupuesto>();
            SPTipoCambio = new HashSet<SPTipoCambio>();
            SPUsuarioPais = new HashSet<SPUsuarioPais>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string nombre { get; set; }

        public int monedaId { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentrodeCosto> CentrodeCosto { get; set; }

        public virtual SPMoneda SPMoneda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPPresupuesto> SPPresupuesto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPTipoCambio> SPTipoCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUsuarioPais> SPUsuarioPais { get; set; }

        public List<ListPaisDto> GetAll()
        {
            var list = new List<ListPaisDto>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = (from p in ctx.SPPais
                            join m in ctx.SPMoneda on p.monedaId equals m.Id
                            where p.estado == "A"
                            select new ListPaisDto {
                                Id = p.Id,
                                nombre = p.nombre,
                                monedaId = p.monedaId,
                                moneda = m.nombre,
                                simbolo_moneda = m.simbolo
                            }).ToList();
                }
            }catch(Exception e)
            {
                throw e;
            }
            return list;
        }

        //obtener por id
        public ListPaisDto GetById(int id)
        {
            var pais = new ListPaisDto();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    pais = (from p in ctx.SPPais
                            join m in ctx.SPMoneda on p.monedaId equals m.Id
                            where p.Id == id
                            select new ListPaisDto
                            {
                                Id = p.Id,
                                nombre = p.nombre,
                                monedaId = p.monedaId,
                                moneda = m.nombre,
                                simbolo_moneda = m.simbolo
                            }).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return pais;
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
                    rpta = "Pais registrada con exito";
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
                    var pais = this.GetById(id);

                    if (pais != null)
                    {
                        pais.estado = "I";
                        ctx.Entry(pais).State = EntityState.Modified;
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
