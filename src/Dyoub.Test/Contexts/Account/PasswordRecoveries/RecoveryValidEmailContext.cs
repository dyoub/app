// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.PasswordRecoveries;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class RecoveryValidEmailContext : ApplicationContext
    {
        public User User { get; private set; }

        public RecoveryValidEmailContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));

            SaveChanges();
        }

        public bool RecoveryTokenForUserWasCreated()
        {
            return PasswordRecoveries.AsNoTracking().WhereEmail(User.Email).Any();
        }
    }
}
