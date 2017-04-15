// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Manage.Stores
{
    public class ListStoreContext : TenantContext
    {
        public ListStoreContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Stores.Add(StoreFactory.Store(tenant));
            Stores.Add(StoreFactory.Store(tenant));

            SaveChanges();
        }
    }
}
