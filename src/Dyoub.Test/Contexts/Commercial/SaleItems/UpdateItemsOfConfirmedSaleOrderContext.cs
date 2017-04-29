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
    public class UpdateItemsOfConfirmedSaleOrderContext : TenantContext
    {
        public SaleOrder SaleOrder { get; private set; }
        public Product Product { get; private set; }
        public Product AnotherProduct { get; private set; }
        public Service Service { get; private set; }
        public Service AnotherService { get; private set; }

        public UpdateItemsOfConfirmedSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            Product = Products.Add(ProductFactory.Product(tenant));
            Service = Services.Add(ServiceFactory.Service(tenant));
            AnotherProduct = Products.Add(ProductFactory.Product(tenant));
            AnotherService = Services.Add(ServiceFactory.Service(tenant));

            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, Product, tenant));
            ProductPrices.Add(ProductPriceFactory.ProductPrice(store, AnotherProduct, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(store, Service, tenant));
            ServicePrices.Add(ServicePriceFactory.ServicePrice(store, AnotherService, tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));
            SaleOrder.SaleProducts = new List<SaleProduct>();
            SaleOrder.SaleProducts.Add(SaleProductFactory.SaleProduct(SaleOrder, Product));
            SaleOrder.SaleServices = new List<SaleService>();
            SaleOrder.SaleServices.Add(SaleServiceFactory.SaleService(SaleOrder, Service));

            SaveChanges();
        }

        public bool SaleItemsNotChanged()
        {
            SaleProduct saleProduct = SaleProducts
                .WhereSaleOrderId(SaleOrder.Id)
                .SingleOrDefault();

            SaleService saleService = SaleServices
                .WhereSaleOrderId(SaleOrder.Id)
                .SingleOrDefault();

            return saleProduct.ProductId == Product.Id &&
                   saleService.ServiceId == Service.Id;
        }
    }
}
