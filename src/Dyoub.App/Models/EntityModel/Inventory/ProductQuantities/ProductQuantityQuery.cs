// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.ProductQuantities
{
    public static class ProductQuantityQuery
    {
        public static IQueryable<ProductQuantity> OrderByName(this IQueryable<ProductQuantity> products)
        {
            return products.OrderBy(product => product.Name);
        }
    }
}
