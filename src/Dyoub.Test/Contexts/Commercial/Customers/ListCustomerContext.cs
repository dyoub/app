// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.Customers
{
    public class ListCustomerContext : TenantContext
    {
        public ListCustomerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Customers.Add(CustomerFactory.Customer(tenant));
            Customers.Add(CustomerFactory.Customer(tenant));

            SaveChanges();
        }
    }
}
