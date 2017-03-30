// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Account;
using Dyoub.App.Models.ViewModel.Account.PasswordRecoveries;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Account.PasswordRecoveries;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Account
{
    [TestClass]
    public class RecoveryControllerTest
    {
        [TestMethod]
        public async Task RecoveryInexistentEmail()
        {
            RecoveryInexistentEmailContext context = new RecoveryInexistentEmailContext();
            MailerFake mailer = new MailerFake();

            CreatePasswordRecoveryViewModel viewModel = new CreatePasswordRecoveryViewModel();
            viewModel.Email = context.UnregisteredUser.Email;

            RecoveryController controller = new RecoveryController(context, mailer);
            controller.ControllerContext = new ControllerContextFake(controller);

            ActionResult result = await controller.Create(viewModel);

            Assert.IsTrue(mailer.EmailNotSent);
            Assert.IsTrue(result is ModelErrorsJson);
            Assert.IsTrue(context.RecoveryTokenWasNotCreated());
        }

        [TestMethod]
        public async Task RecoveryValidEmail()
        {
            RecoveryValidEmailContext context = new RecoveryValidEmailContext();
            MailerFake mailer = new MailerFake();

            CreatePasswordRecoveryViewModel viewModel = new CreatePasswordRecoveryViewModel();
            viewModel.Email = context.User.Email;

            RecoveryController controller = new RecoveryController(context, mailer);
            controller.ControllerContext = new ControllerContextFake(controller);

            ActionResult result = await controller.Create(viewModel);

            Assert.IsTrue(mailer.EmailSent);
            Assert.IsTrue(result is SuccessStatusCode);
            Assert.IsTrue(context.RecoveryTokenForUserWasCreated());
        }

        [TestMethod]
        public async Task ResetPasswordWithExpiredToken()
        {
            ResetPasswordWithExpiredTokenContext context = new ResetPasswordWithExpiredTokenContext();

            UpdatePasswordRecoveryViewModel viewModel = new UpdatePasswordRecoveryViewModel();
            viewModel.Token = context.PasswordRecovery.Token;
            viewModel.NewPassword = "NewPassword";

            RecoveryController controller = new RecoveryController(context, new MailerFake());
            controller.ControllerContext = new ControllerContextFake(controller);

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
            Assert.IsTrue(context.UserPasswordWasNotChanged());
        }

        [TestMethod]
        public async Task ResetPasswordWithValidToken()
        {
            ResetPasswordWithValidTokenContext context = new ResetPasswordWithValidTokenContext();

            UpdatePasswordRecoveryViewModel viewModel = new UpdatePasswordRecoveryViewModel();
            viewModel.Token = context.PasswordRecovery.Token;
            viewModel.NewPassword = "NewPassword";

            RecoveryController controller = new RecoveryController(context, new MailerFake());
            controller.ControllerContext = new ControllerContextFake(controller);

            ActionResult result = await controller.Update(viewModel);
            
            Assert.IsTrue(result is SuccessStatusCode);
            Assert.IsTrue(context.UserPasswordWasChanged());
        }
    }
}
