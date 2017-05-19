// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.ProductPrices
{
    public static class ProductPriceQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.Select(productPrice => new ItemPrice
            {
                TenantId = productPrice.TenantId,
                StoreId = productPrice.StoreId,
                ProductId = productPrice.ProductId,
                ServiceId = null,
                Name = productPrice.Product.Name,
                Code = productPrice.Product.Code,
                Marketed = productPrice.Product.Marketed,
                CanFraction = productPrice.Product.CanFraction,
                UnitRentPrice = productPrice.UnitRentPrice,
                UnitSalePrice = productPrice.UnitSalePrice
            });
        }

        public static IQueryable<ProductPrice> IncludeProduct(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.Include(productPrice => productPrice.Product);
        }

        public static IQueryable<ProductPrice> OrderedByProductName(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.OrderBy(productPrice => productPrice.Product.Name);
        }

        public static IQueryable<ProductPrice> WhereProductNameOrCode(this IQueryable<ProductPrice> productPrices, string[] words)
        {
            if (words.Count() == 1)
            {
                string word = words.First();

                return productPrices.Where(productPrice =>
                    productPrice.Product.Code == word ||
                    productPrice.Product.Name.Contains(word));
            }

            foreach (string word in words)
            {
                productPrices = productPrices.Where(productPrice => productPrice.Product.Name.Contains(word));
            }

            return productPrices;
        }

        public static IQueryable<ProductPrice> WhereProductId(this IQueryable<ProductPrice> productPrices, int productId)
        {
            return productPrices.Where(productPrice => productPrice.ProductId == productId);
        }

        public static IQueryable<ProductPrice> WhereProductIdIn(this IQueryable<ProductPrice> productPrices, IEnumerable<int> productIds)
        {
            return productPrices.Where(productPrice => productIds.Contains(productPrice.ProductId));
        }

        public static IQueryable<ProductPrice> WhereProductMarketed(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.Where(productPrice => productPrice.Product.Marketed);
        }

        public static IQueryable<ProductPrice> WhereStoreId(this IQueryable<ProductPrice> productPrices, int storeId)
        {
            return productPrices.Where(productPrice => productPrice.StoreId == storeId);
        }

        public static IQueryable<ProductPrice> WhereUnitSalePriceNotNull(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.Where(productPrice => productPrice.UnitSalePrice != null);
        }

        public static IQueryable<ProductPrice> WhereUnitRentPriceNotNull(this IQueryable<ProductPrice> productPrices)
        {
            return productPrices.Where(productPrice => productPrice.UnitRentPrice != null);
        }
    }
}
