// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Manage.TeamRules;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Manage.Teams
{
    public class Team : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamRule> TeamRules { get; set; }
    }
}
