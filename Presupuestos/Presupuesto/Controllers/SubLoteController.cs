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
    public class SubLoteController : ApiController
    {
        private readonly SPSubLote _sublote = new SPSubLote();

        public HttpResponseMessage GetAll()
        {

            try
            {
                var list = _sublote.GetAllSublotes();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los sublotes";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetSublotes(int loteId)
        {

            try
            {
                var list = _sublote.GetAll(loteId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los sublotes";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetByFinca(string fincaId)
        {

            try
            {
                var list = _sublote.GetByFinca(fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las actividades";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        //public HttpResponseMessage GetByCultivo(int subloteId, string cultivoId)
        //{
        //    try
        //    {
        //        var list = _detalleLote.GetByCultivo(subloteId, cultivoId);
        //        return Request.CreateResponse(HttpStatusCode.OK, list);
        //    }
        //    catch
        //    {
        //        var message = "No se ha podido obtener los cultivos";
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        //    }

        //}

        public HttpResponseMessage Delete(int subloteId, string estado)
        {
            try
            {
                var rpta = _sublote.ActivarODesactivarLote(subloteId, estado);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido completar la operacion";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        //api guardar lote

        public HttpResponseMessage Post(CreateSubLoteDto model)
        {
            try
            {
                var rpta = _sublote.Save(model);

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
