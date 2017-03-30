// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Account;
using Dyoub.App.Models.ViewModel.Account.Users;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Account.Users;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Account
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public async Task SigninWithWrongPassword()
        {
            SigninWrongPasswordContext context = new SigninWrongPasswordContext();
            
            UsersController controller = new UsersController(context, new MailerFake());
            controller.ControllerContext = new ControllerContextFake(controller);

            SigninViewModel viewModel = new SigninViewModel();
            viewModel.Email = context.User.Email;
            viewModel.Password = "WrongPassword";

            ActionResult result = await controller.Signin(viewModel);

            Assert.IsTrue(result is UnauthorizedStatusCode);
            Assert.IsTrue(context.UserIsNotSiginedIn());
        }

        [TestMethod]
        public async Task SigninCorrectly()
        {
            SigninCorrectlyContext context = new SigninCorrectlyContext();

            UsersController controller = new UsersController(context, new MailerFake());
            controller.ControllerContext = new ControllerContextFake(controller);

            SigninViewModel viewModel = new SigninViewModel();
            viewModel.Email = context.User.Email;
            viewModel.Password = context.User.Password;

            ActionResult result = await controller.Signin(viewModel);
            
            Assert.IsTrue(result is SuccessStatusCode);
            Assert.IsTrue(context.UserIsSiginedIn());
        }

        [TestMethod]
        public async Task SignupCorrectly()
        {
            SignupCorrectlyContext context = new SignupCorrectlyContext();
            MailerFake mailer = new MailerFake();

            UsersController controller = new UsersController(context, mailer);
            controller.ControllerContext = new ControllerContextFake(controller);

            SignupViewModel viewModel = new SignupViewModel();
            viewModel.Name = context.User.Name;
            viewModel.Email = context.User.Email;
            viewModel.Password = context.User.Password;

            ActionResult result = await controller.Signup(viewModel);

            Assert.IsTrue(mailer.EmailSent);
            Assert.IsTrue(result is SuccessStatusCode);
            Assert.IsTrue(context.TheUserWasCreated());
            Assert.IsTrue(context.ATenantWasCreatedForTheUser());
            Assert.IsTrue(context.AClosureRequestWasCreatedForTheUser());
        }

        [TestMethod]
        public async Task SignupWithAnEmailAlreadyTaken()
        {
            SignupEmailAlreadyTakenContext context = new SignupEmailAlreadyTakenContext();
            MailerFake mailer = new MailerFake();

            UsersController controller = new UsersController(context, mailer);
            controller.ControllerContext = new ControllerContextFake(controller);

            SignupViewModel viewModel = new SignupViewModel();
            viewModel.Name = "Another User";
            viewModel.Email = context.User.Email;
            viewModel.Password = "12345678";

            ActionResult result = await controller.Signup(viewModel);
            
            Assert.IsTrue(mailer.EmailNotSent);
            Assert.IsTrue(result is ModelErrorsJson);
            Assert.IsTrue(context.HasOnlyOneUserWithThisEmail());
        }
    }
}
