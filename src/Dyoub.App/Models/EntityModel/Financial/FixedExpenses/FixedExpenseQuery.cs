// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;
using System.Data.Entity;
using System;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;

namespace Dyoub.App.Models.EntityModel.Financial.FixedExpenses
{
    public static class FixedExpenseQuery
    {
        public static IQueryable<CashActivity> AsCashActivity(this IQueryable<FixedExpense> fixedExpenses)
        {
            return fixedExpenses
                .Select(fixedExpense => new CashActivity
                {
                    StartDate = fixedExpense.StartDate,
                    EndDate = fixedExpense.EndDate,
                    Activity = CashActivityType.FixedExpense,
                    Total = fixedExpense.Value
                });
        }

        public static IQueryable<FixedExpense> IncludeStore(this IQueryable<FixedExpense> fixedExpenses)
        {
            return fixedExpenses.Include(fixedExpense => fixedExpense.Store);
        }

        public static IQueryable<FixedExpense> OrderByDescription(this IQueryable<FixedExpense> fixedExpenses)
        {
            return fixedExpenses.OrderBy(fixedExpense => fixedExpense.Description);
        }

        public static IQueryable<FixedExpense> ToCurrentMonth(this IQueryable<FixedExpense> fixedExpenses)
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return fixedExpenses.WhereEffectiveFrom(startDate).WhereEffectiveUntil(endDate);
        }

        public static IQueryable<FixedExpense> WhereDescriptionContains(this IQueryable<FixedExpense> fixedExpenses, params string[] words)
        {
            foreach (string word in words)
            {
                fixedExpenses = fixedExpenses.Where(fixedExpense => fixedExpense.Description.Contains(word));
            }

            return fixedExpenses;
        }

        public static IQueryable<FixedExpense> WhereEffectiveIn(this IQueryable<FixedExpense> fixedExpenses, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return fixedExpenses.WhereEffectiveFrom(startDate).WhereEffectiveUntil(endDate);
        }

        public static IQueryable<FixedExpense> WhereEffectiveFrom(this IQueryable<FixedExpense> fixedExpenses, DateTime? startDate)
        {
            if (startDate == null)
            {
                return fixedExpenses;
            }

            startDate = startDate.Value.Date;

            return fixedExpenses.Where(fixedExpense => fixedExpense.EndDate == null || fixedExpense.EndDate >= startDate);
        }

        public static IQueryable<FixedExpense> WhereEffectiveUntil(this IQueryable<FixedExpense> fixedExpenses, DateTime? endDate)
        {
            if (endDate == null)
            {
                return fixedExpenses;
            }

            endDate = endDate.Value.Date;

            return fixedExpenses.Where(fixedExpense => fixedExpense.StartDate <= endDate);
        }

        public static IQueryable<FixedExpense> WhereId(this IQueryable<FixedExpense> fixedExpenses, int id)
        {
            return fixedExpenses.Where(fixedExpense => fixedExpense.Id == id);
        }

        public static IQueryable<FixedExpense> WhereStoreId(this IQueryable<FixedExpense> fixedExpenses, int? storeId)
        {
            if (storeId == null)
            {
                return fixedExpenses;
            }

            return fixedExpenses.Where(fixedExpense => fixedExpense.StoreId == storeId);
        }
    }
}
