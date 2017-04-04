// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Manage;

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
