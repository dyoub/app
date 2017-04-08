// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Overview;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class ManagementOverviewJson : JsonResult
    {
        public ManagementCount ManagementCount { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    stores = ManagementCount.Stores
                }
            };

            base.ExecuteResult(context);
        }
    }
}
