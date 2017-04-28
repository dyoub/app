// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.Test.Factories.Account;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Financial.Wallets
{
    public class CreateWalletContext : TenantContext
    {
        public CreateWalletContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            SaveChanges();
        }

        public bool WalletWasCreated()
        {
            return Wallets.Any();
        }
    }
}
