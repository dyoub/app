// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.Customers
{
    public class UpdateCustomerContext : TenantContext
    {
        private Customer original;

        public Customer Customer { get; private set; }

        public UpdateCustomerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Customer = Customers.Add(CustomerFactory.Customer(tenant));

            original = CustomerFactory.Customer(tenant);

            SaveChanges();
        }

        public bool CustomerWasUpdated()
        {
            Entry(Customer).Reload();

            return Customer.Name != original.Name &&
                   Customer.NationalId != original.NationalId &&
                   Customer.Email != original.Email &&
                   Customer.PhoneNumber != original.PhoneNumber &&
                   Customer.AlternativePhoneNumber != original.AlternativePhoneNumber;
        }
    }
}
