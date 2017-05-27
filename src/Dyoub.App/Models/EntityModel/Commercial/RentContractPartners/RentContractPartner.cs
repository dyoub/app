// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;

namespace Dyoub.App.Models.EntityModel.Commercial.RentContractPartners
{
    public class RentContractPartner : ITenantData
    {
        public int RentContractId { get; set; }
        public int PartnerId { get; set; }
        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual RentContract RentContract { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
