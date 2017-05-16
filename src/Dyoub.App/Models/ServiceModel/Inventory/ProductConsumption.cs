// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
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
        public SaleOrder SaleOrder { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }
        public bool InsufficientBalance { get; set; }

        public ProductConsumption(TenantContext tenant, SaleOrder saleOrder)
        {
            Tenant = tenant;
            SaleOrder = saleOrder;
        }

        private async Task RetrieveProductsAndQuantities()
        {
            IEnumerable<int> productIds = SaleOrder.SaleProducts.Select(p => p.ProductId);

            Products = await Tenant.Products
                .WhereIdIn(productIds)
                .ToListAsync();

            ProductQuantities = await Tenant.ProductStockMovements
                .WhereStoreId(SaleOrder.StoreId)
                .WhereProductIdIn(productIds)
                .WhereUntilDate(SaleOrder.IssueDate)
                .AsProductQuantity()
                .ToListAsync();
        }

        private void CheckQuantityAvailableOf(SaleProduct saleProduct)
        {
            ProductQuantity productQuantity = ProductQuantities
                .SingleOrDefault(p => p.Id == saleProduct.ProductId);

            InsufficientBalance = 
                productQuantity == null ||
                productQuantity.TotalAvailable < saleProduct.Quantity;
        }

        private void RegisterStockTransactionFor(SaleProduct saleProduct)
        {
            saleProduct.StockTransactionId = Guid.NewGuid();

            Tenant.ProductStockMovements.Add(new ProductStockMovement
            {
                TransactionId = saleProduct.StockTransactionId.Value,
                StoreId = SaleOrder.StoreId,
                ProductId = saleProduct.ProductId,
                Date = SaleOrder.IssueDate,
                Quantity = saleProduct.Quantity * (-1)
            });
        }

        public async Task<bool> Confirm()
        {
            await RetrieveProductsAndQuantities();
            
            foreach (Product product in Products)
            {
                if (product.StockMovement)
                {
                    SaleProduct saleProduct = SaleOrder.SaleProducts
                        .Single(p => p.ProductId == product.Id);

                    CheckQuantityAvailableOf(saleProduct);

                    if (InsufficientBalance) return false;

                    RegisterStockTransactionFor(saleProduct);
                }
            }

            return true;
        }

        public async Task Revert()
        {
            IEnumerable<Guid> stockTransactionIds = SaleOrder.SaleProducts
                .Where(saleProduct => saleProduct.StockTransactionId != null)
                .Select(saleProduct => saleProduct.StockTransactionId.Value)
                .ToList();

            foreach (SaleProduct saleProduct in SaleOrder.SaleProducts)
            {
                saleProduct.StockTransactionId = null;
            }

            ICollection<ProductStockMovement> stockMovements = await Tenant.ProductStockMovements
                .WhereTransactionIdIn(stockTransactionIds)
                .ToListAsync();

            Tenant.ProductStockMovements.RemoveRange(stockMovements);
        }
    }
}
