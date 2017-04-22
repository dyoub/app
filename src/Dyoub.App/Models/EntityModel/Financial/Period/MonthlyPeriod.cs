// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Financial.Period
{
    public class MonthlyPeriod : DailyPeriod, IEnumerable<DateTime>, IEnumerator<DateTime>
    {
        public MonthlyPeriod(DateTime startDate, DateTime endDate)
            : base(startDate, endDate) { }

        bool IEnumerator.MoveNext()
        {
            Current = Current == DateTime.MinValue ? StartDate : Current.AddMonths(1);
            return Current <= EndDate;
        }
    }
}
