// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Account.PasswordRecoveries
{
    public static class PasswordRecoveryQuery
    {
        public static IQueryable<PasswordRecovery> NotExpiredByDate(this IQueryable<PasswordRecovery> passwordRecoveries)
        {
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.ExpiryDate > DateTime.UtcNow);
        }

        public static IQueryable<PasswordRecovery> WhereToken(this IQueryable<PasswordRecovery> passwordRecoveries, string token)
        {
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.Token == token);
        }

        public static IQueryable<PasswordRecovery> WhereEmail(this IQueryable<PasswordRecovery> passwordRecoveries, string email)
        {
            return passwordRecoveries.Where(passwordRecovery => passwordRecovery.Email == email);
        }
    }
}
