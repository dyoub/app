// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.PaymentMethods
{
    public class PaymentMethodJson : JsonResult
    {
        public PaymentMethod PaymentMethod { get; private set; }

        public PaymentMethodJson(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (PaymentMethod != null)
            {
                IEnumerable<PaymentMethodFee> paymentMethodFees = PaymentMethod.PaymentMethodFees
                    .OrderBy(paymentMethodFee => paymentMethodFee.MinimumInstallment);

                Data = new
                {
                    id = PaymentMethod.Id,
                    name = PaymentMethod.Name,
                    installmentLimit = PaymentMethod.InstallmentLimit,
                    daysToReceive = PaymentMethod.DaysToReceive,
                    earlyReceipt = PaymentMethod.EarlyReceipt,
                    paymentFees = paymentMethodFees.Select(paymentMethodFee => new
                    {
                        minimumInstallment = paymentMethodFee.MinimumInstallment,
                        feePercentage = paymentMethodFee.FeePercentage,
                        feeFixedValue = paymentMethodFee.FeeFixedValue
                    }),
                    active = PaymentMethod.Active,
                };
            }

            base.ExecuteResult(context);
        }
    }
}
