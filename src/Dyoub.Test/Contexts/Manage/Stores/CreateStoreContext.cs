// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Manage.Stores
{
    public class CreateStoreContext : TenantContext
    {
        public CreateStoreContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant  tenant = Tenants.Add(TenantFactory.Tenant());
            SaveChanges();
        }

        public bool StoreWasCreated()
        {
            return Stores.Any();
        }
    }
}
