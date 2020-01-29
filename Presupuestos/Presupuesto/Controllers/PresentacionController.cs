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
    public class PresentacionController : ApiController
    {
        private readonly Presentacion _presentacion = new Presentacion();

        public HttpResponseMessage GetActividades()
        {
            try
            {
                var list = _presentacion.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las presentaciones";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
