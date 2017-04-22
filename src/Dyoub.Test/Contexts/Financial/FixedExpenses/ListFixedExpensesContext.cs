// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Financial.FixedExpenses
{
    public class ListFixedExpenseContext : TenantContext
    {
        public ListFixedExpenseContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            FixedExpenses.Add(FixedExpenseFactory.FixedExpense(store));
            FixedExpenses.Add(FixedExpenseFactory.FixedExpense(store));

            SaveChanges();
        }
    }
}
