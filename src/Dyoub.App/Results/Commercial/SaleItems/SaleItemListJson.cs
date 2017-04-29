using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SaleItems
{
    public class SaleItemListJson : JsonResult
    {
        public SaleOrder SaleOrder { get; private set; }
        public IEnumerable<SaleItem> Items { get; private set; }

        public SaleItemListJson(SaleOrder saleOrder, IEnumerable<SaleItem> items)
        {
            SaleOrder = saleOrder;
            Items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = SaleOrder == null ? null : new
            {
                id = SaleOrder.Id,
                storeId = SaleOrder.StoreId,
                confirmed = SaleOrder.Confirmed,
                itemList = Items.Select(item => new
                {
                    id = item.Id,
                    isService = item.IsService,
                    isProduct = item.IsProduct,
                    name = item.Name,
                    code = item.Code,
                    quantity = item.Quantity,
                    unitPrice = item.UnitPrice,
                    total = item.Total,
                    discount = item.Discount,
                    totalPayable = item.TotalPayable
                })
            };

            base.ExecuteResult(context);
        }
    }
}
