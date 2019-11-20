using DDCA.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DDCA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(UsersController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { controller = "Users", action = "Login" }, namespaces);

            routes.MapRoute("Logout", "logout", new { controller = "Users", action = "Logout" }, namespaces);

            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);
        }
    }
}
