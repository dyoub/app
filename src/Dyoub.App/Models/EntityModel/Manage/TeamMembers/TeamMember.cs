// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Manage.Teams;

namespace Dyoub.App.Models.EntityModel.Manage.TeamMembers
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
