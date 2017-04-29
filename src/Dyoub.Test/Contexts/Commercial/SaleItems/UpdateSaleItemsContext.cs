// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.SaleOrders
{
    public class UpdateSaleItemsContext : TenantContext
    {
        public SaleOrder SaleOrder { get; private set; }
        public Product AnotherProduct { get; private set; }
        public Service AnotherService { get; private set; }

        public UpdateSaleItemsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            Service service = Services.Add(ServiceFactory.Service(tenant));

            AnotherProduct = Products.Add(ProductFactory.Product(tenant));
            AnotherService = Services.Add(ServiceFactory.Service(tenant));

            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, product, tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, AnotherProduct, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(store, service, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(store, AnotherService, tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store));
            SaleOrder.SaleProducts = new List<SaleProduct>();
            SaleOrder.SaleProducts.Add(SaleProductFactory.SaleProduct(SaleOrder, product));
            SaleOrder.SaleServices = new List<SaleService>();
            SaleOrder.SaleServices.Add(SaleServiceFactory.SaleService(SaleOrder, service));

            SaveChanges();
        }

        public bool SaleItemsWasUpdated()
        {
            SaleProduct saleProduct = SaleProducts
                .WhereSaleOrderId(SaleOrder.Id)
                .SingleOrDefault();

            SaleService saleService = SaleServices
                .WhereSaleOrderId(SaleOrder.Id)
                .SingleOrDefault();

            return saleProduct.ProductId == AnotherProduct.Id &&
                   saleService.ServiceId == AnotherService.Id;
        }
    }
}
