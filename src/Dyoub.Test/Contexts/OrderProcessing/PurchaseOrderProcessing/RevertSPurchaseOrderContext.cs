// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.OrderProcessing.PurchaseOrderProcessing
{
    public class RevertPurchaseOrderContext : TenantContext
    {
        private PurchasedProduct purchasedProduct;
        private PurchasePayment purchasePayment;

        public PurchaseOrder PurchaseOrder { get; private set; }

        public RevertPurchaseOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));

            PurchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));

            purchasedProduct = PurchasedProducts.Add(PurchasedProductFactory.PurchasedProduct(PurchaseOrder, product));
            ProductStockMovements.Add(ProductStockMovementFactory.ProductStockMovement(purchasedProduct));

            purchasePayment = PurchasePayments.Add(PurchasePaymentFactory.PurchasePayment(PurchaseOrder));
            purchasePayment.NumberOfInstallments = 2;

            SaveChanges();
        }

        public bool PurchaseOrderWasReverted()
        {
            Entry(PurchaseOrder).Reload();
            Entry(purchasePayment).Reload();

            return !PurchaseOrder.Confirmed &&
                   PurchaseOrder.TotalCost == 0 &&
                   purchasedProduct.StockTransactionId == null &&
                   !ProductStockMovements.Any();
        }
    }
}
