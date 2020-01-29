using Model;
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
    public class PermisoController : ApiController
    {
        private readonly SPPermiso _permiso = new SPPermiso();

        public HttpResponseMessage GetPermisos()
        {
            try
            {
                var list = _permiso.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los persmisos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
