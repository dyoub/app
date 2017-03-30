// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.Account;
using Dyoub.App.Models.ServiceModel.Mail;
using Dyoub.App.Models.ViewModel.Account.PasswordRecoveries;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Account
{
    public class RecoveryController : Controller
    {
        private ApplicationContext context;
        private BackgroundTask backgroundTask;
        private Mailer mailer;

        public RecoveryController() : this(new ApplicationContext(), new BackgroundTask(), new DetectedMailer()) { }

        public RecoveryController(ApplicationContext context, BackgroundTask backgroundTask, Mailer mailer)
        {
            this.context = context;
            this.backgroundTask = backgroundTask;
            this.mailer = mailer;
        }

        [HttpGet]
        [Route("reset-password")]
        public ActionResult New()
        {
            return View("~/Views/Account/PasswordRecoveries/NewPasswordRecovery.cshtml");
        }

        [HttpGet]
        [Route("reset-password/{token}")]
        public ActionResult Edit(string token)
        {
            return View("~/Views/Account/PasswordRecoveries/EditPasswordRecovery.cshtml");
        }

        [HttpPost]
        [Route("reset-password/confirmation")]
        public async Task<ActionResult> Create(CreatePasswordRecoveryViewModel viewModel)
        {
            AccountRecovery accountRecovery = new AccountRecovery(context);
            
            if (!await accountRecovery.Recovery(viewModel.Email))
            {
                return this.Error("Email not found.");
            }
            
            mailer.Recipients.Add(viewModel.Email);
            mailer.Content = this.ViewToString("~/Views/EmailTemplates/pt-BR/PasswordRecoveryEmail.cshtml",
                accountRecovery.PasswordRecovery);

            backgroundTask.Execute(cancellationToken => mailer.Send());

            return this.Success();
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> Update(UpdatePasswordRecoveryViewModel viewModel)
        {
            AccountRecovery accountRecovery = new AccountRecovery(context);
            
            if (!await accountRecovery.ResetPassword(viewModel.Token, viewModel.NewPassword))
            {
                return this.Error("Recovery token expired.");
            }
            
            return this.Success();
        }
    }
}
