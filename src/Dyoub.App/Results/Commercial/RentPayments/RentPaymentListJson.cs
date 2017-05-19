// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentPayments
{
    public class RentPaymentListJson : JsonResult
    {
        public RentContract RentContract { get; set; }

        public RentPaymentListJson(RentContract saleOrder)
        {
            RentContract = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                total = RentContract.Total,
                discount = RentContract.Discount,
                totalPayable = RentContract.TotalPayable,
                confirmed = RentContract.Confirmed,
                billedAmount = RentContract.BilledAmount,
                paymentList = RentContract.RentPayments.Select(payment => new
                {
                    method = new
                    {
                        id = payment.PaymentMethod.Id,
                        name = payment.PaymentMethod.Name,
                        installmentLimit = payment.PaymentMethod.InstallmentLimit
                    },
                    numberOfInstallments = payment.NumberOfInstallments,
                    installmentValue = payment.InstallmentValue,
                    installmentBilling = payment.InstallmentBilling,
                    feePercentage = payment.FeePercentage,
                    feeFixedValue = payment.FeeFixedValue,
                    total = payment.Total,
                    billedAmount = payment.BilledAmount,
                    date = payment.Date.ToJson()
                })
            };

            base.ExecuteResult(context);
        }
    }
}
