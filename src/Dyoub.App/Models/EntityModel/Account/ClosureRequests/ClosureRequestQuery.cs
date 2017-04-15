// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Account.ClosureRequests
{
    public static class ClosureRequestQuery
    {
        public static IQueryable<ClosureRequest> WhereId(this IQueryable<ClosureRequest> closureRequests, string token)
        {
            return closureRequests.Where(closureRequest => closureRequest.Token == token);
        }

        public static IQueryable<ClosureRequest> WhereEmail(this IQueryable<ClosureRequest> closureRequests, string email)
        {
            return closureRequests.Where(closureRequest => closureRequest.Email == email);
        }
    }
}
