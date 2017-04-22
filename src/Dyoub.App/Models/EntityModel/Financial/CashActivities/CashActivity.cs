// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Models.EntityModel.Financial.CashActivities
{
    public class CashActivity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CashActivityType Activity { get; set; }
        public decimal Total { get; set; }
    }
}
