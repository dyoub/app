// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using System.Web.Mvc;

namespace Dyoub.App.Configuration
{
    public class GlobalFilterConfiguration
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandlerAttribute());
            filters.Add(new ModelStateAttribute());
        }
    }
}
