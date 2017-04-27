// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SaleOrders
{
    public class SaleOrderIdJson : JsonResult
    {
        public SaleOrder SaleOrder { get; private set; }

        public SaleOrderIdJson(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                saleOrderId = SaleOrder.Id
            };

            base.ExecuteResult(context);
        }
    }
}
