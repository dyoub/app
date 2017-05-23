// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial;
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
        public ICommercialDocument Document { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductConsumption(TenantContext tenant, ICommercialDocument document)
        {
            Tenant = tenant;
            Document = document;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = Document.MarketedProducts.Select(p => p.ProductId);

            Products = await Tenant.Products
                .WhereIdIn(productIds)
                .ToListAsync();

            ProductQuantities = await Tenant.ProductStockMovements
                .WhereStoreId(Document.StoreId)
                .WhereProductIdIn(productIds)
                .WhereUntilDate(Document.Date)
                .AsProductQuantity()
                .ToListAsync();
        }

        private void CheckQuantityAvailableOf(IMarketedProduct MarketedProduct)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == MarketedProduct.ProductId);

            InsufficientBalance = 
                productQuantity == null ||
                productQuantity.TotalAvailable < MarketedProduct.Quantity;
        }

        private void RegisterStockMovementFor(IMarketedProduct marketedProduct)
        {
            marketedProduct.StockTransactionId = Guid.NewGuid();

            Tenant.ProductStockMovements.Add(new ProductStockMovement
            {
                TransactionId = marketedProduct.StockTransactionId.Value,
                StoreId = Document.StoreId,
                ProductId = marketedProduct.ProductId,
                Date = Document.Date,
                Quantity = marketedProduct.Quantity * (-1)
            });
        }

        public async Task<bool> Confirm()
        {
            await RetrieveProductsAndQuantities();
            
            foreach (Product product in Products)
            {
                if (product.StockMovement)
                {
                    IMarketedProduct marketedProduct = Document.MarketedProducts
                        .Single(p => p.ProductId == product.Id);

                    CheckQuantityAvailableOf(marketedProduct);

                    if (InsufficientBalance) return false;

                    RegisterStockMovementFor(marketedProduct);
                }
            }

            return true;
        }

        public async Task Revert()
        {
            IEnumerable<Guid> stockTransactionIds = Document.MarketedProducts
                .Where(marketedProduct => marketedProduct.StockTransactionId != null)
                .Select(marketedProduct => marketedProduct.StockTransactionId.Value)
                .ToList();

            foreach (IMarketedProduct marketedProduct in Document.MarketedProducts)
            {
                marketedProduct.StockTransactionId = null;
            }

            ICollection<ProductStockMovement> stockMovements = await Tenant.ProductStockMovements
                .WhereTransactionIdIn(stockTransactionIds)
                .ToListAsync();

            Tenant.ProductStockMovements.RemoveRange(stockMovements);
        }
    }
}
