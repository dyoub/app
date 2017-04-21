// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;

namespace Dyoub.Test.Factories.Commercial
{
    public class PaymentMethodFactory
    {
        public static PaymentMethod PaymentMethod(Tenant tenant)
        {
            return new PaymentMethod
            {
                Name = "Payment Method Test",
                InstallmentLimit = 1,
                Active = true,
                Tenant = tenant,
            };
        }
    }
}
