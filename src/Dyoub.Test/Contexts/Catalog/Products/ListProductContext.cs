// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Effort;

namespace Dyoub.Test.Contexts.Catalog.Products
{
    public class ListProductContext : TenantContext
    {
        public ListProductContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Products.Add(ProductFactory.Product(tenant));
            Products.Add(ProductFactory.Product(tenant));

            SaveChanges();
        }
    }
}
