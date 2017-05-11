// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.Test.Contexts.Inventory.PurchaseOrders
{
    public class UpdatePurchasedProductsContext : TenantContext
    {
        public PurchaseOrder PurchaseOrder { get; private set; }
        public Product AnotherProduct { get; private set; }

        public UpdatePurchasedProductsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));

            AnotherProduct = Products.Add(ProductFactory.Product(tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, product, tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, AnotherProduct, tenant));

            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.PurchaseOrder(store));
            PurchaseOrder.PurchasedProducts = new List<PurchasedProduct>();
            PurchaseOrder.PurchasedProducts.Add(PurchasedProductFactory.PurchasedProduct(PurchaseOrder, product));

            SaveChanges();
        }

        public bool PurchasedProductsWasUpdated()
        {
            PurchasedProduct purchasedProduct = PurchasedProducts
                .WherePurchaseOrderId(PurchaseOrder.Id)
                .SingleOrDefault();
            
            return purchasedProduct.ProductId == AnotherProduct.Id;
        }
    }
}
