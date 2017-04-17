// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;

namespace Dyoub.App.Models.EntityModel.Catalog.ServicePrices
{
    public class ServicePrice : ITenantData
    {
        public int StoreId { get; set; }
        public int ServiceId { get; set; }
        public int TenantId { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Store Store { get; set; }
        public virtual Service Service { get; set; }
    }
}
