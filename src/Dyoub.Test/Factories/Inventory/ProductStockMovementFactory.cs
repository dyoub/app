// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using System;

namespace Dyoub.Test.Factories.Inventory
{
    public class ProductStockMovementFactory
    {
        public static ProductStockMovement ProductStockMovement(PurchasedProduct purchasedProduct)
        {
            purchasedProduct.StockTransactionId = Guid.NewGuid();

            return new ProductStockMovement
            {
                TransactionId = purchasedProduct.StockTransactionId.Value,
                Date = purchasedProduct.PurchaseOrder.IssueDate,
                Store = purchasedProduct.PurchaseOrder.Store,
                Product = purchasedProduct.Product,
                Tenant = purchasedProduct.Tenant,
                Quantity = purchasedProduct.Quantity
            };
        }

        public static ProductStockMovement ProductStockMovement(SaleProduct saleProduct)
        {
            saleProduct.StockTransactionId = Guid.NewGuid();

            return new ProductStockMovement
            {
                TransactionId = saleProduct.StockTransactionId.Value,
                Date = saleProduct.SaleOrder.IssueDate,
                Store = saleProduct.SaleOrder.Store,
                Product = saleProduct.Product,
                Tenant = saleProduct.Tenant,
                Quantity = saleProduct.Quantity * (-1)
            };
        }
    }
}
