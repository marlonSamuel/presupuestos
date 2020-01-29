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
    public class ValidationController : ApiController
    {
        private readonly SPDetallePresupuesto _presupuesto = new SPDetallePresupuesto();



        public HttpResponseMessage GetValidation(int presupuetoId, int fincaId)
        {
            try
            {
                var rpta = _presupuesto.IsFinished(presupuetoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido obtener las presupuestos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
