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
    public class CultivoController : ApiController
    {
        private readonly Cultivos _cultivo = new Cultivos();
        // GET api/values
        public HttpResponseMessage GetCultivos()
        {
            try
            {
                var list = _cultivo.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetActivos(string activos)
        {
            try
            {
                var list = _cultivo.GetActivos();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
