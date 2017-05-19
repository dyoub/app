// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Collections.Generic;

namespace Dyoub.Test.Contexts.Commercial.RentContracts
{
    public class ListRentedProductsContext : TenantContext
    {
        public RentContract RentContract { get; private set; }

        public ListRentedProductsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));

            RentContract = RentContracts.Add(RentContractFactory.RentContract(store));
            RentContract.RentedProducts = new List<RentedProduct>();
            RentContract.RentedProducts.Add(RentedProductFactory.RentedProduct(RentContract, product));

            SaveChanges();
        }
    }
}
