// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using System;

namespace Dyoub.Test.Factories.Account
{
    public class TenantFactory
    {
        public static Tenant Tenant(string owner = "tenant@email.com")
        {
            return new Tenant
            {
                Owner = owner,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
