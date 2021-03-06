﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;

namespace Dyoub.Test.Factories.Financial
{
    public class SaleIncomeFactory
    {
        public static SaleIncome SaleIncome(SalePayment salePayment)
        {
            return new SaleIncome
            {
                SalePayment = salePayment,
                Tenant = salePayment.Tenant,
                AmountReceived = salePayment.InstallmentValue,
                PaymentDate = salePayment.Date
            };
        }
    }
}
