// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;
using System;

namespace Dyoub.Test.Contexts.OrderProcessing.PurchaseOrderProcessing
{
    public class ConfirmConfirmedPurchaseOrderContext : TenantContext
    {
        private PurchaseOrder originial;
        private PurchasePayment purchasePayment;

        public PurchaseOrder PurchaseOrder { get; private set; }

        public ConfirmConfirmedPurchaseOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));
            PurchaseOrder.ConfirmationDate = DateTime.Today.AddDays(-1);


            purchasePayment = PurchasePayments.Add(PurchasePaymentFactory.PurchasePayment(PurchaseOrder));
            purchasePayment.NumberOfInstallments = 2;
            purchasePayment.InstallmentValue = purchasePayment.Total / purchasePayment.NumberOfInstallments;

            originial = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));
            originial.ConfirmationDate = PurchaseOrder.ConfirmationDate;

            SaveChanges();
        }

        public bool PurchaseOrderWasNotConfirmedMoreThanOnce()
        {
            Entry(PurchaseOrder).Reload();
            return PurchaseOrder.ConfirmationDate == originial.ConfirmationDate;
        }
    }
}
