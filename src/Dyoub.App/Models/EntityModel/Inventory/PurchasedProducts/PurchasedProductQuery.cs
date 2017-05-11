// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts
{
    public static class PurchasedProductQuery
    {
        public static IQueryable<PurchasedProduct> IncludeProduct(this IQueryable<PurchasedProduct> purchasedProducts)
        {
            return purchasedProducts.Include(purchasedProduct => purchasedProduct.Product);
        }

        public static IQueryable<PurchasedProduct> WhereProductId(this IQueryable<PurchasedProduct> purchasedProducts, int? productId)
        {
            return purchasedProducts.Where(purchasedProduct => purchasedProduct.ProductId == productId.Value);
        }

        public static IQueryable<PurchasedProduct> WherePurchaseOrderId(this IQueryable<PurchasedProduct> purchasedProducts, int purchaseOrderId)
        {
            return purchasedProducts.Where(purchasedProduct => purchasedProduct.PurchaseOrderId == purchaseOrderId);
        }
    }
}
