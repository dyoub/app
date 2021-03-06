﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Overview;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class CatalogOverviewJson : JsonResult
    {
        public CatalogCount Counter { get; private set; }

        public CatalogOverviewJson(CatalogCount counter)
        {
            Counter = counter;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    products = Counter.Products,
                    services = Counter.Services
                }
            };

            base.ExecuteResult(context);
        }
    }
}
