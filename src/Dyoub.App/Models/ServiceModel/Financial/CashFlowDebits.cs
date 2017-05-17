// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class CashFlowDebits
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        
        public IEnumerable<CashActivity> CashActivities { get; private set; }
        
        public IEnumerable<CashActivity> Purchases
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.Purchase);
            }
        }

        public IEnumerable<CashActivity> FixedExpenses
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.FixedExpense);
            }
        }

        public IEnumerable<CashActivity> Others
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.Other &&
                    cashFlow.Total < 0);
            }
        }

        public CashFlowDebits(IEnumerable<CashActivity> cashActivities)
        {
            CashActivities = cashActivities;
        }

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Money Total()
        {
            return TotalPurchases()
                .Add(TotalFixedExpenses())
                .Add(TotalOthers());
        }

        public Money Total(DateTime date)
        {
            return TotalPurchases(date)
                .Add(TotalFixedExpenses(date))
                .Add(TotalOthers(date));
        }

        public Money Total(int month, int year)
        {
            return TotalPurchases(month, year)
                .Add(TotalFixedExpenses(month, year))
                .Add(TotalOthers(month, year));
        }
        
        public Money TotalPurchases()
        {
            return Purchases.Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalPurchases(DateTime date)
        {
            return Purchases
                .Where(activity => activity.StartDate == date)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalPurchases(int month, int year)
        {
            return Purchases
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalFixedExpenses()
        {
            return FixedExpenses.Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalFixedExpenses(DateTime date)
        {
            return FixedExpenses
                .Where(activity => activity.StartDate.Day == date.Day)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalFixedExpenses(int month, int year)
        {
            return FixedExpenses
                .Where(activity =>
                    activity.EndDate == null ||
                    activity.EndDate.Value.Month >= month &&
                    activity.EndDate.Value.Year >= year)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }
        
        public Money TotalOthers()
        {
            return Others.Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalOthers(DateTime date)
        {
            return Others
                .Where(activity => activity.StartDate == date)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalOthers(int month, int year)
        {
            return Others
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }
    }
}
