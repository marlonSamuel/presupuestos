using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Presupuesto.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class FileController : ApiController
    {
        private readonly SPDetallePresupuesto _dPresupuesto = new SPDetallePresupuesto();
        private readonly SPPresupuestoContable _dContable = new SPPresupuestoContable();
        public HttpResponseMessage Post(CargaMasivaCreateDto model)
        {
            try
            {
                var rpta = _dPresupuesto.saveFile(model);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar carga de presupuestos, verifique los datos e intente nuevamente";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("fileContable")]
        [HttpPost]
        public HttpResponseMessage PostContable(CargaMasivaCt model)
        {
            try
            {
                var rpta = _dContable.SaveFile(model);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "error al guardar carga de presupuestos, verifique los datos e intente nuevamente";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
