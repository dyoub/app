// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.PurchaseOrders
{
    public class ListPurchaseOrderContext : TenantContext
    {
        public ListPurchaseOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            PurchaseOrders.Add(PurchaseOrderFactory.PurchaseOrder(store));
            PurchaseOrders.Add(PurchaseOrderFactory.PurchaseOrder(store));

            SaveChanges();
        }
    }
}
