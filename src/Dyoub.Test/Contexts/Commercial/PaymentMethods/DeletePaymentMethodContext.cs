// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.PaymentMethods
{
    public class DeletePaymentMethodContext : TenantContext
    {
        public PaymentMethod PaymentMethod { get; private set; }

        public DeletePaymentMethodContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            PaymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaveChanges();
        }

        public bool PaymentMethodWasDeleted()
        {
            return !PaymentMethods.Any();
        }
    }
}
