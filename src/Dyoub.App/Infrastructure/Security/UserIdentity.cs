// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Infrastructure.Security
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsOwner { get; set; }
        public IEnumerable<TeamRule> TeamRules { get; set; }

        public bool HasPermission(string scope)
        {
            if (IsOwner || string.IsNullOrEmpty(scope))
            {
                return true;
            }

            return TeamRules != null &&
                TeamRules.Any(teamRule => teamRule.Scope == scope);
        }
    }
}
