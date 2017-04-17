// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Services;

namespace Dyoub.Test.Factories.Catalog
{
    public class ServiceFactory
    {
        public static Service Service(Tenant tenant)
        {
            return new Service
            {
                Name = "Service Test",
                Marketed = true,
                Tenant = tenant,
            };
        }
    }
}
