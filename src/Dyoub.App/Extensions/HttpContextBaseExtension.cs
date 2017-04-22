// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.Security;
using System.Web;

namespace Dyoub.App.Extensions
{
    public static class HttpContextBaseExtension
    {
        public static UserIdentity UserIdentity(this HttpContextBase httpContext)
        {
            return httpContext.Items["UserIdentity"] as UserIdentity;
        }

        public static void SetUserIdentity(this HttpContextBase httpContext, UserIdentity userIdentity)
        {
            httpContext.Items["UserIdentity"] = userIdentity;
        }
    }
}
