// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Manage;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Manage.Stores
{
    public class UpdateStoreContext : TenantContext
    {
        private Store original;

        public Store Store { get; private set; }

        public UpdateStoreContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store = Stores.Add(StoreFactory.Store(tenant));

            original = StoreFactory.Store(tenant);

            SaveChanges();
        }

        public bool StoreWasUpdated()
        {
            Entry(Store).Reload();

            return Store.Name != original.Name &&
                   Store.Active != original.Active;
        }
    }
}
