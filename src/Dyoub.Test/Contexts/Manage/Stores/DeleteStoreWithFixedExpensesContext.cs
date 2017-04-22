// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Manage.Stores
{
    public class DeleteStoreWithFixedExpensesContext : TenantContext
    {
        public Store Store { get; private set; }

        public DeleteStoreWithFixedExpensesContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store = Stores.Add(StoreFactory.Store(tenant));
            FixedExpenses.Add(FixedExpenseFactory.FixedExpense(Store));

            SaveChanges();
        }
    }
}
