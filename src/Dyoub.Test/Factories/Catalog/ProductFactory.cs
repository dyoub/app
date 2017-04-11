// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Catalog;

namespace Dyoub.Test.Factories.Catalog
{
    public class ProductFactory
    {
        public static Product Product(Tenant tenant)
        {
            return new Product
            {
                Name = "Product Test",
                Marketed = true,
                Tenant = tenant,
            };
        }
    }
}
