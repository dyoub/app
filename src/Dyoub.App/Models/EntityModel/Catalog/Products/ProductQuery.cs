// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
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

        public static IQueryable<Product> OrderByName(this IQueryable<Product> products)
        {
            return products.OrderBy(product => product.Name);
        }

        public static IQueryable<Product> WhereCode(this IQueryable<Product> products, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return products;
            }

            return products.Where(product => product.Code == code);
        }

        public static IQueryable<Product> WhereId(this IQueryable<Product> products, int id)
        {
            return products.Where(product => product.Id == id);
        }

        public static IQueryable<Product> WhereNameContains(this IQueryable<Product> products, params string[] words)
        {
            foreach (string word in words)
            {
                products = products.Where(product => product.Name.Contains(word));
            }

            return products;
        }
    }
}
