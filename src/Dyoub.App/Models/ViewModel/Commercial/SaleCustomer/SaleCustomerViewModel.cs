// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleCustomer
{
    public class SaleCustomerViewModel
    {
        [Required]
        public int? SaleOrderId { get; set; }
        
        public int? CustomerId { get; set; }
    }
}
