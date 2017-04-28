// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Effort;

namespace Dyoub.Test.Contexts.Financial.Wallets
{
    public class ListWalletContext : TenantContext
    {
        public ListWalletContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());

            Wallets.Add(WalletFactory.Wallet(tenant));
            Wallets.Add(WalletFactory.Wallet(tenant));

            SaveChanges();
        }
    }
}
