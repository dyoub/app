// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.ProductQuantities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.ProductStock
{
    public class ProductQuantityListJson : JsonResult
    {
        public IEnumerable<ProductQuantity> ProductQuantities { get; private set; }

        public ProductQuantityListJson(IEnumerable<ProductQuantity> productQuantities)
        {
            ProductQuantities = productQuantities;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = ProductQuantities.Select(productQuantity => new
            {
                name = productQuantity.Name,
                code = productQuantity.Code,
                marketed = productQuantity.Marketed,
                totalAvailable = productQuantity.TotalAvailable
            });

            base.ExecuteResult(context);
        }
    }
}
