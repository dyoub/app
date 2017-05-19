// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Catalog.PricingTables
{
    public class CopyPricingTableContext : TenantContext
    {
        public Store Store { get; private set; }
        public Store SourceStore { get; private set; }

        public CopyPricingTableContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Store = Stores.Add(StoreFactory.Store(tenant));
            SourceStore = Stores.Add(StoreFactory.Store(tenant));

            Product product = Products.Add(ProductFactory.Product(tenant));
            Service service = Services.Add(ServiceFactory.Service(tenant));

            ProductPrices.Add(ProductPriceFactory.ProductPrice(SourceStore, product, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(SourceStore, service, tenant));

            SaveChanges();
        }

        public bool PricingTableWasCopied()
        {
            ProductPrice productPrice = ProductPrices.WhereStoreId(SourceStore.Id).Single();
            ServicePrice servicePrice = ServicePrices.WhereStoreId(SourceStore.Id).Single();

            ProductPrice copiedProductPrice = ProductPrices.WhereStoreId(Store.Id).Single();
            ServicePrice copiedServicePrice = ServicePrices.WhereStoreId(Store.Id).Single();

            return copiedProductPrice.UnitSalePrice == productPrice.UnitSalePrice &&
                   copiedServicePrice.UnitPrice == servicePrice.UnitPrice;
        }
    }
}
