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
    public class LoteController : ApiController
    {
        private readonly SPLote _lote = new SPLote();

        public HttpResponseMessage GetLotes(int paisId)
        {
            
            try
            {
                var list = _lote.GetAll(paisId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las actividades";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetByFinca(string fincaId)
        {

            try
            {
                var list = _lote.GetByFinca(fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las actividades";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        //public HttpResponseMessage GetByCultivo(int loteId, string cultivoId)
        //{
        //    try
        //    {
        //        var list = _detalleLote.GetByCultivo(loteId,cultivoId);
        //        return Request.CreateResponse(HttpStatusCode.OK, list);
        //    }
        //    catch
        //    {
        //        var message = "No se ha podido obtener los cultivos";
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        //    }

        //}

        public HttpResponseMessage Delete(int loteId, string estado)
        {
            try
            {
                var rpta = _lote.ActivarODesactivarLote(loteId, estado);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido completar la operacion";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        //api guardar lote
   
        public HttpResponseMessage Post(SPLote model)
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
    }
}
