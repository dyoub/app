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
    public class UpdatePricingTableContext : TenantContext
    {
        private ProductPrice initialShirtPrice;
        private ServicePrice initialSeamPrice;

        public Store Store { get; private set; }
        public Product Shirt { get; private set; }
        public Product Sneakers { get; private set; }
        public Product Pants { get; private set; }
        public Service Seam { get; private set; }
        public Service Clean { get; private set; }
        public Service Adjustment { get; private set; }

        public UpdatePricingTableContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Store = Stores.Add(StoreFactory.Store(tenant));

            Shirt = Products.Add(ProductFactory.Product(tenant));
            Sneakers = Products.Add(ProductFactory.Product(tenant));
            Pants = Products.Add(ProductFactory.Product(tenant));
            Seam = Services.Add(ServiceFactory.Service(tenant));
            Clean = Services.Add(ServiceFactory.Service(tenant));
            Adjustment = Services.Add(ServiceFactory.Service(tenant));

            initialShirtPrice = ProductPrices.Add(ProductPriceFactory.ProductPrice(Store, Shirt, tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(Store, Sneakers, tenant));

            initialSeamPrice = ServicePrices.Add(ServicePriceFactory.ServicePrice(Store, Seam, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(Store, Clean, tenant));

            SaveChanges();
        }

        public bool NewPricesWasCreated()
        {
            return ProductPrices.WhereProductId(Pants.Id).Any() &&
                   ServicePrices.WhereServiceId(Adjustment.Id).Any();
        }

        public bool ChangedPricesWasUpdated()
        {
            ProductPrice newShirtPrice = ProductPrices.WhereProductId(Shirt.Id).Single();
            ServicePrice newSeamPrice = ServicePrices.WhereServiceId(Seam.Id).Single();

            return newShirtPrice.UnitSalePrice != initialShirtPrice.UnitSalePrice &&
                   newSeamPrice.UnitPrice != initialSeamPrice.UnitPrice;
        }

        public bool EmptyPricesWasDeleted()
        {
            return !ProductPrices.WhereProductId(Sneakers.Id).Any() &&
                   !ServicePrices.WhereServiceId(Clean.Id).Any();
        }
    }
}
