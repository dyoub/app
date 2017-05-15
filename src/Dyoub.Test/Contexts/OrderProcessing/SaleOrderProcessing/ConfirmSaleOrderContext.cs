// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
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
    public class ConfirmSaleOrderContext : TenantContext
    {
        private SalePayment salePayment;
        private SaleProduct saleProduct;

        public SaleOrder SaleOrder { get; private set; }

        public ConfirmSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            PurchaseOrder purchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));
            PurchasedProduct purchasedProduct = PurchasedProducts.Add(PurchasedProductFactory.PurchasedProduct(purchaseOrder, product));

            ProductStockMovements.Add(ProductStockMovementFactory.ProductStockMovement(purchasedProduct));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store));
            
            saleProduct = SaleProducts.Add(SaleProductFactory.SaleProduct(SaleOrder, product));

            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;
            salePayment.InstallmentValue = salePayment.Total / salePayment.NumberOfInstallments;

            SaveChanges();
        }

        public bool SaleOrderHasBeenConfirmed()
        {
            Entry(SaleOrder).Reload();
            
            return SaleOrder.Confirmed;
        }

        public bool BillingValuesHaveBeenCalculated()
        {
            return SaleOrder.BilledAmount == salePayment.BilledAmount;
        }

        public bool SaleIncomesHaveBeenGenerated()
        {
            SaleIncome[] incomes = SaleIncomes.ToArray();

            return incomes[0].PaymentDate == salePayment.Date &&
                   incomes[0].AmountReceived == salePayment.InstallmentValue &&
                   incomes[1].PaymentDate == salePayment.Date.AddMonths(1) &&
                   incomes[1].AmountReceived == salePayment.InstallmentValue;
        }

        public bool StockMovementHasBeenRegistered()
        {
            Entry(saleProduct).Reload();
            
            return ProductStockMovements
                .Where(movement => movement.TransactionId == saleProduct.StockTransactionId.Value)
                .Any();
        }
    }
}
