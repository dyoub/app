// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.Results.Account.Users;
using Dyoub.App.Models.ServiceModel.Account.Users;
using Dyoub.App.Models.ServiceModel.Mail;
using Dyoub.App.Models.ViewModel.Account.Users;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Account
{
    public class UsersController : Controller
    {
        private ApplicationContext context;
        private BackgroundTask backgroundTask;
        private Mailer mailer;

        public UsersController() : this(new ApplicationContext(), new BackgroundTask(), new DetectedMailer()) { }

        public UsersController(ApplicationContext context, BackgroundTask backgroundTask, Mailer mailer)
        {
            this.context = context;
            this.backgroundTask = backgroundTask;
            this.mailer = mailer;
        }
        
        [HttpGet]
        [Route("signin")]
        public async Task<ActionResult> Signin()
        {
            if (await context.Users.WhereToken(Request.AccessToken()).AnyAsync())
            {
                return RedirectToRoute("dashboard");
            }

            return View("~/Views/Account/Users/Signin.cshtml");
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> Signin(SigninViewModel viewModel)
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SigninAsync(viewModel.Email, viewModel.Password))
            {
                Response.SetAccessToken(authentication.User.Token);
                return this.Success();
            }

            return this.Unauthorized();
        }

        [HttpGet]
        [Route("signout")]
        public async Task<ActionResult> Signout()
        {
            UserAuthentication authentication = new UserAuthentication(context);

            if (await authentication.SignoutAsync(Request.AccessToken()))
            {
                Response.ClearAccessToken();
            }

            return RedirectToRoute("signin");
        }

        [HttpGet]
        [Route("signup")]
        public ActionResult Signup()
        {
            return View("~/Views/Account/Users/Signup.cshtml");
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult> Signup(SignupViewModel viewModel)
        {
            UserSignup userSignup = new UserSignup(context, viewModel.MapToUser());
            await userSignup.SignupAsync();

            if (userSignup.EmailAlreadyTaken)
            {
                return this.Error("Email already taken.");
            }

            mailer.Recipients.Add(userSignup.User.Email);
            mailer.Content = this.ViewToString("~/Views/Emails/pt-BR/SignupEmail.cshtml",
                new SignupEmail(userSignup.User, userSignup.ClosureRequest));

            backgroundTask.Execute(cancellationToken => mailer.Send());
    
            Response.SetAccessToken(userSignup.User.Token);

            return this.Success();
        }
    }
}
