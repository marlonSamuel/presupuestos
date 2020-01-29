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
    public class SubCCostoController : ApiController
    {
        private readonly SPSubCentroCosto CCosto = new SPSubCentroCosto();

        public HttpResponseMessage GetAll()
        {
            try
            {
                var list = CCosto.GetAllSubCentros();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage Get(string codigo)
        {
            try
            {
                var list = CCosto.GetAll(codigo);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetDetalle(int subcentroId)
        {
            try
            {
                var ccosto = CCosto.GetDetalle(subcentroId);
                return Request.CreateResponse(HttpStatusCode.OK, ccosto);
            }
            catch
            {
                var message = "No se ha podido obtener el centro de costo";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(CreateSubcentroDto model)
        {
            try
            {
                var rpta = CCosto.Save(model);

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
                var rpta = CCosto.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el sub centro de costo";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
