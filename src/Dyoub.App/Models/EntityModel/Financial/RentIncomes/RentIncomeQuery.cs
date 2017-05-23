// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Financial.RentIncomes
{
    public static class RentIncomeQuery
    {
        public static IQueryable<CashActivity> AsCashActivity(this IQueryable<RentIncome> rentIncomes)
        {
            return rentIncomes
                .Select(rentIncome => new CashActivity
                {
                    StartDate = (rentIncome.AnticipationDate ?? rentIncome.ReceivedDate).Value,
                    EndDate = rentIncome.AnticipationDate ?? rentIncome.ReceivedDate,
                    Activity = CashActivityType.Rental,
                    Total = rentIncome.AmountAnticipated ?? rentIncome.AmountReceived
                });
        }

        public static IQueryable<RentIncome> ReceivedOnDate(this IQueryable<RentIncome> rentIncomes, DateTime? date)
        {
            if (date == null)
            {
                return rentIncomes;
            }

            date = date.Value.Date;

            return rentIncomes.Where(rentIncome => (rentIncome.AnticipationDate ?? rentIncome.ReceivedDate) == date);
        }

        public static IQueryable<RentIncome> ReceivedToday(this IQueryable<RentIncome> rentIncomes)
        {
            return rentIncomes.ReceivedOnDate(DateTime.UtcNow.Date);
        }

        public static IQueryable<RentIncome> WhereReceivedDateStartAt(this IQueryable<RentIncome> rentIncomes, DateTime? startDate)
        {
            if (startDate == null)
            {
                return rentIncomes;
            }

            startDate = startDate.Value.Date;

            return rentIncomes.Where(rentIncome => (rentIncome.AnticipationDate ?? rentIncome.ReceivedDate) >= startDate);
        }

        public static IQueryable<RentIncome> WhereReceivedDateEndAt(this IQueryable<RentIncome> rentIncomes, DateTime? endDate)
        {
            if (endDate == null)
            {
                return rentIncomes;
            }

            endDate = endDate.Value.Date.AddDays(1);

            return rentIncomes.Where(rentIncome => (rentIncome.AnticipationDate ?? rentIncome.ReceivedDate) < endDate);
        }

        public static IQueryable<RentIncome> WhereStoreId(this IQueryable<RentIncome> rentIncomes, int? storeId)
        {
            if (storeId == null)
            {
                return rentIncomes;
            }

            return rentIncomes.Where(rentIncome => rentIncome.RentPayment.RentContract.StoreId == storeId);
        }
    }
}
