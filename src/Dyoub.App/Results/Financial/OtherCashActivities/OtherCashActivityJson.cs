// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using System;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.OtherCashActivities
{
    public class OtherCashActivityJson : JsonResult
    {
        public OtherCashActivity OtherCashActivity { get; private set; }

        public OtherCashActivityJson(OtherCashActivity fixedExpense)
        {
            OtherCashActivity = fixedExpense;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = OtherCashActivity == null ? null : new
            {
                id = OtherCashActivity.Id,
                store = new
                {
                    id = OtherCashActivity.Store.Id,
                    name = OtherCashActivity.Store.Name
                },
                description = OtherCashActivity.Description,
                date = OtherCashActivity.Date.ToJson(),
                operation = OtherCashActivity.Value > 0 ? "Credit" : "Debit",
                value = Math.Abs(OtherCashActivity.Value)
            };

            base.ExecuteResult(context);
        }
    }
}
