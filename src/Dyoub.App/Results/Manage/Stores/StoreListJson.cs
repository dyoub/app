// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Manage.Stores
{
    public class StoreListJson : JsonResult
    {
        public IEnumerable<Store> Stores { get; private set; }

        public StoreListJson(IEnumerable<Store> stores)
        {
            Stores = stores;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Stores.Select(store => new
            {
                id = store.Id,
                name = store.Name,
                active = store.Active
            });

            base.ExecuteResult(context);
        }
    }
}
