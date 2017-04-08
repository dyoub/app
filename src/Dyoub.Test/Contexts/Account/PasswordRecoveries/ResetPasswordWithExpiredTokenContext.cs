// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using Effort;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class ResetPasswordWithExpiredTokenContext : ApplicationContext
    {
        private string oldPassword;

        public User User { get; private set; }
        public PasswordRecovery PasswordRecovery { get; private set; }

        public ResetPasswordWithExpiredTokenContext() : base(DbConnectionFactory.CreateTransient())
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
