// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using Dyoub.App.Models.EntityModel.Financial.Period;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class CashFlowAnalysis
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DailyPeriod DailyPeriod
        {
            get { return new DailyPeriod(StartDate, EndDate); }
        }

        public MonthlyPeriod MonthlyPeriod
        {
            get { return new MonthlyPeriod(StartDate, EndDate); }
        }

        public IEnumerable<CashActivity> CashActivities { get; set; }

        public IEnumerable<CashActivity> Sales
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.Sale);
            }
        }

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
                    cashFlow.Activity == CashActivityType.Other);
            }
        }

        public void ToCurrentMonth()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            StartDate = new DateTime(year, month, 1);
            EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Money Balance()
        {
            return TotalSales()
                .Add(TotalOtherCredits())
                .Subtract(TotalPurchases())
                .Subtract(TotalFixedExpenses())
                .Subtract(TotalOtherDebits());
        }

        public Money Balance(DateTime date)
        {
            return TotalSales(date)
                .Add(TotalOtherCredits(date))
                .Subtract(TotalPurchases(date))
                .Subtract(TotalFixedExpenses(date))
                .Subtract(TotalOtherDebits(date));
        }

        public Money Balance(int month, int year)
        {
            return TotalSales(month, year)
                .Add(TotalOtherCredits(month, year))
                .Subtract(TotalPurchases(month, year))
                .Subtract(TotalFixedExpenses(month, year))
                .Subtract(TotalOtherDebits(month, year));
        }

        public Money TotalSales()
        {
            return Sales.Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalSales(DateTime date)
        {
            return Sales
                .Where(activity => activity.StartDate == date)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalSales(int month, int year)
        {
            return Sales
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
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
                .Where(activity =>
                    activity.StartDate.Day == date.Day)
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

        public Money TotalOtherCredits()
        {
            return Others
                .Where(activity => activity.Total > 0)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalOtherCredits(DateTime date)
        {
            return Others
                .Where(activity => activity.StartDate == date && activity.Total > 0)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalOtherCredits(int month, int year)
        {
            return Others
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year &&
                    activity.Total > 0)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalOtherDebits()
        {
            return Others
                .Where(activity => activity.Total < 0)
                .Sum(activity => (decimal?)activity.Total * -1) ?? 0;
        }

        public Money TotalOtherDebits(DateTime date)
        {
            return Others
                .Where(activity => activity.StartDate == date && activity.Total < 0)
                .Sum(activity => (decimal?)activity.Total * -1) ?? 0;
        }

        public Money TotalOtherDebits(int month, int year)
        {
            return Others
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year &&
                    activity.Total < 0)
                .Sum(activity => (decimal?)activity.Total * -1) ?? 0;
        }
    }
}
