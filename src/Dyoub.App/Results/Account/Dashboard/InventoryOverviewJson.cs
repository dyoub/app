// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Overview;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class InventoryOverviewJson : JsonResult
    {
        public InventoryCount Counter { get; private set; }

        public InventoryOverviewJson(InventoryCount counter)
        {
            Counter = counter;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    suppliers = Counter.Suppliers
                }
            };

            base.ExecuteResult(context);
        }
    }
}
