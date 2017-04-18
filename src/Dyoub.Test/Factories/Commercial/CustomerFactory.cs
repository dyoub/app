// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Customers;

namespace Dyoub.Test.Factories.Commercial
{
    public class CustomerFactory
    {
        public static Customer Customer(Tenant tenant)
        {
            return new Customer
            {
                Name = "Customer Test",
                Email = "customer@email.com",
                NationalId = "123456",
                PhoneNumber = "99 99999-9999",
                AlternativePhoneNumber = "99 88888-8888",
                Tenant = tenant,
            };
        }
    }
}
