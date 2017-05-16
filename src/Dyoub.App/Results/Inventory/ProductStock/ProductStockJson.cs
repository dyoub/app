// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage.Stores;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.ProductStock
{
    public class ProductStockJson : JsonResult
    {
        public Store Store { get; private set; }

        public ProductStockJson(Store store)
        {
            Store = store;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Store == null ? null : new
            {
                id = Store.Id,
                name = Store.Name
            };

            base.ExecuteResult(context);
        }
    }
}
