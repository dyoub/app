// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Inventory
{
    public class PurchaseOrderFactory
    {
        public static PurchaseOrder PurchaseOrder(Store store, Supplier supplier = null, Wallet wallet = null)
        {
            return new PurchaseOrder
            {
                Store = store,
                Tenant = store.Tenant,
                Supplier = supplier,
                Wallet = wallet,
                IssueDate = DateTime.Today,
                AdditionalInformation = "Additional information.",
                Author = "user@email.com",
                Total = 20.00M,
                Discount = 10,
                TotalPayable = 18.00M,
                CreatedAt = DateTime.Now
            };
        }

        public static PurchaseOrder ConfirmedPurchaseOrder(Store store, Supplier supplier = null)
        {
            PurchaseOrder saleOrder = PurchaseOrder(store, supplier);
            saleOrder.ConfirmationDate = DateTime.Now;

            return saleOrder;
        }
    }
}
