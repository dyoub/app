﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Effort;

namespace Dyoub.Test.Contexts.Financial.Wallets
{
    public class FindWalletContext : TenantContext
    {
        public Wallet Wallet { get; private set; }

        public FindWalletContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Wallet = Wallets.Add(WalletFactory.Wallet(tenant));

            SaveChanges();
        }
    }
}
