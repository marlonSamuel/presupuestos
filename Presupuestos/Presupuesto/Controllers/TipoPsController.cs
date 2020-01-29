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
    public class TipoPsController : ApiController
    {
        private readonly SPTipoPresupuesto _tipoP = new SPTipoPresupuesto();

        // GET api/values
        public HttpResponseMessage GetAll()
        {
            try
            {
                var list = _tipoP.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetByUser(int userId)
        {
            try
            {
                var list = _tipoP.GetByUser(userId);
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
