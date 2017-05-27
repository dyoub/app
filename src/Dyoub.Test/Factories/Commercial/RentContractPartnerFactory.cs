// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentContractPartners;

namespace Dyoub.Test.Factories.Commercial
{
    public class RentContractPartnerFactory
    {
        public static RentContractPartner RentContractPartner(RentContract rentContract, Partner partner)
        {
            return new RentContractPartner
            {
                RentContract = rentContract,
                Partner = partner,
                Tenant = rentContract.Tenant
            };
        }
    }
}
