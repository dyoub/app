// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class RecoveryValidEmailContext : InMemoryContext
    {
        public User User { get; private set; }

        public RecoveryValidEmailContext()
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
