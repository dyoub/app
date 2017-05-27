// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.App.Models.EntityModel.Commercial.RentContractPartners;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.RentContracts
{
    public class UpdateRentContractPartnersContext : TenantContext
    {
        public RentContract RentContract { get; private set; }
        public Partner AnotherPartner { get; private set; }

        public UpdateRentContractPartnersContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Partner partner = Partners.Add(PartnerFactory.Partner(tenant));

            AnotherPartner = Partners.Add(PartnerFactory.Partner(tenant));
            RentContract = RentContracts.Add(RentContractFactory.RentContract(store));

            RentContractPartners.Add(RentContractPartnerFactory.RentContractPartner(RentContract, partner));

            SaveChanges();
        }

        public bool RentContractPartnersHasBeenUpdated()
        {
            RentContractPartner rentedPartner = RentContractPartners
                .WhereRentContractId(RentContract.Id)
                .SingleOrDefault();

            return rentedPartner.PartnerId == AnotherPartner.Id;
        }
    }
}
