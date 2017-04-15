// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Account.Tenants
{
    public static class TenantQuery
    {
        public static IQueryable<Tenant> WhereId(this IQueryable<Tenant> tenants, int id)
        {
            return tenants.Where(tenant => tenant.Id == id);
        }

        public static IQueryable<Tenant> WhereOwner(this IQueryable<Tenant> tenants, string owner)
        {
            return tenants.Where(tenant => tenant.Owner == owner);
        }
    }
}
