// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Inventory;
using Effort;

namespace Dyoub.Test.Contexts.Inventory.Suppliers
{
    public class UpdateSupplierContext : TenantContext
    {
        private Supplier original;

        public Supplier Supplier { get; private set; }

        public UpdateSupplierContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Supplier = Suppliers.Add(SupplierFactory.Supplier(tenant));

            original = SupplierFactory.Supplier(tenant);

            SaveChanges();
        }

        public bool SupplierWasUpdated()
        {
            Entry(Supplier).Reload();

            return Supplier.Name != original.Name &&
                   Supplier.NationalId != original.NationalId &&
                   Supplier.Email != original.Email &&
                   Supplier.Homepage != original.Homepage &&
                   Supplier.Facebook != original.Facebook &&
                   Supplier.Instagram != original.Instagram &&
                   Supplier.Twitter != original.Twitter &&
                   Supplier.GooglePlus != original.GooglePlus &&
                   Supplier.PhoneNumber != original.PhoneNumber &&
                   Supplier.AlternativePhoneNumber != original.AlternativePhoneNumber;
        }
    }
}
