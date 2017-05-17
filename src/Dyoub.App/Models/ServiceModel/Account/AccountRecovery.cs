// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.PasswordRecoveries;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Infrastructure.Security;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Account
{
    public class AccountRecovery
    {
        public ApplicationContext Context { get; private set; }
        public PasswordRecovery PasswordRecovery { get; private set; }

        public AccountRecovery(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<bool> Recovery(string email)
        {
            if (await Context.Users.WhereEmail(email).AnyAsync())
            {
                PasswordRecovery = new PasswordRecovery();
                PasswordRecovery.Token = Guid.NewGuid().ToString("N");
                PasswordRecovery.Email = email;
                PasswordRecovery.RequestDate = DateTime.UtcNow;
                PasswordRecovery.ExpiryDate = PasswordRecovery.RequestDate.AddHours(PasswordRecovery.TokenExpiryTime);

                Context.PasswordRecoveries.Add(PasswordRecovery);
                await Context.SaveChangesAsync();
            }

            return PasswordRecovery != null;
        }
        
        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            PasswordRecovery = await Context.PasswordRecoveries
                .NotExpiredByDate().WhereToken(token).SingleOrDefaultAsync();

            if (PasswordRecovery != null)
            {
                User user = await Context.Users
                    .WhereEmail(PasswordRecovery.Email)
                    .SingleOrDefaultAsync();

                if (user != null && user.LastChangePassword < PasswordRecovery.RequestDate)
                {
                    user.Salt = Guid.NewGuid().ToString("N");
                    user.Password = new Sha256Hash(newPassword, user.Salt).ToString();
                    user.LastChangePassword = DateTime.UtcNow;

                    await Context.SaveChangesAsync();
                }
            }

            return PasswordRecovery != null;
        }
    }
}
