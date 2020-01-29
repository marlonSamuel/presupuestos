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
    public class ActividadController : ApiController
    {
        private readonly SPActividad _actividad = new SPActividad();

        public HttpResponseMessage GetActividades()
        {
            try
            {
                var list = _actividad.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las actividades";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var actividad = _actividad.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, actividad);
            }
            catch
            {
                var message = "No se ha podido obtener la actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetByFinca(int fincaId, string tipo)
        {
            try
            {
                var actividad = _actividad.GetByFinca(fincaId, tipo);
                return Request.CreateResponse(HttpStatusCode.OK, actividad);
            }
            catch
            {
                var message = "No se ha podido obtener la actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad

        public HttpResponseMessage Post(SPActividad model)
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
                var rpta = _actividad.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar la actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }


    }
}
