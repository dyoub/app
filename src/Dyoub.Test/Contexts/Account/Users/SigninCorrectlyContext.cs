// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Users
{
    public class SigninCorrectlyContext : ApplicationContext
    {
        public User User { get; private set; }

        public SigninCorrectlyContext() : base(DbConnectionFactory.CreateTransient())
        {
            User = UserFactory.User();

            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Users.Add(UserFactory.User(tenant));
            SaveChanges();
        }

        public bool UserIsSiginedIn()
        {
            return Users.WhereEmail(User.Email).First().Token != null;
        }
    }
}
