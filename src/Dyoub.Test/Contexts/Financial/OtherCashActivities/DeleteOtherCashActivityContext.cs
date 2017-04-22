// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Financial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Financial.OtherCashActivities
{
    public class DeleteOtherCashActivityContext : TenantContext
    {
        public OtherCashActivity OtherCashActivity { get; private set; }

        public DeleteOtherCashActivityContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            OtherCashActivity = OtherCashActivities.Add(OtherCashActivityFactory.OtherCashActivity(store));

            SaveChanges();
        }

        public bool OtherCashActivityWasDeleted()
        {
            return !OtherCashActivities.Any();
        }
    }
}
