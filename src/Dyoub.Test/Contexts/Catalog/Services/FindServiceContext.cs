// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Effort;

namespace Dyoub.Test.Contexts.Catalog.Services
{
    public class FindServiceContext : TenantContext
    {
        public Service Service { get; private set; }

        public FindServiceContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Service = Services.Add(ServiceFactory.Service(tenant));

            SaveChanges();
        }
    }
}
