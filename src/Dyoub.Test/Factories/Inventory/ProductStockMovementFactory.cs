// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Inventory
{
    public class ProductStockMovementFactory
    {
        public static ProductStockMovement ProductStockMovement(Store store, Product product, decimal quantity)
        {
            return new ProductStockMovement
            {
                TransactionId = Guid.NewGuid(),
                Date = DateTime.UtcNow.Date,
                Store = store,
                Product = product,
                Tenant = product.Tenant,
                Quantity = quantity
            };
        }

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

        public static ProductStockMovement ProductStockMovement(RentedProduct rentedProduct)
        {
            rentedProduct.StockTransactionIdOut = Guid.NewGuid();

            return new ProductStockMovement
            {
                TransactionId = rentedProduct.StockTransactionIdOut.Value,
                Date = rentedProduct.RentContract.StartDate,
                Store = rentedProduct.RentContract.Store,
                Product = rentedProduct.Product,
                Tenant = rentedProduct.Tenant,
                Quantity = rentedProduct.Quantity * (-1)
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
