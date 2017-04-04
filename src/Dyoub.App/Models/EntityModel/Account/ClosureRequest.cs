// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class ClosureRequest
    {
        public const int TokenExpiryTime = 48;
        
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
