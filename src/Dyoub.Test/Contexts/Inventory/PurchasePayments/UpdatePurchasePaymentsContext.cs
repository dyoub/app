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
using System.Linq;

namespace Dyoub.Test.Contexts.Inventory.PurchasePayments
{
    public class UpdatePurchasePaymentsContext : TenantContext
    {
        private PurchasePayment original;

        public PurchasePayment PurchasePayment { get; private set; }

        public UpdatePurchasePaymentsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PurchaseOrder purchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.PurchaseOrder(store));
            
            PurchasePayment = PurchasePayments.Add(PurchasePaymentFactory.PurchasePayment(purchaseOrder));
            original = PurchasePayments.Add(PurchasePaymentFactory.PurchasePayment(purchaseOrder));

            SaveChanges();
        }

        public bool PurchasePaymentsWasUpdated()
        {
            PurchasePayment purchasePayment = PurchasePayments.First();

            return purchasePayment.Date != original.Date &&
                   purchasePayment.NumberOfInstallments != original.NumberOfInstallments &&
                   purchasePayment.InstallmentValue != original.InstallmentValue;
        }
    }
}
