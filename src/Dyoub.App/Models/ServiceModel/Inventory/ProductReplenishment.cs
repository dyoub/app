// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Inventory
{
    public class ProductReplenishment
    {
        public TenantContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public ICollection<Product> Products { get; private set; }
        public bool HasProductThatNotMovementStock { get; private set; }

        public ProductReplenishment(TenantContext tenant, PurchaseOrder purchaseOrder)
        {
            Tenant = tenant;
            PurchaseOrder = purchaseOrder;
        }

        public async Task<bool> Confirm()
        {
            Products = await Tenant.Products
                .WhereIdIn(PurchaseOrder.PurchasedProducts.Select(p => p.ProductId))
                .ToListAsync();

            foreach (Product product in Products)
            {
                if (HasProductThatNotMovementStock = !product.StockMovement) return false;

                PurchasedProduct purchasedProduct = PurchaseOrder.PurchasedProducts
                    .Single(p => p.ProductId == product.Id);

                purchasedProduct.StockTransactionId = Guid.NewGuid();

                Tenant.ProductStockMovements.Add(new ProductStockMovement
                {
                    TransactionId = purchasedProduct.StockTransactionId.Value,
                    StoreId = PurchaseOrder.StoreId,
                    ProductId = purchasedProduct.ProductId,
                    Date = PurchaseOrder.IssueDate,
                    Quantity = purchasedProduct.Quantity
                });
            }

            return true;
        }

        public async Task Revert()
        {
            IEnumerable<Guid> stockTransactionIds = PurchaseOrder.PurchasedProducts
                .Where(purchasedProduct => purchasedProduct.StockTransactionId != null)
                .Select(purchasedProduct => purchasedProduct.StockTransactionId.Value)
                .ToList();

            foreach (PurchasedProduct purchasedProduct in PurchaseOrder.PurchasedProducts)
            {
                purchasedProduct.StockTransactionId = null;
            }

            ICollection<ProductStockMovement> stockMovements = await Tenant.ProductStockMovements
                .WhereTransactionIdIn(stockTransactionIds)
                .ToListAsync();

            Tenant.ProductStockMovements.RemoveRange(stockMovements);
        }
    }
}
