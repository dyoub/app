// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.ServiceModel.Security;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Account
{
    public static class UserQuery
    {
        public static IQueryable<UserIdentity> AsUserIdentity(this IQueryable<User> users, string token)
        {
            return users.Where(user => user.Token == token)
                .Select(u => new UserIdentity
                {
                    UserId = u.Id,
                    TenantId = u.TenantId,
                    Email = u.Email,
                    Name = u.Name,
                    HasFullAccess = u.Email == u.Tenant.Owner
                    //Roles = 
                });
        }

        public static IQueryable<User> WhereEmail(this IQueryable<User> users, string email)
        {
            return users.Where(user => user.Email == email);
        }

        public static IQueryable<User> WhereId(this IQueryable<User> users, int id)
        {
            return users.Where(user => user.Id == id);
        }

        public static IQueryable<User> WhereToken(this IQueryable<User> users, string token)
        {
            return users.Where(user => user.Token == token);
        }
    }
}
