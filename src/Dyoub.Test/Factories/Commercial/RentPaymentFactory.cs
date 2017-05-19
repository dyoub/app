// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using System;

namespace Dyoub.Test.Factories.Commercial
{
    public class RentPaymentFactory
    {
        public static RentPayment RentPayment(RentContract rentContract, PaymentMethod paymentMethod)
        {
            return new RentPayment
            {
                Date = DateTime.UtcNow.Date,
                RentContract = rentContract,
                Tenant = rentContract.Tenant,
                PaymentMethod = paymentMethod,
                NumberOfInstallments = 1,
                InstallmentValue = rentContract.TotalPayable,
                BilledAmount = rentContract.TotalPayable,
                Total = rentContract.TotalPayable
            };
        }
    }
}
