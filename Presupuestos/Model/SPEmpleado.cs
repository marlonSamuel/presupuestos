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

    [Table("SPEmpleado")]
    public partial class SPEmpleado
    {
        [Key]
        public int empleadoId { get; set; }

        [Required]
        [StringLength(25)]
        public string codigo { get; set; }

        [Required]
        [StringLength(5)]
        public string fincaId { get; set; }

        public int tipoId { get; set; }

        public int departamentoId { get; set; }

        public int cargoId { get; set; }

        [Required]
        [StringLength(25)]
        public string primer_nombre { get; set; }

        [StringLength(25)]
        public string segundo_nombre { get; set; }

        [Required]
        [StringLength(25)]
        public string primer_apellido { get; set; }

        [StringLength(25)]
        public string segundo_apellido { get; set; }

        [StringLength(25)]
        public string dpi { get; set; }

        [StringLength(25)]
        public string telefono { get; set; }

        public decimal? salario { get; set; }

        [StringLength(250)]
        public string foto { get; set; }

        public int? tipoPila { get; set; }

        public int? codPila { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        public virtual CentrodeCosto CentrodeCosto { get; set; }

        public virtual SPCargo SPCargo { get; set; }

        public virtual SPDepartamento SPDepartamento { get; set; }
       

        public virtual SPTipoEmpleado SPTipoEmpleado { get; set; }


        //metodo para listar empleados
        public List<EmpleadoDto> GetAll()
        {
            var list = new List<EmpleadoDto>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from e in ctx.SPEmpleado
                            join cc in ctx.CentrodeCosto on e.fincaId equals cc.codigo
                            join t in ctx.SPTipoEmpleado on e.tipoId equals t.tipoId
                            join d in ctx.SPDepartamento on e.departamentoId equals d.departamentoId
                            join c in ctx.SPCargo on e.cargoId equals c.cargoId
                            where e.estado == "A"
                            select new
                            {
                                empleadoId = e.empleadoId,
                                codigo = e.codigo,
                                primer_nombre = e.primer_nombre,
                                segundo_nombre = e.segundo_nombre,
                                primer_apellido = e.primer_nombre,
                                segundo_apellido = e.segundo_apellido,
                                tipoId = e.tipoId,
                                tipo = t.nombre,
                                dpi = e.dpi,
                                fincaId = e.fincaId,
                                centro_costo = cc.descripcion,
                                cargoId = c.cargoId,
                                cargo = c.nombre,
                                departamentoId = d.departamentoId,
                                departamento = d.nombre,
                                salario = e.salario,
                                foto = e.foto,
                                tipo_pila = e.tipoPila,
                                cod_pila = e.codPila,
                                telefono = e.telefono
                            }).Select(x => new EmpleadoDto
                            {
                                empleadoId = x.empleadoId,
                                primer_nombre = x.primer_nombre,
                                segundo_nombre = x.segundo_nombre,
                                primer_apellido = x.primer_apellido,
                                segundo_apellido = x.segundo_apellido,
                                tipoId = x.tipoId,
                                tipo = x.tipo,
                                dpi = x.dpi,
                                fincaId = x.fincaId,
                                centro_costo = x.centro_costo,
                                cargoId = x.cargoId,
                                cargo = x.cargo,
                                departamentoId = x.departamentoId,
                                departamento = x.departamento,
                                salario = x.salario,
                                foto = x.foto,
                                tipoPila = x.tipo_pila,
                                codPila = x.cod_pila,
                                codigo = x.codigo,
                                telefono = x.telefono
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
        public SPEmpleado GetById(int id)
        {
            var empleado = new SPEmpleado();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    empleado = ctx.SPEmpleado.Where(x => x.empleadoId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return empleado;
        }

        //guardar un empleado
        public string Save()
        {
            var rpta = "";
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    this.estado = "A";
                    if (this.empleadoId > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.SPEmpleado.Add(this);
                    ctx.SaveChanges();
                }
            }
            catch
            {

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
                    var empleado = this.GetById(id);

                    if (empleado != null)
                    {
                        empleado.estado = "I";
                        ctx.Entry(empleado).State = EntityState.Modified;
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
