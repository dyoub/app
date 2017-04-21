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
    public class UpdatePaymentMethodContext : TenantContext
    {
        private PaymentMethod original;

        public PaymentMethod PaymentMethod { get; private set; }

        public UpdatePaymentMethodContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            PaymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            original = PaymentMethodFactory.PaymentMethod(tenant);

            SaveChanges();
        }

        public bool PaymentMethodWasUpdated()
        {
            Entry(PaymentMethod).Reload();

            return PaymentMethod.Name != original.Name &&
                   PaymentMethod.InstallmentLimit != original.InstallmentLimit&&
                   PaymentMethod.Active != original.Active &&
                   PaymentMethod.DaysToReceive != original.DaysToReceive &&
                   PaymentMethod.EarlyReceipt != original.EarlyReceipt;
        }
    }
}
