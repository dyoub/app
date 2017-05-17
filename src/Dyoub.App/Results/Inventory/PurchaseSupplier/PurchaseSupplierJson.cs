// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchaseSupplier
{
    public class PurchaseSupplierJson : JsonResult
    {
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseSupplierJson(PurchaseOrder saleOrder)
        {
            PurchaseOrder = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrder == null ? null : new
            {
                id = PurchaseOrder.Id,
                confirmed = PurchaseOrder.Confirmed,
                supplier = PurchaseOrder.Supplier == null ? null : new
                {
                    id = PurchaseOrder.Supplier.Id, 
                    name = PurchaseOrder.Supplier.Name,
                    nationalId = PurchaseOrder.Supplier.NationalId,
                    email = PurchaseOrder.Supplier.Email,
                    phoneNumber = PurchaseOrder.Supplier.PhoneNumber,
                    alternativePhoneNumber = PurchaseOrder.Supplier.AlternativePhoneNumber
                }
            };

            base.ExecuteResult(context);
        }
    }
}
