// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Users
{
    public class SigninCorrectlyContext : InMemoryContext
    {
        public User User { get; private set; }

        public SigninCorrectlyContext()
        {
            User = UserFactory.User();

            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Users.Add(UserFactory.User(tenant));
            SaveChanges();
        }
    }
}
