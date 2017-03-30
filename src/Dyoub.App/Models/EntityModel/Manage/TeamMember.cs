// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class TeamMember
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        [Key, Column(Order = 2)]
        public int TeamId { get; set; }

        [Key, Column(Order = 3)]
        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
    }
}
