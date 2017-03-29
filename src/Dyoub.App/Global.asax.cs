﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Configuration;
using Dyoub.App.Data;
using Dyoub.App.Models.EntityModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dyoub.App
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfiguration.RegisterRoutes(RouteTable.Routes);
            BundleConfiguration.Register(BundleTable.Bundles);
        }
    }
}