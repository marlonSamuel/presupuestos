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
    public class PContableController : ApiController
    {
        private readonly SPPresupuestoContable _pcontable = new SPPresupuestoContable();

        public HttpResponseMessage Get()
        {
            try
            {
                var list = _pcontable.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los presupuestos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var cargo = _pcontable.GetByPresupuesto(id);
                return Request.CreateResponse(HttpStatusCode.OK, cargo);
            }
            catch
            {
                var message = "No se ha podido obtener los presupuestos contables";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetByIdPF(int id, int fincaId)
        {
            try
            {
                var cargo = _pcontable.GetByPresupuestoFinca(id,fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, cargo);
            }
            catch
            {
                var message = "No se ha podido obtener los presupuestos contables";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(SPPresupuestoContable model)
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
                var rpta = _pcontable.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el presupuesto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
