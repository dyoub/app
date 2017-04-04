// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class TeamMember : ITenantData
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int TeamId { get; set; }
        public int StoreId { get; set; }
        
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
        public virtual Store Store { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
