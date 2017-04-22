// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using System;

namespace Dyoub.App.Models.ViewModel.Financial.OtherCashActivities
{
    public class ListOtherCashActivitiesViewModel
    {
        public int? StoreId { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ValidIf]
        public bool StartLowerThanEnd
        {
            get
            {
                return EndDate == null || StartDate.Value < EndDate.Value;
            }
        }

        public int Index { get; set; }
    }
}
