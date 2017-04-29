// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Dyoub.App.Results.Catalog.PricingTables
{
    public class ItemPriceListJson : JsonResult
    {
        public IEnumerable<ItemPrice> ItemPrices;

        public ItemPriceListJson(IEnumerable<ItemPrice> itemPrices)
        {
            ItemPrices = itemPrices;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = ItemPrices.Select(item => new
            {
                id = item.ItemId,
                isProduct = item.IsProduct,
                isService = item.IsService,
                name = item.Name,
                code = item.Code,
                marketed = item.Marketed,
                unitPrice = item.UnitPrice,
                canFraction = item.CanFraction
            });

            base.ExecuteResult(context);
        }
    }
}
