// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Products;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Catalog.Products
{
    public class ProductListJson : JsonResult
    {
        public IEnumerable<Product> Products { get; private set; }

        public ProductListJson(IEnumerable<Product> products)
        {
            Products = products;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Products.Select(product => new
            {
                id = product.Id,
                name = product.Name,
                code = product.Code,
                marketed = product.Marketed,
                additionalInformation = product.AdditionalInformation
            });

            base.ExecuteResult(context);
        }
    }
}
