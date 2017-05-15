// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchasePayments
{
    public class PurchasePaymentListJson : JsonResult
    {
        public PurchaseOrder PurchaseOrder { get; set; }

        public PurchasePaymentListJson(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrder == null ? null : new
            {
                id = PurchaseOrder.Id,
                total = PurchaseOrder.Total,
                shippingCost = PurchaseOrder.ShippingCost,
                otherTaxes = PurchaseOrder.OtherTaxes,
                discount = PurchaseOrder.Discount,
                totalPayable = PurchaseOrder.TotalPayable,
                totalCost = PurchaseOrder.TotalCost,
                confirmed = PurchaseOrder.Confirmed,
                paymentList = PurchaseOrder.PurchasePayments.Select(payment => new
                {
                    numberOfInstallments = payment.NumberOfInstallments,
                    installmentValue = payment.InstallmentValue,
                    total = payment.Total,
                    date = payment.Date,
                    expenses = payment.PurchaseExpenses
                        .OrderBy(expense => expense.PaymentDate)
                        .Select(expense => new
                        {
                            paymentDate = expense.PaymentDate,
                            amountPaid = expense.AmountPaid
                        })
                })
            };

            base.ExecuteResult(context);
        }
    }
}
