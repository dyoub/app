// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage;
using System.Web.Mvc;

namespace Dyoub.App.Results.Manage.Stores
{
    public class StoreJson : JsonResult
    {
        public Store Store { get; private set; }

        public StoreJson(Store store)
        {
            Store = store;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Store != null)
            {
                Data = new
                {
                    id = Store.Id,
                    name = Store.Name,
                    active = Store.Active
                };
            }

            base.ExecuteResult(context);
        }
    }
}
