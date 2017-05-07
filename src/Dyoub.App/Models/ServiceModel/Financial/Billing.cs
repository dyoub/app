// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class Billing
    {
        public TenantContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public Billing(TenantContext tenant)
        {
            Tenant = tenant;
        }
        
        private bool SaleOrderIsPending()
        {
            HasNoItems = SaleOrder.TotalPayable == 0;
            HasPendingPayment = SaleOrder.TotalPayable != SaleOrder.SalePayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        private void AssignPaymentMethodFee(SalePayment payment)
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

        private void CalculateTotals(SalePayment payment, SaleIncome income, int numberOfInstallments)
        {
            payment.BilledAmount = payment.Total;

            payment.BilledAmount = new Money(payment.BilledAmount)
                .SubtractPercentage(payment.FeePercentage ?? 0)
                .Subtract(payment.FeeFixedValue ?? 0);

            payment.InstallmentBilling = new Money(payment.BilledAmount).Divide(numberOfInstallments);

            income.AmountReceived = payment.InstallmentBilling;
        }

        private void CalculateReceiptTiming(SalePayment payment, SaleIncome income, int installment)
        {
            income.PaymentDate = payment.Date.AddMonths(installment - 1);

            if (payment.PaymentMethod.DaysToReceive != null)
            {
                DateTime baseDate = payment.PaymentMethod.EarlyReceipt ? payment.Date : income.PaymentDate;
                income.ReceivedDate = baseDate.AddDays(payment.PaymentMethod.DaysToReceive.Value);
            }
        }

        private IEnumerable<SaleIncome> CalculateIncomes()
        {
            foreach (SalePayment payment in SaleOrder.SalePayments)
            {
                int installments = payment.PaymentMethod.EarlyReceipt ? 1 : payment.NumberOfInstallments;

                AssignPaymentMethodFee(payment);

                for (int installment = 1; installment <= installments; installment++)
                {
                    SaleIncome income = new SaleIncome();
                    income.SalePaymentId = payment.Id;

                    CalculateTotals(payment, income, installments);
                    CalculateReceiptTiming(payment, income, installment);

                    yield return income;
                }
            }
        }

        public async Task<bool> Confirm(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludePaymentMethodsAndFees()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || SaleOrder.Confirmed || SaleOrderIsPending())
            {
                return false;
            }

            Tenant.SaleIncomes.AddRange(CalculateIncomes());

            SaleOrder.ConfirmationDate = DateTime.Now;
            SaleOrder.BilledAmount = new Money(SaleOrder.SalePayments.Sum(p => p.BilledAmount));
            
            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludeSaleIncomes()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || !SaleOrder.Confirmed)
            {
                return false;
            }

            foreach (SalePayment payment in SaleOrder.SalePayments)
            {
                payment.FeePercentage = null;
                payment.FeeFixedValue = null;
                payment.InstallmentBilling = 0;
                payment.BilledAmount = 0;
            }

            SaleOrder.ConfirmationDate = null;
            SaleOrder.BilledAmount = 0;

            Tenant.SaleIncomes.RemoveRange(SaleOrder.SalePayments
                .SelectMany(s => s.SaleIncomes));

            await Tenant.SaveChangesAsync();
            
            return true;
        }
    }
}
