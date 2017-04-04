// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Manage;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Manage;

namespace Dyoub.Test.Contexts.Account.Autorization
{
    public class AccessWithScopePermissionContext : InMemoryContext
    {
        public Tenant Tenant { get; private set; }
        public User User { get; private set; }
        public TeamRule TeamRule { get; private set; }

        public AccessWithScopePermissionContext()
        {
            Tenant = Tenants.Add(TenantFactory.Tenant());
            User = Users.Add(UserFactory.AuthenticatedUser(Tenant));
            Store store = Stores.Add(StoreFactory.Store(Tenant));
            Team team = Teams.Add(TeamFactory.Team(Tenant));
            TeamMembers.Add(TeamMemberFactory.TeamMember(User, store, team, Tenant));
            TeamRule = TeamRules.Add(TeamRuleFactory.TeamRule(team, Tenant));

            SaveChanges();
        }
    }
}
