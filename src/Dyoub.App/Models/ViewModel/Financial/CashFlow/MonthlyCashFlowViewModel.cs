// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Financial.CashFlow
{
    public class MonthlyCashFlowViewModel
    {
        public int? StoreId { get; set; }

        [Required, Range(1900, 2999)]
        public int? Year { get; set; }

        public DateTime FromDate
        {
            get
            {
                return new DateTime(Year.Value, 1, 1);
            }
        }

        public DateTime ToDate
        {
            get
            {
                return new DateTime(Year.Value, 12, 31);
            }
        }
    }
}
