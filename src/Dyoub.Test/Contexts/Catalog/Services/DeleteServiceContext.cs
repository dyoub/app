// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Catalog.Services
{
    public class DeleteServiceContext : TenantContext
    {
        public Service Service { get; private set; }

        public DeleteServiceContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Service = Services.Add(ServiceFactory.Service(tenant));

            SaveChanges();
        }

        public bool ServiceWasDeleted()
        {
            return !Services.Any();
        }
    }
}
