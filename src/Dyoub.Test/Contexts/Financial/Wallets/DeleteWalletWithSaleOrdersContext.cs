// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Financial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Financial.Wallets
{
    public class DeleteWalletWithSaleOrdersContext : TenantContext
    {
        public Wallet Wallet { get; private set; }

        public DeleteWalletWithSaleOrdersContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Wallet = Wallets.Add(WalletFactory.Wallet(tenant));
            SaleOrders.Add(SaleOrderFactory.SaleOrder(store, wallet: Wallet));

            SaveChanges();
        }

        public bool WalletWasNotDeleted()
        {
            return Wallets.Any();
        }
    }
}
