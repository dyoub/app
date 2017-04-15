// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;

namespace Dyoub.Test.Contexts.Account.Autorization
{
    public class ValidateValidTokenContext : ApplicationContext
    {
        public User User { get; private set; }

        public ValidateValidTokenContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.AuthenticatedUser(tenant));

            SaveChanges();
        }
    }
}
