using Model;
using Model.DTO;
using Presupuesto.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Presupuesto.Controllers
{
    [Autenticado]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Fincas]
        public ActionResult Cultivo()
        {
            return View();
        }

        [Fincas]
        public ActionResult CCosto()
        {
            return View();
        }

        [Fincas]
        public ActionResult Variedad()
        {
            return View();
        }

        [Fincas]
        public ActionResult Actividad()
        {
            return View();
        }

        [Fincas]
        public ActionResult Lote()
        {
            return View();
        }

        [Fincas]
        public ActionResult Unidad()
        {
            return View();
        }

        [Fincas]
        public ActionResult Presentacion()
        {
            return View();
        }

        public ActionResult Departamento()
        {
            return View();
        }

        public ActionResult TipoEmpleado()
        {
            return View();
        }

        [Almacen]
        public ActionResult Categoria()
        {
            return View();
        }

        [Almacen]
        public ActionResult Producto()
        {
            return View();
        }

        public ActionResult Cargo()
        {
            return View();
        }

        public ActionResult Empleado()
        {
            return View();
        }

        [Acceso]
        public ActionResult Permiso()
        {
            return View();
        }

        [Acceso]
        public ActionResult Usuario()
        {
            return View();
        }

        [Pais]
        public ActionResult Moneda()
        {
            return View();
        }

        [Pais]
        public ActionResult Pais()
        {
            return View();
        }

        [Dimension]
        public ActionResult GrupoDimension()
        {
            return View();
        }

        [Dimension]
        public ActionResult Dimension()
        {
            return View();
        }

        [Fincas]
        public ActionResult TipoActividad()
        {
            return View();
        }

        [Pais]
        public ActionResult TipoCambio()
        {
            return View();
        }
    }
}
