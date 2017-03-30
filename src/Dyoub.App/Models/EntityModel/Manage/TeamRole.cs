// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class TeamRole
    {
        [Key, Column(Order = 1)]
        public int TeamId { get; set; }

        [Key, Column(Order = 2)]
        public int TenantId { get; set; }

        [Key, Column(Order = 3)]
        public string Role { get; set; }

        public bool CanEdit { get; set; }

        public virtual Team Team { get; set; }
    }
}
