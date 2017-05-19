// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.RentPayments
{
    public class UpdateRentPaymentsViewModel
    {
        [Required]
        public int? RentContractId { get; set; }

        [Range(0, 99999999.99)]
        public decimal? Discount { get; set; }

        public List<RentPaymentViewModel> Payments { get; private set; }

        [ValidIf]
        public bool HasAtLeastOnePaymentMethod
        {
            get
            {
                return Payments.Any();
            }
        }

        public UpdateRentPaymentsViewModel()
        {
            Payments = new List<RentPaymentViewModel>();
        }

        public IEnumerable<RentPayment> MapRentPayments()
        {
            return Payments.Select(payment => payment.MapTo(new RentPayment
            {
                RentContractId = RentContractId.Value
            }));
        }
    }
}
