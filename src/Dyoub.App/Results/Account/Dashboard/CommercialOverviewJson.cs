// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Overview;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class CommercialOverviewJson : JsonResult
    {
        public CommercialCount Counter { get; private set; }

        public CommercialOverviewJson(CommercialCount counter)
        {
            Counter = counter;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    customers = Counter.Customers,
                    paymentMethods = Counter.PaymentMethods
                }
            };

            base.ExecuteResult(context);
        }
    }
}
