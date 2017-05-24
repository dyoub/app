// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.Partners
{
    public class UpdatePartnerContext : TenantContext
    {
        private Partner original;

        public Partner Partner { get; private set; }

        public UpdatePartnerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Partner = Partners.Add(PartnerFactory.Partner(tenant));

            original = PartnerFactory.Partner(tenant);

            SaveChanges();
        }

        public bool PartnerWasUpdated()
        {
            Entry(Partner).Reload();

            return Partner.Name != original.Name &&
                   Partner.NationalId != original.NationalId &&
                   Partner.Email != original.Email &&
                   Partner.PhoneNumber != original.PhoneNumber &&
                   Partner.AlternativePhoneNumber != original.AlternativePhoneNumber &&
                   Partner.AdditionalInformation != original.AdditionalInformation;
        }
    }
}
