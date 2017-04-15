// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.ClosureRequests;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Account.Users
{
    public class SignupCorrectlyContext : ApplicationContext
    {
        public User User { get; private set; }

        public SignupCorrectlyContext() : base(DbConnectionFactory.CreateTransient())
        {
            User = UserFactory.User();
        }

        public bool TheUserWasCreated()
        {
            return Users.WhereEmail(User.Email).Any();
        }

        public bool ATenantWasCreatedForTheUser()
        {
            return Tenants.WhereOwner(User.Email).Any();
        }

        public bool AClosureRequestWasCreatedForTheUser()
        {
            return ClosureRequests.WhereEmail(User.Email).Any();
        }
    }
}
