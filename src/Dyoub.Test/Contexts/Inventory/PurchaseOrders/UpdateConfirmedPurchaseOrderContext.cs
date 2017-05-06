// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.PurchaseOrders
{
    public class UpdateConfirmedPurchaseOrderContext : TenantContext
    {
        private PurchaseOrder original;

        public Store AnotherStore { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }

        public UpdateConfirmedPurchaseOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            AnotherStore = Stores.Add(StoreFactory.Store(tenant));
            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));

            original = PurchaseOrderFactory.PurchaseOrder(store);

            SaveChanges();
        }

        public bool PurchaseOrderWasNotChanged()
        {
            Entry(PurchaseOrder).Reload();

            return PurchaseOrder.Store.Id == original.Store.Id &&
                   PurchaseOrder.IssueDate == original.IssueDate &&
                   PurchaseOrder.AdditionalInformation == original.AdditionalInformation;
        }
    }
}
