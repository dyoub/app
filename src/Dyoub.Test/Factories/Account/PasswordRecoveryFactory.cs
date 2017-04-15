// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.PasswordRecoveries;
using Dyoub.App.Models.EntityModel.Account.Users;
using System;

namespace Dyoub.Test.Factories.Account
{
    public class PasswordRecoveryFactory
    {
        public static PasswordRecovery PasswordRecovery(User user)
        {
            return new PasswordRecovery
            {
                Token = Guid.NewGuid().ToString("N"),
                Email = user.Email,
                ExpiryDate = DateTime.Today.AddDays(1),
                RequestDate = DateTime.Now
            };
        }

        public static PasswordRecovery PasswordRecoveryExpired(User user)
        {
            PasswordRecovery passwordRecovery = PasswordRecovery(user);
            passwordRecovery.ExpiryDate = user.LastChangePassword.AddDays(-1);

            return passwordRecovery;
        }
    }
}
