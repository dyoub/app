// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.PurchaseSupplier
{
    public class UpdateSupplierToConfirmedPurchaseOrderContext : TenantContext
    {
        private PurchaseOrder original;

        public PurchaseOrder PurchaseOrder { get; private set; }
        public Supplier AnotherSupplier { get; private set; }

        public UpdateSupplierToConfirmedPurchaseOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Supplier supplier = Suppliers.Add(SupplierFactory.Supplier(tenant));
            Store store = Stores.Add(StoreFactory.Store(tenant));

            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store, supplier));
            AnotherSupplier = Suppliers.Add(SupplierFactory.Supplier(tenant));

            original = PurchaseOrderFactory.PurchaseOrder(store, supplier);

            SaveChanges();
        }

        public bool PurchaseOrderWasNotChanged()
        {
            Entry(PurchaseOrder).Reload();
            return PurchaseOrder.Supplier.Id == original.Supplier.Id;
        }
    }
}
