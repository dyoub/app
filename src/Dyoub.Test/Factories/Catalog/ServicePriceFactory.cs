// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;

namespace Dyoub.Test.Factories.Catalog
{
    public class ServicePriceFactory
    {
        public static ServicePrice ServicePrice(Store store, Service service, Tenant tenant)
        {
            return new ServicePrice
            {
                Store = store,
                Service = service,
                Tenant = tenant,
                UnitPrice = 10
            };
        }
    }
}
