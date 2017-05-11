// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;

namespace Dyoub.Test.Factories.Inventory
{
    public class PurchasedProductFactory
    {
        public static PurchasedProduct PurchasedProduct(PurchaseOrder purchaseOrder, Product product)
        {
            return new PurchasedProduct
            {
                PurchaseOrder = purchaseOrder,
                Product = product,
                Tenant = purchaseOrder.Tenant,
                Quantity = 1,
                UnitCost = 5.90M,
                Total = 5.90M,
                Discount = null,
                TotalCost = 5.90M
            };
        }
    }
}
