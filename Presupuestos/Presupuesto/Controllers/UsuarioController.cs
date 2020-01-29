using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Presupuesto.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class UsuarioController : ApiController
    {
        private readonly SPUsuario _usuario = new SPUsuario();
        // GET api/values

        public HttpResponseMessage Get()
        {
            try
            {
                var list = _usuario.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "error al obtener los usuarios";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetPermisos(int usuarioId)
        {
            try
            {
                var list = _usuario.GetPermisos(usuarioId);

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetPaises(int userId)
        {
            try
            {
                var list = _usuario.GetPaises(userId);

                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
        

        //api guardar actividad
        public HttpResponseMessage Post(CreateUsuarioDto model)
        {
            try
            {
                var rpta = _usuario.Save(model);

                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
        //eliminar a nivel logico
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var rpta = _usuario.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido desactivar el usuario";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
