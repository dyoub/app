// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Manage;

namespace Dyoub.Test.Factories.Manage
{
    public class StoreFactory
    {
        public static Store Store(Tenant tenant)
        {
            return new Store
            {
                Name = "Store Test",
                Tenant = tenant
            };
        }
    }
}
