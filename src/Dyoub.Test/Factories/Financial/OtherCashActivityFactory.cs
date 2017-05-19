// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Financial
{
    public class OtherCashActivityFactory
    {
        public static OtherCashActivity OtherCashActivity(Store store)
        {
            return new OtherCashActivity
            {
                Description = "Other Cash Activity Test",
                Date = DateTime.UtcNow.Date,
                Value = 25.90M,
                Store = store,
                Tenant = store.Tenant,
            };
        }
    }
}
