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
    public class DepartamentoController : ApiController
    {
        private readonly SPDepartamento _departamento = new SPDepartamento();

        public HttpResponseMessage GetActividades()
        {
            try
            {
                var list = _departamento.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los departamento";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var departamento = _departamento.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, departamento);
            }
            catch
            {
                var message = "No se ha podido obtener el departamento";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad

        public HttpResponseMessage Post(SPDepartamento model)
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
                var rpta = _departamento.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el departamento";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
