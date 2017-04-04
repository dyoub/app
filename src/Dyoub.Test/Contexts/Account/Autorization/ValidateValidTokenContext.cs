// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;

namespace Dyoub.Test.Contexts.Account.Autorization
{
    public class ValidateValidTokenContext : InMemoryContext
    {
        public User User { get; private set; }

        public ValidateValidTokenContext()
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.AuthenticatedUser(tenant));

            SaveChanges();
        }
    }
}
