// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class PurchaseCost
    {
        public TenantContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseCost(TenantContext tenant, PurchaseOrder purchaseOrder)
        {
            Tenant = tenant;
            PurchaseOrder = purchaseOrder;
        }
        
        private IEnumerable<PurchaseExpense> CalculateExpenses()
        {
            foreach (PurchasePayment payment in PurchaseOrder.PurchasePayments)
            {
                for (int installment = 1; installment <= payment.NumberOfInstallments; installment++)
                {
                    PurchaseExpense expense = new PurchaseExpense();
                    expense.PurchasePaymentId = payment.Id;
                    expense.AmountPaid = payment.InstallmentValue;
                    expense.PaymentDate = payment.Date.AddMonths(installment - 1);

                    yield return expense;
                }
            }
        }

        public void Confirm()
        {
            Tenant.PurchaseExpenses.AddRange(CalculateExpenses());

            PurchaseOrder.TotalCost = new Money(PurchaseOrder.PurchasePayments.Sum(p => p.Total))
                .Add(PurchaseOrder.ShippingCost ?? 0)
                .Add(PurchaseOrder.OtherTaxes ?? 0)
                .SubtractPercentage(PurchaseOrder.Discount ?? 0);
        }

        public void Revert()
        {
            PurchaseOrder.TotalCost = 0;

            Tenant.PurchaseExpenses.RemoveRange(PurchaseOrder.PurchasePayments
                .SelectMany(s => s.PurchaseExpenses));
        }
    }
}
