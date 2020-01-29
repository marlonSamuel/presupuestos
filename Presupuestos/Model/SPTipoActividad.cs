namespace Model
{
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPTipoActividad")]
    public partial class SPTipoActividad : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]


        public int Id { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(250)]
        public string descripcion { get; set; }

        public int actividad_principal { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]


        public List<SPTipoActividad> GetAll()
        {
            var list = new List<SPTipoActividad>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPTipoActividad.Where(x => x.estado == "A" & x.actividad_principal > 0).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public List<SPTipoActividad> GetPrincipales()
        {
            var list = new List<SPTipoActividad>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPTipoActividad.Where(x => x.estado == "A" && x.actividad_principal == 0).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPTipoActividad GetById(int id)
        {
            var tipo = new SPTipoActividad();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    tipo = ctx.SPTipoActividad.Where(x => x.Id == id).SingleOrDefault();
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
                    rpta = "Tipo de actividad registrada con exito";
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
