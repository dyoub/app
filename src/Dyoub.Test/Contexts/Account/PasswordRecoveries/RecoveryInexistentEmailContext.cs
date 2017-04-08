// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.PasswordRecoveries
{
    public class RecoveryInexistentEmailContext : ApplicationContext
    {
        public User UnregisteredUser { get; private set; }

        public RecoveryInexistentEmailContext() : base(DbConnectionFactory.CreateTransient())
        {
            UnregisteredUser = UserFactory.User();
        }

        public bool RecoveryTokenWasNotCreated()
        {
            return !PasswordRecoveries.AsNoTracking().WhereEmail(UnregisteredUser.Email).Any();
        }
    }
}
