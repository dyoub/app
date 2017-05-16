// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dyoub.App
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfiguration.RegisterRoutes(RouteTable.Routes);
            BundleConfiguration.RegisterBundles(BundleTable.Bundles);
            ModelBindersConfiguration.RegisterModelBinders(ModelBinders.Binders);
            GlobalFiltersConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
