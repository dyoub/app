// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.OtherCashActivities
{
    public class OtherCashActivityListJson : JsonResult
    {
        public IEnumerable<OtherCashActivity> OtherCashActivities { get; set; }

        public OtherCashActivityListJson(IEnumerable<OtherCashActivity> otherCashActivities)
        {
            OtherCashActivities = otherCashActivities;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = OtherCashActivities.Select(otherCashActivity => new
            {
                id = otherCashActivity.Id,
                description = otherCashActivity.Description,
                store = otherCashActivity.Store.Name,
                date = otherCashActivity.Date.ToJson(),
                operation = otherCashActivity.Value > 0 ? "Credit" : "Debit",
                value = Math.Abs(otherCashActivity.Value)
            });

            base.ExecuteResult(context);
        }
    }
}
