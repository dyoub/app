// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class CashFlowCredits
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public IEnumerable<CashActivity> CashActivities { get; private set; }

        public IEnumerable<CashActivity> RentContracts
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.RentContract);
            }
        }

        public IEnumerable<CashActivity> Sales
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.Sale);
            }
        }

        public IEnumerable<CashActivity> Others
        {
            get
            {
                return CashActivities.Where(cashFlow =>
                    cashFlow.Activity == CashActivityType.Other &&
                    cashFlow.Total > 0);
            }
        }

        public CashFlowCredits(IEnumerable<CashActivity> cashActivities)
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
            return TotalRentContracts()
                .Add(TotalSales())
                .Add(TotalOthers());
        }

        public Money Total(DateTime date)
        {
            return TotalRentContracts(date)
                .Add(TotalSales(date))
                .Add(TotalOthers(date));
        }

        public Money Total(int month, int year)
        {
            return TotalRentContracts(month, year)
                .Add(TotalSales(month, year))
                .Add(TotalOthers(month, year));
        }

        public Money TotalRentContracts()
        {
            return RentContracts.Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalRentContracts(DateTime date)
        {
            return RentContracts
                .Where(activity => activity.StartDate == date)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
        }

        public Money TotalRentContracts(int month, int year)
        {
            return RentContracts
                .Where(activity =>
                    activity.StartDate.Month == month &&
                    activity.StartDate.Year == year)
                .Sum(activity => (decimal?)activity.Total) ?? 0;
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
