// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class ResetPasswordWithValidTokenContext : ApplicationContext
    {
        private string oldPassword;

        public User User { get; private set; }
        public PasswordRecovery PasswordRecovery { get; private set; }

        public ResetPasswordWithValidTokenContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));
            PasswordRecovery = PasswordRecoveries.Add(PasswordRecoveryFactory.PasswordRecovery(User));

            oldPassword = User.Password;

            SaveChanges();
        }

        public bool UserPasswordWasChanged()
        {
            return User.Password != oldPassword;
        }
    }
}
