using Helpers;
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
    public class LoginController : Controller
    {
        // GET: Login

        private SPUsuario usuario = new SPUsuario();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Acceder(string Email, string Password)
        {
            var rm = usuario.Acceder(Email, Password);
            //var route = "/presupuestos/";
            var route = "/";

            if (rm.response)
            {
                  rm.href = Url.Content(route + "login/seleccionar");
            }

            return Json(rm);
        }

        [Autenticado]
        public ActionResult Seleccionar()
        {

            if (Request.Cookies["pais"] != null)
            {
                HttpCookie myCookie = new HttpCookie("pais");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            return View();
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }
    }
}