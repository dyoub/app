// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Financial.FixedExpenses
{
    public class CreateFixedExpenseViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        [Required, MaxLength(80)]
        public string Description { get; set; }

        [Required]
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

        [Required]
        [Range(0.01, 999999.99)]
        public decimal? Value { get; set; }

        public FixedExpense MapTo(FixedExpense fixedExpense)
        {
            fixedExpense.StoreId = StoreId.Value;
            fixedExpense.Description = Description;
            fixedExpense.StartDate = StartDate.Value;
            fixedExpense.EndDate = EndDate;
            fixedExpense.Value = Value.Value;

            return fixedExpense;
        }
    }
}
