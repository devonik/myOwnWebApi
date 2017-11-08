using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace myOwnWebApiNet_4._5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
            config.EnableCors();
            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "get" }
            );
        }
    }
}
