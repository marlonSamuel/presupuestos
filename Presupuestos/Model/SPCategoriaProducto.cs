namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPCategoriaProducto")]
    public partial class SPCategoriaProducto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPCategoriaProducto()
        {
            SPProducto = new HashSet<SPProducto>();
        }

        [Key]
        public int categoriaId { get; set; }

        public int? codigo { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(500)]
        public string descripcion { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPProducto> SPProducto { get; set; }

        public List<SPCategoriaProducto> GetAll()
        {
            var list = new List<SPCategoriaProducto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPCategoriaProducto.Where(x => x.estado == "A").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        //obtener por id
        public SPCategoriaProducto GetById(int id)
        {
            var categoria = new SPCategoriaProducto();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    categoria = ctx.SPCategoriaProducto.Where(x => x.categoriaId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return categoria;
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
                    if (this.categoriaId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rpta = "Categoria registrada con exito";
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
                    var categoria = this.GetById(id);

                    if (categoria != null)
                    {
                        categoria.estado = "I";
                        ctx.Entry(categoria).State = EntityState.Modified;
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
