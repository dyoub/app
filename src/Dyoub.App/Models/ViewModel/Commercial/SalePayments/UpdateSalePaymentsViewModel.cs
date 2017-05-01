// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.SalePayments
{
    public class UpdateSalePaymentsViewModel
    {
        [Required]
        public int? SaleOrderId { get; set; }

        [Range(0, 99999999.99)]
        public decimal? Discount { get; set; }

        public List<SalePaymentViewModel> Payments { get; private set; }
        
        [ValidIf]
        public bool HasAtLeastOnePaymentMethod
        {
            get
            {
                return Payments.Any();
            }
        }

        public UpdateSalePaymentsViewModel()
        {
            Payments = new List<SalePaymentViewModel>();
        }

        public IEnumerable<SalePayment> MapSalePayments()
        {
            return Payments.Select(payment => payment.MapTo(new SalePayment
            {
                SaleOrderId = SaleOrderId.Value
            }));
        }
    }
}
