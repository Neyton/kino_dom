using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kino_dom.Cookie
{
    public class CookieModel
    {
        public string GetName()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket tiket = id.Ticket;
                    string str = tiket.Name;
                    return str;
                }
            }
            return null;
        }

    }
}