// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.TeamRules;
using Dyoub.App.Models.EntityModel.Manage.Teams;

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
