// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Security
{
    public class UserIdentity
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool HasFullAccess { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public bool HasRole(string role)
        {
            if (HasFullAccess)
            {
                return true;
            }

            return Roles != null && Roles.Contains(role);
        }
    }
}