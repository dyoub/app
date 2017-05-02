// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Financial.SaleIncomes
{
    public static class SaleIncomeQuery
    {
        public static IQueryable<CashActivity> AsCashActivity(this IQueryable<SaleIncome> saleIncomes)
        {
            return saleIncomes
                .Select(saleIncome => new CashActivity
                {
                    StartDate = (saleIncome.AnticipationDate ?? saleIncome.ReceivedDate).Value,
                    EndDate = saleIncome.AnticipationDate ?? saleIncome.ReceivedDate,
                    Activity = CashActivityType.Sale,
                    Total = saleIncome.AmountAnticipated ?? saleIncome.AmountReceived
                });
        }

        public static IQueryable<SaleIncome> ReceivedOnDate(this IQueryable<SaleIncome> saleIncomes, DateTime? date)
        {
            if (date == null)
            {
                return saleIncomes;
            }

            date = date.Value.Date;

            return saleIncomes.Where(saleIncome => (saleIncome.AnticipationDate ?? saleIncome.ReceivedDate) == date);
        }

        public static IQueryable<SaleIncome> ReceivedToday(this IQueryable<SaleIncome> saleIncomes)
        {
            return saleIncomes.ReceivedOnDate(DateTime.Today);
        }

        public static IQueryable<SaleIncome> WhereReceivedDateStartAt(this IQueryable<SaleIncome> saleIncomes, DateTime? startDate)
        {
            if (startDate == null)
            {
                return saleIncomes;
            }

            startDate = startDate.Value.Date;

            return saleIncomes.Where(saleIncome => (saleIncome.AnticipationDate ?? saleIncome.ReceivedDate) >= startDate);
        }

        public static IQueryable<SaleIncome> WhereReceivedDateEndAt(this IQueryable<SaleIncome> saleIncomes, DateTime? endDate)
        {
            if (endDate == null)
            {
                return saleIncomes;
            }

            endDate = endDate.Value.Date.AddDays(1);

            return saleIncomes.Where(saleIncome => (saleIncome.AnticipationDate ?? saleIncome.ReceivedDate) < endDate);
        }

        public static IQueryable<SaleIncome> WhereStoreId(this IQueryable<SaleIncome> saleIncomes, int? storeId)
        {
            if (storeId == null)
            {
                return saleIncomes;
            }

            return saleIncomes.Where(saleIncome => saleIncome.SalePayment.SaleOrder.StoreId == storeId);
        }
    }
}
