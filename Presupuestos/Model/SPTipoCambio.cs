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

    [Table("SPTipoCambio")]
    public partial class SPTipoCambio : IAuditable
    {
        public int Id { get; set; }

        public int paisId { get; set; }
        

        public DateTime? fecha { get; set; }

        public decimal? tipo_cambio { get; set; }
        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        public virtual SPPais SPPais { get; set; }

        public List<ListTipoCambioDto> GetAll()
        {
            var list = new List<ListTipoCambioDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from t in ctx.SPTipoCambio
                            join p in ctx.SPPais on t.paisId equals p.Id
                            join m in ctx.SPMoneda on p.monedaId equals m.Id
                            select new ListTipoCambioDto
                            {
                                Id = t.Id,
                                paisId = t.paisId,
                                pais = p.nombre,
                                moneda = m.nombre,
                                fecha = t.fecha,
                                tipo_cambio = t.tipo_cambio
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
        public SPTipoCambio GetById(int id)
        {
            var tipo = new SPTipoCambio();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    tipo = ctx.SPTipoCambio.Where(x => x.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return tipo;
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
                    rpta = "Tipo cambio registrada con exito";
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
                    var tipo = this.GetById(id);

                    if (tipo != null)
                    {
                        tipo.estado = "I";
                        ctx.Entry(tipo).State = EntityState.Modified;
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
