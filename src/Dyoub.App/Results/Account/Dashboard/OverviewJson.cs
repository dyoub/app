// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using System.Web.Mvc;
using System.Linq;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class OverviewJson : JsonResult
    {
        public UserIdentity UserIdentity { get; set; }
        
        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                user = new
                {
                    name = UserIdentity.Name.Words().First(),
                    email = UserIdentity.Email
                }
            };

            base.ExecuteResult(context);
        }
    }
}
