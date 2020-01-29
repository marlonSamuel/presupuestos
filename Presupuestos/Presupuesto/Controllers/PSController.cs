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
    public class PSController : ApiController
    {
        private readonly SPPresupuesto _presupuesto = new SPPresupuesto();

        public HttpResponseMessage Get(int tipoId, string opcion, int paisId)
        {
            try
            {
                var list = _presupuesto.GetAll(tipoId, opcion, paisId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los presupeustos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        //public HttpResponseMessage GetById(int id)
        //{
        //    try
        //    {
        //        var categoria = _presupuesto.GetById(id);
        //        return Request.CreateResponse(HttpStatusCode.OK, categoria);
        //    }
        //    catch
        //    {
        //        var message = "No se ha podido obtener la categoria";
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        //    }
        //}

        //api guardar actividad
        public HttpResponseMessage Post(SPPresupuesto model)
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
        public HttpResponseMessage Delete(int id, string estado)
        {
            try
            {
                var rpta = _presupuesto.Delete(id, estado);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha anular el presupuesto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
