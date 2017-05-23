// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Financial.RentIncomes;

namespace Dyoub.Test.Factories.Financial
{
    public class RentIncomeFactory
    {
        public static RentIncome RentIncome(RentPayment rentPayment)
        {
            return new RentIncome
            {
                RentPayment = rentPayment,
                Tenant = rentPayment.Tenant,
                AmountReceived = rentPayment.InstallmentValue,
                PaymentDate = rentPayment.Date
            };
        }
    }
}
