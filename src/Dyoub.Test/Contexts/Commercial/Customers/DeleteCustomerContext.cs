// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.Customers
{
    public class DeleteCustomerContext : TenantContext
    {
        public Customer Customer { get; private set; }

        public DeleteCustomerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Customer = Customers.Add(CustomerFactory.Customer(tenant));

            SaveChanges();
        }

        public bool CustomerWasDeleted()
        {
            return !Customers.Any();
        }
    }
}
