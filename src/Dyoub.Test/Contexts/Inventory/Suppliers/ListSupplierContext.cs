// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.Suppliers
{
    public class ListSupplierContext : TenantContext
    {
        public ListSupplierContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Suppliers.Add(SupplierFactory.Supplier(tenant));
            Suppliers.Add(SupplierFactory.Supplier(tenant));

            SaveChanges();
        }
    }
}
