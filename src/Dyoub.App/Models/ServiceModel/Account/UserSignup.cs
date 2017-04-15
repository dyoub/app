// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Account.ClosureRequests;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Infrastructure.Security;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Account
{
    public class UserSignup
    {
        public ApplicationContext Context { get; private set; }
        public User User { get; private set; }
        public ClosureRequest ClosureRequest { get; private set; }
        public bool EmailAlreadyTaken { get; private set; }

        public UserSignup(ApplicationContext context, User user)
        {
            Context = context;
            User = user;
        }

        public async Task SignupAsync()
        {
            EmailAlreadyTaken = await Context.Users.WhereEmail(User.Email).AnyAsync();

            if (!EmailAlreadyTaken)
            {
                User.Token = new AccessToken().ToString();
                User.LastLogin = DateTime.Now;
                User.Salt = Guid.NewGuid().ToString("N");
                User.Password = new Sha256Hash(User.Password, User.Salt).ToString();
                User.LastChangePassword = DateTime.Now;
                User.Tenant = new Tenant();
                User.Tenant.Owner = User.Email;
                User.Tenant.CreatedAt = DateTime.Now;

                ClosureRequest = new ClosureRequest();
                ClosureRequest.Token = Guid.NewGuid().ToString("N");
                ClosureRequest.Email = User.Email;
                ClosureRequest.RequestDate = User.LastLogin;
                ClosureRequest.ExpiryDate = ClosureRequest.RequestDate
                    .AddHours(ClosureRequest.TokenExpiryTime);

                Context.Users.Add(User);
                Context.ClosureRequests.Add(ClosureRequest);

                await Context.SaveChangesAsync();
            }
        }
    }
}
