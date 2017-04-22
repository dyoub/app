// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Financial.OtherCashActivities
{
    public class CreateOtherCashActivityViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        [Required]
        public string Operation { get; set; }

        public bool IsCredit
        {
            get { return Operation == "Credit"; }
        }

        public bool IsDebit
        {
            get { return Operation == "Debit"; }
        }

        [ValidIf]
        public bool OperationIsCreditOrDebit
        {
            get
            {
                return IsCredit || IsDebit;
            }
        }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal? Value { get; set; }

        public OtherCashActivity MapTo(OtherCashActivity otherCashActivity)
        {
            otherCashActivity.StoreId = StoreId.Value;
            otherCashActivity.Description = Description;
            otherCashActivity.Date = Date.Value;
            otherCashActivity.Value = Value.Value;

            if (IsDebit)
            {
                otherCashActivity.Value *= -1;
            }

            return otherCashActivity;
        }
    }
}
