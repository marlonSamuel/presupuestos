using Presupuesto.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Configuration;
using Helpers;

namespace Presupuesto.Controllers
{

    [Autenticado]
    public class HomeController : Controller
    {
        SPPais _pais = new SPPais();
        public ActionResult Index(int id = 0)
        {
            if(id > 0)
            {
                HttpCookie myCookie = new HttpCookie("pais");

                //Add key-values in the cookie
                myCookie.Values.Add("paisId", id.ToString());

                //set cookie expiry date-time. Made it to last for next 12 hours.
                myCookie.Expires = DateTime.Now.AddMonths(3);

                //Most important, write the cookie to client.
                Response.Cookies.Add(myCookie);
            }

            return View();
        }
    }
}
