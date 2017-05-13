// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using System;

namespace Dyoub.Test.Factories.Inventory
{
    public class PurchasePaymentFactory
    {
        public static PurchasePayment PurchasePayment(PurchaseOrder purchaseOrder)
        {
            return new PurchasePayment
            {
                Date = DateTime.Today,
                PurchaseOrder = purchaseOrder,
                Tenant = purchaseOrder.Tenant,
                NumberOfInstallments = 1,
                InstallmentValue = purchaseOrder.TotalPayable,
                Total = purchaseOrder.TotalPayable
            };
        }
    }
}
