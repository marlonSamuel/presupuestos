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
    public class VariedadController : ApiController
    {
        private readonly Variedades _variedad = new Variedades();
        // GET api/values
        public HttpResponseMessage Get()
        {
            try
            {
                var list = _variedad.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetByGrupo(string grupo)
        {
            try
            {
                var list = _variedad.GetByCultivo(grupo);
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
