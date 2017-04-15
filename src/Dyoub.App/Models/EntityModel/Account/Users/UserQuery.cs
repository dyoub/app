// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.Security;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Account.Users
{
    public static class UserQuery
    {
        public static IQueryable<UserIdentity> AsUserIdentity(this IQueryable<User> users, string token)
        {
            return users.Where(user => user.Token == token)
                .Select(user => new UserIdentity
                {
                    UserId = user.Id,
                    TenantId = user.TenantId,
                    Email = user.Email,
                    Name = user.Name,
                    IsOwner = user.Email == user.Tenant.Owner,
                    TeamRules = user.TeamMember.Team.TeamRules
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
