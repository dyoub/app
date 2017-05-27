using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchasedProducts
{
    public class PurchasedProductListJson : JsonResult
    {
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchasedProductListJson(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrder == null ? null : new
            {
                id = PurchaseOrder.Id,
                storeId = PurchaseOrder.StoreId,
                confirmed = PurchaseOrder.Confirmed,
                productList = PurchaseOrder.PurchasedProducts.Select(purchasedProduct => new
                {
                    id = purchasedProduct.Product.Id,
                    name = purchasedProduct.Product.Name,
                    code = purchasedProduct.Product.Code,
                    quantity = purchasedProduct.Quantity,
                    unitCost = purchasedProduct.UnitCost,
                    total = purchasedProduct.Total,
                    discount = purchasedProduct.Discount,
                    totalCost = purchasedProduct.TotalCost
                })
            };

            base.ExecuteResult(context);
        }
    }
}
