// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Financial;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SalePayments
{
    public class SalePaymentViewModel
    {
        [Required]
        public int? PaymentMethodId { get; set; }

        [Required, Range(1, 24)]
        public int? NumberOfInstallments { get; set; }

        [Required, Range(0, 99999999.99)]
        public decimal? Total { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public SalePayment MapTo(SalePayment salePayment)
        {
            salePayment.PaymentMethodId = PaymentMethodId.Value;
            salePayment.NumberOfInstallments = NumberOfInstallments.Value;
            salePayment.Date = Date.Value.Date;
            salePayment.Total = Total.Value;
            salePayment.InstallmentValue = new Money(salePayment.Total)
                .Divide(salePayment.NumberOfInstallments);

            return salePayment;
        }
    }
}
