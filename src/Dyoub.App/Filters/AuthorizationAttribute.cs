// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Infrastructure.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dyoub.App.Filters
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public ApplicationContext Context { get; private set; }
        public string Scope { get; set; }

        public AuthorizationAttribute() : this(new ApplicationContext()) { }

        public AuthorizationAttribute(ApplicationContext context)
        {
            Context = context;
        }

        private void RespondWithAccessDenied(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Users" },
                    { "action", "Signin" }
                });
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.Result = new EmptyResult();
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string token = filterContext.HttpContext.Request.AccessToken();

            if (token != null)
            {
                UserIdentity userIdentity = Context.Users.AsUserIdentity(token).SingleOrDefault();

                if (userIdentity != null && userIdentity.HasPermission(Scope))
                {
                    filterContext.HttpContext.SetUserIdentity(userIdentity);
                    return;
                }
            }

            RespondWithAccessDenied(filterContext);
        }
    }
}
