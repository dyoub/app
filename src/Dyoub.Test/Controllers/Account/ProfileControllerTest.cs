// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Account;
using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using Dyoub.App.Models.ViewModel.Account.Profile;
using Dyoub.App.Results.Account.Profile;
using Dyoub.Test.Contexts.Account.Profile;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Account
{
    [TestClass]
    public class ProfileControllerTest
    {
        [TestMethod]
        public async Task FindProfile()
        {
            FindProfileContext context = new FindProfileContext();

            ProfileController controller = new ProfileController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(new UserIdentity { UserId = context.User.Id });

            ActionResult result = await controller.Find();

            Assert.IsTrue(result is ProfileDetailsJson);
        }

        [TestMethod]
        public async Task UpdateProfile()
        {
            UpdateProfileContext context = new UpdateProfileContext();

            ProfileController controller = new ProfileController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(new UserIdentity { UserId = context.User.Id });

            UpdateProfileViewModel viewModel = new UpdateProfileViewModel();
            viewModel.Name = context.User.Name + "Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.ProfileWasUpdated());
        }

        [TestMethod]
        public async Task ChangePassword()
        {
            ChangePasswordContext context = new ChangePasswordContext();

            ProfileController controller = new ProfileController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(new UserIdentity { UserId = context.User.Id });

            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();
            viewModel.OldPassword = UserFactory.User().Password;
            viewModel.NewPassword = UserFactory.User().Password + "Updated";

            await controller.ChangePassword(viewModel);

            Assert.IsTrue(context.PasswordWasChanged());
        }
    }
}
