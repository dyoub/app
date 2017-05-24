// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.Partners
{
    public class DeletePartnerContext : TenantContext
    {
        public Partner Partner { get; private set; }

        public DeletePartnerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Partner = Partners.Add(PartnerFactory.Partner(tenant));

            SaveChanges();
        }

        public bool PartnerWasDeleted()
        {
            return !Partners.Any();
        }
    }
}
