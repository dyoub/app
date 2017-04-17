// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Manage.Stores;

namespace Dyoub.Test.Factories.Catalog
{
    public class ProductPriceFactory
    {
        public static ProductPrice ProductPrice(Store store, Product product, Tenant tenant)
        {
            return new ProductPrice
            {
                Store = store,
                Product = product,
                Tenant = tenant,
                UnitPrice = 20
            };
        }
    }
}
