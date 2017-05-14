// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

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
    }
}
