// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Financial.FixedExpenses
{
    public class UpdateFixedExpenseContext : TenantContext
    {
        private FixedExpense original;

        public FixedExpense FixedExpense { get; private set; }
        public Store AnotherStore { get; private set; }

        public UpdateFixedExpenseContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            AnotherStore = Stores.Add(StoreFactory.Store(tenant));

            FixedExpense = FixedExpenses.Add(FixedExpenseFactory.FixedExpense(store));
            original = FixedExpenseFactory.FixedExpense(store);

            SaveChanges();
        }

        public bool FixedExpenseWasUpdated()
        {
            Entry(FixedExpense).Reload();

            return FixedExpense.StoreId != original.StoreId &&
                   FixedExpense.Description != original.Description &&
                   FixedExpense.StartDate != original.StartDate &&
                   FixedExpense.EndDate != original.EndDate &&
                   FixedExpense.Value != original.Value;
        }
    }
}
