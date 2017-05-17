// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Infrastructure.Security;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Account
{
    public class UserAuthentication
    {
        public ApplicationContext Context { get; private set; }
        public User User { get; private set; }

        public UserAuthentication(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            User = await Context.Users.WhereId(userId).SingleOrDefaultAsync();

            Sha256Hash oldPasswordHash = new Sha256Hash(oldPassword, User.Salt);

            if (User.Password != oldPasswordHash.ToString()) return false;

            User.Salt = Guid.NewGuid().ToString("N");
            User.Password = new Sha256Hash(newPassword, User.Salt).ToString();
            User.LastChangePassword = DateTime.UtcNow;

            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SigninAsync(string email, string password)
        {
            User = await Context.Users
                .WhereEmail(email)
                .SingleOrDefaultAsync();

            if (User != null)
            {
                Sha256Hash passwordHash = new Sha256Hash(password, User.Salt);

                if (User.Password == passwordHash.ToString())
                {
                    User.Token = new AccessToken().ToString();
                    User.LastLogin = DateTime.UtcNow;

                    await Context.SaveChangesAsync();
                }
            }

            return User != null && User.Token != null;
        }

        public async Task<bool> SignoutAsync(string token)
        {
            User = await Context.Users
                .WhereToken(token)
                .SingleOrDefaultAsync();

            if (User != null)
            {
                User.Token = null;
                await Context.SaveChangesAsync();
            }

            return User != null;
        }
    }
}
