// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Catalog;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Effort;

namespace Dyoub.Test.Contexts.Catalog.Products
{
    public class UpdateProductContext : TenantContext
    {
        private Product original;

        public Product Product { get; private set; }

        public UpdateProductContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Product = Products.Add(ProductFactory.Product(tenant));

            original = ProductFactory.Product(tenant);

            SaveChanges();
        }

        public bool ProductWasUpdated()
        {
            Entry(Product).Reload();

            return Product.Name != original.Name &&
                   Product.Code != original.Code &&
                   Product.AdditionalInformation != original.AdditionalInformation &&
                   Product.IsManufactured != original.IsManufactured &&
                   Product.StockMovement != original.StockMovement &&
                   Product.CanFraction != original.CanFraction &&
                   Product.Marketed != original.Marketed;
        }
    }
}
