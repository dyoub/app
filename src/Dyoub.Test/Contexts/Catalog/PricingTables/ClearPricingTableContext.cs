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
    public class ClearPricingTableContext : TenantContext
    {
        public Store Store { get; private set; }

        public ClearPricingTableContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Store = Stores.Add(StoreFactory.Store(tenant));

            Product product = Products.Add(ProductFactory.Product(tenant));
            Service service = Services.Add(ServiceFactory.Service(tenant));

            ProductPrices.Add(ProductPriceFactory.ProductPrice(Store, product, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(Store, service, tenant));

            SaveChanges();
        }

        public bool PricingTableWasErased()
        {
            return !ProductPrices.WhereStoreId(Store.Id).Any() &&
                   !ServicePrices.WhereStoreId(Store.Id).Any();
        }
    }
}
