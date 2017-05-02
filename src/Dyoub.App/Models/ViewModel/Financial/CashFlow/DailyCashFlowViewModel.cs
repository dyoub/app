// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Financial.CashFlow
{
    public class DailyCashFlowViewModel
    {
        public int? StoreId { get; set; }

        [Required, Range(1, 12)]
        public int? Month { get; set; }

        [Required, Range(1900, 2999)]
        public int? Year { get; set; }

        public DateTime FromDate
        {
            get
            {
                return new DateTime(Year.Value, Month.Value, 1);
            }
        }

        public DateTime ToDate
        {
            get
            {
                return new DateTime(Year.Value, Month.Value, DateTime.DaysInMonth(Year.Value, Month.Value));
            }
        }
    }
}
