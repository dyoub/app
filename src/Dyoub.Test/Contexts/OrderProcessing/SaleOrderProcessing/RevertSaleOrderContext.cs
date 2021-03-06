﻿// Copyright (c) Dyoub Applications. All rights reserved.
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
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.OrderProcessing.SaleOrderProcessing
{
    public class RevertSaleOrderContext : TenantContext
    {
        private SalePayment salePayment;
        private SaleProduct saleProduct;

        public SaleOrder SaleOrder { get; private set; }

        public RevertSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));

            saleProduct = SaleProducts.Add(SaleProductFactory.SaleProduct(SaleOrder, product));
            ProductStockMovements.Add(ProductStockMovementFactory.ProductStockMovement(saleProduct));
            
            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;

            SaveChanges();
        }

        public bool SaleOrderHasBeenReverted()
        {
            Entry(SaleOrder).Reload();

            return !SaleOrder.Confirmed;       
        }

        public bool BillingValuesHaveBeenReset()
        {
            Entry(salePayment).Reload();

            return SaleOrder.BilledAmount == 0 &&
                   salePayment.InstallmentBilling == 0 &&
                   salePayment.BilledAmount == 0;
        }

        public bool SaleIncomesHaveBeenRemoved()
        {
            return !SaleIncomes.Any();
        }

        public bool StockTransacionsHaveBeenRemoved()
        {
            Entry(saleProduct).Reload();

            return saleProduct.StockTransactionId == null &&
                   !ProductStockMovements.Any();
        }
    }
}
