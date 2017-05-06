// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchaseOrders
{
    public class PurchaseOrderIdJson : JsonResult
    {
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseOrderIdJson(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                purchaseOrderId = PurchaseOrder.Id
            };

            base.ExecuteResult(context);
        }
    }
}
