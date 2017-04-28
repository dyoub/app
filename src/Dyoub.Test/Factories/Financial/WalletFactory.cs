// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.Wallets;

namespace Dyoub.Test.Factories.Financial
{
    public class WalletFactory
    {
        public static Wallet Wallet(Tenant tenant)
        {
            return new Wallet
            {
                Name = "Wallet Test",
                AdditionalInformation = "Additional information.",
                Active = true,
                Tenant = tenant
            };
        }
    }
}
