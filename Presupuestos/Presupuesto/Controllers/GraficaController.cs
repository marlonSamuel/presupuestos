using Model.BI;
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
    public class GraficaController : ApiController
    {
        private readonly Graficas grafica = new Graficas();

        [Route("graficas")]
        [HttpGet]
        public HttpResponseMessage GetPresupuestos()
        {
            try
            {
                var list = grafica.GetPresupuestos();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los graficas";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
