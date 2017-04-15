// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Catalog.Products
{
    public class DeleteProductContext : TenantContext
    {
        public Product Product { get; private set; }

        public DeleteProductContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Product = Products.Add(ProductFactory.Product(tenant));

            SaveChanges();
        }

        public bool ProductWasDeleted()
        {
            return !Products.Any();
        }
    }
}
