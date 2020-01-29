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
    public class MonedaController : ApiController
    {
        private readonly SPMoneda _moneda = new SPMoneda();

        public HttpResponseMessage GetAll()
        {
            try
            {
                var list = _moneda.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las monedas";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var categoria = _moneda.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, categoria);
            }
            catch
            {
                var message = "No se ha podido obtener la moneda";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(SPMoneda model)
        {
            try
            {
                var rpta = model.Save();

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
                var rpta = _moneda.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar la moneda";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
