// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Profile
{
    public class ChangePasswordContext : ApplicationContext
    {
        private User original;

        public User User { get; private set; }

        public ChangePasswordContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));

            original = UserFactory.User(tenant);

            SaveChanges();
        }

        public bool PasswordWasChanged()
        {
            Entry(User).Reload();

            return User.Password != original.Password &&
                   User.Salt != original.Salt &&
                   User.LastChangePassword != original.LastChangePassword;
        }
    }
}
