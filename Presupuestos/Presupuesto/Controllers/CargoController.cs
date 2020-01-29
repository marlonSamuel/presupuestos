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
    public class CargoController : ApiController
    {
        private readonly SPCargo _cargo = new SPCargo();

        public HttpResponseMessage Get()
        {
            try
            {
                var list = _cargo.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los cargos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var cargo = _cargo.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, cargo);
            }
            catch
            {
                var message = "No se ha podido obtener la categoria";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(SPCargo model)
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
                var rpta = _cargo.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el cargo";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
