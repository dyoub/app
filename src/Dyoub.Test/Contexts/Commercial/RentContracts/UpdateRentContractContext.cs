// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.RentContracts
{
    public class UpdateRentContractContext : TenantContext
    {
        private RentContract original;

        public Store AnotherStore { get; private set; }
        public RentContract RentContract { get; private set; }

        public UpdateRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            AnotherStore = Stores.Add(StoreFactory.Store(tenant));
            RentContract = RentContracts.Add(RentContractFactory.RentContract(store));

            original = RentContractFactory.RentContract(store);

            SaveChanges();
        }

        public bool RentContractWasUpdated()
        {
            Entry(RentContract).Reload();

            return RentContract.StoreId != original.StoreId &&
                   RentContract.StartDate != original.StartDate &&
                   RentContract.EndDate != original.EndDate &&
                   RentContract.Location != original.Location &&
                   RentContract.AdditionalInformation != original.AdditionalInformation;
        }
    }
}
