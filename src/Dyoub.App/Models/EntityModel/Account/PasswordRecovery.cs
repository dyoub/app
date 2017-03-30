// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class PasswordRecovery
    {
        public const int TokenExpiryTime = 48;

        [Key]
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
