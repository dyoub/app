// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class ResetPasswordWithExpiredTokenContext : InMemoryContext
    {
        private string oldPassword;

        public User User { get; private set; }
        public PasswordRecovery PasswordRecovery { get; private set; }

        public ResetPasswordWithExpiredTokenContext()
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));
            PasswordRecovery = PasswordRecoveries.Add(PasswordRecoveryFactory.PasswordRecoveryExpired(User));

            oldPassword = User.Password;

            SaveChanges();
        }

        public bool UserPasswordWasNotChanged()
        {
            return User.Password == oldPassword;
        }
    }
}
