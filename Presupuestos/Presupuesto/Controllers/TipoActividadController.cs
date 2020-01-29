﻿using Model;
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
    public class TipoActividadController : ApiController
    {
        private readonly SPTipoActividad _tipo = new SPTipoActividad();

        public HttpResponseMessage GetActividades()
        {
            try
            {
                var list = _tipo.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los tipos de actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }


        [Route("actividades")]
        [HttpGet]
        public HttpResponseMessage GetPrincipales()
        {
            try
            {
                var list = _tipo.GetPrincipales();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var message = "No se ha podido obtener los tipos de actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        public HttpResponseMessage GetById(int id)
        {
            try
            {
                var categoria = _tipo.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, categoria);
            }
            catch
            {
                var message = "No se ha podido obtener el tipo de actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        //api guardar actividad
        public HttpResponseMessage Post(SPTipoActividad model)
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
                var rpta = _tipo.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, rpta);
            }
            catch
            {
                var message = "No se ha podido eliminar el tipo de actividad";
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }
    }
}