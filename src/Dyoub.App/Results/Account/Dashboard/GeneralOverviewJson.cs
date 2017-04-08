// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class GeneralOverviewJson : JsonResult
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
                },
                counter = new { }
            };

            base.ExecuteResult(context);
        }
    }
}
