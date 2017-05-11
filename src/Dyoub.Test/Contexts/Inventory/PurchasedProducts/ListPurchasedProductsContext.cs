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

namespace Dyoub.Test.Contexts.Inventory.PurchaseOrders
{
    public class ListPurchasedProductsContext : TenantContext
    {
        public PurchaseOrder PurchaseOrder { get; private set; }

        public ListPurchasedProductsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));

            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.PurchaseOrder(store));
            PurchaseOrder.PurchasedProducts = new List<PurchasedProduct>();
            PurchaseOrder.PurchasedProducts.Add(PurchasedProductFactory.PurchasedProduct(PurchaseOrder, product));

            SaveChanges();
        }
    }
}
