// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Users
{
    public class SignupEmailAlreadyTakenContext : ApplicationContext
    {
        public User User { get; private set; }

        public SignupEmailAlreadyTakenContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));
            SaveChanges();
        }

        public bool HasOnlyOneUserWithThisEmail()
        {
            return Users.AsNoTracking().WhereEmail(User.Email).Count() == 1;
        }
    }
}
