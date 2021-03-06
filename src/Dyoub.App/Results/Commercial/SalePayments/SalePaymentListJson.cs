﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SalePayments
{
    public class SalePaymentListJson : JsonResult
    {
        public SaleOrder SaleOrder { get; set; }

        public SalePaymentListJson(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = SaleOrder == null ? null : new
            {
                id = SaleOrder.Id,
                total = SaleOrder.Total,
                discount = SaleOrder.Discount,
                totalPayable = SaleOrder.TotalPayable,
                confirmed = SaleOrder.Confirmed,
                billedAmount = SaleOrder.BilledAmount,
                paymentList = SaleOrder.SalePayments.Select(payment => new
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
                    date = payment.Date.ToJson(),
                    incomes = payment.SaleIncomes
                        .OrderBy(income => income.ReceivedDate)
                        .Select(income => new
                        {
                            receivedDate = income.ReceivedDate.ToJson(),
                            amountReceived = income.AmountReceived
                        })
                })
            };

            base.ExecuteResult(context);
        }
    }
}
