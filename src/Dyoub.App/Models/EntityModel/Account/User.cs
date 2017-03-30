// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class User
    {
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        [Key, Column(Order = 2)]
        public int TenantId { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastChangePassword { get; set; }
        
        public virtual Tenant Tenant { get; set; }
    }
}
