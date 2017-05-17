// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.RentContractCustomer
{
    public class RentContractCustomerViewModel
    {
        [Required]
        public int? RentContractId { get; set; }

        public int? CustomerId { get; set; }
    }
}
