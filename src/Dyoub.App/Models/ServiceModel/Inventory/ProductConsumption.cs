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
    public class ProductConsumption
    {
        public TenantContext Tenant { get; private set; }
        public IOutgoingOrder Order { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductConsumption(TenantContext tenant, IOutgoingOrder order)
        {
            Tenant = tenant;
            Order = order;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = Order.OutgoingList.Select(p => p.ProductId);

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

        private void CheckQuantityAvailableFor(IOutgoingProduct outgoing)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == outgoing.ProductId);

            InsufficientBalance = 
                productQuantity == null ||
                productQuantity.TotalAvailable < outgoing.Quantity;
        }

        private void RegisterStockMovementFor(IOutgoingProduct outgoing)
        {
            outgoing.StockTransactionId = Guid.NewGuid();

            Tenant.ProductStockMovements.Add(new ProductStockMovement
            {
                StoreId = Order.StoreId,
                Date = Order.Date,
                TransactionId = outgoing.StockTransactionId.Value,
                ProductId = outgoing.ProductId,
                Quantity = outgoing.Quantity * (-1)
            });
        }

        public async Task<bool> Confirm()
        {
            await RetrieveProductsAndQuantities();
            
            foreach (Product product in Products)
            {
                if (product.StockMovement)
                {
                    IOutgoingProduct outgoing = Order.OutgoingList
                        .Single(p => p.ProductId == product.Id);

                    CheckQuantityAvailableFor(outgoing);

                    if (InsufficientBalance) return false;

                    RegisterStockMovementFor(outgoing);
                }
            }

            return true;
        }

        public async Task Revert()
        {
            IEnumerable<Guid> stockTransactionIds = Order.OutgoingList
                .Where(outgoing => outgoing.StockTransactionId != null)
                .Select(outgoing => outgoing.StockTransactionId.Value)
                .ToList();

            foreach (IOutgoingProduct outgoing in Order.OutgoingList)
            {
                outgoing.StockTransactionId = null;
            }

            ICollection<ProductStockMovement> stockMovements = await Tenant.ProductStockMovements
                .WhereTransactionIdIn(stockTransactionIds)
                .ToListAsync();

            Tenant.ProductStockMovements.RemoveRange(stockMovements);
        }
    }
}
