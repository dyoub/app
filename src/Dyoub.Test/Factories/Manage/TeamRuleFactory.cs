// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Manage;

namespace Dyoub.Test.Factories.Manage
{
    public class TeamRuleFactory
    {
        public static TeamRule TeamRule(Team team, Tenant tenant)
        {
            return new TeamRule
            {
                Scope = "scope.test",
                Team = team,
                Tenant = tenant
            };
        }
    }
}
