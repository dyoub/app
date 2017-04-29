// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;

namespace Dyoub.Test.Factories.Commercial
{
    public class SaleProductFactory
    {
        public static SaleProduct SaleProduct(SaleOrder saleOrder, Product product)
        {
            return new SaleProduct
            {
                SaleOrder = saleOrder,
                Product = product,
                Tenant = saleOrder.Tenant,
                Quantity = 1,
                UnitPrice = 10.90M,
                Total = 10.90M,
                TotalPayable = 10.90M
            };
        }
    }
}
