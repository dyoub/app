// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Teams;

namespace Dyoub.Test.Factories.Manage
{
    public class TeamFactory
    {
        public static Team Team(Tenant tenant)
        {
            return new Team
            {
                Name = "Team Test",
                Tenant = tenant
            };
        }
    }
}
