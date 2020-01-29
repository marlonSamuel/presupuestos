using Model;
using Model.BI;
using Model.NAV;
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
    public class BIController : ApiController
    {
        private readonly PSAgricola _agricola = new PSAgricola();
        private readonly CategoriaAgricola _categoria = new CategoriaAgricola();
        private readonly PSVentas _ventas = new PSVentas();
        private readonly GrupoProducto _grupo = new GrupoProducto();

        public HttpResponseMessage GetCategorias()
        {
            try
            {
                var list = _categoria.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener las categorias del presupuesto agricola";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("grupos/{categoriaId}")]
        [HttpGet]
        public HttpResponseMessage GetByCategoria(string categoriaId)
        {
            try
            {
                var list = _grupo.GetAll(categoriaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("productos/{grupoId}")]
        [HttpGet]
        public HttpResponseMessage GetByGrupo(string grupoId)
        {
            try
            {
                var list = _agricola.GetAll(grupoId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("productos/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetDetallePresupuestoAgricola(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _agricola.GetDetalleAgricola(presupuestoId,fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("ventas/{grupoId}")]
        [HttpGet]
        public HttpResponseMessage GetProductosVenta(string grupoId)
        {
            try
            {
                var list = _ventas.GetAll(grupoId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("ventas/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetDetallePresupuestoVentas(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _ventas.GetDetalleVentas(presupuestoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("productosagricola/{categoriaId}")]
        [HttpGet]
        public HttpResponseMessage GetProductosByCategoria(string categoriaId)
        {
            try
            {
                var list = _agricola.GetByCategoria(categoriaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        [Route("productosventa")]
        [HttpGet]
        public HttpResponseMessage GetProductosByCategoriaVenta()
        {
            try
            {
                var list = _ventas.GetByCategoria();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los productos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("detallegrupos/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetDetalleDetalleGroupByGrupo(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _agricola.GetDetalleGroupByGrupo(presupuestoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("detallecategorias/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetDetalleDetalleGroupByCategorias(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _agricola.GetDetalleGroupByCategoria(presupuestoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("detallegruposventa/{presupuestoId}/{fincaId}")]
        [HttpGet]
        public HttpResponseMessage GetDetalleDetalleGroupByGrupoVenta(int presupuestoId, int fincaId)
        {
            try
            {
                var list = _ventas.GetDetalleGroupByGroupo(presupuestoId, fincaId);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("ProductosAgricolas")]
        [HttpGet]
        public HttpResponseMessage GetAllProductosA()
        {
            try
            {
                var list = _agricola.GetAllProductos();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        [Route("ProductosParaVentas")]
        [HttpGet]
        public HttpResponseMessage GetAllProductosV()
        {
            try
            {
                var list = _ventas.GetAllProductos();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido los insumos";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }
    }
}
