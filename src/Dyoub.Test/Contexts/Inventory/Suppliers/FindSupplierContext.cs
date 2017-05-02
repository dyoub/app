// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.Suppliers
{
    public class FindSupplierContext : TenantContext
    {
        public Supplier Supplier { get; private set; }

        public FindSupplierContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Supplier = Suppliers.Add(SupplierFactory.Supplier(tenant));

            SaveChanges();
        }
    }
}
