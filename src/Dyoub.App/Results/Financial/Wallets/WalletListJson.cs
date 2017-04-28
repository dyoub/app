// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.Wallets;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.Wallets
{
    public class WalletListJson : JsonResult
    {
        public IEnumerable<Wallet> Wallets { get; private set; }

        public WalletListJson(IEnumerable<Wallet> wallets)
        {
            Wallets = wallets;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Wallets.Select(wallet => new
            {
                id = wallet.Id,
                name = wallet.Name,
                additionalInformation = wallet.AdditionalInformation,
                active = wallet.Active
            });

            base.ExecuteResult(context);
        }
    }
}
