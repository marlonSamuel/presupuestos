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

    [Table("SPProducto")]
    public partial class SPProducto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPProducto()
        {
            SPProductoPresentacion = new HashSet<SPProductoPresentacion>();
        }

        [Key]
        public int productoId { get; set; }

        [Required]
        [StringLength(50)]
        public string codigo { get; set; }

        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(10)]
        public string tipo { get; set; }

        public int categoriaId { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        [Required]
        [StringLength(5)]
        public string cultivoId { get; set; }

        public virtual Gruposcultivos Gruposcultivos { get; set; }

        public virtual SPCategoriaProducto SPCategoriaProducto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPProductoPresentacion> SPProductoPresentacion { get; set; }

        //metodo para listar lotes
        public List<ProductoDto> GetAll(int categoriaId)
        {
            var list = new List<ProductoDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from p in ctx.SPProducto
                            join c in ctx.SPCategoriaProducto on p.categoriaId equals c.categoriaId
                            join g in ctx.Gruposcultivos on p.cultivoId equals g.Codigo
                            where p.estado == "A" && p.categoriaId == categoriaId
                            select new
                            {
                                productoId = p.productoId,
                                nombre = p.nombre,
                                codigo = p.codigo,
                                tipo = p.tipo,
                                categoriaId = p.categoriaId,
                                cultivoId = p.cultivoId,
                                cultivo = g.Descripcion,
                                categoria = c.nombre
                                
                            }).Select(x => new ProductoDto
                            {
                                productoId = x.productoId,
                                nombre = x.nombre,
                                codigo = x.codigo,
                                tipo = x.tipo,
                                categoriaId = x.categoriaId,
                                cultivoId = x.cultivoId,
                                cultivo = x.cultivo,
                                categoria = x.categoria
                            }).ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }

        public SPProducto GetById(int id)
        {
            var producto = new SPProducto();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    producto = ctx.SPProducto.Where(x => x.productoId == id).SingleOrDefault();
                }
            }
            catch
            {

            }

            return producto;
        }


        public List<SPProductoPresentacion> GetDetalles(int id)
        {
            var list = new List<SPProductoPresentacion>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = ctx.SPProductoPresentacion.Where(x => x.productoId == id).ToList();
                }
            }
            catch
            {

            }
            return list;
        }

        //metodo para guardar lote y detalle lote
        public string Save(ProductoDto model)
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
                            var producto = new SPProducto
                            {
                                productoId = model.productoId,
                                codigo = model.codigo,
                                nombre = model.nombre,
                                categoriaId = model.categoriaId,
                                cultivoId = model.cultivoId,
                                tipo = model.tipo,
                                estado = "A",
                            };

                            if (producto.productoId > 0) ctx.Entry(producto).State = EntityState.Modified;
                            else ctx.SPProducto.Add(producto);
                            ctx.SaveChanges();

                            //obtenemos el ultimo id insertado
                            var productoId = producto.productoId;

                            //eliminamos los existentes para sustituirlos
                            var detalles = ctx.SPProductoPresentacion.Where(x => x.productoId == productoId && x.productoId == model.productoId).ToList();
                            ctx.SPProductoPresentacion.RemoveRange(detalles);
                            ctx.SaveChanges();

                            if(model.detalle != null)
                            {
                                foreach (var d in model.detalle)
                                {
                                    var detalle = new SPProductoPresentacion
                                    {
                                        Id = d.Id,
                                        productoId = productoId,
                                        presentacionId = d.presentacionId,
                                    };


                                    ctx.SPProductoPresentacion.Add(detalle);
                                    ctx.SaveChanges();
                                }
                            }
                          
                            transaction.Commit();
                            rpta = "producto registado correctamente";
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

        public string Delete(int productoId)
        {
            var rpta = "";
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    var producto = ctx.SPProducto.Where(x => x.productoId == productoId).SingleOrDefault();
                    producto.estado = estado;
                    ctx.Entry(producto).State = EntityState.Modified;
                    ctx.SaveChanges();
                    rpta = "producto ha sido removido";
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
