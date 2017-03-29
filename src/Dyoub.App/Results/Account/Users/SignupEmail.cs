// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;

namespace Dyoub.App.Models.Results.Account.Users
{
    public class SignupEmail
    {
        public User User { get; private set; }
        public ClosureRequest ClosureRequest { get; private set; }

        public SignupEmail(User user, ClosureRequest closureRequest)
        {
            User = user;
            ClosureRequest = closureRequest;
        }
    }
}
