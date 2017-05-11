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
        public IEnumerable<PurchasedProduct> PurchasedProducts { get; private set; }

        public PurchasedProductListJson(PurchaseOrder purchaseOrder, IEnumerable<PurchasedProduct> puchasedProducts)
        {
            PurchaseOrder = purchaseOrder;
            PurchasedProducts = puchasedProducts;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrder == null ? null : new
            {
                id = PurchaseOrder.Id,
                storeId = PurchaseOrder.StoreId,
                confirmed = PurchaseOrder.Confirmed,
                productList = PurchasedProducts.Select(purchasedProduct => new
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
