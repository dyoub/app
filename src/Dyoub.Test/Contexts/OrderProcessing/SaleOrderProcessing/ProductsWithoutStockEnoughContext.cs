// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.OrderProcessing.SaleOrderProcessing
{
    public class ProductsWithoutStockEnoughContext : TenantContext
    {
        private SalePayment salePayment;
        private SaleProduct saleProduct;

        public SaleOrder SaleOrder { get; private set; }

        public ProductsWithoutStockEnoughContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store));

            saleProduct = SaleProducts.Add(SaleProductFactory.SaleProduct(SaleOrder, product));

            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;
            salePayment.InstallmentValue = salePayment.Total / salePayment.NumberOfInstallments;

            SaveChanges();
        }
    }
}
