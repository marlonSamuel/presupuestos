namespace Model
{
    using Helpers;
    using Model.DTO;
    using Model.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("SPUsuario")]
    public partial class SPUsuario : IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPUsuario()
        {
            SPUsuarioPais = new HashSet<SPUsuarioPais>();
            SPUsuarioPermiso = new HashSet<SPUsuarioPermiso>();
            SPUsuarioPresupuesto = new HashSet<SPUsuarioPresupuesto>();
        }

        public int Id { get; set; }

        [StringLength(25)]
        public string usuario { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(250)]
        public string password { get; set; }

        [StringLength(1)]
        public string estado { get; set; }

        #region Auditoria
        public int? CreadoPor { get; set; }
        public DateTime? CreadoFecha { get; set; }
        public int? ActualizadoPor { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
        #endregion


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUsuarioPais> SPUsuarioPais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUsuarioPermiso> SPUsuarioPermiso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPUsuarioPresupuesto> SPUsuarioPresupuesto { get; set; }

        public List<SPUsuario> GetAll()
        {
            var list = new List<SPUsuario>();
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    list = ctx.SPUsuario.ToList();
                }
            }catch(Exception)
            {

            }

            return list;
        }

        //get permisos por usuaios
        public List<UserInfo> GetPermisos(int usuarioId)
        {
            var list = new List<UserInfo>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from up in ctx.SPUsuarioPermiso
                            join p in ctx.SPPermiso on up.permisoId equals p.Id
                            where up.usuarioId == usuarioId
                            select new {
                                Id = up.Id,
                                usuarioId = up.usuarioId,
                                permisoId = up.permisoId,
                                codigo_permiso = p.codigo
                            }).Select(x => new UserInfo
                            {
                                Id = x.Id,
                                usuarioId = x.usuarioId,
                                permisoId = x.permisoId,
                                codigo_permiso = x.codigo_permiso
                            }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        //get paises por usuaios
        public List<UserPais> GetPaises(int usuarioId)
        {
            var list = new List<UserPais>();
            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    list = (from up in ctx.SPUsuarioPais
                            join p in ctx.SPPais on up.paisId equals p.Id
                            where up.usuarioId == usuarioId
                            select new
                            {
                                Id = up.Id,
                                usuarioId = up.usuarioId,
                                paisId = up.paisId,
                                pais = p.nombre
                            }).Select(x => new UserPais
                            {
                                Id = x.Id,
                                usuarioId = x.usuarioId,
                                paisId = x.paisId,
                                pais = x.pais
                            }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return list;
        }

        public ResponseModel Acceder(string user, string contraseña)
        {
            var rm = new ResponseModel();

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    contraseña = HashHelper.SHA256(contraseña);

                    var usuario = ctx.SPUsuario.Where(x => x.email == user || x.usuario == user)
                                               .Where(x => x.password == contraseña && x.estado == "A")
                                               .SingleOrDefault();

                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(usuario.Id.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Correo o contraseña incorrecta");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rm;
        }

        public SPUsuario Obtener(int id)
        {
            var usuario = new SPUsuario();

            try
            {
                using (var ctx = new PresupuestoContext())
                {
                    usuario = ctx.SPUsuario.Where(x => x.Id == id)
                                         .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return usuario;
        }

        public bool Save(CreateUsuarioDto model)
        {
            var rpta = false;
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    using(DbContextTransaction transacion = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            if(model.Id == 0) model.password = HashHelper.SHA256(model.password);

                            var usuario = new SPUsuario
                            {
                                Id = model.Id,
                                email = model.email,
                                usuario = model.usuario,
                                password = model.password,
                                estado = "A"
                            };

                            if (usuario.Id > 0) ctx.Entry(usuario).State = EntityState.Modified;
                            else ctx.Entry(usuario).State = EntityState.Added;
                            ctx.SaveChanges();

                            var usuarioId = usuario.Id;

                            //eliminamos los existentes para sustituirlos
                            var permisos = ctx.SPUsuarioPermiso.Where(x => x.usuarioId == usuarioId).ToList();
                            ctx.SPUsuarioPermiso.RemoveRange(permisos);
                            ctx.SaveChanges();

                            foreach (var u in model.permisos)
                            {
                                var usuario_permiso = new SPUsuarioPermiso
                                {
                                    usuarioId = usuarioId,
                                    permisoId = u
                                };

                                ctx.Entry(usuario_permiso).State = EntityState.Added;
                                ctx.SaveChanges();
                            }

                            //eliminamos los existentes de la tabla paises
                            var paises = ctx.SPUsuarioPais.Where(x => x.usuarioId == usuarioId).ToList();
                            ctx.SPUsuarioPais.RemoveRange(paises);
                            ctx.SaveChanges();

                            foreach (var p in model.paises)
                            {
                                var usuario_pais = new SPUsuarioPais
                                {
                                    usuarioId = usuarioId,
                                    paisId = p
                                };

                                ctx.Entry(usuario_pais).State = EntityState.Added;
                                ctx.SaveChanges();
                            }

                            //eliminamos los presupuestos asignados anteriormente
                            var presupuestos = ctx.SPUsuarioPresupuesto.Where(x => x.usuarioId == usuarioId).ToList();
                            ctx.SPUsuarioPresupuesto.RemoveRange(presupuestos);
                            ctx.SaveChanges();
                            if (model.presupuestos != null)
                            {
                                foreach(var p in model.presupuestos)
                                {
                                    var usuario_p = new SPUsuarioPresupuesto {
                                        usuarioId = usuarioId,
                                        tipoId = p
                                    };

                                    ctx.Entry(usuario_p).State = EntityState.Added;
                                    ctx.SaveChanges();
                                }
                            }

                            transacion.Commit();
                            rpta = true;
                        }catch(Exception e)
                        {
                            transacion.Rollback();
                            throw e;
                        }
                    }
                }
            }catch(Exception e)
            {
                throw e;
            }
            return rpta;
        }

        public bool Delete(int id)
        {
            var rpta = false;
            try
            {
                using(var ctx = new PresupuestoContext())
                {
                    var usuario = this.Obtener(id);

                    if(usuario != null)
                    {
                        usuario.estado = "I";
                        ctx.Entry(usuario).State = EntityState.Modified;
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
