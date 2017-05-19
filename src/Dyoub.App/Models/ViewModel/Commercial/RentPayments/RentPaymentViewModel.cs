// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Financial;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.RentPayments
{
    public class RentPaymentViewModel
    {
        [Required]
        public int? PaymentMethodId { get; set; }

        [Required, Range(1, 24)]
        public int? NumberOfInstallments { get; set; }

        [Required, Range(0, 99999999.99)]
        public decimal? Total { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public RentPayment MapTo(RentPayment rentPayment)
        {
            rentPayment.PaymentMethodId = PaymentMethodId.Value;
            rentPayment.NumberOfInstallments = NumberOfInstallments.Value;
            rentPayment.Date = Date.Value.Date;
            rentPayment.Total = Total.Value;
            rentPayment.InstallmentValue = new Money(rentPayment.Total)
                .Divide(rentPayment.NumberOfInstallments);

            return rentPayment;
        }
    }
}
