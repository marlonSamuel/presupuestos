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
    public class DetallePresupuestoController : ApiController
    {
        private readonly SPDetallePresupuesto _manoObra = new SPDetallePresupuesto();

        public HttpResponseMessage GetByPresupuesto(int presupuetoId)
        {
            try
            {
                    var list = _manoObra.GetByPresupuesto(presupuetoId);
                    return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener el detalle de presupuesto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage Get(int presupuetoId, bool filterCCosto)
        {
            try
            {
                if (!filterCCosto)
                {
                    var list = _manoObra.GetByPresupuesto(presupuetoId);
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
                else{
                    var list = _manoObra.GetByAsignaciones(presupuetoId);
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
            catch
            {
                var message = "No se ha podido obtener el detalle de presupuesto";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        public HttpResponseMessage GetByCCosto(int presupuetoId, int fincaId)
        {
            try
            {
                var list = _manoObra.GetByCCosto(presupuetoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los presupuestos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        [Route("cosecha/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetByCCostoCosecha(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _manoObra.GetByCCostoCosecha(presupuestoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los presupuestos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetByGrupo(int presupuetoId, int fincaId, string opcion)
        {
            try
            {
                var list = _manoObra.GetByGroup(presupuetoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las categorias";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        //api guardar actividad
        public HttpResponseMessage Post(CreateDetalleManoObraDto model)
        {
            try
            {
                var rpta = _manoObra.Save(model);

                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        public HttpResponseMessage Put(int presupuestoId, decimal total_estimado)
        {
            try
            {
                var rpta = _manoObra.updateTotal(presupuestoId, total_estimado);

                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //finalizamos el presupuesto
        public HttpResponseMessage PutFinalizar(int presupuestoId, string fincaId)
        {
            try
            {
                var rpta = _manoObra.Finalizar(presupuestoId, fincaId);

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
                var rpta = _manoObra.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el registro";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}
