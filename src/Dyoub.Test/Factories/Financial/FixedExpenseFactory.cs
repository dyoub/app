// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Financial
{
    public class FixedExpenseFactory
    {
        public static FixedExpense FixedExpense(Store store)
        {
            return new FixedExpense
            {
                Description = "FixedExpense Test",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date.AddMonths(1),
                Value = 25.90M,
                Store = store,
                Tenant = store.Tenant,
            };
        }
    }
}
