// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Financial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public abstract class Billing<TIncome> where TIncome : IIncome
    {
        public TenantContext Tenant { get; private set; }
        public ICommercialDocument Document { get; private set; }

        public Billing(TenantContext tenant, ICommercialDocument document)
        {
            Tenant = tenant;
            Document = document;
        }
        
        private void AssignPaymentMethodFee(IPayment payment)
        {
            PaymentMethodFee paymentMethodFee = payment.PaymentMethod.PaymentMethodFees
                .OrderByDescending(fee => fee.MinimumInstallment)
                .FirstOrDefault(fee => payment.NumberOfInstallments >= fee.MinimumInstallment);

            if (paymentMethodFee != null)
            {
                payment.FeePercentage = paymentMethodFee.FeePercentage;
                payment.FeeFixedValue = paymentMethodFee.FeeFixedValue;
            }
        }
        
        private void CalculateTotals(IPayment payment, IIncome income, int numberOfInstallments)
        {
            payment.BilledAmount = payment.Total;

            payment.BilledAmount = new Money(payment.BilledAmount)
                .SubtractPercentage(payment.FeePercentage ?? 0)
                .Subtract(payment.FeeFixedValue ?? 0);

            payment.InstallmentBilling = new Money(payment.BilledAmount).Divide(numberOfInstallments);

            income.AmountReceived = payment.InstallmentBilling;
        }
        
        private void CalculateReceiptTiming(IPayment payment, IIncome income, int installment)
        {
            income.PaymentDate = payment.Date.AddMonths(installment - 1);

            if (payment.PaymentMethod.DaysToReceive != null)
            {
                DateTime baseDate = payment.PaymentMethod.EarlyReceipt ? payment.Date : income.PaymentDate;
                income.ReceivedDate = baseDate.AddDays(payment.PaymentMethod.DaysToReceive.Value);
            }
        }
        
        private IEnumerable<TIncome> CalculateIncomes()
        {
            foreach (IPayment payment in Document.Payments)
            {
                int installments = payment.PaymentMethod.EarlyReceipt ? 1 : payment.NumberOfInstallments;

                AssignPaymentMethodFee(payment);

                for (int installment = 1; installment <= installments; installment++)
                {
                    TIncome income = NewIncome();
                    income.PaymentId = payment.Id;

                    CalculateTotals(payment, income, installments);
                    CalculateReceiptTiming(payment, income, installment);

                    yield return income;
                }
            }
        }

        protected abstract void AddIncomes(IEnumerable<TIncome> incomes);

        protected abstract TIncome NewIncome();

        protected abstract void RemoveIncomes();

        public void Confirm()
        {
            AddIncomes(CalculateIncomes());

            Document.ConfirmationDate = DateTime.UtcNow;
            Document.BilledAmount = new Money(Document.Payments.Sum(p => p.BilledAmount));
        }

        public void Revert()
        {
            foreach (IPayment payment in Document.Payments)
            {
                payment.FeePercentage = null;
                payment.FeeFixedValue = null;
                payment.InstallmentBilling = 0;
                payment.BilledAmount = 0;
            }

            Document.ConfirmationDate = null;
            Document.BilledAmount = 0;

            RemoveIncomes();
        }
    }
}
