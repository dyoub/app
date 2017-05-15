// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchasePayments
{
    public class UpdatePurchasePaymentsViewModel
    {
        [Required]
        public int? PurchaseOrderId { get; set; }

        [Range(0, 100)]
        public decimal? Discount { get; set; }

        [Range(0, 999999.99)]
        public decimal? ShippingCost { get; set; }

        [Range(0, 999999.99)]
        public decimal? OtherTaxes { get; set; }

        public List<PurchasePaymentViewModel> Payments { get; private set; }

        [ValidIf]
        public bool HasAtLeastOnePaymentMethod
        {
            get
            {
                return Payments.Any();
            }
        }

        public UpdatePurchasePaymentsViewModel()
        {
            Payments = new List<PurchasePaymentViewModel>();
        }

        public IEnumerable<PurchasePayment> MapPurchasePayments()
        {
            return Payments.Select(payment => payment.MapTo(new PurchasePayment
            {
                PurchaseOrderId = PurchaseOrderId.Value
            }));
        }
    }
}
