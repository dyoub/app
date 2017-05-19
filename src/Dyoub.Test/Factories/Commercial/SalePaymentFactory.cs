// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using System;

namespace Dyoub.Test.Factories.Commercial
{
    public class SalePaymentFactory
    {
        public static SalePayment SalePayment(SaleOrder saleOrder, PaymentMethod paymentMethod)
        {
            return new SalePayment
            {
                Date = DateTime.UtcNow.Date,
                SaleOrder = saleOrder,
                Tenant = saleOrder.Tenant,
                PaymentMethod = paymentMethod,
                NumberOfInstallments = 1,
                InstallmentValue = saleOrder.TotalPayable,
                BilledAmount = saleOrder.TotalPayable,
                Total = saleOrder.TotalPayable
            };
        }
    }
}
