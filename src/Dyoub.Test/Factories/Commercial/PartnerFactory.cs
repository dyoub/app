// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Partners;

namespace Dyoub.Test.Factories.Commercial
{
    public class PartnerFactory
    {
        public static Partner Partner(Tenant tenant)
        {
            return new Partner
            {
                Name = "Partner Test",
                NationalId = "123456890",
                Email = "supplier@email.com",
                PhoneNumber = "99 99999-9999",
                AlternativePhoneNumber = "99 88888-8888",
                AdditionalInformation = "Additional information",
                Tenant = tenant,
            };
        }
    }
}
