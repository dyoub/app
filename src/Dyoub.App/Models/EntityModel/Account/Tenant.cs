﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Manage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeactivatedAt { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        public virtual ICollection<TeamRole> TeamRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
