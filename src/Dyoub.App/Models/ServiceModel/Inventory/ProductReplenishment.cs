// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory;
using Dyoub.App.Models.EntityModel.Inventory.ProductQuantities;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
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
        public IIncomingOrder Order { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool HasProductThatNotMovementStock { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductReplenishment(TenantContext tenant, IIncomingOrder order)
        {
            Tenant = tenant;
            Order = order;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = Order.IncomingList.Select(p => p.ProductId);

            Products = await Tenant.Products
                .WhereIdIn(productIds)
                .ToListAsync();

            ProductQuantities = await Tenant.ProductStockMovements
                .WhereStoreId(Order.StoreId)
                .WhereProductIdIn(productIds)
                .WhereUntilDate(Order.Date)
                .AsProductQuantity()
                .ToListAsync();
        }

        private void CheckQuantityAvailableForRevert(IIncomingProduct incoming)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == incoming.ProductId);

            InsufficientBalance =
                productQuantity == null ||
                productQuantity.TotalAvailable < incoming.Quantity;
        }

        public async Task<bool> Confirm()
        {
            Products = await Tenant.Products
                .WhereIdIn(Order.IncomingList.Select(p => p.ProductId))
                .ToListAsync();

            foreach (Product product in Products)
            {
                if (HasProductThatNotMovementStock = !product.StockMovement) return false;

                IIncomingProduct incoming = Order.IncomingList
                    .Single(p => p.ProductId == product.Id);

                incoming.StockTransactionId = Guid.NewGuid();

                Tenant.ProductStockMovements.Add(new ProductStockMovement
                {
                    StoreId = Order.StoreId,
                    Date = Order.Date,
                    TransactionId = incoming.StockTransactionId.Value,
                    ProductId = incoming.ProductId,
                    Quantity = incoming.Quantity
                });
            }

            return true;
        }

        public async Task<bool> Revert()
        {
            await RetrieveProductsAndQuantities();

            IEnumerable<Guid> stockTransactionIds = Order.IncomingList
                .Where(incoming => incoming.StockTransactionId != null)
                .Select(incoming => incoming.StockTransactionId.Value)
                .ToList();

            foreach (IIncomingProduct incoming in Order.IncomingList)
            {
                incoming.StockTransactionId = null;

                CheckQuantityAvailableForRevert(incoming);

                if (InsufficientBalance) return false;
            }

            ICollection<ProductStockMovement> stockMovements = await Tenant.ProductStockMovements
                .WhereTransactionIdIn(stockTransactionIds)
                .ToListAsync();

            Tenant.ProductStockMovements.RemoveRange(stockMovements);

            return true;
        }
    }
}
