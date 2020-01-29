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
    public class UnidadController : ApiController
    {
        private readonly SPUnidadMedida _unidad = new SPUnidadMedida();

        public HttpResponseMessage GetActividades()
        {
            try
            {
                var list = _unidad.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las unidades de medida";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var actividad = _unidad.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, actividad);
            }
            catch
            {
                var message = "No se ha podido obtener la unidad de medida";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad

        public HttpResponseMessage Post(SPUnidadMedida model)
        {
            try
            {
                var rpta = model.Save();

                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //eliminar a nivel logico

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var rpta = _unidad.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar la unidad de medida";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
