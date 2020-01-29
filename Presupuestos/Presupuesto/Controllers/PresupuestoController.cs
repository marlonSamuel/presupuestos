using Presupuesto.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presupuesto.Controllers
{
    [Autenticado]
    public class PresupuestoController : Controller
    {
        // GET: Presupuesto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Detalle_a()
        {
            return View();
        }

        public ActionResult Detalle_c()
        {
            return View();
        }

        public ActionResult Detalle_v()
        {
            return View();
        }
    }
}