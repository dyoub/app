// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using Dyoub.App.Models.EntityModel.Financial;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchasePayments
{
    public class PurchasePaymentViewModel
    {
        [Required, Range(1, 36)]
        public int? NumberOfInstallments { get; set; }

        [Required, Range(0, 99999999.99)]
        public decimal? Total { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        public PurchasePayment MapTo(PurchasePayment purchasePayment)
        {
            purchasePayment.NumberOfInstallments = NumberOfInstallments.Value;
            purchasePayment.Date = Date.Value.Date;
            purchasePayment.Total = Total.Value;
            purchasePayment.InstallmentValue = new Money(purchasePayment.Total)
                .Divide(purchasePayment.NumberOfInstallments);

            return purchasePayment;
        }
    }
}
