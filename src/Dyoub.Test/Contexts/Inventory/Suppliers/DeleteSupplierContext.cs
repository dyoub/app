// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Inventory.Suppliers
{
    public class DeleteSupplierContext : TenantContext
    {
        public Supplier Supplier { get; private set; }

        public DeleteSupplierContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Supplier = Suppliers.Add(SupplierFactory.Supplier(tenant));

            SaveChanges();
        }

        public bool SupplierWasDeleted()
        {
            return !Suppliers.Any();
        }
    }
}
