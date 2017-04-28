// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.Wallets;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.Wallets
{
    public class WalletJson : JsonResult
    {
        public Wallet Wallet { get; private set; }

        public WalletJson(Wallet wallet)
        {
            Wallet = wallet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Wallet == null ? null : new
            {
                id = Wallet.Id,
                name = Wallet.Name,
                additionalInformation = Wallet.AdditionalInformation,
                active = Wallet.Active
            };

            base.ExecuteResult(context);
        }
    }
}
