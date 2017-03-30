// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class Team
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<TeamRole> TeamRoles { get; set; }
    }
}
