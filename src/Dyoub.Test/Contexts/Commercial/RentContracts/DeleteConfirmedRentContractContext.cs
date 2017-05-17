﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.RentContracts
{
    public class DeleteConfirmedRentContractContext : TenantContext
    {
        public RentContract RentContract { get; private set; }

        public DeleteConfirmedRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            RentContract = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store));

            SaveChanges();
        }

        public bool RentContractWasNotDeleted()
        {
            return RentContracts.WhereId(RentContract.Id).Any();
        }
    }
}