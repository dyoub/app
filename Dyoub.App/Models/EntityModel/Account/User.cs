// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class User
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

        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
    }
}
