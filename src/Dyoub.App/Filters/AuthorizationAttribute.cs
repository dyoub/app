// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Infrastructure.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dyoub.App.Filters
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        
    }
}
