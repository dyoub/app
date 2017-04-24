// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Users;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Account.Profile;
using Dyoub.App.Models.ViewModel.Account.Profile;
using Dyoub.App.Models.ServiceModel.Account;

namespace Dyoub.App.Controllers.Account
{
    public class ProfileController : Controller
    {
        private ApplicationContext context;

        public ProfileController() : this(new ApplicationContext()) { }

        public ProfileController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet, Route("profile/change-password"), Authorization]
        public ActionResult ChangePassword()
        {
            return View("~/Views/Account/Profile/ChangePassword.cshtml");
        }

        [HttpGet, Route("profile"), Authorization]
        public ActionResult Details()
        {
            return View("~/Views/Account/Profile/ProfileDetails.cshtml");
        }

        [HttpGet, Route("profile/edit"), Authorization]
        public ActionResult Edit()
        {
            return View("~/Views/Account/Profile/ProfileEdit.cshtml");
        }

        [HttpPost, Route("profile/find"), Authorization]
        public async Task<ActionResult> Find()
        {
            User user = await context.Users
                .WhereId(HttpContext.UserIdentity().UserId)
                .SingleOrDefaultAsync();

            return new ProfileDetailsJson(user);
        }

        [HttpPost, Route("profile/update"), Authorization]
        public async Task<ActionResult> Update(UpdateProfileViewModel viewModel)
        {
            User user = await context.Users
                .WhereId(HttpContext.UserIdentity().UserId)
                .SingleOrDefaultAsync();

            viewModel.MapTo(user);
            await context.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("profile/change-password"), Authorization]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            bool passwordChanged = await new UserAuthentication(context)
                .ChangePasswordAsync(HttpContext.UserIdentity().UserId,
                    viewModel.OldPassword, viewModel.NewPassword);

            if (!passwordChanged)
            {
                return this.Error("Old password is incorrect.");
            }

            return this.Success();
        }
    }
}
