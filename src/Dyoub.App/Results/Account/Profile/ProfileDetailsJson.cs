// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Users;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Profile
{
    public class ProfileDetailsJson : JsonResult
    {
        public User User { get; private set; }

        public ProfileDetailsJson(User user)
        {
            User = user;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                name = User.Name,
                email = User.Email,
                lastLogin = User.LastLogin,
                lastChangePassword = User.LastChangePassword
            };

            base.ExecuteResult(context);
        }
    }
}
