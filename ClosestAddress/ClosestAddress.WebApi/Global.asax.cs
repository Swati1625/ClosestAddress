﻿using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ClosestAddress.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IWindsorContainer container = new WindsorContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
