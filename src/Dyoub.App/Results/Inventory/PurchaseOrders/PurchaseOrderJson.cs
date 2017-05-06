// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchaseOrders
{
    public class PurchaseOrderJson : JsonResult
    {
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseOrderJson(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrder == null? null : new
            {
                id = PurchaseOrder.Id,
                issueDate = PurchaseOrder.IssueDate,
                confirmationDate = PurchaseOrder.ConfirmationDate,
                additionalInformation = PurchaseOrder.AdditionalInformation,
                total = PurchaseOrder.Total,
                totalCost = PurchaseOrder.TotalCost,
                confirmed = PurchaseOrder.Confirmed,
                store = new
                {
                    id = PurchaseOrder.Store.Id,
                    name = PurchaseOrder.Store.Name
                },
                wallet = PurchaseOrder.Wallet == null ? null : new
                {
                    id = PurchaseOrder.Wallet.Id,
                    name = PurchaseOrder.Wallet.Name
                }
            };

            base.ExecuteResult(context);
        }
    }
}
