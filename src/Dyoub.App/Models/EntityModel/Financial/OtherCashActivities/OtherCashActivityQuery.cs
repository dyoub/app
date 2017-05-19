// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Financial.OtherCashActivities
{
    public static class OtherCashActivityQuery
    {
        public static IQueryable<CashActivity> AsCashActivity(this IQueryable<OtherCashActivity> otherCashActivities)
        {
            return otherCashActivities
                .Select(otherCashActivity => new CashActivity
                {
                    StartDate = otherCashActivity.Date,
                    EndDate = otherCashActivity.Date,
                    Activity = CashActivityType.Other,
                    Total = otherCashActivity.Value
                });
        }

        public static IQueryable<OtherCashActivity> IncludeStore(this IQueryable<OtherCashActivity> otherCashActivities)
        {
            return otherCashActivities.Include(otherCashActivity => otherCashActivity.Store);
        }

        public static IQueryable<OtherCashActivity> FromDate(this IQueryable<OtherCashActivity> otherCashActivities, DateTime? date)
        {
            if (date == null)
            {
                return otherCashActivities;
            }

            date = date.Value.Date;

            return otherCashActivities.Where(otherCashActivity => otherCashActivity.Date >= date);
        }

        public static IQueryable<OtherCashActivity> OrderByDescription(this IQueryable<OtherCashActivity> otherCashActivities)
        {
            return otherCashActivities.OrderBy(otherCashActivity => otherCashActivity.Description);
        }

        public static IQueryable<OtherCashActivity> ToCurrentMonth(this IQueryable<OtherCashActivity> otherCashActivities)
        {
            int year = DateTime.UtcNow.Date.Year;
            int month = DateTime.UtcNow.Date.Month;

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return otherCashActivities.FromDate(startDate).UntilDate(endDate);
        }

        public static IQueryable<OtherCashActivity> UntilDate(this IQueryable<OtherCashActivity> otherCashActivities, DateTime? date)
        {
            if (date == null)
            {
                return otherCashActivities;
            }

            date = date.Value.Date.AddDays(1);

            return otherCashActivities.Where(otherCashActivity => otherCashActivity.Date < date);
        }

        public static IQueryable<OtherCashActivity> WhereDescriptionContains(this IQueryable<OtherCashActivity> otherCashActivities, params string[] words)
        {
            foreach (string word in words)
            {
                otherCashActivities = otherCashActivities.Where(otherCashActivity => otherCashActivity.Description.Contains(word));
            }

            return otherCashActivities;
        }

        public static IQueryable<OtherCashActivity> WhereId(this IQueryable<OtherCashActivity> otherCashActivities, int id)
        {
            return otherCashActivities.Where(otherCashActivity => otherCashActivity.Id == id);
        }

        public static IQueryable<OtherCashActivity> WhereOccurredIn(this IQueryable<OtherCashActivity> otherCashActivities, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return otherCashActivities.FromDate(startDate).UntilDate(endDate);
        }

        public static IQueryable<OtherCashActivity> WhereStoreId(this IQueryable<OtherCashActivity> otherCashActivities, int? storeId)
        {
            if (storeId == null)
            {
                return otherCashActivities;
            }

            return otherCashActivities.Where(otherCashActivity => otherCashActivity.StoreId == storeId);
        }
    }
}
