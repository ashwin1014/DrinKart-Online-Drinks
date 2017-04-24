using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OnlineShopping.Domain.Entities;
using OnlineShopping.WebUI.Binders;

namespace OnlineShopping.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start ( )
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
