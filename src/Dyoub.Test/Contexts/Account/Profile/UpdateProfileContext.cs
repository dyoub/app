// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.Test.Factories.Account;
using Effort;

namespace Dyoub.Test.Contexts.Account.Profile
{
    public class UpdateProfileContext : ApplicationContext
    {
        private User original;

        public User User { get; private set; }

        public UpdateProfileContext() : base(DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.User(tenant));

            original = UserFactory.User(tenant);

            SaveChanges();
        }

        public bool ProfileWasUpdated()
        {
            Entry(User).Reload();

            return User.Name != original.Name;
        }
    }
}
