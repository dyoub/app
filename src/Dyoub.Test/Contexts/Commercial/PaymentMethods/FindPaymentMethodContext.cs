// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.PaymentMethods
{
    public class FindPaymentMethodContext : TenantContext
    {
        public PaymentMethod PaymentMethod { get; private set; }

        public FindPaymentMethodContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            PaymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaveChanges();
        }
    }
}
