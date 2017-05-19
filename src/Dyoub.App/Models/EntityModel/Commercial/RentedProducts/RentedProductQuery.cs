// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.RentedProducts
{
    public static class RentedProductQuery
    {
        public static IQueryable<RentedProduct> IncludeProduct(this IQueryable<RentedProduct> rentedProducts)
        {
            return rentedProducts.Include(rentedProduct => rentedProduct.Product);
        }

        public static IQueryable<RentedProduct> WhereProductId(this IQueryable<RentedProduct> rentedProducts, int? productId)
        {
            return rentedProducts.Where(rentedProduct => rentedProduct.ProductId == productId.Value);
        }

        public static IQueryable<RentedProduct> WhereRentContractId(this IQueryable<RentedProduct> rentedProducts, int rentContractId)
        {
            return rentedProducts.Where(rentedProduct => rentedProduct.RentContractId == rentContractId);
        }
    }
}
