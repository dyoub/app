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
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.RentContracts
{
    public class UpdateItemsOfConfirmedRentContractContext : TenantContext
    {
        public RentContract RentContract { get; private set; }
        public Product Product { get; private set; }
        public Product AnotherProduct { get; private set; }

        public UpdateItemsOfConfirmedRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            Product = Products.Add(ProductFactory.Product(tenant));
            AnotherProduct = Products.Add(ProductFactory.Product(tenant));

            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, Product, tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, AnotherProduct, tenant));

            RentContract = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store));
            RentContract.RentedProducts = new List<RentedProduct>();
            RentContract.RentedProducts.Add(RentedProductFactory.RentedProduct(RentContract, Product));

            SaveChanges();
        }

        public bool RentedProductsNotChanged()
        {
            RentedProduct rentedProduct = RentedProducts
                .WhereRentContractId(RentContract.Id)
                .SingleOrDefault();

            return rentedProduct.ProductId == Product.Id;
        }
    }
}
