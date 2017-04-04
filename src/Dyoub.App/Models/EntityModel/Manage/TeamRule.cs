// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class TeamRule : ITenantData
    {
        public int TeamId { get; set; }
        public int TenantId { get; set; }
        public string Scope { get; set; }

        public virtual Team Team { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
