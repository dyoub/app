// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses
{
    public static class PurchaseExpenseQuery
    {
        public static IQueryable<CashActivity> AsCashActivity(this IQueryable<PurchaseExpense> purchaseExpenses)
        {
            return purchaseExpenses
                .Select(purchaseExpense => new CashActivity
                {
                    StartDate = purchaseExpense.PaymentDate,
                    EndDate = purchaseExpense.PaymentDate,
                    Activity = CashActivityType.Purchase,
                    Total = purchaseExpense.AmountPaid
                });
        }

        public static IQueryable<PurchaseExpense> ReceivedOnDate(this IQueryable<PurchaseExpense> purchaseExpenses, DateTime? date)
        {
            if (date == null)
            {
                return purchaseExpenses;
            }

            date = date.Value.Date;

            return purchaseExpenses.Where(purchaseExpense => purchaseExpense.PaymentDate == date);
        }

        public static IQueryable<PurchaseExpense> ReceivedToday(this IQueryable<PurchaseExpense> purchaseExpenses)
        {
            return purchaseExpenses.ReceivedOnDate(DateTime.Today);
        }

        public static IQueryable<PurchaseExpense> WhereReceivedDateStartAt(this IQueryable<PurchaseExpense> purchaseExpenses, DateTime? startDate)
        {
            if (startDate == null)
            {
                return purchaseExpenses;
            }

            startDate = startDate.Value.Date;

            return purchaseExpenses.Where(purchaseExpense => purchaseExpense.PaymentDate >= startDate);
        }

        public static IQueryable<PurchaseExpense> WhereReceivedDateEndAt(this IQueryable<PurchaseExpense> purchaseExpenses, DateTime? endDate)
        {
            if (endDate == null)
            {
                return purchaseExpenses;
            }

            endDate = endDate.Value.Date.AddDays(1);

            return purchaseExpenses.Where(purchaseExpense => purchaseExpense.PaymentDate < endDate);
        }

        public static IQueryable<PurchaseExpense> WhereStoreId(this IQueryable<PurchaseExpense> purchaseExpenses, int? storeId)
        {
            if (storeId == null)
            {
                return purchaseExpenses;
            }

            return purchaseExpenses.Where(purchaseExpense => purchaseExpense.PurchasePayment.PurchaseOrder.StoreId == storeId);
        }
    }
}
