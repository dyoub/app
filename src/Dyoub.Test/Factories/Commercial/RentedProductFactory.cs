// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;

namespace Dyoub.Test.Factories.Commercial
{
    public class RentedProductFactory
    {
        public static RentedProduct RentedProduct(RentContract rentContract, Product product)
        {
            return new RentedProduct
            {
                RentContract = rentContract,
                Product = product,
                Tenant = rentContract.Tenant,
                Quantity = 1,
                UnitPrice = 100.00M,
                Total = 100.00M,
                Discount = null,
                TotalPayable = 100.00M
            };
        }
    }
}
