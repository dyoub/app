// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Catalog.PricingTables
{
    public class ProductPriceListJson : JsonResult
    {
        public IEnumerable<ProductPrice> ProductPrices;

        public ProductPriceListJson(IEnumerable<ProductPrice> productPrices)
        {
            ProductPrices = productPrices;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = ProductPrices.Select(productPrice => new
            {
                id = productPrice.Product.Id,
                name = productPrice.Product.Name,
                code = productPrice.Product.Code,
                marketed = productPrice.Product.Marketed,
                unitPrice = productPrice.UnitRentPrice,
                canFraction = productPrice.Product.CanFraction
            });

            base.ExecuteResult(context);
        }
    }
}
