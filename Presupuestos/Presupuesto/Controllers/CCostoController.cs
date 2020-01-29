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
    public class CCostoController : ApiController
    {
        private readonly CentrodeCosto CCosto = new CentrodeCosto();
       
        public HttpResponseMessage Get(int paisId)
        {
            try
            {
                var list = CCosto.GetAll(paisId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception)
            {
                var message = "algo ah fallado";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage GetDetalle(int codigo)
        {
            try
            {
                var ccosto = CCosto.GetDetalle(codigo);
                return Request.CreateResponse(HttpStatusCode.OK, ccosto);
            }
            catch
            {
                var message = "No se ha podido obtener el centro de costo";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(CentroCostoDto model, string op)
        {
            try
            {
                var rpta = CCosto.Save(model,op);

                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //eliminar a nivel logico
        public HttpResponseMessage Delete(string codigo)
        {
            try
            {
                var rpta = CCosto.Delete(codigo);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el codigo";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
