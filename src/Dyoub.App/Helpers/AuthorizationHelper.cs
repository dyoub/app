// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dyoub.App.Helpers
{
    public static class AuthorizationHelper
    {
        public static AuthorizedView Authorize(this HtmlHelper htmlHelper, string scope)
        {
            StringBuilder html = ((StringWriter)htmlHelper.ViewContext.Writer).GetStringBuilder();
            return new AuthorizedView(htmlHelper.ViewContext.HttpContext, html, scope);
        }
        
        public class AuthorizedView : IDisposable
        {
            private HttpContextBase httpContext;
            private StringBuilder html;
            private StringBuilder rollbackHtml;
            private string scope;

            public AuthorizedView(HttpContextBase httpContext, StringBuilder html, string scope)
            {
                this.httpContext = httpContext;
                this.html = html;
                this.scope = scope;

                rollbackHtml = new StringBuilder(html.ToString());
            }

            public void Dispose()
            {
                UserIdentity userIdentity = httpContext.UserIdentity();

                if (!userIdentity.HasPermission(scope))
                {
                    html.Clear();
                    html.Append(rollbackHtml);
                }
            }
        }
    }
}
