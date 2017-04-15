// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Manage.Stores
{
    public class DeleteStoreContext : TenantContext
    {
        public Store Store { get; private set; }

        public DeleteStoreContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store = Stores.Add(StoreFactory.Store(tenant));

            SaveChanges();
        }

        public bool StoreWasDeleted()
        {
            return !Stores.Any();
        }
    }
}
