// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog;
using System.Web.Mvc;

namespace Dyoub.App.Results.Catalog.Products
{
    public class ProductJson : JsonResult
    {
        public Product Product { get; private set; }

        public ProductJson(Product product)
        {
            Product = product;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Product == null ? null : new
            {
                id = Product.Id,
                name = Product.Name,
                code = Product.Code,
                marketed = Product.Marketed,
                isManufactured = Product.IsManufactured,
                stockMovement = Product.StockMovement,
                canFraction = Product.CanFraction,
                additionalInformation = Product.AdditionalInformation
            };

            base.ExecuteResult(context);
        }
    }
}
