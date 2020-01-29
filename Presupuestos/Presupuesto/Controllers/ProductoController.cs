using Model;
using Model.DTO;
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
    public class ProductoController : ApiController
    {
        private readonly SPProducto _producto = new SPProducto();
        public HttpResponseMessage Get(int categoriaId)
        {
            try
            {
                var list = _producto.GetAll(categoriaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener producto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
        public HttpResponseMessage GetDetalle(int id, bool detalle)
        {
            try
            {
                var categoria = _producto.GetDetalles(id);
                return Request.CreateResponse(HttpStatusCode.OK, categoria);
            }
            catch
            {
                var message = "No se ha podido obtener el prdoucto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
        //api guardar actividad
        public HttpResponseMessage Post(ProductoDto model)
        {
            try
            {
                var rpta = _producto.Save(model);

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
                var rpta = _producto.Delete(id);
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
