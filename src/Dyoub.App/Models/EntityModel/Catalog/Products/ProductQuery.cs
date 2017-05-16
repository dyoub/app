// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using Dyoub.App.Models.EntityModel.Inventory.ProductQuantities;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.Products
{
    public static class ProductQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<Product> products, int storeId)
        {
            return products.Select(product => new ItemPrice
            {
                TenantId = product.TenantId,
                StoreId = storeId,
                ProductId = product.Id,
                ServiceId = null,
                Name = product.Name,
                Code = product.Code,
                Marketed = product.Marketed,
                CanFraction = product.CanFraction,
                UnitPrice = product.ProductPrices
                    .Where(productPrice => productPrice.StoreId == storeId)
                    .Select(productPrice => (decimal?)productPrice.UnitPrice)
                    .FirstOrDefault()
            });
        }

        public static IQueryable<ProductQuantity> AsProductQuantity(this IQueryable<Product> products, int storeId)
        {
            return products
                .WhereStockMovement(true)
                .Select(product => new ProductQuantity
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Marketed = product.Marketed,
                    TotalAvailable = product.ProductStockMovements
                        .Where(stock => stock.StoreId == storeId)
                        .Sum(stock => (decimal?)stock.Quantity) ?? 0
                });
        }

        public static IQueryable<Product> OrderByName(this IQueryable<Product> products)
        {
            return products.OrderBy(product => product.Name);
        }
        
        public static IQueryable<Product> WhereNameOrCode(this IQueryable<Product> products, params string[] words)
        {
            if (words.Count() == 1)
            {
                string word = words.First();
                return products.Where(product => product.Code == word || product.Name.Contains(word));
            }

            foreach (string word in words)
            {
                products = products.Where(product => product.Name.Contains(word));
            }

            return products;
        }

        public static IQueryable<Product> WhereId(this IQueryable<Product> products, int id)
        {
            return products.Where(product => product.Id == id);
        }

        public static IQueryable<Product> WhereIdIn(this IQueryable<Product> products, IEnumerable<int> ids)
        {
            return products.Where(product => ids.Contains(product.Id));
        }
        
        public static IQueryable<Product> WhereStockMovement(this IQueryable<Product> products, bool? stockMovement)
        {
            if (stockMovement == null)
            {
                return products;
            }
            
            return products.Where(product => product.StockMovement == stockMovement);
        }
    }
}
