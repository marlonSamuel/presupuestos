using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

namespace Helpers
{
    public class SessionHelper
    {
        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        public static void DestroyUserSession()
        {
            var Request = HttpContext.Current.Request;
            FormsAuthentication.SignOut();
        }
        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }
        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        //public System.Web.HttpCookieCollection Cookies { get; }

        public static int GetPais()
        {
            var Request = HttpContext.Current.Request;
            int paisId = 0;
            HttpCookie myCookie = Request.Cookies["pais"];

            if (myCookie == null)
            {
                //No cookie found or cookie expired.
                //Handle the situation here, Redirect the user or simply return;
            }

            //si existe el valor de la coockie
          if(myCookie != null)
            {
                if (!string.IsNullOrEmpty(myCookie.Values["paisId"]))
                {
                    paisId = Convert.ToInt32(myCookie.Values["paisId"]);
                }
            }

            return paisId;
        }
    }
}