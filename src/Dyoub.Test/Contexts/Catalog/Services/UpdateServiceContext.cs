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
    public class UpdateServiceContext : TenantContext
    {
        private Service original;

        public Service Service { get; private set; }

        public UpdateServiceContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Service = Services.Add(ServiceFactory.Service(tenant));

            original = ServiceFactory.Service(tenant);

            SaveChanges();
        }

        public bool ServiceWasUpdated()
        {
            Entry(Service).Reload();

            return Service.Name != original.Name &&
                   Service.Code != original.Code &&
                   Service.AdditionalInformation != original.AdditionalInformation &&
                   Service.CanFraction != original.CanFraction &&
                   Service.Marketed != original.Marketed;
        }
    }
}
