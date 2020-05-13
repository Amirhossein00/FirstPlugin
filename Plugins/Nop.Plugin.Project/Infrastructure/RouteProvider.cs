using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Framework.Seo;

namespace Nop.Plugin.Projects.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {


            routeBuilder.MapLocalizedRoute("ProjectsDetail", "{SeName}",
                new { controller = "Project", action = "Detail" });
        }


        public int Priority
        {
            get
            {
                return -1000000;
            }
        }
    }
}
