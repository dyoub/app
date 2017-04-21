// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.PaymentMethods
{
    public class PaymentMethodFeeViewModel
    {
        [Required]
        public int? MinimumInstallment { get; set; }

        [Range(0, 100)]
        public decimal? FeePercentage { get; set; }

        [Range(0, 999.99)]
        public decimal? FeeFixedValue { get; set; }

        [ValidIf]
        public bool HasPercentageOrFixedValue
        {
            get
            {
                return FeePercentage != null || FeeFixedValue != null;
            }
        }

        public PaymentMethodFee MapTo(PaymentMethodFee paymentMethodFee)
        {
            paymentMethodFee.MinimumInstallment = MinimumInstallment.Value;
            paymentMethodFee.FeePercentage = FeePercentage;
            paymentMethodFee.FeeFixedValue = FeeFixedValue;

            return paymentMethodFee;
        }
    }
}
