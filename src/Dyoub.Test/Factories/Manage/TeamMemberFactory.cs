// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Manage.Teams;

namespace Dyoub.Test.Factories.Manage
{
    public class TeamMemberFactory
    {
        public static TeamMember TeamMember(User user, Store store, Team team, Tenant tenant)
        {
            return new TeamMember
            {
                User = user,
                Store = store,
                Team = team,
                Tenant = tenant
            };
        }
    }
}
