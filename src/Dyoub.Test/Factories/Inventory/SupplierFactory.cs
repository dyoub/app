// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;

namespace Dyoub.Test.Factories.Inventory
{
    public class SupplierFactory
    {
        public static Supplier Supplier(Tenant tenant)
        {
            return new Supplier
            {
                Name = "Supplier Test",
                NationalId = "123456890",
                Email = "supplier@email.com",
                Homepage = "www.supplier.com",
                Facebook = "/supplier",
                Twitter = "@supplier",
                Instagram = "@supplier",
                GooglePlus = "/supplier",
                PhoneNumber = "99 99999-9999",
                AlternativePhoneNumber = "99 88888-8888",
                Tenant = tenant,
            };
        }
    }
}
