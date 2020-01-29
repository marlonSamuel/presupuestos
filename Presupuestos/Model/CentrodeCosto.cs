namespace Model
{
    using Model.DTO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CentrodeCosto")]
    public partial class CentrodeCosto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CentrodeCosto()
        {
            SPLote = new HashSet<SPLote>();
            SPSubCentroCosto = new HashSet<SPSubCentroCosto>();
        }

        [Key]
        [StringLength(5)]
        public string codigo { get; set; }

        [StringLength(40)]
        public string descripcion { get; set; }

        [StringLength(8)]
        public string Correlativo { get; set; }

        [StringLength(10)]
        public string Region { get; set; }

        [StringLength(10)]
        public string CodigoNAV { get; set; }

        public int? paisId { get; set; }

        [StringLength(8)]
        public string CorrelativoTD { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public virtual SPPais SPPais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPLote> SPLote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPSubCentroCosto> SPSubCentroCosto { get; set; }

        public List<CentrodeCosto> GetAll(int paisId)
        {
            var list = new List<CentrodeCosto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    list = ctx.CentrodeCosto.Where(x=>x.paisId == paisId).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        public List<SPCCostoActividad> GetDetalle(int fincaId)
        {
            var list = new List<SPCCostoActividad>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPCCostoActividad.Where(x=>x.fincaId == fincaId).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        //obtener por id
        public CentrodeCosto GetById(string codigo)
        {
            var ccosto = new CentrodeCosto();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    ccosto = ctx.CentrodeCosto.Where(x => x.codigo == codigo).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return ccosto;
        }

        //metodo para guardar lote y detalle lote
        public string Save(CentroCostoDto model, string op)
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
                            var ccosto = new CentrodeCosto
                            {
                                codigo = model.codigo,
                                descripcion = model.descripcion,
                                Region = model.Region,
                                CorrelativoTD = model.CorrelativoTD,
                                Correlativo = model.Correlativo,
                                CodigoNAV = model.CodigoNAV,
                                paisId = model.paisId,
                                estado = "A"
                            };

                            if (op == "G") ctx.CentrodeCosto.Add(ccosto);
                            else ctx.Entry(ccosto).State = EntityState.Modified;
                            ctx.SaveChanges();

                            transaction.Commit();
                            rpta = "Centro de costo registado correctamente";
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



        //metodo para eliminar a nivel logico una actividad
        public bool Delete(string codigo)
        {
            var rpta = false;
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var ccosto = this.GetById(codigo);

                    if (ccosto != null)
                    {
                        ccosto.estado = "I";
                        ctx.Entry(ccosto).State = EntityState.Modified;
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
