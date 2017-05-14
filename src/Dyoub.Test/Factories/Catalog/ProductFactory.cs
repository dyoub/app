// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;

namespace Dyoub.Test.Factories.Catalog
{
    public class ProductFactory
    {
        public static Product Product(Tenant tenant)
        {
            return new Product
            {
                Tenant = tenant,
                Name = "Product Test",
                Marketed = true,
                StockMovement = true
            };
        }
    }
}
