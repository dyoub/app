// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using Dyoub.App.Models.EntityModel.Financial.Period;
using System;
using System.Collections.Generic;

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

        public IEnumerable<CashActivity> CashActivities { get; private set; }

        public CashFlowCredits Credits { get; private set; }

        public CashFlowDebits Debits { get; private set; }

        public CashFlowAnalysis(IEnumerable<CashActivity> cashActivities)
        {
            CashActivities = cashActivities;
            Credits = new CashFlowCredits(cashActivities);
            Debits = new CashFlowDebits(cashActivities);
        }
        
        public void ToCurrentMonth()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            StartDate = new DateTime(year, month, 1);
            EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            ToPeriod(StartDate, EndDate);
        }

        public void ToPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;

            Credits.ToPeriod(StartDate, EndDate);
            Debits.ToPeriod(StartDate, EndDate);
        }

        public Money Balance()
        {
            return Credits.Total()
                .Subtract(Debits.Total());
        }

        public Money Balance(DateTime date)
        {
            return Credits.Total(date)
                .Subtract(Debits.Total(date));
        }

        public Money Balance(int month, int year)
        {
            return Credits.Total(month, year)
                .Subtract(Debits.Total(month, year));
        }
    }
}
