// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using System;

namespace Dyoub.App.Models.EntityModel.Account.Users
{
    public class User : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastChangePassword { get; set; }
        
        public virtual Tenant Tenant { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}
