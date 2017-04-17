// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.Collections.Generic;
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
                UnitPrice = productPrice.UnitPrice
            });
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
    }
}
